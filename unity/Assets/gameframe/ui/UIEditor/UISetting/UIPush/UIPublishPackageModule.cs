using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEditor;

#if UNITY_EDITOR

namespace Zeng.GameFrame.UIS.Editor
{
    public class UIPublishPackageModule
    {
        private UIPushEditor m_UIPublishModule;
        
        [LabelText("模块名")]
        [ReadOnly]
        public string PkgName;

        [FolderPath]
        [LabelText("模块路径")]
        [ReadOnly]
        public string PkgPath;
        
        
        [LabelText("当前模块所有组件")]
        [ReadOnly]
        [ShowInInspector]
        // [ShowIf("m_UIPublishPackageData", EUIPublishPackageData.CDETable)]
        private List<UIBindCDETable> m_AllCDETable = new List<UIBindCDETable>();
        
        
        
        [GUIColor(0.4f, 0.8f, 1)]
        [Button("发布当前模块", 50)]
        [PropertyOrder(-999)]
        private void PublishCurrent()
        {
            PublishCurrent(true);
        }

        public void PublishCurrent(bool showTips)
        {
            if (!UIOperationHelper.CheckUIOperation()) return;

            foreach (var current in m_AllCDETable)
            {
                current.CreateUICode(false, false);
            }

            if (showTips)
                UnityTipsHelper.CallBackOk($"UI当前模块 {PkgName} 发布完毕", UISettingEditor.CloseWindowRefresh);
        }
        
        
        public UIPublishPackageModule(UIPushEditor publishModule, string pkgName)
        {
            m_UIPublishModule = publishModule;
            PkgName           = pkgName;
            PkgPath           = $"{UISetting.UIProjectResPath}/{pkgName}";
            FindUIBindCDETableResources();
        }

        private void FindUIBindCDETableResources()
        {
            var strings = AssetDatabase.GetAllAssetPaths().Where(x =>
                x.StartsWith($"{PkgPath}/{UISetting.UIPrefabs}", StringComparison.InvariantCultureIgnoreCase));

            foreach (var path in strings)
            {
                var cdeTable = AssetDatabase.LoadAssetAtPath<UIBindCDETable>(path);
                if (cdeTable == null) continue;
                if (!cdeTable.IsSplitData)
                {
                    m_AllCDETable.Add(cdeTable);
                }
            }
        }
    }
}
#endif