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
    public sealed partial class ReigisterView:ReigisterViewBase
    {

        #region 生命周期
        
        protected override void OnUIInit()
        {
            Debug.Log($"ReigisterView OnUIInit");
        }

        protected override void OnUIEnable()
        {
            Debug.Log($"ReigisterView OnUIEnable");
        }

        protected override void OnUIDisable()
        {
            Debug.Log($"ReigisterView OnUIDisable");
        }

        protected override void OnUIDestroy()
        {
            Debug.Log($"ReigisterView OnUIDestroy");
        }

        protected override async UniTask<bool> OnOpen()
        {
            await UniTask.CompletedTask;
            Debug.Log($"ReigisterView OnOpen");
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
            Panel?.OpenView<LoginView>();
        }
         #endregion Event结束

    }
}