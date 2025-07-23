using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Zeng.GameFrame.UIS
{
    public partial class UIBase
    {
        
        private UIPanel _Panel;
        public UIPanel Panel
        {
            get
            {
                if (_Panel == null)
                {
                    _Panel = GetPanel(OwnerRectTransform);
                }
                return _Panel;
            }
        }

        private UIPanel GetPanel(RectTransform node)
        {
            UIPanel panel =node.GetComponent<UIPanel>();
            if (panel == null)
            {
                panel = node.GetComponentInParent<UIPanel>();
            }
            return panel;
        }

     

    }
}