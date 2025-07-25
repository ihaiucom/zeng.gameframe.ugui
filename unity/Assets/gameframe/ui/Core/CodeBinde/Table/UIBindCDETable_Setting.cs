
using Sirenix.OdinInspector;

namespace Zeng.GameFrame.UIS
{
    //界面参数
    public sealed partial class UIBindCDETable
    {

        [BoxGroup("配置", true, true)]
        [LabelText("组件类型")]
        [ReadOnly]
        #if UNITY_EDITOR
        [OnValueChanged("OnValueChangedEUICodeType")]
        #endif
        public EUICodeType UICodeType = EUICodeType.Component;

        [BoxGroup("配置", true, true)]
        [LabelText("窗口选项")]
        [GUIColor(0, 1, 1)]
        #if UNITY_EDITOR
        [HideIf("UICodeType", EUICodeType.Component)]
        [EnableIf("@UIOperationHelper.CommonShowIf()")]
        #endif
        public EWindowOption WindowOption = EWindowOption.None;

        [BoxGroup("配置", true, true)]
        [GUIColor(0, 1, 1)]
        #if UNITY_EDITOR
        [ShowIf("UICodeType", EUICodeType.Panel)]
        [EnableIf("@UIOperationHelper.CommonShowIf()")]
        [OnValueChanged("OnValueChangedEPanelLayer")]
        #endif
        public EPanelLayer PanelLayer = EPanelLayer.Panel;

        [BoxGroup("配置", true, true)]
        [GUIColor(0, 1, 1)]
        #if UNITY_EDITOR
        [ShowIf("UICodeType", EUICodeType.Panel)]
        [EnableIf("@UIOperationHelper.CommonShowIf()")]
        #endif
        public EPanelOption PanelOption = EPanelOption.None;

        [BoxGroup("配置", true, true)]
        [GUIColor(0, 1, 1)]
        #if UNITY_EDITOR
        [ShowIf("UICodeType", EUICodeType.Panel)]
        [EnableIf("@UIOperationHelper.CommonShowIf()")]
        #endif
        public EPanelStackOption PanelStackOption = EPanelStackOption.VisibleTween;

        [BoxGroup("配置", true, true)]
        [GUIColor(0, 1, 1)]
        #if UNITY_EDITOR
        [ShowIf("UICodeType", EUICodeType.View)]
        [EnableIf("@UIOperationHelper.CommonShowIf()")]
        #endif
        public EViewWindowType ViewWindowType = EViewWindowType.View;

        [BoxGroup("配置", true, true)]
        [GUIColor(0, 1, 1)]
        #if UNITY_EDITOR
        [ShowIf("UICodeType", EUICodeType.View)]
        [EnableIf("@UIOperationHelper.CommonShowIf()")]
        #endif
        public EViewStackOption ViewStackOption = EViewStackOption.VisibleTween;

        [BoxGroup("配置", true, true)]
        [GUIColor(0, 1, 1)]
        [LabelText("缓存时间")]
        #if UNITY_EDITOR
        [ShowIf("ShowCachePanelTime", EUICodeType.Panel)]
        [EnableIf("@UIOperationHelper.CommonShowIf()")]
        #endif
        public float CachePanelTime = 10;

        private bool ShowCachePanelTime => PanelOption.HasFlag(EPanelOption.TimeCache);

        [LabelText("同层级时 优先级高的在前面")] //相同时后开的在前
        [BoxGroup("配置", true, true)]
        [GUIColor(0, 1, 1)]
        #if UNITY_EDITOR
        [ShowIf("UICodeType", EUICodeType.Panel)]
        [EnableIf("@UIOperationHelper.CommonShowIf()")]
        #endif
        public int Priority = 0;


    }
}