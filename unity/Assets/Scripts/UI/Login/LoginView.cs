using System;
using Zeng.GameFrame.UIS;

using UnityEngine;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Games.UI.RoleSelect;

namespace Games.UI.Login
{
    /// <summary>
    /// Author  UI
    /// Date    2025.7.24
    /// </summary>
    public sealed partial class LoginView:LoginViewBase
    {

        #region 生命周期
        
        protected override void Initialize()
        {
            Debug.Log($"LoginView Initialize");
        }

        protected override void OnEnable()
        {
            Debug.Log($"LoginView OnEnable");
        }

        protected override void OnDisable()
        {
            Debug.Log($"LoginView OnDisable");
        }

        protected override void OnDestroy()
        {
            Debug.Log($"LoginView OnDestroy");
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
            // Panel?.OpenView<TestPopupView>();
            uiManager.OpenPanel<RoleSelectPanel>();
        }
        
        protected override void OnEventClickReigisterButtonAction()
        {
            Panel?.OpenView<ReigisterView>();
            
        }
         #endregion Event结束

    }
}