﻿using System;
using UnityEngine;

namespace Zeng.GameFrame.UIS
{
    public abstract class Singleton<T> : ISingleton where T : Singleton<T>, new()
    {
        private static T _i;

        /// <summary>
        /// 是否存在
        /// </summary>
        public static bool Exist => _i != null;

        /// <summary>
        /// 是否已释放
        /// </summary>
        private bool m_Disposed;

        public bool Disposed => m_Disposed;

        protected Singleton()
        {
            if (_i != null)
            {
                #if UNITY_EDITOR
                throw new Exception(this + "是单例类，不能实例化两次");
                #endif
            }
        }

        public static T I
        {
            get
            {
                if (_i == null)
                {
                    if (SingletonMgr.Disposing)
                    {
                        Debug.LogError($" {typeof (T).Name} 单例管理器已释放或未初始化 禁止使用");
                        return null;
                    }
                    
                    _i = new T();
                    _i.OnInitSingleton();
                    SingletonMgr.Add(_i);
                }

                _i.OnUseSingleton();
                return _i;
            }
        }

        //释放方法2: 静态释放
        public static bool DisposeInst()
        {
            if (_i == null)
            {
                return true;
            }

            return _i.Dispose();
        }

        //释放方法1: 对象释放
        public bool Dispose()
        {
            if (m_Disposed)
            {
                return false;
            }

            SingletonMgr.Remove(_i);
            _i     = default;
            m_Disposed = true;
            OnDispose();
            return true;
        }

        /// <summary>
        /// 处理释放相关事情
        /// </summary>
        protected virtual void OnDispose()
        {
        }

        //初始化回调
        protected virtual void OnInitSingleton()
        {
        }

        //每次使用前回调
        protected virtual void OnUseSingleton()
        {
        }
    }
}