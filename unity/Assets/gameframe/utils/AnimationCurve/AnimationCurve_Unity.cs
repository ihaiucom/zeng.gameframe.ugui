#if UNITY
namespace Zeng.Utils
{
    public partial class AnimationCurve
    {
        /** Unity AnimationCurve 互转 **/
        /// <summary>
        /// 从Unity的AnimationCurve创建自定义曲线
        /// </summary>
        public static AnimationCurve FromUnityCurve(UnityEngine.AnimationCurve unityCurve)
        {
            if (unityCurve == null) return new AnimationCurve();

            var keys = new Keyframe[unityCurve.length];
            for (int i = 0; i < unityCurve.length; i++)
            {
                var unityKey = unityCurve[i];
                keys[i] = new Keyframe(
                    unityKey.time,
                    unityKey.value,
                    unityKey.inTangent,
                    unityKey.outTangent,
                    unityKey.inWeight,
                    unityKey.outWeight
                );
            }

            return new AnimationCurve(keys);
        }

        /// <summary>
        /// 转换为Unity的AnimationCurve
        /// </summary>
        public UnityEngine.AnimationCurve ToUnityCurve()
        {
            var unityKeys = new UnityEngine.Keyframe[keys.Count];
            for (int i = 0; i < keys.Count; i++)
            {
                var key = keys[i];
                unityKeys[i] = new UnityEngine.Keyframe(
                    key.Time,
                    key.Value,
                    key.InTangent,
                    key.OutTangent,
                    key.InWeight,
                    key.OutWeight
                );
            }

            return new UnityEngine.AnimationCurve(unityKeys);
        }

        /// <summary>
        /// 将自定义曲线数据刷新到Unity内置曲线（覆盖目标曲线）
        /// </summary>
        /// <param name="unityCurve">目标Unity曲线</param>
        public void RefshToUnityCurve(UnityEngine.AnimationCurve unityCurve)
        {
            if (unityCurve == null)
            {
                UnityEngine.Debug.LogError("无法刷新到Unity曲线：目标曲线为null");
                return;
            }

            // 清空目标曲线
            while (unityCurve.length > 0)
            {
                unityCurve.RemoveKey(0);
            }

            // 将自定义关键帧逐个添加到Unity曲线
            foreach (var key in this.keys)
            {
                unityCurve.AddKey(new UnityEngine.Keyframe(
                    key.Time,
                    key.Value,
                    key.InTangent,
                    key.OutTangent,
                    key.InWeight,
                    key.OutWeight
                ));
            }

            // // 确保曲线数据一致
            // for (int i = 0; i < unityCurve.length; i++)
            // {
            //     unityCurve.SmoothTangents(i, 0);
            // }
        }

        /// <summary>
        /// 从Unity内置曲线刷新数据到自定义曲线（覆盖当前曲线）
        /// </summary>
        /// <param name="unityCurve">源Unity曲线</param>
        public void RefshFromUnityCurve(UnityEngine.AnimationCurve unityCurve)
        {
            if (unityCurve == null)
            {
                UnityEngine.Debug.LogError("无法从Unity曲线刷新：源曲线为null");
                return;
            }

            // 清空当前曲线
            this.keys.Clear();

            // 从Unity曲线添加关键帧
            for (int i = 0; i < unityCurve.length; i++)
            {
                var unityKey = unityCurve[i];
                this.AddKey(new Keyframe(
                    unityKey.time,
                    unityKey.value,
                    unityKey.inTangent,
                    unityKey.outTangent,
                    unityKey.inWeight,
                    unityKey.outWeight
                ));
            }
        }
    }
}
#endif