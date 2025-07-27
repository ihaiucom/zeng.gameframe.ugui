using System;
using UnityEngine;

namespace Zeng.GameFrame.UIS
{
    /// <summary>
    /// 组件的单例类基类
    /// 注意：如果这个单例实现不存在于场景,那么会自动在场景上创建一个GO，并把类挂在他下面
    /// 如果不希望自动创建，请使用MonoSceneSingleton。
    /// 它默认会设置DontDestroyOnLoad， 如果有其它需求，请覆写CanDestroyOnLoad。
    /// 它默认会给go一个合适的名字，如果有其它需求，请覆写CreateName。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class MonoSingleton<T> : DisposerMonoSingleton where T : MonoSingleton<T>
    {
        private static T _i;

        /// <summary>
        /// 是否存在
        /// </summary>
        public static bool Exist => _i != null;

        /// <summary>
        /// 得到单例
        /// </summary>
        public static T I
        {
            get
            {
                if (_i == null)
                {
                    if (!UIOperationHelper.IsPlaying())
                    {
                        Debug.LogError($"非运行时 禁止调用");
                        return null;
                    }

                    if (SingletonMgr.Disposing)
                    {
                        Debug.LogError($" {typeof (T).Name} 单例管理器已释放或未初始化 禁止使用");
                        return null;
                    }

                    GameObject go = new GameObject();
                    _i  = go.AddComponent<T>();
                    go.name = _i.GetCreateName();
                    if (_i.GetDontDestroyOnLoad())
                    {
                        DontDestroyOnLoad(go);
                    }
                    
                    if (_i.GetHideAndDontSave())
                    {
                        go.hideFlags = HideFlags.HideAndDontSave;
                    }
                    
                    SingletonMgr.Add(_i);
                    _i.OnInitSingleton();
                }

                _i.OnUseSingleton();
                return _i;
            }
        }

        private string GetCreateName()
        {
            return $"[Singleton]{typeof(T).Name}";
        }

        protected override bool GetHideAndDontSave()
        {
            return true;
        }

        //子类如果使用这个生命周期记得调用base
        //推荐使用 重写 OnDispose
        protected override void OnDestroy()
        {
            base.OnDestroy();
            SingletonMgr.Remove(_i);
            _i = null;
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
    }
}