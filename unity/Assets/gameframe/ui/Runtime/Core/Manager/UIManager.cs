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


        public async void InitAsync()
        {
            
        }
        
        
        
        
    }
}