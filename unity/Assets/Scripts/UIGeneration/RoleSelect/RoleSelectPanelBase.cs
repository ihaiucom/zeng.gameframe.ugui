using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using Zeng.GameFrame.UIS;

namespace Games.UI.RoleSelect
{



    /// <summary>
    /// 由UI工具自动创建 请勿手动修改
    /// </summary>
    public abstract class RoleSelectPanelBase:UIPanel
    {
        [ShowInInspector]
        public const string PkgName = "RoleSelect";
        
        [ShowInInspector]
        public const string ResName = "RoleSelectPanel";
        
        [ShowInInspector] public override EWindowOption WindowOption => EWindowOption.None;
        [ShowInInspector] public override EPanelLayer Layer => EPanelLayer.Panel;
        [ShowInInspector] public override EPanelOption PanelOption => EPanelOption.None;
        [ShowInInspector] public override EPanelStackOption StackOption => EPanelStackOption.VisibleTween;
        [ShowInInspector] public override int Priority => 0;
        [ShowInInspector] protected UIEventP0 u_EventClickBackButton { get; private set; }
        [ShowInInspector] protected UIEventHandleP0 u_EventClickBackButtonHandle { get; private set; }
        [ShowInInspector] protected UIEventP0 u_EventClickEnterButton { get; private set; }
        [ShowInInspector] protected UIEventHandleP0 u_EventClickEnterButtonHandle { get; private set; }

        
        protected sealed override void UIBind()
        {
            u_EventClickBackButton = EventTable.FindEvent<UIEventP0>("u_EventClickBackButton");
            u_EventClickBackButtonHandle = u_EventClickBackButton.Add(OnEventClickBackButtonAction);
            u_EventClickEnterButton = EventTable.FindEvent<UIEventP0>("u_EventClickEnterButton");
            u_EventClickEnterButtonHandle = u_EventClickEnterButton.Add(OnEventClickEnterButtonAction);

        }

        protected sealed override void UnUIBind()
        {
            u_EventClickBackButton.Remove(u_EventClickBackButtonHandle);
            u_EventClickEnterButton.Remove(u_EventClickEnterButtonHandle);

        }
     
        protected virtual void OnEventClickBackButtonAction(){}
        protected virtual void OnEventClickEnterButtonAction(){}
   
   
    }
}