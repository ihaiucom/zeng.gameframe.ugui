namespace Zeng.GameFrame.UIS
{
    public partial class UIManager
    {
        #region 获取指定Panel 必须是存在的 但不一定是打开的有可能是隐藏

        //字符串类型获取 需要自己转换 建议不要使用
        public UIPanel GetPanel(string panelName)
        {
            var info = GetPanelInfo(panelName);
            return info?.Panel;
        }

        /// <summary>
        /// 获取一个panel
        /// 必须是存在的 但不一定是打开的有可能是隐藏
        /// 这个只能表示对象存在
        /// 不应该滥用 UI与UI之间还是应该使用消息通信 这个是为了解决部分问题
        /// </summary>
        public T GetPanel<T>() where T : UIPanel
        {
            var info = GetPanelInfo<T>();
            if (info is { Panel: { } })
            {
                return (T)info.Panel;
            }

            return null;
        }

        #endregion

        #region 获取指定Panel的指定View 必须是存在的 但不一定是打开的有可能是隐藏

        //字符串类型获取 需要自己转换 建议不要使用
        public UIView GetPanelView(string panelName, string viewName)
        {
            var info = GetPanelInfo(panelName);
            if (info == null) return null;
            if (info.Panel == null) return null;
            var (exist, view) = info.Panel.ExistView(viewName);
            if (exist == false) return null;
            return view;
        }

        /// <summary>
        /// 获取指定Panel的指定View
        /// 必须是存在的 但不一定是打开的有可能是隐藏
        /// 这个只能表示对象存在
        /// 不应该滥用 UI与UI之间还是应该使用消息通信 这个是为了解决部分问题
        /// </summary>
        public TView GetPanelView<TPanel, TView>()
            where TPanel : UIPanel
            where TView : UIView
        {
            var info = GetPanelInfo<TPanel>();
            if (info == null) return null;
            if (info.Panel == null) return null;
            var (exist, view) = info.Panel.ExistView<TView>();
            if (exist == false) return null;
            return (TView)view;
        }

        //字符串类型获取 需要自己转换 建议不要使用
        public UIView GetPanelViewByViewName<TPanel>(string viewName)
            where TPanel : UIPanel
        {
            var info = GetPanelInfo<TPanel>();
            if (info == null) return null;
            if (info.Panel == null) return null;
            var (exist, view) = info.Panel.ExistView(viewName);
            if (!exist) return null;
            return view;
        }

        /// <summary>
        /// 获取指定Panel的指定View
        /// 必须是存在的 但不一定是打开的有可能是隐藏
        /// 这个只能表示对象存在
        /// 不应该滥用 UI与UI之间还是应该使用消息通信 这个是为了解决部分问题
        /// </summary>
        public TView GetPanelViewByPanelName<TView>(string panelName)
            where TView : UIView
        {
            var info = GetPanelInfo(panelName);
            if (info == null) return null;
            if (info.Panel == null) return null;
            var (exist, view) = info.Panel.ExistView<TView>();
            if (exist == false) return null;
            return (TView)view;
        }

        #endregion
    }
}
