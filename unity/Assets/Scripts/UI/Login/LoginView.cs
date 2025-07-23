using System;
using Zeng.GameFrame.UIS;

using UnityEngine;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace Games.UI.Login
{
    /// <summary>
    /// Author  UI
    /// Date    2025.7.24
    /// </summary>
    public sealed partial class LoginView:LoginViewBase
    {

        #region 生命周期
        
        protected override void OnUIInit()
        {
            Debug.Log($"LoginView OnUIInit");
        }

        protected override void OnUIEnable()
        {
            Debug.Log($"LoginView OnUIEnable");
        }

        protected override void OnUIDisable()
        {
            Debug.Log($"LoginView OnUIDisable");
        }

        protected override void OnUIDestroy()
        {
            Debug.Log($"LoginView OnUIDestroy");
        }

        protected override async UniTask<bool> OnOpen()
        {
            await UniTask.CompletedTask;
            Debug.Log($"LoginView OnOpen");
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
            Panel?.OpenView<TestPopupView>();
        }
        
        protected override void OnEventClickReigisterButtonAction()
        {
            Panel?.OpenView<ReigisterView>();
            
        }
         #endregion Event结束

    }
}