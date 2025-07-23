namespace Zeng.GameFrame.UIS
{
    public partial class UIManager
    {
        #region 判断Panel是否存在且显示

        public bool ActiveSelf(string panelName)
        {
            var info = GetPanelInfo(panelName);
            return info?.ActiveSelf ?? false;
        }

        public bool ActiveSelf<T>() where T : UIPanel
        {
            var info = GetPanelInfo<T>();
            return info?.ActiveSelf ?? false;
        }

        #endregion

        #region 判断指定Panel的指定View是否存在且显示

        public bool ActiveSelfView(string panelName, string viewName)
        {
            var info = GetPanelInfo(panelName);
            if (info == null) return false;
            if (info.Panel == null) return false;
            var (exist, entity) = info.Panel.ExistView(viewName);
            if (!exist) return false;
            return entity.ActiveSelf;
        }

        public bool ActiveSelfView<TPanel, TView>()
            where TPanel : UIPanel
            where TView : UIView
        {
            var info = GetPanelInfo<TPanel>();
            if (info == null) return false;
            if (info.Panel == null) return false;
            var (exist, entity) = info.Panel.ExistView<TView>();
            if (!exist) return false;
            return entity.ActiveSelf;
        }

        public bool ActiveSelfViewByViewName<TPanel>(string viewName)
            where TPanel : UIPanel
        {
            var info = GetPanelInfo<TPanel>();
            if (info == null) return false;
            if (info.Panel == null) return false;
            var (exist, entity) = info.Panel.ExistView(viewName);
            if (!exist) return false;
            return entity.ActiveSelf;
        }

        public bool ActiveSelfViewByPanelName<TView>(string panelName)
            where TView : UIView
        {
            var info = GetPanelInfo(panelName);
            if (info == null) return false;
            if (info.Panel == null) return false;
            var (exist, entity) = info.Panel.ExistView<TView>();
            if (!exist) return false;
            return entity.ActiveSelf;
        }

        #endregion
    }
}
