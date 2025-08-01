﻿using System;
using Zeng.GameFrame.UIS;

using UnityEngine;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace Games.UI.Home
{
    /// <summary>
    /// Author  ZengFeng
    /// Date    2025.7.24
    /// </summary>
    public sealed partial class HomePanel:HomePanelBase
    {
    
        private UI3DDisplayExtend m_Ui3DDisplay;
        #region 生命周期
        
        protected override void Initialize()
        {
            Debug.Log($"HomePanel Initialize");
            m_Ui3DDisplay = new UI3DDisplayExtend(u_ComModel);
        }

        protected override void OnEnable()
        {
            Debug.Log($"HomePanel OnEnable");
        }

        protected override void OnDisable()
        {
            Debug.Log($"HomePanel OnDisable");
        }

        protected override void OnDestroy()
        {
            Debug.Log($"HomePanel OnDestroy");
        }

        protected override async UniTask<bool> OnOpen()
        {
            await UniTask.CompletedTask;
            Debug.Log($"HomePanel OnOpen");
            
            
            m_Ui3DDisplay.Show("Assets/GameRes/Units/Cube.prefab");
            return true;
        }

        protected override async UniTask<bool> OnOpen(ParamVo param)
        {
            return await base.OnOpen(param);
        }
        
        #endregion

        #region Event开始


       
        protected override void OnEventClickMenuButtonAction(string p1)
        {
            uiManager.OpenPanel(p1);
        }
        
        protected override void OnEventClickBackButtonAction()
        {
            Close();
            
        }
         #endregion Event结束

    }
}