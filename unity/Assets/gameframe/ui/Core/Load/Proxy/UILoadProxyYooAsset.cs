using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using YooAsset;
using Object = UnityEngine.Object;

namespace Zeng.GameFrame.UIS
{
    public class UILoadProxyYooAsset : Singleton<UILoadProxyYooAsset>
    {

        private ResourcePackage package;

        //这里是简单的本地记录YooAsset 根据你项目应该有一个资源管理器统一管理这里只是演示所以很简陋
        private Dictionary<int, AssetHandle> m_AllHandle = new();

        public void Init(ResourcePackage package)
        {
            this.package = package;

            //UI会用到的各种加载 需要自行实现 Demo中使用的是YooAsset 根据自己项目的资源管理器实现下面的方法
            UILoadProxy.LoadAssetFunc = LoadAsset; //同步加载
            UILoadProxy.LoadAssetAsyncFunc = LoadAssetAsync; //异步加载
            UILoadProxy.ReleaseAction = ReleaseAction; //释放
            UILoadProxy.VerifyAssetValidityFunc = VerifyAssetValidityFunc; //检查
            UILoadProxy.ReleaseAllAction = ReleaseAllAction; //释放所有
        }

        protected override void OnDispose()
        {
            this.package = null;
            m_AllHandle.Clear();
            base.OnDispose();
        }


        /// <summary>
        /// 同步加载
        /// </summary>
        /// <param name="packageName">包名</param>
        /// <param name="location">资源名</param>
        /// <param name="type">类型</param>
        /// <returns>返回值(obj资源对象,唯一ID)</returns>
        private (Object, int) LoadAsset(string packageName, string location, Type type)
        {
            // Debug.Log($"LoadAsset：packageName={packageName}, location={location}, type={type}");
            var handle = package.LoadAssetSync(location, type);
            return LoadAssetHandle(handle);
        }


        //Demo中对YooAsset加载后的一个简单返回封装
        //只有成功加载才返回 否则直接释放
        private (Object, int) LoadAssetHandle(AssetHandle handle)
        {
            if (handle.AssetObject != null)
            {
                var hashCode = handle.GetHashCode();
                m_AllHandle.Add(hashCode, handle);
                return (handle.AssetObject, hashCode);
            }
            else
            {
                handle.Release();
                return (null, 0);
            }
        }
        
        
        /// <summary>
        /// 异步加载
        /// </summary>
        /// <param name="packageName">包名</param>
        /// <param name="location">资源名</param>
        /// <param name="type">类型</param>
        /// <returns>返回值(obj资源对象,唯一ID)</returns>
        private async UniTask<(Object, int)> LoadAssetAsync(string packageName, string location, Type type)
        {
            // Debug.Log($"LoadAssetAsync：packageName={packageName}, location={location}, type={type}");
            var handle = package.LoadAssetAsync(location, type);
            await handle.ToUniTask(); //异步等待 需要实现YooAsset在UniTask中的异步扩展
            return LoadAssetHandle(handle);
        }
        
        
        
        /// <summary>
        /// 释放方法
        /// </summary>
        /// <param name="hashCode">加载时所给到的唯一ID</param>
        private void ReleaseAction(int hashCode)
        {
            if (m_AllHandle.TryGetValue(hashCode, out var value))
            {
                // Debug.Log($"ReleaseAction：hashCode={hashCode}, location={value.GetAssetInfo()?.AssetPath}");
                value.Release();
                m_AllHandle.Remove(hashCode);
            }
            else
            {
                Debug.LogError($"释放了一个未知Code");
            }
        }
        
        
        //释放所有
        private void ReleaseAllAction()
        {
            Debug.Log($"ReleaseAllAction");
            foreach (var handle in m_AllHandle.Values)
            {
                handle.Release();
            }

            m_AllHandle.Clear();
        }
        
        
        //检查合法
        private bool VerifyAssetValidityFunc(string packageName, string location)
        {
            return package.CheckLocationValid(location);
        }

    }
}