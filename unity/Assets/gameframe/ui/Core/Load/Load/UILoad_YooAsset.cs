﻿using System;
using Object = UnityEngine.Object;
using Cysharp.Threading.Tasks;

namespace Zeng.GameFrame.UIS
{
    /// <summary>
    /// YooAsset扩展  因为他不需要pkgName
    /// </summary>
    internal static partial class UILoad
    {
        internal static T LoadAsset<T>(string resName) where T : Object
        {
            return LoadAsset<T>("", resName);
        }

        internal static async UniTask<T> LoadAssetAsync<T>(string resName) where T : Object
        {
            return await LoadAssetAsync<T>("", resName);
        }

        internal static void LoadAssetAsync<T>(string resName, Action<T> action) where T : Object
        {
            LoadAssetAsync<T>("", resName, action);
        }

        internal static bool VerifyAssetValidity(string resName)
        {
            return VerifyAssetValidity("", resName);
        }

        #region 非泛型

        internal static Object LoadAsset(string resName, Type assetType)
        {
            return LoadAsset("", resName, assetType);
        }

        internal static async UniTask<Object> LoadAssetAsync(string resName, Type assetType)
        {
            return await LoadAssetAsync("", resName, assetType);
        }

        internal static void LoadAssetAsync(string resName, Type assetType, Action<Object> action)
        {
            LoadAssetAsync("", resName, assetType, action);
        }

        #endregion
    }
}