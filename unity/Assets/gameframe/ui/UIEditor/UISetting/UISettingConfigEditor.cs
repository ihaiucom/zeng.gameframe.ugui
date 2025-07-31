#if UNITY_EDITOR

using System;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEditor;
using UnityEngine;

namespace Zeng.GameFrame.UIS.Editor
{
    public partial class UISettingConfigEditor : ScriptableObject, ITreeMenu
    {
        private static UISettingConfigEditor _Instance;
        public static UISettingConfigEditor Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = AssetDatabase.LoadAssetAtPath<UISettingConfigEditor>("Assets/Settings/UISettingConfigEditor.asset");
                    if (_Instance == null)
                    {
                        _Instance = ScriptableObject.CreateInstance<UISettingConfigEditor>();
                        AssetDatabase.CreateAsset(_Instance, "Assets/Settings/UISettingConfigEditor.asset");
                    }
                }
                return _Instance;
            }
        }
        
        public static StringPrefs UserNamePrefs = new StringPrefs("UITool_UserName", null, "UI");
        public static BoolPrefs OldCDEInspectorPrefs = new BoolPrefs("UITool_OldCDEInspector", null, false);

        [TitleGroup("本地个人配置", "", alignment: TitleAlignments.Centered, horizontalLine: true, boldTitle: true, indent: false)]
        [LabelText("用户名")]
        [Required("请填写用户名")]
        [ShowInInspector]
        [ShowIf("Load")]
        private static string m_Author;
        
        public static string Author
        {
            get
            {
                Instance.Load();
                if (string.IsNullOrEmpty(m_Author))
                {
                    m_Author = UserNamePrefs.Value;
                }

                return m_Author;
            }
        }
        
        
        [TitleGroup("本地个人配置", "", alignment: TitleAlignments.Centered, horizontalLine: true, boldTitle: true, indent: false)]
        [LabelText("使用老的CDEInspector显示模式")]
        [ShowInInspector]
        private static bool m_DisplayOldCDEInspector;
        public static bool DisplayOldCDEInspector 
        {
            get
            {
                Instance.Load();
                return m_DisplayOldCDEInspector;
            }
        }

        private static bool isLoad = false;
        private static bool isNotLoad {
            get { return !isLoad;}
        }

        [TitleGroup("本地个人配置", "", alignment: TitleAlignments.Centered, horizontalLine: true, boldTitle: true, indent: false)]
        [Button("加载", ButtonSizes.Large)]
        [ShowIf("isNotLoad")]
        public bool Load()
        {
            if(isLoad) return true;
            isLoad = true;
            m_Author = UserNamePrefs.Value;
            m_DisplayOldCDEInspector = OldCDEInspectorPrefs.Value;
            return true;
        }


        [TitleGroup("本地个人配置", "", alignment: TitleAlignments.Centered, horizontalLine: true, boldTitle: true, indent: false)]
        [Button("确定", ButtonSizes.Large)]
        private void ClickOkButton()
        {
            UserNamePrefs.Value = m_Author;
            OldCDEInspectorPrefs.Value = m_DisplayOldCDEInspector;
            PlayerPrefs.Save();
        }

        public void OnSelected()
        {
            Load();
        }
        
        
        
    }
}
#endif