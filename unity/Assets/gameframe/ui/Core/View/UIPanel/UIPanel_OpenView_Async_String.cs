﻿using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Zeng.GameFrame.UIS
{
    /// <summary>
    /// 部类 界面拆分数据
    /// </summary>
    public abstract partial class UIPanel
    {
        private async UniTask<UIView> GetView(string viewName)
        {
            var parent = GetViewParent(viewName);
            if (parent == null)
            {
                Debug.LogError($"不存在这个View  请检查 {viewName}");
                return null;
            }

            using var asyncLock = await AsyncLockMgr.I.Wait(viewName.GetHashCode());

            if (m_ExistView.ContainsKey(viewName))
            {
                return m_ExistView[viewName];
            }

            var value = UIBindHelper.GetBindVoByPath(UIPkgName, viewName);
            if (value == null) return null;
            var bindVo = value.Value;
            var view = (UIView)await UIFactory.InstantiateAsync(bindVo, parent);
            m_ExistView.Add(viewName, view);

            return view;
        }

        #region 打开泛型

        public async UniTask<UIView> OpenViewAsync(string viewName, object param = null)
        {
            var view = await GetView(viewName);
            if (view == null) return null;

            var success = false;

            await OpenViewBefore(view);

            try
            {
                var p = ParamVo.Get(param);
                success = await view.Open(p);
                ParamVo.Put(p);
            }
            catch (Exception e)
            {
                Debug.LogError($"ResName={view.UIResName}, err={e.Message}{e.StackTrace}");
            }

            await OpenViewAfter(view, success);

            return view;
        }

        public async UniTask<UIView> OpenViewAsync(string viewName, object param1, object param2)
        {
            var paramList = ListPool<object>.Get();
            paramList.Add(param1);
            paramList.Add(param2);
            var view = await OpenViewAsync(viewName, paramList);
            ListPool<object>.Put(paramList);
            return view;
        }

        public async UniTask<UIView> OpenViewAsync(string viewName, object param1, object param2, object param3)
        {
            var paramList = ListPool<object>.Get();
            paramList.Add(param1);
            paramList.Add(param2);
            paramList.Add(param3);
            var view = await OpenViewAsync(viewName, paramList);
            ListPool<object>.Put(paramList);
            return view;
        }

        public async UniTask<UIView> OpenViewAsync(string viewName, object param1, object param2, object param3,
                                                        object param4)
        {
            var paramList = ListPool<object>.Get();
            paramList.Add(param1);
            paramList.Add(param2);
            paramList.Add(param3);
            paramList.Add(param4);
            var view = await OpenViewAsync(viewName, paramList);
            ListPool<object>.Put(paramList);
            return view;
        }

        public async UniTask<UIView> OpenViewAsync(string viewName, object param1, object param2, object param3,
                                                        object param4,   params object[] paramMore)
        {
            var paramList = ListPool<object>.Get();
            paramList.Add(param1);
            paramList.Add(param2);
            paramList.Add(param3);
            paramList.Add(param4);
            if (paramMore.Length > 0)
            {
                paramList.AddRange(paramMore);
            }

            var view = await OpenViewAsync(viewName, paramList);
            ListPool<object>.Put(paramList);
            return view;
        }

        #endregion
    }
}