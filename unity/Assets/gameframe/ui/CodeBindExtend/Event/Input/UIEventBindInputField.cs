﻿using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;


namespace Zeng.GameFrame.UIS
{
    [InfoBox("提示: 可用事件参数 <参数1:string(输入的值)>")]
    [LabelText("输入栏<string> 变化时")]
    [RequireComponent(typeof(InputField))]
    [AddComponentMenu("UIBind/Event/输入栏 【InputField】 UIEventBindInputField")]
    public class UIEventBindInputField : UIEventBind
    {
        [SerializeField]
        [ReadOnly]
        [Required("必须有此组件")]
        [LabelText("输入栏")]
        private InputField m_InputField;

        private List<EUIEventParamType> m_FilterParamType = new List<EUIEventParamType>
        {
            EUIEventParamType.String
        };

        protected override bool IsTaskEvent => false;

        protected override List<EUIEventParamType> GetFilterParamType()
        {
            return m_FilterParamType;
        }

        private void Awake()
        {
            m_InputField ??= GetComponent<InputField>();
        }

        private void OnEnable()
        {
            m_InputField.onValueChanged.AddListener(OnValueChanged);
        }

        private void OnDisable()
        {
            m_InputField.onValueChanged.RemoveListener(OnValueChanged);
        }

        private void OnValueChanged(string value)
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