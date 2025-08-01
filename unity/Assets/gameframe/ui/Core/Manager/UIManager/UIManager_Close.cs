﻿using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Zeng.GameFrame.UIS
{
    public partial class UIManager
    {
        /// <summary>
        /// 得到最顶层的面板
        /// 默认将返回所有面板的第一个
        /// 可能没有
        /// </summary>
        public UIWindowInfo GetTopPanel(EPanelLayer  layer        = EPanelLayer.Any,
                                     EPanelOption ignoreOption = EPanelOption.Container)
        {
            var layerCount = (int)EPanelLayer.Count;

            for (var i = 0; i < layerCount; i++)
            {
                var currentLayer = (EPanelLayer)i;

                //如果是任意层级则 从上到下找
                //否则只会在目标层级上找
                if (layer != EPanelLayer.Any && currentLayer != layer)
                {
                    continue;
                }

                var list = GetLayerPanelInfoList(currentLayer);

                foreach (var info in list)
                {
                    //禁止关闭的界面无法获取到
                    if (info.Panel.PanelDisClose)
                    {
                        continue;
                    }

                    //有忽略操作 且满足调节 则这个界面无法获取到
                    if (ignoreOption != EPanelOption.None &&
                        (info.Panel.PanelOption & ignoreOption) != 0)
                    {
                        continue;
                    }

                    if (layer == EPanelLayer.Any || info.Panel.Layer == layer)
                    {
                        return info;
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// 关闭这个层级上的最前面的一个UI 异步
        /// </summary>
        /// <param name="layer">层级</param>
        /// <param name="ignoreOption">忽略操作</param>
        public async UniTask<bool> CloseLayerTopPanelAsync(EPanelLayer  layer,
                                                           EPanelOption ignoreOption = EPanelOption.Container)
        {
            var topPanel = GetTopPanel(layer, ignoreOption);
            if (topPanel == null)
            {
                return false;
            }

            await ClosePanelAsync(topPanel.Name);

            return true;
        }

        /// <summary>
        /// 关闭指定层级上的 最上层UI 同步
        /// </summary>
        /// <param name="layer">层级</param>
        /// <param name="ignoreOption">忽略操作</param>
        public void CloseLayerTopPanel(EPanelLayer layer, EPanelOption ignoreOption = EPanelOption.Container)
        {
            CloseLayerTopPanelAsync(layer, ignoreOption).Forget();
        }

        /// <summary>
        /// 关闭Panel层级上的最上层UI 异步
        /// </summary>
        public async UniTask<bool> CloseTopPanelAsync()
        {
            return await CloseLayerTopPanelAsync(EPanelLayer.Panel);
        }

        /// <summary>
        /// 关闭Panel层级上的最上层UI 同步
        /// </summary>
        public void CloseTopPanel()
        {
            CloseTopPanelAsync().Forget();
        }

        /// <summary>
        /// 关闭一个窗口
        /// </summary>
        /// <param name="panelName">名称</param>
        /// <param name="tween">是否调用关闭动画</param>
        /// <param name="ignoreElse">忽略堆栈操作 -- 不要轻易忽略除非你明白 </param>
        public async UniTask ClosePanelAsync(string panelName, bool tween = true, bool ignoreElse = false)
        {
            #if UIMACRO_PANEL_OPENCLOSE
            Debug.Log($"<color=yellow> 关闭UI: {panelName} </color>");
            #endif

            using var asyncLock = await AsyncLockMgr.I.Wait(panelName.GetHashCode());

            m_PanelCfgMap.TryGetValue(panelName, out var info);

            if (info?.Panel == null) return;

            if (info.Panel.PanelOption.HasFlag(EPanelOption.DisClose))
            {
                bool allowClose = false; //是否允许关闭

                //如果继承禁止关闭接口 可返回是否允许关闭自行处理
                if (info.Panel is IUIBanClose disClose)
                {
                    allowClose = disClose.DoBanClose();
                }

                if (!allowClose)
                {
                    Debug.LogError($"{panelName} 这个界面禁止被关闭 请检查");
                    return;
                }
            }

            if (info.Panel is { WindowLastClose: false })
            {
                await info.Panel.InternalOnWindowCloseTween(tween);
                info.Panel?.OnWindowClose();
            }

            if (!ignoreElse)
                await RemoveUIAddElse(info);

            if (info.Panel is { WindowLastClose: true })
            {
                await info.Panel.InternalOnWindowCloseTween(tween);
                info.Panel?.OnWindowClose();
            }

            RemoveUI(info);
        }

        public void ClosePanel(string panelName, bool tween = true, bool ignoreElse = false)
        {
            ClosePanelAsync(panelName, tween, ignoreElse).Forget();
        }

        /// <summary>
        /// 关闭一个窗口
        /// 异步等待关闭动画
        /// </summary>
        public async UniTask ClosePanelAsync<T>(bool tween = true, bool ignoreElse = false) where T : UIPanel
        {
            await ClosePanelAsync(GetPanelName<T>(), tween, ignoreElse);
        }

        /// <summary>
        /// 同步关闭窗口
        /// 无法等待关闭动画
        /// </summary>
        public void ClosePanel<T>(bool tween = true, bool ignoreElse = false) where T : UIPanel
        {
            ClosePanelAsync(GetPanelName<T>(), tween, ignoreElse).Forget();
        }

        /// <summary>
        /// 回到指定的界面 其他界面全部关闭
        /// </summary>
        /// <param name="homeName">需要被打开的界面 且这个UI是存在的 否则无法打开</param>
        /// <param name="tween">动画</param>
        public async UniTask HomePanel(string homeName, bool tween = true)
        {
            #if UIMACRO_PANEL_OPENCLOSE
            Debug.Log($"<color=yellow> Home关闭其他所有Panel UI: {homeName} </color>");
            #endif

            m_PanelCfgMap.TryGetValue(homeName, out var homeInfo);
            // Debug.Log($"HomePanel : {homeName}, Panel:{homeInfo?.Panel}， ActiveSelf:{homeInfo?.ActiveSelf}");
            if (homeInfo?.Panel != null)
            {
                await RemoveUIToHome(homeInfo, tween);
                // Debug.Log($"HomePanel 关闭其他所有Panel UI后: {homeName}, ActiveSelf:{homeInfo.ActiveSelf}");
                if (!homeInfo.ActiveSelf)
                {
                    await OpenPanelAsync(homeName);
                }
            }
            else
            {
                await OpenPanelAsync(homeName);
            }
        }

        public async UniTask HomePanel<T>(bool tween = true)
        {
            await HomePanel(typeof(T).Name, tween);
        }
    }
}