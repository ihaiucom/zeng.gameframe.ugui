using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace Zeng.GameFrame.UIS
{
    /// <summary>
    /// UIRoot
    /// </summary>
    public partial class UIManager
    {
        #region 快捷获取层级

        private RectTransform UILayerRoot
        {
            get
            {
                return uiRoot.UILayerRoot;
            }
        }


        private RectTransform m_UICache;

        public RectTransform UICache
        {
            get
            {
                if (m_UICache == null)
                {
                    m_UICache = GetLayerRect(EPanelLayer.Cache);
                }

                return m_UICache;
            }
        }

        private RectTransform m_UIPanel;

        public RectTransform UIPanel
        {
            get
            {
                if (m_UIPanel == null)
                {
                    m_UIPanel = GetLayerRect(EPanelLayer.Panel);
                }

                return m_UIPanel;
            }
        }

        #endregion

        public RectTransform GetLayerRect(EPanelLayer panelLayer)
        {
            return uiRoot.GetLayerRect(panelLayer);
        }

        private List<UIWindowInfo> GetLayerPanelInfoList(EPanelLayer panelLayer)
        {
            return uiRoot.GetLayerPanelInfoList(panelLayer);
        }

        private bool RemoveLayerPanelInfo(EPanelLayer panelLayer, UIWindowInfo panelInfo)
        {
            return uiRoot.RemoveLayerPanelInfo(panelLayer, panelInfo);
        }
    }
}