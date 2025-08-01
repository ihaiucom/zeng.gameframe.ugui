﻿using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;


namespace Zeng.GameFrame.UIS
{
    [InfoBox("提示: 可用事件参数 <参数1:Unity.GameObj(被控制的对象),bool(激活状态)>")]
    [LabelText("显隐<Unity.GameObj,bool>")]
    [AddComponentMenu("UIBind/Event/显隐 【Active】 UIEventBindActive")]
    public class UIEventBindActive : UIEventBind
    {
        protected override bool IsTaskEvent => false;

        private List<EUIEventParamType> m_FilterParamType = new List<EUIEventParamType>
        {
            EUIEventParamType.UnityGameObject,
            EUIEventParamType.Bool,
        };

        protected override List<EUIEventParamType> GetFilterParamType()
        {
            return m_FilterParamType;
        }

        private void OnEnable()
        {
            try
            {
                m_UIEvent?.Invoke(gameObject, true);
            }
            catch (Exception e)
            {
                Logger.LogError(e);
                throw;
            }
        }

        private void OnDisable()
        {
            try
            {
                m_UIEvent?.Invoke(gameObject, false);
            }
            catch (Exception e)
            {
                Logger.LogError(e);
                throw;
            }
        }
    }
}