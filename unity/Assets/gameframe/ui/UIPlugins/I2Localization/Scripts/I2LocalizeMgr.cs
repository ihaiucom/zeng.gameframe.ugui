using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using Zeng.GameFrame.UIS;
using Object = UnityEngine.Object;

namespace I2.Loc
{
    [RequireComponent(typeof(LanguageSource))]
    public class I2LocalizeMgr : MonoSingleton<I2LocalizeMgr>, IResourceManager_Bundles
    {

        // private static I2LocalizeMgr s_Instance;
        // public static I2LocalizeMgr I
        // {
        //     get
        //     {
        //         if (s_Instance == null)
        //         {
        //             s_Instance = FindObjectOfType<I2LocalizeMgr>();
        //             if (s_Instance == null)
        //             {
        //                 GameObject gameObject = new GameObject("I2LocalizeMgr");
        //                 gameObject.AddComponent<LanguageSource>();
        //                 s_Instance = gameObject.AddComponent<I2LocalizeMgr>();
        //                 gameObject.hideFlags = HideFlags.DontSave;
        //                 DontDestroyOnLoad(gameObject);
        //             }
        //         }
        //
        //         return s_Instance;
        //     }
        // }
        //
        // private void Awake()
        // {
        //     s_Instance = this;
        // }

        [SerializeField]
        [ReadOnly]
        private LanguageSource m_LanguageSource;

        private LanguageSourceData m_SourceData => m_LanguageSource.SourceData;

        private List<string> m_AllLanguage = new List<string>();

        [SerializeField]
        private bool m_UseRuntimeModule = true; //模拟平台运行时 编辑器资源不加载

        [SerializeField]
        #if UNITY_EDITOR
        [ValueDropdown("GetAllLanguageKeys")]
        [DisableIf("OnValueChangeIf")]
        #endif
        private string m_DefaultLanguage = "Chinese";


        private const string CacheKey = "I2LocalizeMgr_CurrentLanguage";
        private bool IsReadCache = false;
        private string DefaultLanguage
        {
            get
            {
                if (!IsReadCache)
                {
                    IsReadCache = true;
                    m_DefaultLanguage = PlayerPrefs.GetString(CacheKey, m_DefaultLanguage);
                }

                return m_DefaultLanguage;
            }
        }


        [ShowInInspector]
        #if UNITY_EDITOR
        [EnableIf("OnValueChangeIf")]
        [ValueDropdown("GetAllLanguageKeys")]
        [OnValueChanged("OnValueChangedCurrentLanguage")]
        #endif
        private string _currentLanguage;

        private string CurrentLanguage
        {
            get
            {
                return _currentLanguage;
            }
            set
            {
                _currentLanguage = value;
                PlayerPrefs.SetString(CacheKey, value);
                PlayerPrefs.Save();
            }
        }

        #region ResourceManager_Bundles

        public void OnEnable()
        {
            Debug.Log("I2LocalizeMgr OnEnable");
            if (!ResourceManager.pInstance.mBundleManagers.Contains(this))
            {
                ResourceManager.pInstance.mBundleManagers.Add(this);
            }
        }

        public void OnDisable()
        {
            Debug.Log("I2LocalizeMgr OnDisable");
            ResourceManager.pInstance.mBundleManagers.Remove(this);
        }

        protected override void OnDestroy()
        {
            Debug.Log("I2LocalizeMgr OnDestroy");

            ResourceManager.pInstance.mBundleManagers.Remove(this);
            LocalizationManager.OnLanguageChange -= OnLanguageChange;
            base.OnDestroy();
        }

        public virtual Object LoadFromBundle(string path, Type assetType)
        {
            // if(path.EndsWith('\r')) path = path.Replace('\r', ' ');
            var assetObject = UILoad.LoadAsset(path, assetType);
            if (assetObject != null) return assetObject;
            Debug.LogError($"没有加载到目标 {path}  类型 {assetType.Name}");
            return null;
        }

        #endregion

        #if UNITY_EDITOR

        private bool OnValueChangeIf()
        {
            return Application.isPlaying;
        }

        private void OnValueChangedCurrentLanguage()
        {
            var tempLanguage = CurrentLanguage;
            CurrentLanguage = "";
            SetLanguage(tempLanguage);
        }

        private IEnumerable<string> GetAllLanguageKeys()
        {
            var allLanguage = new List<string>();

            foreach (var language in LocalizationManager.GetAllLanguages())
            {
                var newLanguage = Regex.Replace(language, @"[\r\n]", "");
                allLanguage.Add(newLanguage);
            }

            return allLanguage;
        }
        
