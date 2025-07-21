using System;
using Zeng.GameFrame.UIS;

using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;

namespace Games.UI.Login
{



    /// <summary>
    /// 由YIUI工具自动创建 请勿手动修改
    /// </summary>
    public abstract class LoginPanelBase:UIPanel
    {
        [ShowInInspector]
        public const string PkgName = "Login";
        [ShowInInspector]
        public const string ResName = "LoginPanel";
        
        [ShowInInspector]
        public override EWindowOption WindowOption => EWindowOption.None;
        [ShowInInspector]
        public override EPanelLayer Layer => EPanelLayer.Panel;
        [ShowInInspector]
        public override EPanelOption PanelOption => EPanelOption.None;
        [ShowInInspector]
        public override EPanelStackOption StackOption => EPanelStackOption.VisibleTween;
        [ShowInInspector]
        public override int Priority => 0;
        [ShowInInspector]
        public TMPro.TMP_InputField u_ComUserNameInput { get; private set; }
        [ShowInInspector]
        public UnityEngine.RectTransform u_ComPasswordInput { get; private set; }
        [ShowInInspector]
        public UnityEngine.UI.Toggle u_ComRememberLogin { get; private set; }
        [ShowInInspector]
        public UnityEngine.UI.Button u_ComReigisterButton { get; private set; }
        [ShowInInspector]
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