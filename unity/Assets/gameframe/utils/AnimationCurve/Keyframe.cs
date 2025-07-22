using System;

namespace Zeng.Utils
{
    
    /// <summary>
    /// 自定义关键帧结构体
    /// </summary>
    [Serializable]
    public struct Keyframe : IComparable<Keyframe>
    {
        public float Time { get; set; } // 关键帧时间
        public float Value { get; set; } // 关键帧值
        public float InTangent { get; set; } // 入切线（左侧曲线斜率）
        public float OutTangent { get; set; } // 出切线（右侧曲线斜率）
        public float InWeight { get; set; } // 入切线权重
        public float OutWeight { get; set; } // 出切线权重

        /// <summary>
        /// 构造函数（简化版）
        /// </summary>
        public Keyframe(float time, float value)
        {
            Time = time;
            Value = value;
            InTangent = 0;
            OutTangent = 0;
            InWeight = 0;
            OutWeight = 0;
        }

        /// <summary>
        /// 构造函数（带切线）
        /// </summary>
        public Keyframe(float time, float value, float inTangent, float outTangent)
        {
            Time = time;
            Value = value;
            InTangent = inTangent;
            OutTangent = outTangent;
            InWeight = 0;
            OutWeight = 0;
        }

        /// <summary>
        /// 构造函数（完整参数）
        /// </summary>
        public Keyframe(float time, float value, float inTangent, float outTangent, float inWeight, float outWeight)
        {
            Time = time;
            Value = value;
            InTangent = inTangent;
            OutTangent = outTangent;
            InWeight = inWeight;
            OutWeight = outWeight;
        }

        /// <summary>
        /// 比较方法（按时间排序）
        /// </summary>
        public int CompareTo(Keyframe other)
        {
            return Time.CompareTo(other.Time);
        }
    }
}