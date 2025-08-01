﻿using Cysharp.Threading.Tasks;

namespace Zeng.GameFrame.UIS
{
    public partial class UIView
    {
        public void Close(bool tween = true, bool ignoreElse = false)
        {
            CloseAsync(tween).Forget();
        }

        public async UniTask CloseAsync(bool tween = true)
        {
            await InternalOnWindowCloseTween(tween);
            OnWindowClose();
        }
    }
}