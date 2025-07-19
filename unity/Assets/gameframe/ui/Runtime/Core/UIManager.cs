namespace Zeng.GameFrame.UIs
{
    public class UIManager
    {
        private static UIManager _I;

        public static UIManager I
        {
            get
            {
                if (_I == null)
                {
                    _I = new UIManager();
                }

                return _I;
            }
        }

        public void Init()
        {
            
        }

        public T Create<T>()
        {
            return default(T);
        }
        
        public T Open<T>()
        {
            return default(T);
        }

        public T Get<T>()
        {
            return default(T);
        }
        
        public void Close<T>()
        {
            
        }

    }
}