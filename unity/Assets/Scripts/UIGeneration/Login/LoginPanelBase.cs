using System;
using Zeng.GameFrame.UIS;

using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace Games.UI.Login
{



    /// <summary>
    /// 由YIUI工具自动创建 请勿手动修改
    /// </summary>
    public abstract class LoginPanelBase:UIPanel
    {
        public const string PkgName = "Login";
        public const string ResName = "LoginPanel";
        
        public override EWindowOption WindowOption => EWindowOption.None;
        public override EPanelLayer Layer => EPanelLayer.Panel;
        public override EPanelOption PanelOption => EPanelOption.None;
        public override EPanelStackOption StackOption => EPanelStackOption.VisibleTween;
        public override int Priority => 0;
        public TMPro.TMP_InputField u_ComUserNameInput { get; private set; }
        public UnityEngine.RectTransform u_ComPasswordInput { get; private set; }
        public UnityEngine.UI.Toggle u_ComRememberLogin { get; private set; }
        public UnityEngine.UI.Button u_ComReigisterButton { get; private set; }
        public UnityEngine.UI.Button u_ComLoginButton { get; private set; }

        
        protected sealed override void UIBind()
        {
            u_ComUserNameInput = ComponentTable.FindComponent<TMPro.TMP_InputField>("u_ComUserNameInput");
            u_ComPasswordInput = ComponentTable.FindComponent<UnityEngine.RectTransform>("u_ComPasswordInput");
            u_ComRememberLogin = ComponentTable.FindComponent<UnityEngine.UI.Toggle>("u_ComRememberLogin");
            u_ComReigisterButton = ComponentTable.FindComponent<UnityEngine.UI.Button>("u_ComReigisterButton");
            u_ComLoginButton = ComponentTable.FindComponent<UnityEngine.UI.Button>("u_ComLoginButton");

        }

        protected sealed override void UnUIBind()
        {

        }
     
   
   
    }
}