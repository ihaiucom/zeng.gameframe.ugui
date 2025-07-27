using Cysharp.Threading.Tasks;

namespace Zeng.GameFrame.UIS
{
    /// <summary>
    /// UI管理器
    /// </summary>
    public partial class UIManager : MgrSingleton<UIManager>
    {


        public UIRoot uiRoot { get; private set; }
        public UIBlockLayer uiBlockLayer { get; private set; }

        protected override async UniTask<bool> MgrAsyncInit()
        {
            return await InitAsync();
        }

        public async UniTask<bool> InitAsync()
        {
            UIBindHelper.InitAllBind();
            uiRoot = new UIRoot();
            uiBlockLayer = new UIBlockLayer();
            if (!await uiRoot.InitAsync()) return false;
            uiBlockLayer.Init(uiRoot.UILayerRoot);  //所有层级初始化后添加一个终极屏蔽层 可根据API 定时屏蔽UI操作
            UISafeArea.Init(uiRoot.UILayerRoot);

            return true;
        }
        
        
        protected override void OnDispose()
        {
            uiRoot?.ResetRoot();
            uiBlockLayer?.OnBlockDispose();
            uiRoot = null;
            uiBlockLayer = null;
        }
        
        
    }
}