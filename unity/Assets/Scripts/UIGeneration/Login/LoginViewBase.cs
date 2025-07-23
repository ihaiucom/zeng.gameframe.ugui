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
    public abstract class LoginViewBase:UIView
    {
        [ShowInInspector]
        public const string PkgName = "Login";
        
        [ShowInInspector]
        public const string ResName = "LoginView";
        
        [ShowInInspector] public override EWindowOption WindowOption => EWindowOption.None;
        [ShowInInspector] public override EViewWindowType ViewWindowType => EViewWindowType.View;
        [ShowInInspector] public override EViewStackOption StackOption => EViewStackOption.VisibleTween;
        [ShowInInspector] public TMPro.TMP_InputField u_ComUserNameInput { get; private set; }
        [ShowInInspector] public UnityEngine.RectTransform u_ComPasswordInput { get; private set; }
        [ShowInInspector] public UnityEngine.UI.Toggle u_ComRememberLogin { get; private set; }
        [ShowInInspector] public UnityEngine.UI.Button u_ComReigisterButton { get; private set; }
        [ShowInInspector] public UnityEngine.UI.Button u_ComLoginButton { get; private set; }
        [ShowInInspector] public Zeng.GameFrame.UIS.UIDataValueBool u_DataIsRememberLogin { get; private set; }
        [ShowInInspector] protected UIEventP0 u_EventClickLoginButton { get; private set; }
        [ShowInInspector] protected UIEventHandleP0 u_EventClickLoginButtonHandle { get; private set; }
        [ShowInInspector] protected UIEventP0 u_EventClickReigisterButton { get; private set; }
        [ShowInInspector] protected UIEventHandleP0 u_EventClickReigisterButtonHandle { get; private set; }

        
        protected sealed override void UIBind()
        {
            u_ComUserNameInput = ComponentTable.FindComponent<TMPro.TMP_InputField>("u_ComUserNameInput");
            u_ComPasswordInput = ComponentTable.FindComponent<UnityEngine.RectTransform>("u_ComPasswordInput");
            u_ComRememberLogin = ComponentTable.FindComponent<UnityEngine.UI.Toggle>("u_ComRememberLogin");
            u_ComReigisterButton = ComponentTable.FindComponent<UnityEngine.UI.Button>("u_ComReigisterButton");
            u_ComLoginButton = ComponentTable.FindComponent<UnityEngine.UI.Button>("u_ComLoginButton");
            u_DataIsRememberLogin = DataTable.FindDataValue<Zeng.GameFrame.UIS.UIDataValueBool>("u_DataIsRememberLogin");
            u_EventClickLoginButton = EventTable.FindEvent<UIEventP0>("u_EventClickLoginButton");
            u_EventClickLoginButtonHandle = u_EventClickLoginButton.Add(OnEventClickLoginButtonAction);
            u_EventClickReigisterButton = EventTable.FindEvent<UIEventP0>("u_EventClickReigisterButton");
            u_EventClickReigisterButtonHandle = u_EventClickReigisterButton.Add(OnEventClickReigisterButtonAction);

        }

        protected sealed override void UnUIBind()
        {
            u_EventClickLoginButton.Remove(u_EventClickLoginButtonHandle);
            u_EventClickReigisterButton.Remove(u_EventClickReigisterButtonHandle);

        }
     
        protected virtual void OnEventClickLoginButtonAction(){}
        protected virtual void OnEventClickReigisterButtonAction(){}
   
   
    }
}