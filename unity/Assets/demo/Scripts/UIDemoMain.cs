
using System.Collections;
using Cysharp.Threading.Tasks;
using Games.UI.Home;
using Games.UI.Login;
using Games.UI.RoleSelect;
using I2.Loc;
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
            // StartCoroutine(InitYooAsset());
            InitAsync().Forget();
        }

        private IEnumerator InitYooAsset()
        {
            yield return YooAssetInit.I.InitAsync(YooAssetPlayMode, YooAssetDefaultHostServer, YooAsetFallbackHostServer);
            yield return new WaitForSeconds(1);
            InitAsync().Forget();
        }

        private async UniTask InitAsync()
        {
            await YooAssetInit.I.InitAsync(YooAssetPlayMode, YooAssetDefaultHostServer, YooAsetFallbackHostServer);
            
            if (YooAssetInit.I.isInitSuccess)
            {
                UILoadProxyYooAsset.I.Init(YooAssetInit.I.package);
                
                await UIManager.I.InitAsync();
                await I2LocalizeMgr.I.InitAsync();
                
                UIManager.I.OpenPanel<LoginPanel>();
                // UIManager.I.OpenPanel<HomePanel>();
                // UIManager.I.OpenPanel<RoleSelectPanel>();
            }
            
            

           
        }
    }
}