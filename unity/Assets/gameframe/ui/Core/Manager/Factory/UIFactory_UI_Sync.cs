﻿using System;
using UnityEngine;

namespace Zeng.GameFrame.UIS
{
    public static partial class UIFactory
    {
        public static T Instantiate<T>(RectTransform parent = null) where T : UIBase
        {
            var data = UIBindHelper.GetBindVoByType<T>();
            if (data == null) return null;
            var vo = data.Value;

            return Instantiate<T>(vo, parent);
        }

        public static T Instantiate<T>(UIBindVo vo, RectTransform parent = null) where T : UIBase
        {
            var instance = (T)Create(vo);
            if (instance == null) return null;

            SetParent(instance.OwnerRectTransform, parent ? parent : UIManager.I.UICache);

            return instance;
        }

        public static UIBase Instantiate(Type uiType, RectTransform parent = null)
        {
            var data = UIBindHelper.GetBindVoByType(uiType);
            if (data == null) return null;
            var vo = data.Value;

            return Instantiate(vo, parent);
        }

        public static UIBase Instantiate(UIBindVo vo, RectTransform parent = null)
        {
            var instance = Create(vo);
            if (instance == null) return null;

            SetParent(instance.OwnerRectTransform, parent ? parent : UIManager.I.UICache);

            return instance;
        }

        public static UIBase Instantiate(string pkgName, string resName, RectTransform parent = null)
        {
            var data = UIBindHelper.GetBindVoByPath(pkgName, resName);
            if (data == null) return null;
            var vo = data.Value;

            return Instantiate(vo, parent);
        }
    }
}