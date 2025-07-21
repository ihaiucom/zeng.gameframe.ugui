using Cysharp.Threading.Tasks;

namespace Zeng.GameFrame.UIS
{
    /// <summary>
    /// UI管理器
    /// </summary>
    public class UIManager
    {
        private static UIManager _i;

        public static UIManager I
        {
            get
            {
                if (_i == null)
                {
                    _i = new UIManager();
                }
                return _i;
            }
        }

        public UIRoot uiRoot { get; private set; }
        public UIBlockLayer uiBlockLayer { get; private set; }


        public async UniTask InitAsync()
        {
            UIBindHelper.InitAllBind();
            uiRoot = new UIRoot();
            uiBlockLayer = new UIBlockLayer();
            await uiRoot.InitAsync();
            uiBlockLayer.Init(uiRoot.UILayerRoot);  //所有层级初始化后添加一个终极屏蔽层 可根据API 定时屏蔽UI操作
            UISafeArea.Init(uiRoot.UILayerRoot);

        }
        
        
        
        
    }
}