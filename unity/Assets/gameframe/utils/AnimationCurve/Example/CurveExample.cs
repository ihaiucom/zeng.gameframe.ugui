using Sirenix.OdinInspector;
using UnityEngine;
namespace Zeng.Utils
{

    /// <summary>
    /// 动画曲线使用示例
    /// </summary>
    public class CurveExample : MonoBehaviour
    {
        [Header("自定义动画曲线")]
        public AnimationCurve customCurve = new AnimationCurve(
            new Keyframe(0, 0),
            new Keyframe(1, 1),
            new Keyframe(2, 0)
        );
    
        [Header("Unity内置曲线（对比）")]
        public UnityEngine.AnimationCurve unityCurve = new UnityEngine.AnimationCurve(
            new UnityEngine.Keyframe(0, 0),
            new UnityEngine.Keyframe(1, 1),
            new UnityEngine.Keyframe(2, 0)
        );
        public bool useCustomCurve = true;
        
#if UNITY
        [ContextMenu("FromUnityCurve")]
        [Button("FromUnityCurve")]
        public void FromUnityCurve()
        {
            customCurve.RefshFromUnityCurve(unityCurve);
        }
        
        
        [ContextMenu("ToUnityCurve")]
        [Button("ToUnityCurve")]
        public void ToUnityCurve()
        {
            customCurve.RefshToUnityCurve(unityCurve);
        }
#endif
    
        private void Update()
        {
            if (useCustomCurve)
            {
                // 使用自定义曲线控制物体X轴位置
                float customValue = customCurve.Evaluate(Time.time % 2f);
                // transform.position = new Vector3(customValue * 3 - 1.5f, 0, transform.position.z);
                transform.position = new Vector3(transform.position.x, customValue * 3 - 1.5f, transform.position.z);
            }
            else
            {
                
                // 使用Unity曲线控制物体Y轴位置（用于对比）
                float unityValue = unityCurve.Evaluate(Time.time % 2f);
                transform.position = new Vector3(transform.position.x, unityValue * 3 - 1.5f, transform.position.z);
            }
        
        }
    }
}