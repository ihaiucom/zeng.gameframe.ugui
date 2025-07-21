using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Zeng.GameFrame.UIS
{
    /// <summary>
    /// UI基类
    /// </summary>
    public abstract class UIBase : MonoBehaviour
    {
        //用这个不用.单例而已
        protected UIManager UIManager { get; private set; }

        /// <summary>
        /// 绑定信息
        /// </summary>
        public UIBindVo UIBindVo { get; private set; }

        /// <summary>
        /// 当前UI的预设对象
        /// </summary>
        [LabelText("UI对象")]
        public GameObject OwnerGameObject { get; private set; }

        /// <summary>
        /// 当前UI的Tsf
        /// </summary>
        public RectTransform OwnerRectTransform  { get; private set; }
        
        
        /// <summary>
        /// 当前显示状态  显示/隐藏
        /// 不要使用这个设置显影
        /// 应该使用控制器 或调用方法 SetActive();
        /// </summary>
        public bool ActiveSelf
        {
            get
            {
                if (OwnerGameObject == null) return false;
                return OwnerGameObject.activeSelf;
            }
        }


        /// <summary>
        /// 初始化状态
        /// </summary>
        [LabelText("UI是否已初始化")]
        public bool UIBaseInit { get; private set; }


        /// <summary>
        /// 初始化UIBase 由PanelMgr创建对象后调用
        /// 外部禁止
        /// </summary>
        internal bool InitUIBase(UIBindVo uiBindVo, GameObject ownerGameObject)
        {
            if (ownerGameObject == null)
            {
                Debug.LogError($"null对象无法初始化");
                return false;
            }

            UIManager = UIManager.I;
            UIBindVo   = uiBindVo;
            OwnerGameObject    = ownerGameObject;
            OwnerRectTransform = ownerGameObject.GetComponent<RectTransform>();

            UIBaseInitialize();
            UIBaseInit = true;
            return true;
        }


        private void UIBaseInitialize()
        {
            try
            {
                SealedInitialize();
                UIBind();
                OnUIInit();
                if (ActiveSelf)
                    OnUIEnable();
                else
                    OnUIDisable();
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                throw;
            }
        }
        
        private void OnEnable()
        {
            if (UIBaseInit)
            {
                OnUIEnable();
            }
        }
        
        private void OnDisable()
        {
            if (UIBaseInit)
            {
                OnUIDisable();
            }
        }



        private void OnDestroy()
        {
            UnUIBind();
            OnUIDestroy();
            SealedOnDestroy();
            UIFactory.Destroy(this);
        }

        
        
        //这是给基类用的生命周期(BasePanel,BaseView) 为了防止有人重写时不调用基类 所以直接独立
        //没有什么穿插需求怎么办
        //基类会重写这个类且会密封你也调用不到
        //不要问为什么...
        //UIBase 生命周期顺序 1
        protected virtual void SealedInitialize()
        {
        }

        
        //UIBase 生命周期顺序 2
        protected virtual void UIBind()
        {
        }

        //UIBase 生命周期顺序 3
        protected virtual void OnUIInit()
        {
        }
        
        
        //UIBase 生命周期顺序 4
        protected virtual void OnUIEnable()
        {
        }
        
        
        
        
        //UIBase 生命周期顺序 6
        protected virtual void OnUIDisable()
        {
        }
        
        //UIBase 生命周期顺序 7
        protected virtual void UnUIBind()
        {
        }
        
        //UIBase 生命周期顺序 8
        protected virtual void OnUIDestroy()
        {
        }
        
        //UIBase 生命周期顺序 9
        protected virtual void SealedOnDestroy()
        {
        }

    }
}