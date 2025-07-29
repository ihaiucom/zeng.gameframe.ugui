
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Zeng.GameFrame.UIS
{
    public class UIBindProvider
    {
        //业务代码相关程序集的名字
        //默认有Unity默认程序集 可以根据需求修改
#if UI_ET
        internal static string[] LogicAssemblyNames = { "ET.HotfixView" };
#else
        internal static string[] LogicAssemblyNames = { "Assembly-CSharp" };
#endif

        private static Type[] GetLogicTypes()
        {
            return AppDomain.CurrentDomain.GetTypesByAssemblyName(LogicAssemblyNames);
        }

        private Type m_BaseUIWindowType        = typeof(UIPanel);
        private Type m_BaseUISubPanelType      = typeof(UIView);
        private Type m_BaseUIViewType          = typeof(UIComponent);

        public UIBindVo[] Get()
        {
            var gameTypes = GetLogicTypes();
            if (gameTypes.Length < 1)
            {
                return Array.Empty<UIBindVo>();
            }

            var windowTypes     = new List<Type>(); //继承UIWindow的
            var subpanelTypes      = new List<Type>(); //继承UISubPanel的
            var viewTypes = new List<Type>(); //继承UIView的
            var binds          = new List<UIBindVo>();

            foreach (var gameType in gameTypes)
            {
                if (m_BaseUIWindowType.IsAssignableFrom(gameType))
                {
                    windowTypes.Add(gameType);
                }
                else if (m_BaseUISubPanelType.IsAssignableFrom(gameType))
                {
                    subpanelTypes.Add(gameType);
                }
                else if (m_BaseUIViewType.IsAssignableFrom(gameType))
                {
                    viewTypes.Add(gameType);
                }
            }

            //Window绑定
            foreach (var windowType in windowTypes)
            {
                if (windowType.BaseType == null)
                {
                    continue;
                }

                if (GetBindVo(windowType.BaseType, windowType, m_BaseUIWindowType, out var bindVo))
                {
                    binds.Add(bindVo);
                }
            }

            //SubPanel绑定
            foreach (var subpanelType in subpanelTypes)
            {
                if (subpanelType.BaseType == null)
                {
                    continue;
                }

                if (GetBindVo(subpanelType.BaseType, subpanelType, m_BaseUISubPanelType, out var bindVo))
                {
                    binds.Add(bindVo);
                }
            }

            //View绑定
            foreach (var viewType in viewTypes)
            {
                if (viewType.BaseType == null)
                {
                    continue;
                }

                if (GetBindVo(viewType.BaseType, viewType, m_BaseUIViewType, out var bindVo))
                {
                    binds.Add(bindVo);
                }
            }

            return binds.ToArray();
        }

        private static bool GetBindVo(Type uiBaseType, Type creatorType, Type groupType, out UIBindVo bindVo)
        {
            bindVo = new UIBindVo();
            if (uiBaseType == null ||
                !uiBaseType.GetFieldValue("PkgName", out bindVo.PkgName) ||
                !uiBaseType.GetFieldValue("ResName", out bindVo.ResName))
            {
                return false;
            }

            bindVo.CodeType    = uiBaseType.BaseType;
            bindVo.BaseType    = uiBaseType;
            bindVo.CreatorType = creatorType;
            return true;
        }

        public void WriteCode(UIBindVo info, StringBuilder sb)
        {
            sb.Append("            {\r\n");
            sb.AppendFormat("                PkgName     = {0}.PkgName,\r\n", info.BaseType.FullName);
            sb.AppendFormat("                ResName     = {0}.ResName,\r\n", info.BaseType.FullName);
            sb.AppendFormat("                CodeType    = {0},\r\n", GetCodeTypeName(info.CodeType));
            sb.AppendFormat("                BaseType    = typeof({0}),\r\n", info.BaseType.FullName);
            sb.AppendFormat("                CreatorType = typeof({0}),\r\n", info.CreatorType.FullName);
            sb.Append("            };\r\n");
        }

        private string GetCodeTypeName(Type uiBaseType)
        {
            if (uiBaseType == m_BaseUIWindowType)
            {
                return UISetting.UIBasePanelName;
            }
            else if (uiBaseType == m_BaseUISubPanelType)
            {
                return UISetting.UIBaseViewName;
            }
            else if (uiBaseType == m_BaseUIViewType)
            {
                return UISetting.UIBaseComponentName;
            }
            else
            {
                Debug.LogError($"当前类型错误 是否新增了类型 {uiBaseType}");
                return UISetting.UIBaseName;
            }
        }

        public void NewCode(UIBindVo info, StringBuilder sb)
        {
        }
    }
}