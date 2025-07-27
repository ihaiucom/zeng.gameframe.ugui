using LitMotion;
using LitMotion.Extensions;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Zeng.GameFrame.UIS
{
    /// <summary>
    /// 点击按钮影响组件大小
    /// </summary>
    public class UIClickEffect : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [Tooltip("被影响的目标")]
        public RectTransform targetTsf;

        [Tooltip("变化大小 (倍数)")]
        public float scaleValue = 0.9f;

        [Tooltip("变小时间")]
        public float scaleTime = 0.1f;

        [Tooltip("变大时间")]
        public float popTime = 0.1f;

        private Button m_button;

        private Vector3 targetScale; //目标大小
        private Vector3 atScale;     //当前大小

        /// <summary>
        /// 可调整动画状态
        /// </summary>
        public Ease ease = Ease.OutElastic;
        
        private MotionHandle m_motionHandle;

        private void Awake()
        {
            m_button = GetComponent<Button>(); //需要先挂button 否则无效
            if (targetTsf == null)             //如果没有目标则默认自己为目标
            {
                targetTsf = transform.gameObject.GetComponent<RectTransform>();
            }

            atScale     = targetTsf.localScale;
            targetScale = atScale * scaleValue;
        }

        private void OnDestroy()
        {
            MotionKill();
        }

        private void MotionKill()
        {
            if (m_motionHandle != default)
            {
                m_motionHandle.Complete();
                m_motionHandle = default;
            }
        }

        //按下
        public void OnPointerDown(PointerEventData eventData)
        {
            if (m_button)
            {
                if (m_button.enabled && m_button.interactable)
                {
                    MotionKill();
                    m_motionHandle = LMotion.Create(targetTsf.localScale, targetScale, scaleTime)
                        .WithOnComplete(() => { m_motionHandle = default; })
                        .WithEase(ease)
                        .BindToLocalScale(targetTsf)
                        .AddTo(targetTsf);
                }
            }
            else
            {
                MotionKill();
                m_motionHandle = LMotion.Create(targetTsf.localScale, targetScale, scaleTime)
                    .WithOnComplete(() => { m_motionHandle = default; })
                    .WithEase(ease)
                    .BindToLocalScale(targetTsf)
                    .AddTo(targetTsf);
            }
        }

        //抬起
        public void OnPointerUp(PointerEventData eventData)
        {
            MotionKill();
            m_motionHandle = LMotion.Create(targetTsf.localScale, atScale, popTime)
                .WithOnComplete(() => { m_motionHandle = default; })
                .WithEase(ease)
                .BindToLocalScale(targetTsf)
                .AddTo(targetTsf);
            
        }

        #if UNITY_EDITOR
        private void OnValidate()
        {
            if (targetTsf == null) //如果没有目标则默认自己为目标
            {
                targetTsf = transform.gameObject.GetComponent<RectTransform>();
            }
        }
        #endif
    }
}