﻿using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace Zeng.GameFrame.UIS
{
    /// <summary>
    /// 异步对象缓存池
    /// </summary>
    public class ObjAsyncCache<T>
    {
        private   Stack<T>         m_pool;
        protected Func<UniTask<T>> m_createCallback;

        public ObjAsyncCache(Func<UniTask<T>> createCallback, int capacity = 0)
        {
            m_pool = capacity > 0
                ? new Stack<T>(capacity)
                : new Stack<T>();

            m_createCallback = createCallback;
        }

        public async UniTask<T> Get()
        {
            return m_pool.Count > 0 ? m_pool.Pop() : await m_createCallback();
        }

        public void Put(T value)
        {
            m_pool.Push(value);
        }

        public void Clear(bool disposeItem = false)
        {
            if (disposeItem)
            {
                foreach (var item in m_pool)
                {
                    if (item is IDisposer disposer)
                    {
                        disposer.Dispose();
                    }
                    else if (item is IDisposable disposer2)
                    {
                        disposer2.Dispose();
                    }
                }
            }

            m_pool.Clear();
        }

        public void Clear(Action<T> disposeAction)
        {
            while (m_pool.Count >= 1)
            {
                disposeAction?.Invoke(m_pool.Pop());
            }

            m_pool.Clear();
        }
    }
}