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
    public class UIRoot
    {
        public       GameObject    uiRoot;
        public       GameObject    UICanvasRoot;
        public       RectTransform UILayerRoot;
        public       Camera        UICamera;
        public       Canvas        UICanvas;
        public const int           DesignScreenWidth    = 1920;
        public const int           DesignScreenHeight   = 1080;
        public const float         DesignScreenWidth_F  = 1920f;
        public const float         DesignScreenHeight_F = 1080f;

        private const int RootPosOffset = 1000;
        private const int LayerDistance = 1000;

        // 以下名称 禁止修改
        public const string UIRootName      = "UIRoot";
        public const string UILayerRootName = "UILayerRoot";
        public const string UIRootPkgName   = "Common";


        //K1 = 层级枚举 V1 = 层级对应的rect
        //List = 当前层级中的当前所有UI 前面的代表这个UI在前面以此类推
        private Dictionary<EPanelLayer, Dictionary<RectTransform, List<UIWindowInfo>>> m_AllPanelLayer = new ();

        internal async UniTask<bool> InitAsync()
        {
            // 查找各种组件
            if (! await InitRoot())
            {
                return false;
            }
            //分层
            InitLayer();
            
            return true;
        }

        // 查找各种组件
        private async UniTask<bool> InitRoot()
        {
            
            uiRoot = GameObject.Find(UIRootName);
            if (uiRoot == null)
            {
                uiRoot = await UILoad.LoadAssetAsyncInstantiate(UIRootPkgName, UIRootName);
            }

            if (uiRoot == null)
            {
                Debug.LogError($"初始化错误 没有找到UIRoot");
                return false;
            }

            // Node Setting
            uiRoot.name = uiRoot.name.Replace("(Clone)", "");
            Object.DontDestroyOnLoad(uiRoot);
            uiRoot.transform.position = new Vector3(RootPosOffset, RootPosOffset, 0); //root可修改位置防止与世界3D场景重叠导致不好编辑

            UICanvas = uiRoot.GetComponentInChildren<Canvas>();
            if (UICanvas == null)
            {
                Debug.LogError($"初始化错误 没有找到Canvas");
                return false;
            }

            UICanvasRoot = UICanvas.gameObject;

            UILayerRoot = UICanvasRoot.transform.FindChildByName(UILayerRootName)?.GetComponent<RectTransform>();
            if (UILayerRoot == null)
            {
                Debug.LogError($"初始化错误 没有找到UILayerRoot");
                return false;
            }

            UICamera = UICanvasRoot.GetComponentInChildren<Camera>();
            if (UICamera == null)
            {
                Debug.LogError($"初始化错误 没有找到UICamera");
                return false;
            }

            // Canvas Setting
            var canvas = UICanvasRoot.GetComponent<Canvas>();
            if (canvas == null)
            {
                Debug.LogError($"初始化错误 没有找到UICanvasRoot - Canvas");
                return false;
            }

            canvas.renderMode  = RenderMode.ScreenSpaceCamera;
            canvas.worldCamera = UICamera;

            var canvasScaler = UICanvasRoot.GetComponent<CanvasScaler>();
            if (canvasScaler == null)
            {
                Debug.LogError($"初始化错误 没有找到UICanvasRoot - CanvasScaler");
                return false;
            }

            canvasScaler.uiScaleMode         = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            canvasScaler.referenceResolution = new Vector2(DesignScreenWidth, DesignScreenHeight);
            
            
            // UICamera Setting
            UICamera.transform.localPosition =
                new Vector3(UILayerRoot.localPosition.x, UILayerRoot.localPosition.y, -LayerDistance);

            UICamera.clearFlags   = CameraClearFlags.Depth;
            UICamera.orthographic = true;
            
            //根据需求可以修改摄像机的远剪裁平面大小 没必要设置的很大
            //UICamera.farClipPlane = ((len + 1) * LayerDistance) * UICanvasRoot.transform.localScale.x; 

            return true;

        }

        //分层
        private void InitLayer()
        {
            const int len = (int)EPanelLayer.Count;
            m_AllPanelLayer.Clear();
            for (var i = len - 1; i >= 0; i--)
            {
                var layer = new GameObject($"Layer{i}-{(EPanelLayer)i}");
                var rect  = layer.AddComponent<RectTransform>();
                rect.SetParent(UILayerRoot);
                rect.localScale    = Vector3.one;
                rect.pivot         = new Vector2(0.5f, 0.5f);
                rect.anchorMax     = Vector2.one;
                rect.anchorMin     = Vector2.zero;
                rect.sizeDelta     = Vector2.zero;
                rect.localRotation = Quaternion.identity;
                rect.localPosition = new Vector3(0, 0, i * LayerDistance); //这个是为了3D模型时穿插的问题
                var rectDic = new Dictionary<RectTransform, List<UIWindowInfo>> { { rect, new List<UIWindowInfo>() } };
                m_AllPanelLayer.Add((EPanelLayer)i, rectDic);
            }

        }
        internal void ResetRoot()
        {
            for (int i = UILayerRoot.transform.childCount - 1; i >= 0; i--)
            {
                UnityEngine.Object.Destroy(UILayerRoot.transform.GetChild(i).gameObject);
            }
        }

        #region 快捷获取层级

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

        internal RectTransform GetLayerRect(EPanelLayer panelLayer)
        {
            m_AllPanelLayer.TryGetValue(panelLayer, out var rectDic);
            if (rectDic == null)
            {
                Debug.LogError($"没有这个层级 请检查 {panelLayer}");
                return null;
            }

            //只能有一个所以返回第一个
            foreach (var rect in rectDic.Keys)
            {
                return rect;
            }

            return null;
        }

        internal List<UIWindowInfo> GetLayerPanelInfoList(EPanelLayer panelLayer)
        {
            m_AllPanelLayer.TryGetValue(panelLayer, out var rectDic);
            if (rectDic == null)
            {
                Debug.LogError($"没有这个层级 请检查 {panelLayer}");
                return null;
            }

            //只能有一个所以返回第一个
            foreach (var infoList in rectDic.Values)
            {
                return infoList;
            }

            return null;
        }

        internal bool RemoveLayerPanelInfo(EPanelLayer panelLayer, UIWindowInfo panelInfo)
        {
            var list = GetLayerPanelInfoList(panelLayer);
            return list != null && list.Remove(panelInfo);
        }
    }
}