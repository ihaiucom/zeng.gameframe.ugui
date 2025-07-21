using System;
using UnityEngine;
using Cysharp.Threading.Tasks;

namespace Zeng.GameFrame.UIS
{
    public static partial class UIFactory
    {
        public static async UniTask<T> InstantiateAsync<T>(RectTransform parent = null) where T : UIBase
        {
            var data = UIBindHelper.GetBindVoByType<T>();
            if (data == null) return null;
            var vo = data.Value;

            return await InstantiateAsync<T>(vo, parent);
        }

        public static async UniTask<T> InstantiateAsync<T>(UIBindVo vo, RectTransform parent = null) where T : UIBase
        {
            var uiBase = await CreateAsync(vo);
            SetParent(uiBase.OwnerRectTransform, parent ? parent : UIManager.I.uiRoot.UICache);
            return (T)uiBase;
        }

        public static async UniTask<UIBase> InstantiateAsync(UIBindVo vo, RectTransform parent = null)
        {
            var uiBase = await CreateAsync(vo);
            SetParent(uiBase.OwnerRectTransform, parent ? parent : UIManager.I.uiRoot.UICache);
            return uiBase;
        }

        public static async UniTask<UIBase> InstantiateAsync(Type uiType, RectTransform parent = null)
        {
            var data = UIBindHelper.GetBindVoByType(uiType);
            if (data == null) return null;
            var vo = data.Value;

            return await InstantiateAsync(vo, parent);
        }

        public static async UniTask<UIBase> InstantiateAsync(string        pkgName, string resName,
                                                             RectTransform parent = null)
        {
            var data = UIBindHelper.GetBindVoByPath(pkgName, resName);
            if (data == null) return null;
            var vo = data.Value;

            return await InstantiateAsync(vo, parent);
        }

        internal static async UniTask<UIBase> CreatePanelAsync(UIWindowInfo uiWindowInfo)
        {
            var bingVo = UIBindHelper.GetBindVoByPath(uiWindowInfo.PkgName, uiWindowInfo.ResName);
            if (bingVo == null) return null;
            var uiBase = await CreateAsync(bingVo.Value);
            return uiBase;
        }

        private static async UniTask<UIBase> CreateAsync(UIBindVo vo)
        {
            var obj = await UILoad.LoadAssetAsyncInstantiate(vo.PkgName, vo.ResName);
            if (obj == null)
            {
                Debug.LogError($"没有加载到这个资源 {vo.PkgName}/{vo.ResName}");
                return null;
            }

            return CreateByObjVo(vo, obj);
        }
    }
}