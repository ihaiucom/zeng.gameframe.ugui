﻿namespace Zeng.GameFrame.UIS
{
    public abstract partial class UIPanel
    {
        //容器类界面 //比如伤害飘字 此类界面如果做到panel层会被特殊处理 建议还是不要放到panel层
        public virtual bool PanelContainer => PanelOption.HasFlag(EPanelOption.Container);

        //永久缓存界面 //永远不会被摧毁 与禁止关闭不同这个可以关闭 只是不销毁 也可相当于无限长的倒计时
        public virtual bool PanelForeverCache => PanelOption.HasFlag(EPanelOption.ForeverCache);

        //倒计时缓存界面 //被关闭后X秒之后在摧毁 否则理解摧毁
        public virtual bool PanelTimeCache => PanelOption.HasFlag(EPanelOption.TimeCache);

        //禁止关闭的界面 //是需要一直存在的你可以隐藏 但是你不能摧毁
        public virtual bool PanelDisClose => PanelOption.HasFlag(EPanelOption.DisClose);

        //忽略返回 返回操作会跳过这个界面 //他的打开与关闭不会触发返回功能 堆栈功能
        public virtual bool PanelIgnoreBack => PanelOption.HasFlag(EPanelOption.IgnoreBack);
    }
}