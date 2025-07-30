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
        
        protected override void Initialize()
        {
            Debug.Log($"TestComponent Initialize");
        }

        protected override void OnEnable()
        {
            Debug.Log($"TestComponent OnEnable");
        }

        protected override void OnDisable()
        {
            Debug.Log($"TestComponent OnDisable");
        }

        protected override void OnDestroy()
        {
            Debug.Log($"TestComponent OnDestroy");
        }

        #endregion

        #region Event开始


        #endregion Event结束

    }
}