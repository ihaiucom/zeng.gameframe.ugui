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
    public abstract class TestPopupViewBase:UIView
    {
        [ShowInInspector]
        public const string PkgName = "Login";
        
        [ShowInInspector]
        public const string ResName = "TestPopupView";
        
        [ShowInInspector] protected UIEventP0 u_EventOpenReigster { get; private set; }
        [ShowInInspector] protected UIEventHandleP0 u_EventOpenReigsterHandle { get; private set; }
        [ShowInInspector] protected UIEventP0 u_EventClose { get; private set; }
        [ShowInInspector] protected UIEventHandleP0 u_EventCloseHandle { get; private set; }

        
        protected sealed override void UIBind()
        {
            u_EventOpenReigster = EventTable.FindEvent<UIEventP0>("u_EventOpenReigster");
            u_EventOpenReigsterHandle = u_EventOpenReigster.Add(OnEventOpenReigsterAction);
            u_EventClose = EventTable.FindEvent<UIEventP0>("u_EventClose");
            u_EventCloseHandle = u_EventClose.Add(OnEventCloseAction);

        }

        protected sealed override void UnUIBind()
        {
            u_EventOpenReigster.Remove(u_EventOpenReigsterHandle);
            u_EventClose.Remove(u_EventCloseHandle);

        }
     
        protected virtual void OnEventOpenReigsterAction(){}
        protected virtual void OnEventCloseAction(){}
   
   
    }
}