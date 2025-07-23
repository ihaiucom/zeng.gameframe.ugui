using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using YooAsset;

namespace Zeng.Demos
{
    public class YooAssetInit
    {
        private static YooAssetInit _i;
        public static YooAssetInit I
        {
            get
            {
                if (_i == null)
                {
                    _i = new YooAssetInit();
                }
                return _i;
            }
        }
        
        public string defaultHostServer = "http://127.0.0.1/CDN/Android/v1.0";
        public string fallbackHostServer = "http://127.0.0.1/CDN/Android/v1.0";
        public ResourcePackage package { get; private set; }
        public EPlayMode playMode { get; private set; }
        public bool isInitSuccess { get; private set; }

        public IEnumerator InitAsync(EPlayMode playMode, string hostServer , string fallbackHostServer)
        {
            this.defaultHostServer = hostServer;
            this.fallbackHostServer = fallbackHostServer;
            this.playMode = playMode;
            
            // 初始化资源系统
            YooAssets.Initialize();
            
            // 创建默认的资源包
            package = YooAssets.CreatePackage("DefaultPackage");
            
            // 设置该资源包为默认的资源包，可以使用YooAssets相关加载接口加载该资源包内容。
            YooAssets.SetDefaultPackage(package);
            
            
            //YooAsset中需要初始化 且分编辑器下和运行时
            yield return InitPackage(playMode, package);
            
            
    
            // 2. 请求资源清单的版本信息
            var operationVer =package.RequestPackageVersionAsync();
            yield return operationVer;
            if (operationVer.Status == EOperationStatus.Succeed)
            {
                Debug.Log($"请求资源清单的版本信息成功！{operationVer.PackageVersion}");
            }
            else
            {
                Debug.LogError($"请求资源清单的版本信息失败：{operationVer.Error}");
                isInitSuccess = false;
                yield break;
            }
            
            // 更新资源清单
            var operationManifest = package.UpdatePackageManifestAsync(operationVer.PackageVersion);
            yield return operationManifest;

            if (operationManifest.Status == EOperationStatus.Succeed)
            {
                Debug.Log($"更新资源清单成功！{operationVer.PackageVersion}");
            }
            else
            {
                Debug.LogError($"更新资源清单失败：{operationVer.Error}");
                isInitSuccess = false;
                yield break;
            }
    
        }
        


        private IEnumerator InitPackage(EPlayMode playMode, ResourcePackage package)
        {
            switch (playMode)
            {
                case EPlayMode.EditorSimulateMode:
                    yield return InitPackage_EditorSimulateMode(package);
                    break;
                case EPlayMode.OfflinePlayMode:
                    yield return InitPackage_OfflinePlayMode(package);
                    break;
                case EPlayMode.HostPlayMode:
                    yield return InitPackage_HostPlayMode(package);
                    break;
                case EPlayMode.WebPlayMode:
                    yield return InitPackage_WebPlayMode(package);
                    break;
                default:
                    yield return InitPackage_EditorSimulateMode(package);
                    break;
            }
        }
        
        // 编辑器模拟模式 (EditorSimulateMode)
        private IEnumerator InitPackage_EditorSimulateMode(ResourcePackage package)
        {  
            var buildResult = EditorSimulateModeHelper.SimulateBuild("DefaultPackage");    
            var packageRoot = buildResult.PackageRootDirectory;
            var editorFileSystemParams = FileSystemParameters.CreateDefaultEditorFileSystemParameters(packageRoot);
            var initParameters = new EditorSimulateModeParameters();
            initParameters.EditorFileSystemParameters = editorFileSystemParams;
            var initOperation = package.InitializeAsync(initParameters);
            yield return initOperation;

            isInitSuccess = initOperation.Status == EOperationStatus.Succeed;
    
            if(initOperation.Status == EOperationStatus.Succeed)
                Debug.Log("资源包初始化成功！");
            else 
                Debug.LogError($"资源包初始化失败：{initOperation.Error}");
        }
        
