﻿using UnityEngine;

namespace Zeng.GameFrame.UIS
{
    public static partial class UIFactory
    {
        internal static void Destroy(GameObject obj)
        {
            UILoad.ReleaseInstantiate(obj);
        }

        //内部会自动调用
        //一定要使用本类中的创建 否则会有报错提示
        internal static void Destroy(UIBase uiBase)
        {
            if (uiBase.OwnerGameObject == null)
            {
                Debug.LogError($"此UI 是空对象 请检查{uiBase.UIBindVo.PkgName} {uiBase.UIBindVo.ResName}");
                return;
            }

            Destroy(uiBase.OwnerGameObject);
        }
    }
}