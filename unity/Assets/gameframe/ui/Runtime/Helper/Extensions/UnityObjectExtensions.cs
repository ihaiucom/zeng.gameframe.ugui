using UnityEngine;

namespace Zeng.GameFrame.UIs
{
    public static class UnityObjectExtensions
    {
        public static void SafeDestroySelf(
            this Object obj)
        {
            if (obj == null) return;

            #if UNITY_EDITOR
            if (!Application.isPlaying)
                Object.DestroyImmediate(obj);
            else
                Object.Destroy(obj);
            #else
            Object.Destroy(obj);
            #endif
        }
    }
}