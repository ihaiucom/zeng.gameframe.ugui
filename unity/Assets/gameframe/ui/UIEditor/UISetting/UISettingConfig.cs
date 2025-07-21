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
    }
}