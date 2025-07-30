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
        
        protected override void Initialize()
        {
            Debug.Log($"TestPopupView Initialize");
        }

        protected override void OnEnable()
        {
            Debug.Log($"TestPopupView OnEnable");
        }

        protected override void OnDisable()
        {
            Debug.Log($"TestPopupView OnDisable");
        }

        protected override void OnDestroy()
        {
            Debug.Log($"TestPopupView OnDestroy");
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