namespace Zeng.GameFrame.UIS
{
    public partial class UIBaseWindow : UIBase
    {
        /// <summary>
        /// 窗口选项
        /// 最后由Panel View重写
        /// 实现各种配置
        /// 如非必要 所有属性判断可以直接去HasFlag分类中获取 不要直接判断这个
        /// </summary>
        public virtual EWindowOption WindowOption
        {
            get
            {
                return CDETable.WindowOption;
            }

            set
            {
                CDETable.WindowOption = value;
            }
        }
    }
}