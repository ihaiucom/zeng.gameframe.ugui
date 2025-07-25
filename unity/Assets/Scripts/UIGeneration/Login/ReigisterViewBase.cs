using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using Zeng.GameFrame.UIS;

namespace Games.UI.Login
{



    /// <summary>
    /// 由UI工具自动创建 请勿手动修改
    /// </summary>
    public abstract class ReigisterViewBase:UIView
    {
        [ShowInInspector]
        public const string PkgName = "Login";
        
        [ShowInInspector]
        public const string ResName = "ReigisterView";
        
        [ShowInInspector] protected UIEventP0 u_EventClickLoginButton { get; private set; }
        [ShowInInspector] protected UIEventHandleP0 u_EventClickLoginButtonHandle { get; private set; }

        
        protected sealed override void UIBind()
        {
            u_EventClickLoginButton = EventTable.FindEvent<UIEventP0>("u_EventClickLoginButton");
            u_EventClickLoginButtonHandle = u_EventClickLoginButton.Add(OnEventClickLoginButtonAction);

        }

        protected sealed override void UnUIBind()
        {
            u_EventClickLoginButton.Remove(u_EventClickLoginButtonHandle);

        }
     
        protected virtual void OnEventClickLoginButtonAction(){}
   
   
    }
}