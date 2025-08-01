﻿using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;


namespace Zeng.GameFrame.UIS
{
    [InfoBox("提示: 可用事件参数 <参数1:float(当前滚动值 0-1)>")]
    [LabelText("滚动条<Float>")]
    [RequireComponent(typeof(Scrollbar))]
    [AddComponentMenu("UIBind/Event/滚动条 【Scrollbar】 UIEventBindScrollbar")]
    public class UIEventBindScrollbar : UIEventBind
    {
        [SerializeField]
        [ReadOnly]
        [Required("必须有此组件")]
        [LabelText("滚动条")]
        private Scrollbar m_Scrollbar;

        private List<EUIEventParamType> m_FilterParamType = new List<EUIEventParamType>
        {
            EUIEventParamType.Float
        };

        protected override bool IsTaskEvent => false;

        protected override List<EUIEventParamType> GetFilterParamType()
        {
            return m_FilterParamType;
        }

        private void Awake()
        {
            m_Scrollbar ??= GetComponent<Scrollbar>();
        }

        private void OnEnable()
        {
            m_Scrollbar.onValueChanged.AddListener(OnValueChanged);
        }

        private void OnDisable()
        {
            m_Scrollbar.onValueChanged.RemoveListener(OnValueChanged);
        }

        private void OnValueChanged(float value)
        {
            try
            {
                m_UIEvent?.Invoke(value);
            }
            catch (Exception e)
            {
                Logger.LogError(e);
                throw;
            }
        }
    }
}