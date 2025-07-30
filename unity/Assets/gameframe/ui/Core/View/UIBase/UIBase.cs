using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Zeng.GameFrame.UIS
{
    /// <summary>
    /// UI基类
    /// </summary>
    // public abstract partial class UIBase : MonoBehaviour
    public abstract partial class UIBase
    {
        //用这个不用.单例而已
        protected UIManager uiManager { get; private set; }
        

        /// <summary>
        /// 绑定信息
        /// </summary>
        public UIBindVo UIBindVo { get; private set; }
        
        
        /// <summary>
        /// UI的资源包名
        /// </summary>
        public string UIPkgName => UIBindVo.PkgName;

        /// <summary>
        /// UI的资源名称
        /// </summary>
        public string UIResName => UIBindVo.ResName;
        
        protected UIBindCDETable CDETable { get; private set; }
        protected UIBindComponentTable ComponentTable { get; private set; }
        protected UIBindDataTable DataTable { get; private set; }
        public UIBindEventTable EventTable { get; private set; }

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
        /// 设置显隐
        /// </summary>
        public void SetActive(bool value)
        {
            if (OwnerGameObject == null) return;
            OwnerGameObject.SetActive(value);
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

            uiManager = UIManager.I;
            UIBindVo   = uiBindVo;
            OwnerGameObject    = ownerGameObject;
            OwnerRectTransform = ownerGameObject.GetComponent<RectTransform>();
            
            CDETable         = OwnerGameObject.GetComponent<UIBindCDETable>();
            if (CDETable == null)
            {
                Debug.LogError($"{OwnerGameObject.name} 没有UIBindCDETable组件 这是必须的");
                return false;
            }

            ComponentTable = CDETable.ComponentTable;
            DataTable      = CDETable.DataTable;
            EventTable     = CDETable.EventTable;
            CDETable.BindUIBase(this);

            UIBaseInitialize();
            UIBaseInit = true;
            return true;
        }


        private void UIBaseInitialize()
        {
            CDETable.UIBaseStart     = UIBaseStart;
            CDETable.UIBaseOnDestroy = UIBaseOnDestroy;
            try
            {
                SealedInitialize();
                UIBind();
                Initialize();
                if (ActiveSelf)
                    UIBaseOnEnable();
                else
                    UIBaseOnDisable();
                
                
                CDETable.UIBaseOnEnable  = UIBaseOnEnable;
                CDETable.UIBaseOnDisable = UIBaseOnDisable;
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                throw;
            }
        }
        
        
        //------------------ Initialize ---------------
        
        
        //UIBase 生命周期顺序 1
        protected virtual void SealedInitialize()
        {
        }

        //UIBase 生命周期顺序 2
        protected virtual void UIBind()
        {
        }
        
        //UIBase 生命周期顺序 3
        protected virtual void Initialize()
        {
        }
        
        //------------------ Start ---------------
        private void UIBaseStart()
        {
            SealedStart();
            Start();
        }
        
        protected virtual void SealedStart()
        {
        }
        
        protected virtual void Start()
        {
        }
        
        
        
        
        //------------------ OnEnable ---------------
        private void UIBaseOnEnable()
        {
            OnEnable();
        }
        
        //UIBase 生命周期顺序 4
        protected virtual void OnEnable()
        {
        }

        //------------------ OnDisable ---------------

        private void UIBaseOnDisable()
        {
            OnDisable();
        }
        
        
        //UIBase 生命周期顺序 4
        protected virtual void OnDisable()
        {
        }
        
        
        //------------------ OnDestroy ---------------
        private void UIBaseOnDestroy()
        {
            UnUIBind();
            OnDestroy();
            SealedOnDestroy();
            UIFactory.Destroy(this);
        }
        
        
        //UIBase 生命周期顺序 7
        protected virtual void UnUIBind()
        {
        }
        
        
        //UIBase 生命周期顺序 8
        protected virtual void OnDestroy()
        {
        }

        
        //UIBase 生命周期顺序 9
        protected virtual void SealedOnDestroy()
        {
        }
        



        
        

    }
}