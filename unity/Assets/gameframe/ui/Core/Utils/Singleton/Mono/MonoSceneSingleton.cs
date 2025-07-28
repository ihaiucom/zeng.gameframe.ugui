using System;
using UnityEngine;

namespace Zeng.GameFrame.UIS
{
    /// <summary>
    /// 组件的单例类基类
    /// 注意：这个单例实现适用于已经存在于场景的物件，但你需要使用Inst来访问。
    /// 它在场景上找不到时不会自动创建，如果有自动创建的需求，请使用MonoSingleton。
    /// 它也会不自动执行DontDestroyOnLoad, 这需要你自己在组件里写。
    /// </summary>
    public abstract class MonoSceneSingleton<T> : DisposerMonoSingleton where T : MonoSceneSingleton<T>
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

                    Debug.LogError($" {typeof(T).Name} g_inst == null 这个是MonoSceneSingleton 需要自己创建对象的单例 不会自动创建");
                    return null;
                }

                _i.OnUseSingleton();
                return _i;
            }
        }

        protected virtual void Awake()
        {
            if (SingletonMgr.Disposing)
            {
                Debug.LogError($" {typeof(T).Name} 单例管理器已释放或未初始化 禁止使用");
                return;
            }

            if (_i != null)
            {
                Debug.LogError(typeof(T).Name + "是单例组件，不能在场景中存在多个");
                gameObject.SafeDestroySelf();
                return;
            }

            _i          = (T)this;
            gameObject.name = _i.GetCreateName();
            if (_i.GetDontDestroyOnLoad())
            {
                DontDestroyOnLoad(_i);
            }

            if (_i.GetHideAndDontSave())
            {
                gameObject.hideFlags = HideFlags.HideAndDontSave;
            }

            SingletonMgr.Add(_i);
            _i.OnInitSingleton();
        }

        private string GetCreateName()
        {
            return $"[Singleton]{typeof(T).Name}";
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