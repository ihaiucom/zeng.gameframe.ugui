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
    public abstract class TestComponentBase:UIComponent
    {
        [ShowInInspector]
        public const string PkgName = "Login";
        
        [ShowInInspector]
        public const string ResName = "TestComponent";
        
        [ShowInInspector] public UnityEngine.UI.Button u_ComBtn { get; private set; }
        [ShowInInspector] public TMPro.TextMeshProUGUI u_ComTitle { get; private set; }

        
        protected sealed override void UIBind()
        {
            u_ComBtn = ComponentTable.FindComponent<UnityEngine.UI.Button>("u_ComBtn");
            u_ComTitle = ComponentTable.FindComponent<TMPro.TextMeshProUGUI>("u_ComTitle");

        }

        protected sealed override void UnUIBind()
        {

        }
     
   
   
    }
}