        #endif


        
        protected override bool GetHideAndDontSave()
        {
            return false;
        }
        
        protected override async UniTask<bool> MgrAsyncInit()
        {
            if (string.IsNullOrEmpty(DefaultLanguage))
            {
                //TODO 这里也可以读取上一次选择的语言
                //TODO 初始化时还需要配合如果没有这个语言需要从服务器拉取的情况
                //TODO 也可以在语言设置界面 如果设置某个语言 发现没有这些数据 当时就加载 然后重启游戏
                Debug.LogError($"必须设置默认语言");
                return false;
            }

            m_LanguageSource = this.GetComponent<LanguageSource>();
            
            #if UNITY_EDITOR
            if (!m_UseRuntimeModule)
            {
                LocalizationManager.RegisterSourceInEditor();
                UpdateAllLanguages();
                SetLanguage(DefaultLanguage);
            }
            else
            {
                m_SourceData.Awake();
                await LoadLanguage(DefaultLanguage, true);
            }
            #else
                m_SourceData.Awake();
                await LoadLanguage(m_DefaultLanguage, true);
            #endif
            
            LocalizationManager.OnLanguageChange += OnLanguageChange;

            return true;
        }
        
        private void OnLanguageChange(string language)
        {
            Debug.Log($"切换语言 {language}");
            CurrentLanguage = language;
        }
        
        //根据需求可提前加载语言
        public async UniTask LoadLanguage(string language, bool setCurrent = false)
        {
            #if UNITY_EDITOR
            if (!m_UseRuntimeModule)
            {
                Debug.LogError($"禁止在此模式下 动态加载语言 {language}");
                return;
            }
            #endif

            if (CheckLanguage(language))
            {
                Debug.LogError($"当前语言已存在 请勿重复加载 {language}");
                return;
            }
            
            var assetName = GetLanguageAssetName(language);
            Debug.Log($"加载语言 {language}, {assetName}");

            var assetTextAsset = await UILoad.LoadAssetAsync<TextAsset>(assetName);
            if (assetTextAsset == null)
            {
                Debug.LogError($"没有加载到目标语言资源 {language}");
                return;
            }

            Debug.Log($"加载语言成功 {language}");
            
            UseLocalizationCSV(assetTextAsset.text, !setCurrent);
            if (setCurrent)
            {
                SetLanguage(language);
            }

            //语言加载完毕后就可以释放资源了
            UILoad.Release(assetTextAsset);
        }

        private string GetLanguageAssetName(string language)
        {
            return $"{I2LocalizeHelper.I2ResAssetNamePrefix}{language}";
        }

        private void UseLocalizationCSV(string text, bool isLocalizeAll = false)
        {
            m_SourceData.Import_CSV(string.Empty, text, eSpreadsheetUpdateMode.Replace, ',');
            if (isLocalizeAll)
            {
                LocalizationManager.LocalizeAll(); // 强制使用新数据本地化所有启用的标签/精灵
            }

            UpdateAllLanguages();
        }

        private void UpdateAllLanguages()
        {
            m_AllLanguage.Clear();
            foreach (var language in LocalizationManager.GetAllLanguages())
            {
                var newLanguage = Regex.Replace(language, @"[\r\n]", "");
                m_AllLanguage.Add(newLanguage);
            }
        }

        public bool CheckLanguage(string language)
        {
            return m_AllLanguage.Contains(language);
        }

        //运行时注意 需要提前加载你需要的所有语言
        public bool SetLanguage(string language, bool load = false)
        {
            if (!CheckLanguage(language))
            {
                if (load)
                {
                    LoadLanguage(language, true).Forget();
                    return true;
                }

                Debug.LogError($"当前没有这个语言无法切换到此语言 {language}");
                return false;
            }

            if (CurrentLanguage == language)
            {
                return true;
            }

            Debug.Log($"设置当前语言 = {language}");
            LocalizationManager.CurrentLanguage = language;
            CurrentLanguage                   = language;
            return true;
        }

        public bool SetLanguage(int id)
        {
            if (id < 0 || id >= m_AllLanguage.Count)
            {
                Debug.LogError($"错误的语言ID 无法设定 请检查 {id}  Language.Count = {m_AllLanguage.Count}");
                return false;
            }

            var language = m_AllLanguage[id];
            return SetLanguage(language);
        }
    }
}