using System;
using Zeng.GameFrame.UIS;

using UnityEngine;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Games.UI.Home;

namespace Games.UI.RoleSelect
{
    /// <summary>
    /// Author  ZengFeng
    /// Date    2025.7.24
    /// </summary>
    public sealed partial class RoleSelectPanel:RoleSelectPanelBase
    {
    
        #region 生命周期
        
        protected override void OnUIInit()
        {
            Debug.Log($"RoleSelectPanel OnUIInit");
        }

        protected override void OnUIEnable()
        {
            Debug.Log($"RoleSelectPanel OnUIEnable");
        }

        protected override void OnUIDisable()
        {
            Debug.Log($"RoleSelectPanel OnUIDisable");
        }

        protected override void OnUIDestroy()
        {
            Debug.Log($"RoleSelectPanel OnUIDestroy");
        }

        protected override async UniTask<bool> OnOpen()
        {
            await UniTask.CompletedTask;
            Debug.Log($"RoleSelectPanel OnOpen");
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
            uiManager.CloseTopPanel();
        }
        
        protected override void OnEventClickEnterButtonAction()
        {
            uiManager.HomePanel<HomePanel>();
        }
         #endregion Event结束

    }
}