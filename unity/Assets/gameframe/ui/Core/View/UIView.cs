namespace Zeng.GameFrame.UIS
{
    /// <summary>
    /// UI 子面板
    /// </summary>
    public class UIView : UIBaseWindow
    {
        public virtual EViewWindowType ViewWindowType => EViewWindowType.View;

        public virtual EViewStackOption StackOption => EViewStackOption.Visible;
    }
}