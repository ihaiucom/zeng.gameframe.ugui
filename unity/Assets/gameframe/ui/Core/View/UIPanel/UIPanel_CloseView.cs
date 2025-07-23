using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Zeng.GameFrame.UIS
{
    public abstract partial class UIPanel
    {
        public void CloseView<T>(bool tween = true)
            where T : UIView
        {
            CloseViewAsync<T>(tween).Forget();
        }

        public void CloseView(string resName, bool tween = true)
        {
            CloseViewAsync(resName, tween).Forget();
        }

        public async UniTask<bool> CloseViewAsync<TView>(bool tween = true)
            where TView : UIView
        {
            var (exist, entity) = ExistView<TView>();
            if (!exist) return false;
            return await CloseViewAsync(entity, tween);
        }

        public async UniTask<bool> CloseViewAsync(string resName, bool tween = true)
        {
            var (exist, entity) = ExistView(resName);
            if (!exist) return false;
            return await CloseViewAsync(entity, tween);
        }

        private async UniTask<bool> CloseViewAsync(UIView view, bool tween)
        {
            if (view == null) return false;
            await view.CloseAsync(tween);
            return true;
        }
    }
}
