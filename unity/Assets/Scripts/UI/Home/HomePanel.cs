using System;
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
    
        #region 生命周期
        
        protected override void OnUIInit()
        {
            Debug.Log($"HomePanel OnUIInit");
        }

        protected override void OnUIEnable()
        {
            Debug.Log($"HomePanel OnUIEnable");
        }

        protected override void OnUIDisable()
        {
            Debug.Log($"HomePanel OnUIDisable");
        }

        protected override void OnUIDestroy()
        {
            Debug.Log($"HomePanel OnUIDestroy");
        }

        protected override async UniTask<bool> OnOpen()
        {
            await UniTask.CompletedTask;
            Debug.Log($"HomePanel OnOpen");
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
            
        }
         #endregion Event结束

    }
}