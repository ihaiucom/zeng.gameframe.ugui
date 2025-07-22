using System;
using System.Collections.Generic;
using Unity.Mathematics;

namespace Zeng.Utils
{
    /// <summary>
    /// 自定义动画曲线类
    /// </summary>
    [Serializable] // 可序列化，以便在Inspector中显示
    public partial class AnimationCurve
    {
#if UNITY_EDITOR
        [UnityEngine.SerializeField]
#endif
        private List<Keyframe> keys = new List<Keyframe>(); // 关键帧列表

        // 关键帧数量
        public int Length => keys.Count;

        // 获取所有关键帧数组
        public Keyframe[] Keys => keys.ToArray();

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public AnimationCurve()
        {
        }

        /// <summary>
        /// 使用指定关键帧初始化动画曲线
        /// </summary>
        /// <param name="keyframes">关键帧数组</param>
        public AnimationCurve(params Keyframe[] keyframes)
        {
            if (keyframes == null || keyframes.Length == 0)
                return;

            keys.AddRange(keyframes);
            keys.Sort((a, b) => a.Time.CompareTo(b.Time)); // 按时间排序
        }

        /// <summary>
        /// 添加关键帧（自动排序）
        /// </summary>
        public void AddKey(Keyframe key)
        {
            int index = keys.FindIndex(k => k.Time > key.Time);
            if (index == -1)
                keys.Add(key);
            else
                keys.Insert(index, key);
        }

        /// <summary>
        /// 添加关键帧（简化版）
        /// </summary>
        public void AddKey(float time, float value)
        {
            AddKey(new Keyframe(time, value));
        }

        /// <summary>
        /// 添加关键帧（带切线）
        /// </summary>
        public void AddKey(float time, float value, float inTangent, float outTangent)
        {
            AddKey(new Keyframe(time, value, inTangent, outTangent));
        }

        /// <summary>
        /// 移除指定索引的关键帧
        /// </summary>
        public void RemoveKey(int index)
        {
            if (index < 0 || index >= keys.Count)
                throw new IndexOutOfRangeException("关键帧索引越界");

            keys.RemoveAt(index);
        }

        /// <summary>
        /// 索引器，获取或设置指定索引的关键帧
        /// </summary>
        public Keyframe this[int index]
        {
            get => keys[index];
            set
            {
                if (index < 0 || index >= keys.Count)
                    throw new IndexOutOfRangeException("关键帧索引越界");
            
                keys[index] = value;
                // 确保关键帧保持排序状态
                keys.Sort((a, b) => a.Time.CompareTo(b.Time));
            }
        }

        /// <summary>
        /// 评估曲线在指定时间的值
        /// </summary>
        public float Evaluate(float time)
        {
            if (keys.Count == 0) return 0f;
            if (keys.Count == 1) return keys[0].Value;

            // 早于第一个关键帧
            if (time <= keys[0].Time) return keys[0].Value;

            // 晚于最后一个关键帧
            if (time >= keys[keys.Count - 1].Time) return keys[keys.Count - 1].Value;

            // 查找包围该时间的关键帧
            int rightIndex = keys.FindIndex(k => k.Time > time);
            int leftIndex = rightIndex - 1;

            Keyframe leftKey = keys[leftIndex];
            Keyframe rightKey = keys[rightIndex];

            // 线性插值（当切线为0时）
            if (leftKey.OutTangent == 0 && rightKey.InTangent == 0)
            {
                return math.lerp(leftKey.Value, rightKey.Value,
                    (time - leftKey.Time) / (rightKey.Time - leftKey.Time));
            }

            // 三次Hermite样条插值
            return CubicHermiteInterpolation(leftKey, rightKey, time);
        }

        /// <summary>
        /// 三次Hermite样条插值
        /// </summary>
        private float CubicHermiteInterpolation(Keyframe left, Keyframe right, float time)
        {
            float t = (time - left.Time) / (right.Time - left.Time);
            float t2 = t * t;
            float t3 = t2 * t;

            float a = 2 * t3 - 3 * t2 + 1;
            float b = t3 - 2 * t2 + t;
            float c = -2 * t3 + 3 * t2;
            float d = t3 - t2;

            return a * left.Value + b * (right.Time - left.Time) * left.OutTangent
                                  + c * right.Value + d * (right.Time - left.Time) * right.InTangent;
        }

        /// <summary>
        /// 平滑指定关键帧的切线
        /// </summary>
        public void SmoothTangents(int index, float weight)
        {
            if (index < 0 || index >= keys.Count)
                throw new IndexOutOfRangeException("关键帧索引越界");

            if (keys.Count == 1)
            {
                keys[index] = new Keyframe(keys[index].Time, keys[index].Value, 0, 0);
                return;
            }

            if (index == 0)
            {
                // 第一个关键帧 - 只平滑出切线
                Keyframe next = keys[index + 1];
                float tangent = (next.Value - keys[index].Value) / (next.Time - keys[index].Time);
                keys[index] = new Keyframe(keys[index].Time, keys[index].Value, 0, tangent);
                return;
            }

            if (index == keys.Count - 1)
            {
                // 最后一个关键帧 - 只平滑入切线
                Keyframe prev = keys[index - 1];
                float tangent = (keys[index].Value - prev.Value) / (keys[index].Time - prev.Time);
                keys[index] = new Keyframe(keys[index].Time, keys[index].Value, tangent, 0);
                return;
            }

            // 中间关键帧 - 平滑两个切线
            Keyframe prevKey = keys[index - 1];
            Keyframe nextKey = keys[index + 1];

            float inTangent = (keys[index].Value - prevKey.Value) / (keys[index].Time - prevKey.Time);
            float outTangent = (nextKey.Value - keys[index].Value) / (nextKey.Time - keys[index].Time);

            keys[index] = new Keyframe(
                keys[index].Time,
                keys[index].Value,
                inTangent * weight,
                outTangent * weight
            );
        }

    }

}