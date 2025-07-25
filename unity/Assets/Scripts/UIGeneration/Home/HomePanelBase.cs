using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using Zeng.GameFrame.UIS;

namespace Games.UI.Home
{



    /// <summary>
    /// 由UI工具自动创建 请勿手动修改
    /// </summary>
    public abstract class HomePanelBase:UIPanel
    {
        [ShowInInspector]
        public const string PkgName = "Home";
        
        [ShowInInspector]
        public const string ResName = "HomePanel";
        
        [ShowInInspector] public override EWindowOption WindowOption => EWindowOption.None;
        [ShowInInspector] public override EPanelLayer Layer => EPanelLayer.Panel;
        [ShowInInspector] public override EPanelOption PanelOption => EPanelOption.None;
        [ShowInInspector] public override EPanelStackOption StackOption => EPanelStackOption.VisibleTween;
        [ShowInInspector] public override int Priority => 0;
        [ShowInInspector] public Zeng.GameFrame.UIS.UI3DDisplay u_ComModel { get; private set; }
        [ShowInInspector] protected UIEventP1<string> u_EventClickMenuButton { get; private set; }
        [ShowInInspector] protected UIEventHandleP1<string> u_EventClickMenuButtonHandle { get; private set; }
        [ShowInInspector] protected UIEventP0 u_EventClickBackButton { get; private set; }
        [ShowInInspector] protected UIEventHandleP0 u_EventClickBackButtonHandle { get; private set; }

        
        protected sealed override void UIBind()
        {
            u_ComModel = ComponentTable.FindComponent<Zeng.GameFrame.UIS.UI3DDisplay>("u_ComModel");
            u_EventClickMenuButton = EventTable.FindEvent<UIEventP1<string>>("u_EventClickMenuButton");
            u_EventClickMenuButtonHandle = u_EventClickMenuButton.Add(OnEventClickMenuButtonAction);
            u_EventClickBackButton = EventTable.FindEvent<UIEventP0>("u_EventClickBackButton");
            u_EventClickBackButtonHandle = u_EventClickBackButton.Add(OnEventClickBackButtonAction);

        }

        protected sealed override void UnUIBind()
        {
            u_EventClickMenuButton.Remove(u_EventClickMenuButtonHandle);
            u_EventClickBackButton.Remove(u_EventClickBackButtonHandle);

        }
     
        protected virtual void OnEventClickMenuButtonAction(string p1){}
        protected virtual void OnEventClickBackButtonAction(){}
   
   
    }
}