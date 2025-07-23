using System;
using UnityEngine;

namespace Zeng.GameFrame.UIS
{
    /// <summary>
    /// 加入 移除
    /// </summary>
    public partial class UIManager
    {
        /// <summary>
        /// 加入UI到对应层级 设置顺序 大小
        /// </summary>
        private void AddUI(UIWindowInfo uiWindowInfo)
        {
            var uiBasePanel = uiWindowInfo.Panel;
            var panelLayer  = uiBasePanel.Layer;
            var priority    = uiBasePanel.Priority;
            var uiRect      = uiBasePanel.OwnerRectTransform;

            var layerRect = GetLayerRect(panelLayer);
            if (layerRect == null)
            {
                panelLayer = EPanelLayer.Bottom;
                layerRect  = GetLayerRect(panelLayer);
                Debug.LogError($"没有找到这个UILayer {panelLayer}  强制修改为使用最低层 请检查");
            }

            var addLast = true; //放到最后 也就是最前面

            var infoList     = GetLayerPanelInfoList(panelLayer);
            var removeResult = infoList.Remove(uiWindowInfo);
            if (removeResult)
                uiRect.SetParent(uiRoot.UILayerRoot);
            
            /*
             * 使用Unity的层级作为前后显示
             * 大的在前 小的在后
             * 所以根据优先级 从小到大排序
             * 当前优先级 >= 目标优先级时 插入
             */

            for (var i = infoList.Count - 1; i >= 0; i--)
            {
                var info         = infoList[i];
                var infoPriority = info.Panel?.Priority ?? 0;

                if (i == infoList.Count - 1 && priority >= infoPriority) break;

                if (priority >= infoPriority)
                {
                    infoList.Insert(i + 1, uiWindowInfo);
                    uiRect.SetParent(layerRect);
                    uiRect.SetSiblingIndex(i + 1);
                    addLast = false;
                    break;
                }

                if (i <= 0)
                {
                    infoList.Insert(0, uiWindowInfo);
                    uiRect.SetParent(layerRect);
                    uiRect.SetSiblingIndex(0);
                    addLast = false;
                    break;
                }
            }

            if (addLast)
            {
                infoList.Add(uiWindowInfo);
                uiRect.SetParent(layerRect);
                uiRect.SetAsLastSibling();
            }

            uiRect.ResetToFullScreen();
            uiRect.ResetLocalPosAndRot();
            uiWindowInfo.Panel.StopCountDownDestroyPanel();
        }

        /// <summary>
        /// 移除一个UI
        /// </summary>
        private void RemoveUI(UIWindowInfo uiWindowInfo)
        {
            if (uiWindowInfo.Panel == null)
            {
                Debug.LogError($"无法移除一个null panelInfo 数据 {uiWindowInfo.ResName}");
                return;
            }

            var uiBasePanel  = uiWindowInfo.Panel;
            var foreverCache = uiBasePanel.PanelForeverCache;
            var timeCache    = uiBasePanel.PanelTimeCache;
            var panelLayer   = uiBasePanel.Layer;
            RemoveLayerPanelInfo(panelLayer, uiWindowInfo);

            if (foreverCache || timeCache)
            {
                //缓存界面只是单纯的吧界面隐藏
                //再次被打开 如何重构界面需要自行设置
                var layerRect = GetLayerRect(EPanelLayer.Cache);
                var uiRect    = uiBasePanel.OwnerRectTransform;
                uiRect.SetParent(layerRect, false);
                uiBasePanel.SetActive(false);

                if (timeCache && !foreverCache)
                {
                    //根据配置时间X秒后自动摧毁
                    //如果X秒内又被打开则可复用
                    uiBasePanel.CacheTimeCountDownDestroyPanel();
                }
            }
            else
            {
                var uiObj = uiBasePanel.OwnerGameObject;
                UnityEngine.Object.Destroy(uiObj);
                uiWindowInfo.Reset(null);
            }
        }

        internal void RemoveUIReset(string panelName)
        {
            var panelInfo = GetPanelInfo(panelName);
            panelInfo?.Reset(null);
        }
    }
}