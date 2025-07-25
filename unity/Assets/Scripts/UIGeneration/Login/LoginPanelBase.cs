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
        
        [ShowInInspector] public Games.UI.Login.TestComponent u_UITestComponent { get; private set; }
        [ShowInInspector] protected UIEventP0 u_EventClickBackButton { get; private set; }
        [ShowInInspector] protected UIEventHandleP0 u_EventClickBackButtonHandle { get; private set; }

        
        protected sealed override void UIBind()
        {
            u_EventClickBackButton = EventTable.FindEvent<UIEventP0>("u_EventClickBackButton");
            u_EventClickBackButtonHandle = u_EventClickBackButton.Add(OnEventClickBackButtonAction);
            u_UITestComponent = CDETable.FindUIBase<Games.UI.Login.TestComponent>("TestComponent");

        }

        protected sealed override void UnUIBind()
        {
            u_EventClickBackButton.Remove(u_EventClickBackButtonHandle);

        }
     
        protected virtual void OnEventClickBackButtonAction(){}
   
   
    }
}