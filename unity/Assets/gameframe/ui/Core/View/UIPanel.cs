namespace Zeng.GameFrame.UIS
{
    /// <summary>
    /// UI 窗口面板
    /// </summary>
    public class UIPanel : UIBaseWindow
    {
        /// <summary>
        /// 所在层级
        /// </summary>
        public virtual EPanelLayer Layer => EPanelLayer.Panel;

        /// <summary>
        /// 界面选项
        /// </summary>
        public virtual EPanelOption PanelOption => EPanelOption.None;

        /// <summary>
        /// 堆栈操作
        /// </summary>
        public virtual EPanelStackOption StackOption => EPanelStackOption.Visible;

        /// <summary>
        /// 优先级，用于同层级排序,
        /// 大的在前 小的在后
        /// 相同时 后添加的在前
        /// </summary>
        public virtual int Priority => 0;
    }
}