namespace Zeng.GameFrame.UIS
{
    /// <summary>
    /// UI 子面板
    /// </summary>
    public partial class UIView : UIBaseWindow
    {
        public virtual EViewWindowType ViewWindowType
        {
            get
            {
                return CDETable.ViewWindowType;
            }
            
            set
            {
                CDETable.ViewWindowType = value;
            }
        }

        public virtual EViewStackOption StackOption 
        {
            get
            {
                return CDETable.ViewStackOption;
            }
            
            set
            {
                CDETable.ViewStackOption = value;
            }
        }
    }
}