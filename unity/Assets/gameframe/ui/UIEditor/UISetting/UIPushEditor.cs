using Sirenix.OdinInspector;
using Sirenix.Utilities;

namespace Zeng.GameFrame.UIS.Editor
{
    public class UIPushEditor
    {
        private static UIPushEditor instance;
        public static UIPushEditor Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new UIPushEditor();
                }

                return instance;
            }
        }
        
        [GUIColor(0f, 1f, 1f)]
        [Button("UI自动生成绑定替代反射代码", 50)]
        [PropertyOrder(-9999)]
        public static void CreateUIBindProvider()
        {
            CreateUIBindProviderModule.Instance.Create();
        }
    }
}