        // 单机运行模式 (OfflinePlayMode)
        private IEnumerator InitPackage_OfflinePlayMode(ResourcePackage package)
        {
            var buildinFileSystemParams = FileSystemParameters.CreateDefaultBuildinFileSystemParameters();
            var initParameters = new OfflinePlayModeParameters();
            initParameters.BuildinFileSystemParameters = buildinFileSystemParams;
            var initOperation = package.InitializeAsync(initParameters);
            yield return initOperation;
    
            isInitSuccess = initOperation.Status == EOperationStatus.Succeed;
            if(initOperation.Status == EOperationStatus.Succeed)
                Debug.Log("资源包初始化成功！");
            else 
                Debug.LogError($"资源包初始化失败：{initOperation.Error}");
        }
        
        
        // 联机运行模式 (HostPlayMode)
        private IEnumerator InitPackage_HostPlayMode(ResourcePackage package)
        {
            string defaultHostServer = "http://127.0.0.1/CDN/Android/v1.0";
            string fallbackHostServer = "http://127.0.0.1/CDN/Android/v1.0";
            IRemoteServices remoteServices = new RemoteServices(defaultHostServer, fallbackHostServer);
            var cacheFileSystemParams = FileSystemParameters.CreateDefaultCacheFileSystemParameters(remoteServices);
            var buildinFileSystemParams = FileSystemParameters.CreateDefaultBuildinFileSystemParameters();   
    
            var initParameters = new HostPlayModeParameters();
            initParameters.BuildinFileSystemParameters = buildinFileSystemParams; 
            initParameters.CacheFileSystemParameters = cacheFileSystemParams;
            var initOperation = package.InitializeAsync(initParameters);
            yield return initOperation;
            isInitSuccess = initOperation.Status == EOperationStatus.Succeed;
    
            if(initOperation.Status == EOperationStatus.Succeed)
                Debug.Log("资源包初始化成功！");
            else 
                Debug.LogError($"资源包初始化失败：{initOperation.Error}");
        }
        
        /// <summary>
        /// 远端资源地址查询服务类
        /// </summary>
        private class RemoteServices : IRemoteServices
        {
            private readonly string _defaultHostServer;
            private readonly string _fallbackHostServer;

            public RemoteServices(string defaultHostServer, string fallbackHostServer)
            {
                _defaultHostServer = defaultHostServer;
                _fallbackHostServer = fallbackHostServer;
            }
            string IRemoteServices.GetRemoteMainURL(string fileName)
            {
                return $"{_defaultHostServer}/{fileName}";
            }
            string IRemoteServices.GetRemoteFallbackURL(string fileName)
            {
                return $"{_fallbackHostServer}/{fileName}";
            }
        }
        
        
        // Web运行模式 (WebPlayMode)
        private IEnumerator InitPackage_WebPlayMode(ResourcePackage package)
        {
            //说明：RemoteServices类定义请参考联机运行模式！
            IRemoteServices remoteServices = new RemoteServices(defaultHostServer, fallbackHostServer);
            var webServerFileSystemParams = FileSystemParameters.CreateDefaultWebServerFileSystemParameters();
            var webRemoteFileSystemParams = FileSystemParameters.CreateDefaultWebRemoteFileSystemParameters(remoteServices); //支持跨域下载
    
            var initParameters = new WebPlayModeParameters();
            initParameters.WebServerFileSystemParameters = webServerFileSystemParams;
            initParameters.WebRemoteFileSystemParameters = webRemoteFileSystemParams;
    
            var initOperation = package.InitializeAsync(initParameters);
            yield return initOperation;
            isInitSuccess = initOperation.Status == EOperationStatus.Succeed;
    
            if(initOperation.Status == EOperationStatus.Succeed)
                Debug.Log("资源包初始化成功！");
            else 
                Debug.LogError($"资源包初始化失败：{initOperation.Error}");
        }
        
        // 自定义运行模式 (CustomPlayMode)
        // private IEnumerator InitPackage_CustomPlayMode(ResourcePackage package)
        // {
        //     var initParameters = new CustomPlayModeParameters();
        //     initParameters.FileSystemParameterList.Add(FileSystemParamsA);
        //     initParameters.FileSystemParameterList.Add(FileSystemParamsB);
        //     initParameters.FileSystemParameterList.Add(FileSystemParamsC);
        //
        //     var initOperation = package.InitializeAsync(initParameters);
        //     yield return initOperation;
        //
        //     if(initOperation.Status == EOperationStatus.Succeed)
        //         Debug.Log("资源包初始化成功！");
        //     else 
        //         Debug.LogError($"资源包初始化失败：{initOperation.Error}");
        // }
        
    }
}