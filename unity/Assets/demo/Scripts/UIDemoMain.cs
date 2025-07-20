using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using YooAsset;
using Zeng.GameFrame.UIS;

namespace Zeng.Demos
{
    public class UIDemoMain : MonoBehaviour
    {
        public EPlayMode YooAssetPlayMode = EPlayMode.EditorSimulateMode;
        public string YooAssetDefaultHostServer = "http://127.0.0.1/CDN/Android/v1.0";
        public string YooAsetFallbackHostServer = "http://127.0.0.1/CDN/Android/v1.0";
        
        private void Start()
        {
            InitAsync().Forget();
        }

        private async UniTask InitAsync()
        {
            (bool success, ResourcePackage package) = await YooAssetInit.I.InitAsync(YooAssetPlayMode, YooAssetDefaultHostServer, YooAsetFallbackHostServer);
            if (success)
            {
                UILoadProxyYooAsset.I.Init(package);
                
                await UIManager.I.InitAsync();
            }
            
            

           
        }
    }
}