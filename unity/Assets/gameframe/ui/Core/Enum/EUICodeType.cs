using Sirenix.OdinInspector;

namespace Zeng.GameFrame.UIS
{
    [LabelText("组件类型")]
    public enum EUICodeType
    {
        [LabelText("Window 面板")]
        Window,

        [LabelText("SubPanel 界面")]
        SubPanel,

        [LabelText("View 组件")]
        View,
    }
}