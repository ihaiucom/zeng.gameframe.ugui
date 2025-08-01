﻿using System;
using UnityEngine;
using Object = UnityEngine.Object;
using Cysharp.Threading.Tasks;

namespace Zeng.GameFrame.UIS
{
    /// <summary>
    /// 不使用泛型 使用type加载的方式
    /// </summary>
    internal static partial class UILoad
    {
        internal static Object LoadAsset(string pkgName, string resName, Type assetType)
        {
            var load = LoadHelper.GetLoad(pkgName, resName);
            load.AddRefCount();
            var loadObj = load.Object;
            if (loadObj != null)
            {
                return loadObj;
            }

            var (obj, hashCode) = UILoadProxy.LoadAsset(pkgName, resName, assetType);
            if (obj == null)
            {
                load.RemoveRefCount();
                return null;
            }

            if (!LoadHelper.AddLoadHandle(obj, load))
            {
                load.RemoveRefCount();
                return null;
            }

            load.ResetHandle(obj, hashCode);
            return obj;
        }

        internal static async UniTask<Object> LoadAssetAsync(string pkgName, string resName, Type assetType)
        {
            var load = LoadHelper.GetLoad(pkgName, resName);
            load.AddRefCount();
            var loadObj = load.Object;
            if (loadObj != null)
            {
                return loadObj;
            }

            if (load.WaitAsync)
            {
                await UniTask.WaitUntil(() => !load.WaitAsync);

                loadObj = load.Object;
                if (loadObj != null)
                {
                    return loadObj;
                }
                else
                {
                    load.RemoveRefCount();
                    return null;
                }
            }

            load.SetWaitAsync(true);

            var (obj, hashCode) = await UILoadProxy.LoadAssetAsync(pkgName, resName, assetType);

            if (obj == null)
            {
                load.SetWaitAsync(false);
                load.RemoveRefCount();
                return null;
            }

            if (!LoadHelper.AddLoadHandle(obj, load))
            {
                load.SetWaitAsync(false);
                load.RemoveRefCount();
                return null;
            }

            load.ResetHandle(obj, hashCode);
            load.SetWaitAsync(false);
            return obj;
        }

        internal static void LoadAssetAsync(string pkgName, string resName, Type assetType, Action<Object> action)
        {
            LoadAssetAsyncAction(pkgName, resName, assetType, action).Forget();
        }

        private static async UniTaskVoid LoadAssetAsyncAction(
            string         pkgName,
            string         resName,
            Type           assetType,
            Action<Object> action)
        {
            var asset = await LoadAssetAsync(pkgName, resName, assetType);
            if (asset == null)
            {
                Debug.LogError($"异步加载对象失败 {pkgName} {resName}");
                return;
            }

            action?.Invoke(asset);
        }
    }
}