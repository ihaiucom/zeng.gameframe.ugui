using Cysharp.Threading.Tasks;

namespace Zeng.GameFrame.UIS
{
    /// <summary>
    /// 打开泛型 同步方法 内部还是异步打开
    /// 不想提供快捷操作可以删除此类
    /// </summary>
    public partial class UIManager
    {
        public void OpenPanel<T>()
            where T : UIPanel, new()
        {
            OpenPanelAsync<T>().Forget();
        }

        public void OpenPanel<T, P1>(P1 p1)
            where T : UIPanel, IUIOpen<P1>, new()
        {
            OpenPanelAsync<T, P1>(p1).Forget();
        }

        public void OpenPanel<T, P1, P2>(P1 p1, P2 p2)
            where T : UIPanel, IUIOpen<P1, P2>, new()
        {
            OpenPanelAsync<T, P1, P2>(p1, p2).Forget();
        }

        public void OpenPanel<T, P1, P2, P3>(P1 p1, P2 p2, P3 p3)
            where T : UIPanel, IUIOpen<P1, P2, P3>, new()
        {
            OpenPanelAsync<T, P1, P2, P3>(p1, p2, p3).Forget();
        }

        public void OpenPanel<T, P1, P2, P3, P4>(P1 p1, P2 p2, P3 p3, P4 p4)
            where T : UIPanel, IUIOpen<P1, P2, P3, P4>, new()
        {
            OpenPanelAsync<T, P1, P2, P3, P4>(p1, p2, p3, p4).Forget();
        }

        public void OpenPanel<T, P1, P2, P3, P4, P5>(P1 p1, P2 p2, P3 p3, P4 p4, P5 p5)
            where T : UIPanel, IUIOpen<P1, P2, P3, P4, P5>, new()
        {
            OpenPanelAsync<T, P1, P2, P3, P4, P5>(p1, p2, p3, p4, p5).Forget();
        }
    }
}