namespace Zeng.GameFrame.UIS
{
    /// <summary>
    /// UI 窗口面板
    /// </summary>
    public partial class UIPanel : UIBaseWindow
    {
        /// <summary>
        /// 所在层级
        /// </summary>
        public virtual EPanelLayer Layer 
        {
            get
            {
                return CDETable.PanelLayer;
            }
            
            set
            {
                CDETable.PanelLayer = value;
            }
        }

        /// <summary>
        /// 界面选项
        /// </summary>
        public virtual EPanelOption PanelOption
        {
            get
            {
                return CDETable.PanelOption;
            }
            
            set
            {
                CDETable.PanelOption = value;
            }
        }

        /// <summary>
        /// 堆栈操作
        /// </summary>
        public virtual EPanelStackOption StackOption
        {
            get
            {
                return CDETable.PanelStackOption;
            }
            
            set
            {
                CDETable.PanelStackOption = value;
            }
        }

        /// <summary>
        /// 优先级，用于同层级排序,
        /// 大的在前 小的在后
        /// 相同时 后添加的在前
        /// </summary>
        public virtual int Priority 
        {
            get
            {
                return CDETable.Priority;
            }
            
            set
            {
                CDETable.Priority = value;
            }
        }

        protected virtual float CachePanelTime
        {
            get
            {
                return CDETable.CachePanelTime;
            }

            set
            {
                CDETable.CachePanelTime = value;
            }
        }
        
        protected sealed override void SealedInitialize()
        {
            InitPanelViewData();
        }
    }
}