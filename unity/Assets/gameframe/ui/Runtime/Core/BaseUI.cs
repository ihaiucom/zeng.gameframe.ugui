using UnityEngine;

namespace Zeng.GameFrame.UIs
{
    public class BaseUI : MonoBehaviour
    {
        private BaseWindow window;

        public BaseWindow Window
        {
            get
            {
                if (window == null)
                {
                    window = FrindWindow(transform as RectTransform);
                }

                return window;  
            }           
        }

        private static BaseWindow FrindWindow(RectTransform rectTransform)
        {
            if (rectTransform == null)
            {
                return null;
            }
            BaseWindow window = rectTransform.GetComponent<BaseWindow>();
            if (window!= null)
            {
                return window;
            }
            return FrindWindow(rectTransform.parent as RectTransform);
        }
    }
}