﻿namespace Zeng.GameFrame.UIS
{
    /// <summary>
    /// 当一个界面 EPanelOption.DisClose 时 (禁止关闭)
    /// 且又被调用时 则会触发 可根据需求继承
    /// </summary>
    public interface IUIBanClose
    {
        //根据需求返回 是否可以被关闭
        //返回true 就是可以被关闭
        bool DoBanClose();
    }
}