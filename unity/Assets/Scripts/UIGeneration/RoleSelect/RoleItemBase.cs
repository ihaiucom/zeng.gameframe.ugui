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
    public abstract class RoleItemBase:UIComponent
    {
        [ShowInInspector]
        public const string PkgName = "RoleSelect";
        
        [ShowInInspector]
        public const string ResName = "RoleItem";
        
        [ShowInInspector] public TMPro.TextMeshProUGUI u_ComName { get; private set; }
        [ShowInInspector] public UnityEngine.UI.Image u_ComIcon { get; private set; }
        [ShowInInspector] public TMPro.TextMeshProUGUI u_ComPrice { get; private set; }
        [ShowInInspector] public UnityEngine.UI.Image u_ComSelectImage { get; private set; }
        [ShowInInspector] public Zeng.GameFrame.UIS.UIDataValueBool u_DataIsSelected { get; private set; }
        [ShowInInspector] public Zeng.GameFrame.UIS.UIDataValueString u_DataIconPath { get; private set; }
        [ShowInInspector] protected UIEventP0 u_EventClickSelect { get; private set; }
        [ShowInInspector] protected UIEventHandleP0 u_EventClickSelectHandle { get; private set; }

        
        protected sealed override void UIBind()
        {
            u_ComName = ComponentTable.FindComponent<TMPro.TextMeshProUGUI>("u_ComName");
            u_ComIcon = ComponentTable.FindComponent<UnityEngine.UI.Image>("u_ComIcon");
            u_ComPrice = ComponentTable.FindComponent<TMPro.TextMeshProUGUI>("u_ComPrice");
            u_ComSelectImage = ComponentTable.FindComponent<UnityEngine.UI.Image>("u_ComSelectImage");
            u_DataIsSelected = DataTable.FindDataValue<Zeng.GameFrame.UIS.UIDataValueBool>("u_DataIsSelected");
            u_DataIconPath = DataTable.FindDataValue<Zeng.GameFrame.UIS.UIDataValueString>("u_DataIconPath");
            u_EventClickSelect = EventTable.FindEvent<UIEventP0>("u_EventClickSelect");
            u_EventClickSelectHandle = u_EventClickSelect.Add(OnEventClickSelectAction);

        }

        protected sealed override void UnUIBind()
        {
            u_EventClickSelect.Remove(u_EventClickSelectHandle);

        }
     
        protected virtual void OnEventClickSelectAction(){}
   
   
    }
}