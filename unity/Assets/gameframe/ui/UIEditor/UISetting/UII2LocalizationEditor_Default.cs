#if UNITY_EDITOR && !UI_ET
using Sirenix.OdinInspector;

namespace Zeng.GameFrame.UIS.Editor
{
    public partial class UII2LocalizationEditor 
    {
        [BoxGroup("配置", false, true)]
        [LabelText("全数据名称")]
        [ShowInInspector]
        [ReadOnly]
        public string UII2SourceResName = "AllSource";

        [BoxGroup("配置", false, true)]
        [LabelText("全数据保存路径")]
        [FolderPath]
        [ShowInInspector]
        [ReadOnly]
        public string UII2SourceResPath = "Assets/Settings/I2Localization"; //这是编辑器下的数据 平台运行时 是不需要的
        
        
        [BoxGroup("配置", false, true)]
        [LabelText("全数据配置文件路径")]
        [FolderPath]
        [ShowInInspector]
        [ReadOnly]
        public string I2GlobalSourcesEditorPath = "Assets/Settings/I2Localization/I2Languages.asset";

        [BoxGroup("配置", false, true)]
        [LabelText("指定数据保存路径")]
        [FolderPath]
        [ShowInInspector]
        [ReadOnly]
        public string UII2TargetLanguageResPath = "Assets/GameRes/I2Localization"; //运行时的资源是拆分的 根据需求加载

    }
}
#endif