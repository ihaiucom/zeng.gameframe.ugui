using Sirenix.OdinInspector;
using Sirenix.Utilities;

namespace Zeng.GameFrame.UIS.Editor
{
    public class UISettingConfig : GlobalConfig<UISettingConfig>
    {
        public static StringPrefs UserNamePrefs = new StringPrefs("UITool_UserName", null, "UI");

        [LabelText("用户名")]
        [Required("请填写用户名")]
        [ShowInInspector]
        private static string m_Author;
        
        public static string Author
        {
            get
            {
                if (string.IsNullOrEmpty(m_Author))
                {
                    m_Author = UserNamePrefs.Value;
                }

                return m_Author;
            }
        }

        protected override void OnConfigAutoCreated()
        {
            base.OnConfigAutoCreated();
            m_Author = UserNamePrefs.Value;
        }

        [Button("确定", ButtonSizes.Large)]
        private void ClickOkButton()
        {
            UserNamePrefs.Value = m_Author;
        }
    }
}