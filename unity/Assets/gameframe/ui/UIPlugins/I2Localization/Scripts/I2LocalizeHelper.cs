

namespace I2.Loc
{
    public static class I2LocalizeHelper
    {
        #if UNITY_EDITOR
        public static string I2GlobalSourcesEditorPath {
            get
            {
                return Zeng.GameFrame.UIS.Editor.UII2LocalizationEditor.Instance.I2GlobalSourcesEditorPath;
            }
        }
#endif

        public const string I2ResAssetNamePrefix = "I2_";

    }
}