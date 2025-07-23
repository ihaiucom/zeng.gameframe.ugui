using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using Zeng.GameFrame.UIS;

namespace Games.UI.Login
{

    public enum ELoginPanelViewEnum
    {
        ReigisterView = 1,
        LoginView = 2,
        TestPopupView = 3,
    }

    /// <summary>
    /// 由UI工具自动创建 请勿手动修改
    /// </summary>
    public abstract class LoginPanelBase:UIPanel
    {
        [ShowInInspector]
        public const string PkgName = "Login";
        
        [ShowInInspector]
        public const string ResName = "LoginPanel";
        
        [ShowInInspector] public override EWindowOption WindowOption => EWindowOption.None;
        [ShowInInspector] public override EPanelLayer Layer => EPanelLayer.Panel;
        [ShowInInspector] public override EPanelOption PanelOption => EPanelOption.None;
        [ShowInInspector] public override EPanelStackOption StackOption => EPanelStackOption.VisibleTween;
        [ShowInInspector] public override int Priority => 0;
        [ShowInInspector] public Games.UI.Login.TestComponent u_UITestComponent { get; private set; }

        
        protected sealed override void UIBind()
        {
            u_UITestComponent = CDETable.FindUIBase<Games.UI.Login.TestComponent>("TestComponent");

        }

        protected sealed override void UnUIBind()
        {

        }
     
   
   
    }
}