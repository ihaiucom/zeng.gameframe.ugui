

namespace Zeng.GameFrame.UIS
{
    //UI最高层级之上的 屏蔽所有操作的模块
    //屏蔽层显示时所有UI操作会被屏蔽 默认隐藏
    //可通过API 快捷屏蔽操作
    //适用于 统一动画播放时 某些操作需要等待时 都可以调节
    public partial class UIManager
    {
        //永久屏蔽
        //适用于 不知道要屏蔽多久 但是能保证可以成对调用的
        //这个没有放到API类中
        //因为如果你不能保证请不要用
        //至少过程中要try起来finally 保证不会出错 否则请不要使用这个功能
        public int BanLayerOptionForever()
        {
            return uiBlockLayer.BanLayerOptionForever();
        }

        //恢复永久屏蔽
        public void RecoverLayerOptionForever(int code)
        {
            uiBlockLayer.RecoverLayerOptionForever(code);
        }

    }
}