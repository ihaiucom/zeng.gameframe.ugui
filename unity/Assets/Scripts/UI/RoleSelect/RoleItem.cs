using System;
using Zeng.GameFrame.UIS;

using UnityEngine;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace Games.UI.RoleSelect
{
    /// <summary>
    /// Author  ZengFeng
    /// Date    2025.7.24
    /// </summary>
    public sealed partial class RoleItem:RoleItemBase
    {
        
        private RoleData m_ShopData;

        public void UpdateShopData(RoleData shopData)
        {
            m_ShopData = shopData;
            UpdateItem();
        }

        private void UpdateItem()
        {
            u_ComName.text = m_ShopData.Name;
            u_ComPrice.text = m_ShopData.Price.ToString();
            u_DataIconPath.SetValue(m_ShopData.ItemIcon);
        }

        public void Select(bool select)
        {
            u_DataIsSelected.SetValue(select);
        }
        
    

        #region Event开始


       
        protected override void OnEventClickSelectAction()
        {
            
        }
         #endregion Event结束

    }
}