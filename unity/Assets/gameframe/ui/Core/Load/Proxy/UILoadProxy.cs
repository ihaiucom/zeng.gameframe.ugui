using System;
using Cysharp.Threading.Tasks;
using Object = UnityEngine.Object;

namespace Zeng.GameFrame.UIS
{
    /// <summary>
    /// UI 资源加载代理
    /// </summary>
    public class UILoadProxy
    {
        public static (Object, int) LoadAsset(string packageName, string location, Type type)
        {
            string path = $"Assets/GameRes/UI/{packageName}/Prefabs/{location}.prefab";
            return LoadAssetFunc("", path, type);
        }
        
        
        public static  UniTask<(Object, int)> LoadAssetAsync(string packageName, string location, Type type)
        {
            string path = $"Assets/GameRes/UI/{packageName}/Prefabs/{location}.prefab";
            return LoadAssetAsyncFunc("", path, type);
        }

        /// <summary>
        /// 同步加载
        /// </summary>
        /// <param name="packageName">包名</param>
        /// <param name="location">资源名</param>
        /// <param name="type">类型</param>
        /// <returns>返回值(obj资源对象,唯一ID)</returns>
        public static Func<string, string, Type, (Object, int)> LoadAssetFunc { internal get; set; }

        /// <summary>
        /// 异步加载方法
        /// </summary>
        /// <param name="packageName">包名</param>
        /// <param name="location">资源名</param>
        /// <param name="type">类型</param>
        /// <returns>返回值UniTask<(obj资源对象,唯一ID)></returns>
        public static Func<string, string, Type, UniTask<(Object, int)>> LoadAssetAsyncFunc { internal get; set; }

        /// <summary>
        /// 验证是否有效
        /// </summary>
        /// <param name="packageName">包名</param>
        /// <param name="location">资源名</param>
        /// <returns>返回值bool</returns>
        public static Func<string, string, bool> VerifyAssetValidityFunc { internal get; set; }

        /// <summary>
        /// 释放方法
        /// </summary>
        /// <param name="hadCode">唯一ID</param>
        public static Action<int> ReleaseAction { internal get; set; }

        /// <summary>
        /// 释放所有方法
        /// </summary>
        public static Action ReleaseAllAction { internal get; set; }
    }
}