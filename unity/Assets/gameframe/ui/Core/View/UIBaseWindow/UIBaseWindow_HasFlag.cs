﻿namespace Zeng.GameFrame.UIS
{
    //窗口选项值 根据需求以下都可以重写 可动态改变值
    //所有窗口选项的属性
    //可快捷操作
    //亦可重写后根据需求动态调整
    public abstract partial class UIBaseWindow
    {
        /// <summary>
        /// 当打开的参数不一致时
        /// 是否可以使用基础Open
        /// 如果允许 别人调用open了一个不存在的Open参数时
        /// 也可以使用默认的open打开界面 则你可以改为true
        /// </summary>
        public virtual bool WindowCanUseBaseOpen => WindowOption.HasFlag(EWindowOption.CanUseBaseOpen);

        /// <summary>
        /// 禁止使用ParamOpen
        /// </summary>
        public virtual bool WindowBanParamOpen => WindowOption.HasFlag(EWindowOption.BanParamOpen);

        /// <summary>
        /// 我有其他IOpen时 允许用open
        /// 默认fasle 我有其他iopen接口时 是不允许使用open的
        /// </summary>
        public virtual bool WindowHaveIOpenAllowOpen => WindowOption.HasFlag(EWindowOption.HaveIOpenAllowOpen);

        //先开
        public virtual bool WindowFirstOpen => WindowOption.HasFlag(EWindowOption.FirstOpen);

        //后关
        public virtual bool WindowLastClose => WindowOption.HasFlag(EWindowOption.LastClose);

        //禁止动画
        public virtual bool WindowBanTween => WindowOption.HasFlag(EWindowOption.BanTween);

        //打开动画不可重复播放
        public virtual bool WindowBanRepetitionOpenTween => WindowOption.HasFlag(EWindowOption.BanRepetitionOpenTween);

        //关闭动画不可重复播放
        public virtual bool WindowBanRepetitionCloseTween =>
            WindowOption.HasFlag(EWindowOption.BanRepetitionCloseTween);

        //不等待打开动画
        public virtual bool WindowBanAwaitOpenTween => WindowOption.HasFlag(EWindowOption.BanAwaitOpenTween);

        //不等待关闭动画
        public virtual bool WindowBanAwaitCloseTween => WindowOption.HasFlag(EWindowOption.BanAwaitCloseTween);

        //我关闭时跳过其他的打开动画
        public virtual bool WindowSkipOtherOpenTween => WindowOption.HasFlag(EWindowOption.SkipOtherOpenTween);

        //我打开时跳过其他的关闭动画
        public virtual bool WindowSkipOtherCloseTween => WindowOption.HasFlag(EWindowOption.SkipOtherCloseTween);

        //Home时 跳过我自己的打开动画
        public virtual bool WindowSkipHomeOpenTween => WindowOption.HasFlag(EWindowOption.SkipHomeOpenTween);

        //播放动画时 可以操作  默认播放动画的时候是不能操作UI的 不然容易出问题
        public virtual bool WindowAllowOptionByTween => WindowOption.HasFlag(EWindowOption.AllowOptionByTween);
    }
}