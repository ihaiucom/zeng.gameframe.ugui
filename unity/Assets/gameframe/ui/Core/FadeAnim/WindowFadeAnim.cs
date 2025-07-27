using System;
using Cysharp.Threading.Tasks;
using LitMotion;
using LitMotion.Extensions;
using UnityEngine;

namespace Zeng.GameFrame.UIS
{
    /// <summary>
    /// 简单的窗口动画
    /// </summary>
    public static class WindowFadeAnim
    {
        private static Vector3 m_AnimScale = new Vector3(0.8f, 0.8f, 0.8f);

        //淡入
        public static async UniTask In(UIBase uiBase, float time = 0.25f)
        {
            var obj = uiBase?.OwnerGameObject;
            if (obj == null) return;

            uiBase.SetActive(true);
            obj.transform.localScale = m_AnimScale;

            var handle = LMotion.Create(obj.transform.localScale, Vector3.one, time)
                .BindToLocalScale(obj.transform).AddTo(obj);
            
            try
            {
                await handle.ToUniTask(CancelBehavior.Complete);
            }
            catch (OperationCanceledException e)
            {
                Debug.LogException(e);
            }
        }

        //淡出
        public static async UniTask Out(UIBase uiBase, float time = 0.25f)
        {
            var obj = uiBase?.OwnerGameObject;
            if (obj == null) return;

            obj.transform.localScale = Vector3.one;
            
            
            var handle =LMotion.Create(obj.transform.localScale, m_AnimScale, time)
                .BindToLocalScale(obj.transform).AddTo(obj);
            
            try
            {
                await handle.ToUniTask(CancelBehavior.Complete);
            }
            catch (OperationCanceledException e)
            {
                Debug.LogException(e);
            }

            uiBase.SetActive(false);
            obj.transform.localScale = Vector3.one;
        }
    }
}