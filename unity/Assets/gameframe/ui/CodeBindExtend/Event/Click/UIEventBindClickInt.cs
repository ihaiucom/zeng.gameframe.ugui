﻿using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Zeng.GameFrame.UIS
{
    /// <summary>
    /// 点击事件绑定
    /// 与按钮无关
    /// 只要是任何可以被射线检测的物体都可以响应点击事件
    /// 额外多一个int 参数自定义
    /// </summary>
    [InfoBox("提示: 可用事件参数 1个")]
    [LabelText("点击<int>")]
    [AddComponentMenu("UIBind/Event/点击 【Click Int】 UIEventBindClickInt")]
    public class UIEventBindClickInt : UIEventBindClick
    {
        [SerializeField]
        [LabelText("额外int参数值")]
        private int m_ExtraParam;

        [NonSerialized]
        private List<EUIEventParamType> m_FilterParamType = new List<EUIEventParamType>
        {
            EUIEventParamType.Int,
        };

        protected override List<EUIEventParamType> GetFilterParamType()
        {
            return m_FilterParamType;
        }

        protected override void OnUIEvent(PointerEventData eventData)
        {
            m_UIEvent?.Invoke(m_ExtraParam);
        }
    }
}