using Zeng.GameFrame.UIS;

namespace UICodeGenerated
{
    /// <summary>
    /// 由UI工具自动创建 请勿手动修改
    /// 用法: UIBindHelper.InternalGameGetUIBindVoFunc = UICodeGenerated.UIBindProvider.Get;
    /// </summary>
    public static class UIBindProvider
    {
        public static UIBindVo[] Get()
        {
            var UIPanel     = typeof(UIPanel);
            var UIView      = typeof(UIView);
            var UIComponent = typeof(UIComponent);
            var list          = new UIBindVo[8];
            list[0] = new UIBindVo
            {
                PkgName     = Games.UI.RoleSelect.RoleSelectPanelBase.PkgName,
                ResName     = Games.UI.RoleSelect.RoleSelectPanelBase.ResName,
                CodeType    = UIPanel,
                BaseType    = typeof(Games.UI.RoleSelect.RoleSelectPanelBase),
                CreatorType = typeof(Games.UI.RoleSelect.RoleSelectPanel),
            };
            list[1] = new UIBindVo
            {
                PkgName     = Games.UI.Login.LoginPanelBase.PkgName,
                ResName     = Games.UI.Login.LoginPanelBase.ResName,
                CodeType    = UIPanel,
                BaseType    = typeof(Games.UI.Login.LoginPanelBase),
                CreatorType = typeof(Games.UI.Login.LoginPanel),
            };
            list[2] = new UIBindVo
            {
                PkgName     = Games.UI.Home.HomePanelBase.PkgName,
                ResName     = Games.UI.Home.HomePanelBase.ResName,
                CodeType    = UIPanel,
                BaseType    = typeof(Games.UI.Home.HomePanelBase),
                CreatorType = typeof(Games.UI.Home.HomePanel),
            };
            list[3] = new UIBindVo
            {
                PkgName     = Games.UI.Login.LoginViewBase.PkgName,
                ResName     = Games.UI.Login.LoginViewBase.ResName,
                CodeType    = UIView,
                BaseType    = typeof(Games.UI.Login.LoginViewBase),
                CreatorType = typeof(Games.UI.Login.LoginView),
            };
            list[4] = new UIBindVo
            {
                PkgName     = Games.UI.Login.ReigisterViewBase.PkgName,
                ResName     = Games.UI.Login.ReigisterViewBase.ResName,
                CodeType    = UIView,
                BaseType    = typeof(Games.UI.Login.ReigisterViewBase),
                CreatorType = typeof(Games.UI.Login.ReigisterView),
            };
            list[5] = new UIBindVo
            {
                PkgName     = Games.UI.Login.TestPopupViewBase.PkgName,
                ResName     = Games.UI.Login.TestPopupViewBase.ResName,
                CodeType    = UIView,
                BaseType    = typeof(Games.UI.Login.TestPopupViewBase),
                CreatorType = typeof(Games.UI.Login.TestPopupView),
            };
            list[6] = new UIBindVo
            {
                PkgName     = Games.UI.RoleSelect.RoleItemBase.PkgName,
                ResName     = Games.UI.RoleSelect.RoleItemBase.ResName,
                CodeType    = UIComponent,
                BaseType    = typeof(Games.UI.RoleSelect.RoleItemBase),
                CreatorType = typeof(Games.UI.RoleSelect.RoleItem),
            };
            list[7] = new UIBindVo
            {
                PkgName     = Games.UI.Login.TestComponentBase.PkgName,
                ResName     = Games.UI.Login.TestComponentBase.ResName,
                CodeType    = UIComponent,
                BaseType    = typeof(Games.UI.Login.TestComponentBase),
                CreatorType = typeof(Games.UI.Login.TestComponent),
            };

            return list;
        }
    }
}