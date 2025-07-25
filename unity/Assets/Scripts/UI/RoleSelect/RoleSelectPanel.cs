using System;
using Zeng.GameFrame.UIS;

using UnityEngine;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Games.UI.Home;

namespace Games.UI.RoleSelect
{
    public class RoleData
    {
        
        public string Name       = "";
        public string ItemIcon   = "";
        public int    Price      = 100;
    }

    /// <summary>
    /// Author  ZengFeng
    /// Date    2025.7.24
    /// </summary>
    public sealed partial class RoleSelectPanel:RoleSelectPanelBase
    {
    
        #region 生命周期
        
        private UILoopScroll<RoleData, RoleItem> m_LoopScroll;
        private List<RoleData>                   m_AllData;
        protected override void OnUIInit()
        {
            Debug.Log($"RoleSelectPanel OnUIInit");
            m_AllData = new List<RoleData>();
            for (int i = 1; i <= 18; i++)
            {
                m_AllData.Add(new RoleData()
                {
                    Name = $"Role {i}",
                    ItemIcon   = $"shop_{i.ToString("00")}",
                    Price = 100 * i,
                });
            }
            
            
            m_LoopScroll = new UILoopScroll<RoleData, RoleItem>(u_ComRoleListLoopScroll, ShopItemRenderer);
            m_LoopScroll.SetOnClickInfo("u_EventClickSelect", OnClickItemEventHandler);
            
            
        }
        
        private void ShopItemRenderer(int index, RoleData data, RoleItem item, bool select)
        {
            item.UpdateShopData(data);
            item.Select(select);
        }
        
        private void OnClickItemEventHandler(int index, RoleData data, RoleItem item, bool select)
        {
            Debug.Log($"Item {index} , select {select}");
            item.Select(select);
        }

        protected override void OnUIEnable()
        {
            Debug.Log($"RoleSelectPanel OnUIEnable");
        }

        protected override void OnUIDisable()
        {
            Debug.Log($"RoleSelectPanel OnUIDisable");
        }

        protected override void OnUIDestroy()
        {
            Debug.Log($"RoleSelectPanel OnUIDestroy");
        }

        protected override async UniTask<bool> OnOpen()
        {
            await UniTask.CompletedTask;
            Debug.Log($"RoleSelectPanel OnOpen");
            m_LoopScroll.SetDataRefresh(m_AllData,1);
            return true;
        }

        protected override async UniTask<bool> OnOpen(ParamVo param)
        {
            return await base.OnOpen(param);
        }
        
        #endregion

        #region Event开始


       
        protected override void OnEventClickBackButtonAction()
        {
            // uiManager.CloseTopPanel();
            Close();
        }
        
        protected override void OnEventClickEnterButtonAction()
        {
            uiManager.HomePanel<HomePanel>();
        }
         #endregion Event结束

    }
}