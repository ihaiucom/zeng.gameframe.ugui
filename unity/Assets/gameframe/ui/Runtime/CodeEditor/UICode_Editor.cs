#if UNITY_EDITOR
using System.Reflection;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using Zeng.GameFrame.UIs.Editor;
namespace Zeng.GameFrame.UIs
{
    public partial class UICode 
    {
        
        [LabelText("组件类型")]
        [OnValueChanged("OnValueChangedEUICodeType")]
        // [ReadOnly]
        public EUICodeType UICodeType = EUICodeType.Component;
        
        

        [ShowIf("UICodeType", EUICodeType.Window)]
        [BoxGroup("配置", true, true)]
        [OnValueChanged("OnValueChangedEPanelLayer")]
        [GUIColor(0, 1, 1)]
        [EnableIf("@UIOperationHelper.CommonShowIf()")]
        public EWindowLayer WindowLayer = EWindowLayer.Panel;
        
        
        [InlineButton("AddComponentTable", "Add")]
        [EnableIf("@UIOperationHelper.CommonShowIf()")]
        public UICodeComponentTable ComponentTable;
        
        private void OnValidate()
        {
            ComponentTable ??= GetComponent<UICodeComponentTable>();
            // DataTable      ??= GetComponent<UIBindDataTable>();
            // EventTable     ??= GetComponent<UIBindEventTable>();
        }
        
        private void AddComponentTable()
        {
            if (!UIOperationHelper.CheckUIOperation()) return;

            ComponentTable = gameObject.GetOrAddComponent<UICodeComponentTable>();
        }
        
        private void OnValueChangedEUICodeType()
        {
            if (name.EndsWith(UIStaticHelper.UIWindowName) )
            {
                if (UICodeType != EUICodeType.Window)
                {
                    Debug.LogWarning($"{name} 结尾{UIStaticHelper.UIWindowName} 必须设定为{UIStaticHelper.UIWindowName}类型");
                }

                UICodeType = EUICodeType.Window;
            }
            else if (name.EndsWith(UIStaticHelper.UIViewName))
            {
                if (UICodeType != EUICodeType.View)
                {
                    Debug.LogWarning($"{name} 结尾{UIStaticHelper.UIViewName} 必须设定为{UIStaticHelper.UIViewName}类型");
                }

                UICodeType = EUICodeType.View;
            }
            else
            {
                if (UICodeType != EUICodeType.Component)
                {
                    Debug.LogWarning($"{name} 想设定为其他类型 请按照规则设定 请勿强行修改");
                }

                UICodeType = EUICodeType.Component;
            }
        }
        
        
        private void OnValueChangedEPanelLayer()
        {
            if (WindowLayer >= EWindowLayer.Cache)
            {
                Debug.LogError($" {name} 层级类型 选择错误 请重新选择");
                WindowLayer = EWindowLayer.Panel;
            }
        }
        
        
        private bool ShowAutoCheckBtn()
        {
            if (!UIOperationHelper.CheckUIOperation(false)) return false;
            return true;
        }

        [GUIColor(1, 1, 0)]
        [Button("自动检查所有", 30)]
        [PropertyOrder(-100)]
        [ShowIf("ShowAutoCheckBtn")]
        private void AutoCheckBtn()
        {
            AutoCheck();
        }
        
        
        internal bool AutoCheck()
        {
            // if (!UIOperationHelper.CheckUIOperation(this)) return false;
            // if (!UICreateModule.InitVoName(this)) return false;
            // OnValueChangedEUICodeType();
            // OnValueChangedEPanelLayer();
            //
            // UICreateModule.RefreshChildCdeTable(this);
            // ComponentTable?.AutoCheck();
            // DataTable?.AutoCheck();
            // EventTable?.AutoCheck();
            return true;
        }
        internal void CreateUICode(bool refresh, bool tips)
        {
            // UICreateModule.Create(this, refresh, tips);
        }
        
        [GUIColor(0, 1, 1)]
        [Button("保存选中", 50)]
        [PropertyOrder(-100)]
        [HideIf("ShowCreateBtn")]
        private void SaveSelectSelf()
        {
            var stage = PrefabStageUtility.GetPrefabStage(gameObject);

            if (stage == null)
            {
                Debug.LogError($"未知错误 没有找到预制 {gameObject.name}");
                return;
            }

            var methodInfo = stage.GetType().GetMethod("Save", BindingFlags.Instance | BindingFlags.NonPublic);

            if (methodInfo != null)
            {
                bool result = (bool)methodInfo.Invoke(stage, null);
                if (!result)
                {
                    Debug.LogError("自动保存失败 注意请手动保存");
                }
            }
            else
            {
                Debug.LogError("Save方法不存在 自动保存失败 注意请手动保存");
            }

            var assetObj = AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(stage.assetPath);
            EditorGUIUtility.PingObject(assetObj);
            Selection.activeObject = assetObj;
        }
        
        private bool ShowCreateBtnByHierarchy()
        {
            if (string.IsNullOrEmpty(PkgName) || string.IsNullOrEmpty(ResName)) return false;
            if (ResName.Contains("Source")) return false;
            if (!UIOperationHelper.CheckUIOperation(this, false)) return false;
            return !PrefabUtility.IsPartOfPrefabAsset(this);
        }

        [GUIColor(0f, 0.5f, 1f)]
        [Button("生成", 50)]
        [ShowIf("ShowCreateBtnByHierarchy")]
        internal void CreateUICodeByHierarchy()
        {
            if (!ShowCreateBtnByHierarchy()) return;

            var prefabStage = UnityEditor.SceneManagement.PrefabStageUtility.GetCurrentPrefabStage();
            if (prefabStage == null)
            {
                Debug.LogError($"当前不在预制体编辑器模式下");
                return;
            }

            var path = prefabStage.assetPath;
            var root = prefabStage.prefabContentsRoot;
            PrefabUtility.SaveAsPrefabAsset(root, path, out var success);
            if (!success)
            {
                Debug.LogError("快捷保存失败 请检查");
                return;
            }

            prefabStage.ClearDirtiness();

            var cdeTable = AssetDatabase.LoadAssetAtPath<UICode>(path);
            if (cdeTable == null) return;
            cdeTable.CreateUICode();

            AssetDatabase.OpenAsset(cdeTable);
        }
        
        private bool ShowCreateBtn()
        {
            if (!UIOperationHelper.CheckUIOperationAll(this, false)) return false;
            return true;
        }

        [GUIColor(0.7f, 0.4f, 0.8f)]
        [Button("生成", 50)]
        [ShowIf("ShowCreateBtn")]
        internal void CreateUICode()
        {
            if (!UIOperationHelper.CheckUIOperation(this)) return;

            CreateUICode(true, true);
        }
        
        

    }
}
#endif