﻿using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;


namespace Zeng.GameFrame.UIS
{
    [InfoBox("提示: 可用事件参数 <参数1:float(当前滑动值)>")]
    [LabelText("滑动条<Float>")]
    [RequireComponent(typeof(Slider))]
    [AddComponentMenu("UIBind/Event/滑动条 【Slider】 UIEventBindSlider")]
    public class UIEventBindSlider : UIEventBind
    {
        [SerializeField]
        [ReadOnly]
        [Required("必须有此组件")]
        [LabelText("滑动条")]
        private Slider m_Slider;

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
            m_Slider ??= GetComponent<Slider>();
        }

        private void OnEnable()
        {
            m_Slider.onValueChanged.AddListener(OnValueChanged);
        }

        private void OnDisable()
        {
            m_Slider.onValueChanged.RemoveListener(OnValueChanged);
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