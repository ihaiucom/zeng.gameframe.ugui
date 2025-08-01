﻿using System;
using UnityEngine;
using Cysharp.Threading.Tasks;

namespace Zeng.GameFrame.UIS
{
    /// <summary>
    /// 异步打开
    /// </summary>
    public partial class UIManager
    {
        private async UniTask<UIWindowInfo> OpenPanelStartAsync(string panelName)
        {
            if (string.IsNullOrEmpty(panelName))
            {
                Debug.LogError($"<color=red> 无法打开 这是一个空名称 </color>");
                return null;
            }

            using var asyncLock = await AsyncLockMgr.I.Wait(panelName.GetHashCode());

            #if UIMACRO_PANEL_OPENCLOSE
            Debug.Log($"<color=yellow> 打开UI: {panelName} </color>");
            #endif
            
            if (!m_PanelCfgMap.TryGetValue(panelName, out var info))
            {
                Debug.LogError($"请检查 {panelName} 没有获取到PanelInfo  1. 必须继承IPanel 的才可行  2. 检查是否没有注册上");
                return null;
            }

            if (info.Panel == null)
            {
                var uiBase = await UIFactory.CreatePanelAsync(info);
                if (uiBase == null)
                {
                    Debug.LogError($"面板[{panelName}]没有创建成功，packName={info.PkgName}, resName={info.ResName}");
                    return null;
                }
                uiBase.SetActive(false);
                info.Reset(uiBase);
            }

            AddUI(info);

            return info;
        }

        public async UniTask<T> OpenPanelAsync<T>()
            where T : UIPanel, new()
        {
            var info = await OpenPanelStartAsync(GetPanelName<T>());
            if (info == null) return default;

            var success = false;

            await OpenPanelBefore(info);

            try
            {
                success = await info.Panel.Open();
            }
            catch (Exception e)
            {
                Debug.LogError($"panel={info.ResName}, err={e.Message}{e.StackTrace}");
            }
            
            UIPanel panel = await OpenPanelAfter(info, success);
            T result = (T)panel;

            return result;
        }

        public async UniTask<T> OpenPanelAsync<T, P1>(P1 p1)
            where T : UIPanel, IUIOpen<P1>, new()
        {
            var info = await OpenPanelStartAsync(GetPanelName<T>());
            if (info == null) return default;

            var success = false;

            await OpenPanelBefore(info);

            try
            {
                success = await info.Panel.Open(p1);
            }
            catch (Exception e)
            {
                Debug.LogError($"panel={info.ResName}, err={e.Message}{e.StackTrace}");
            }

            return (T)await OpenPanelAfter(info, success);
        }

        public async UniTask<T> OpenPanelAsync<T, P1, P2>(P1 p1, P2 p2)
            where T : UIPanel, IUIOpen<P1, P2>, new()
        {
            var info = await OpenPanelStartAsync(GetPanelName<T>());
            if (info == null) return default;

            var success = false;

            await OpenPanelBefore(info);

            try
            {
                success = await info.Panel.Open(p1, p2);
            }
            catch (Exception e)
            {
                Debug.LogError($"panel={info.ResName}, err={e.Message}{e.StackTrace}");
            }

            return (T)await OpenPanelAfter(info, success);
        }

        public async UniTask<T> OpenPanelAsync<T, P1, P2, P3>(P1 p1, P2 p2, P3 p3)
            where T : UIPanel, IUIOpen<P1, P2, P3>, new()
        {
            var info = await OpenPanelStartAsync(GetPanelName<T>());
            if (info == null) return default;

            var success = false;

            await OpenPanelBefore(info);

            try
            {
                success = await info.Panel.Open(p1, p2, p3);
            }
            catch (Exception e)
            {
                Debug.LogError($"panel={info.ResName}, err={e.Message}{e.StackTrace}");
            }

            return (T)await OpenPanelAfter(info, success);
        }

        public async UniTask<T> OpenPanelAsync<T, P1, P2, P3, P4>(P1 p1, P2 p2, P3 p3, P4 p4)
            where T : UIPanel, IUIOpen<P1, P2, P3, P4>, new()
        {
            var info = await OpenPanelStartAsync(GetPanelName<T>());
            if (info == null) return default;

            var success = false;

            await OpenPanelBefore(info);

            try
            {
                success = await info.Panel.Open(p1, p2, p3, p4);
            }
            catch (Exception e)
            {
                Debug.LogError($"panel={info.ResName}, err={e.Message}{e.StackTrace}");
            }

            return (T)await OpenPanelAfter(info, success);
        }

        public async UniTask<T> OpenPanelAsync<T, P1, P2, P3, P4, P5>(P1 p1, P2 p2, P3 p3, P4 p4, P5 p5)
            where T : UIPanel, IUIOpen<P1, P2, P3, P4, P5>, new()
        {
            var info = await OpenPanelStartAsync(GetPanelName<T>());
            if (info == null) return default;

            var success = false;

            await OpenPanelBefore(info);

            try
            {
                success = await info.Panel.Open(p1, p2, p3, p4, p5);
            }
            catch (Exception e)
            {
                Debug.LogError($"panel={info.ResName}, err={e.Message}{e.StackTrace}");
            }

            return (T)await OpenPanelAfter(info, success);
        }
    }
}