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
        
        protected override void Initialize()
        {
            Debug.Log($"LoginPanel Initialize");
        }

        protected override void OnEnable()
        {
            Debug.Log($"LoginPanel OnEnable");
        }

        protected override void OnDisable()
        {
            Debug.Log($"LoginPanel OnDisable");
        }

        protected override void OnDestroy()
        {
            Debug.Log($"LoginPanel OnDestroy");
        }

        protected override async UniTask<bool> OnOpen()
        {
            // await UniTask.CompletedTask;
            Debug.Log($"LoginPanel OnOpen");
            await OpenViewAsync<LoginView>();
            return true;
        }

        protected override async UniTask<bool> OnOpen(ParamVo param)
        {
            return await base.OnOpen(param);
        }
        
        #endregion

        #region Event开始


        
        protected override void OnEventClickBackButtonAction()
        {
            Close();
            
        }
         #endregion Event结束

    }
}