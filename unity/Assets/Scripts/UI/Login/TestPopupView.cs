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
    public sealed partial class TestPopupView:TestPopupViewBase
    {

        #region 生命周期
        
        protected override void OnUIInit()
        {
            Debug.Log($"TestPopupView OnUIInit");
        }

        protected override void OnUIEnable()
        {
            Debug.Log($"TestPopupView OnUIEnable");
        }

        protected override void OnUIDisable()
        {
            Debug.Log($"TestPopupView OnUIDisable");
        }

        protected override void OnUIDestroy()
        {
            Debug.Log($"TestPopupView OnUIDestroy");
        }

        protected override async UniTask<bool> OnOpen()
        {
            await UniTask.CompletedTask;
            Debug.Log($"TestPopupView OnOpen");
            return true;
        }

        protected override async UniTask<bool> OnOpen(ParamVo param)
        {
            return await base.OnOpen(param);
        }
        
        #endregion

        #region Event开始


       
        protected override void OnEventOpenReigsterAction()
        {
            Panel?.OpenView<ReigisterView>();
        }
        
        protected override void OnEventCloseAction()
        {
            Close();
        }
         #endregion Event结束

    }
}