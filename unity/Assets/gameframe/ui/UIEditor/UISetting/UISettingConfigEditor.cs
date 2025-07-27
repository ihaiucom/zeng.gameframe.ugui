#if UNITY_EDITOR

using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;

namespace Zeng.GameFrame.UIS.Editor
{
    public class UISettingConfigEditor : GlobalConfig<UISettingConfigEditor>, ITreeMenu
    {
        public static StringPrefs UserNamePrefs = new StringPrefs("UITool_UserName", null, "UI");
        public static BoolPrefs OldCDEInspectorPrefs = new BoolPrefs("UITool_OldCDEInspector", null, false);

        [LabelText("用户名")]
        [Required("请填写用户名")]
        [ShowInInspector]
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
        public void Load()
        {
            if(isLoad) return;
            isLoad = true;
            m_Author = UserNamePrefs.Value;
            m_DisplayOldCDEInspector = OldCDEInspectorPrefs.Value;
        }


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