﻿using UnityEngine;
using Cysharp.Threading.Tasks;

namespace Zeng.GameFrame.UIS
{
    public static partial class UIFactory
    {
        //普通的UI预制体 创建与摧毁 一定要成对
        //为了防止忘记 所以默认自动回收
        public static GameObject InstantiateGameObject(string pkgName, string resName)
        {
            var obj = UILoad.LoadAssetInstantiate(pkgName, resName);
            if (obj == null)
            {
                Debug.LogError($"没有加载到这个资源 {pkgName}/{resName}");
                return null;
            }

            //强制添加 既然你要使用这个方法那就必须接受 否则请使用其他方式
            //被摧毁时 自动回收 无需调用 UIFactory.Destroy
            obj.AddComponent<UIInstantiateRelease>();

            return obj;
        }

        public static async UniTask<GameObject> InstantiateGameObjectAsync(string pkgName, string resName)
        {
            var obj = await UILoad.LoadAssetAsyncInstantiate(pkgName, resName);
            if (obj == null)
            {
                Debug.LogError($"没有加载到这个资源 {pkgName}/{resName}");
                return null;
            }

            obj.AddComponent<UIInstantiateRelease>();

            return obj;
        }
    }
}