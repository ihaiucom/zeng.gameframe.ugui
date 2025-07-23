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
    public sealed partial class TestComponent:TestComponentBase
    {
    
        #region 生命周期
        
        protected override void OnUIInit()
        {
            Debug.Log($"TestComponent OnUIInit");
        }

        protected override void OnUIEnable()
        {
            Debug.Log($"TestComponent OnUIEnable");
        }

        protected override void OnUIDisable()
        {
            Debug.Log($"TestComponent OnUIDisable");
        }

        protected override void OnUIDestroy()
        {
            Debug.Log($"TestComponent OnUIDestroy");
        }

        #endregion

        #region Event开始


        #endregion Event结束

    }
}