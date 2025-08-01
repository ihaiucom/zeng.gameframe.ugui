﻿using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Zeng.GameFrame.UIS
{
    /// <summary>
    /// 3DDisplay的扩展
    /// 自带创建与对象池 基础需求使用更简单
    /// </summary>
    public partial class UI3DDisplayExtend
    {
        public async UniTask<GameObject> ShowAsync(string resName, string cameraName = "")
        {
            var obj = await GetDisplayObjectAsync(resName);
            if (obj == null) return null;
            var camera = string.IsNullOrEmpty(cameraName) ? m_UI3DDisplay.ShowCamera : GetCamera(obj, cameraName);
            if (camera == null) return obj; 
            m_UI3DDisplay.Show(obj, camera);
            return obj;
        }

        private async UniTask<GameObject> GetDisplayObjectAsync(string resName)
        {
            if (!m_ObjPool.ContainsKey(resName))
            {
                var newObj = await CreateObjectAsync(resName);
                m_ObjPool.Add(resName, newObj);
            }

            return m_ObjPool[resName];
        }

        private async UniTask<GameObject> CreateObjectAsync(string resName)
        {
            var obj = await UIFactory.InstantiateGameObjectAsync("", resName);
            if (obj == null)
            {
                Debug.LogError($"实例化失败 {resName}  请检查为何没有加载成功 是否配置");
            }

            return obj;
        }
    }
}