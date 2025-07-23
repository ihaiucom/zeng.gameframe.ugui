using System;
using Zeng.GameFrame.UIS;

using UnityEngine;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace Games.UI.Login
{
    /// <summary>
    /// Author  UI
    /// Date    2025.7.21
    /// </summary>
    public sealed partial class LoginPanel:LoginPanelBase
    {
    
        #region 生命周期
        
        protected override void OnUIInit()
        {
            Debug.Log($"LoginPanel OnUIInit");
        }

        protected override void OnUIEnable()
        {
            Debug.Log($"LoginPanel OnUIEnable");
        }

        protected override void OnUIDisable()
        {
            Debug.Log($"LoginPanel OnUIDisable");
        }

        protected override void OnUIDestroy()
        {
            Debug.Log($"LoginPanel OnUIDestroy");
        }

        protected override async UniTask<bool> OnOpen()
        {
            await UniTask.CompletedTask;
            Debug.Log($"LoginPanel OnOpen");
            return true;
        }

        protected override async UniTask<bool> OnOpen(ParamVo param)
        {
            return await base.OnOpen(param);
        }
        
        #endregion

        #region Event开始


       
        protected override void OnEventClickLoginButtonAction()
        {
            
            Debug.Log($"LoginPanel OnEventClickLoginButtonAction");
        }
         #endregion Event结束

    }
}