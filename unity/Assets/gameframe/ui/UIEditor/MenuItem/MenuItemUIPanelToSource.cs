﻿#if UNITY_EDITOR
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;


namespace Zeng.GameFrame.UIS.Editor
{
    /// <summary>
    /// 面板逆向 转换为源数据
    /// </summary>
    public static class MenuItemUIPanelToSource
    {
        [MenuItem("Assets/UI Frame/Panel 逆向 源数据 Source", false, 1)]
        static void CreateUIPanelByFolder()
        {
            var activeObject = Selection.activeObject as GameObject;
            if (activeObject == null)
            {
                UnityTipsHelper.ShowError("必须选择一个Panel 对象");
                return;
            }

            var path = AssetDatabase.GetAssetPath(Selection.activeObject);

            if (!path.Contains(UISettingConfigEditor.Instance.UIProjectResPath))
            {
                UnityTipsHelper.ShowError(
                    $"请在路径 {UISettingConfigEditor.Instance.UIProjectResPath}/xxx/{UISettingConfigEditor.Instance.UIPanelName} 下右键选择一个Panel 进行转换");
                return;
            }

            var panelCdeTable = activeObject.GetComponent<UIBindCDETable>();
            if (panelCdeTable == null)
            {
                UnityTipsHelper.ShowError("预设错误 没有 UIBindCDETable");
                return;
            }

            if (panelCdeTable.UICodeType != EUICodeType.Panel)
            {
                UnityTipsHelper.ShowError("预设错误 必须是Panel");
                return;
            }

            if (panelCdeTable.IsSplitData)
            {
                UnityTipsHelper.ShowError("这是一个源数据 无法逆向转换");
                return;
            }

            if (string.IsNullOrEmpty(panelCdeTable.PkgName))
            {
                panelCdeTable.AutoCheck();
            }

            if (string.IsNullOrEmpty(panelCdeTable.PkgName))
            {
                UnityTipsHelper.ShowError("未知错误 无法识别 包名");
                return;
            }

            var newSourceName = $"{panelCdeTable.name}{UISettingConfigEditor.Instance.UISource}";
            var savePath =
                $"{UISettingConfigEditor.Instance.UIProjectPackageResPath}/{panelCdeTable.PkgName}/{UISettingConfigEditor.Instance.UISource}/{newSourceName}.prefab";

            if (AssetDatabase.LoadAssetAtPath(savePath, typeof(Object)) != null)
            {
                UnityTipsHelper.CallBack("源数据已存在!!! 此操作将会覆盖旧的数据!!!", () => { CreateNewSource(path, savePath); });
            }
            else
            {
                CreateNewSource(path, savePath);
            }
        }

        private static void CreateNewSource(string loadPath, string savePath)
        {
            var loadPanel = (GameObject)AssetDatabase.LoadAssetAtPath(loadPath, typeof(Object));
            if (loadPanel == null)
            {
                UnityTipsHelper.ShowError($"未知错误 没有加载到Panel 请检查 {loadPath}");
                return;
            }

            var newSource   = UIMenuItemHelper.CopyGameObject(loadPanel);
            var oldCdeTable = newSource.GetComponent<UIBindCDETable>();
            oldCdeTable.IsSplitData = true;
            newSource.name          = $"{loadPanel.name}{UISettingConfigEditor.Instance.UISource}";

            CorrelationView(oldCdeTable);
            
            string dir = Path.GetDirectoryName(savePath);
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            
            

            PrefabUtility.SaveAsPrefabAsset(newSource, savePath);
            Object.DestroyImmediate(newSource);

            UnityTipsHelper.Show($"Panel 逆向 源数据 完毕");
            AssetDatabase.Refresh();
        }

        //关联UI
        private static void CorrelationView(UIBindCDETable cdeTable)
        {
            CorrelationViewByParent(cdeTable.PkgName, cdeTable.PanelSplitData.AllCommonView);
            CorrelationViewByParent(cdeTable.PkgName, cdeTable.PanelSplitData.AllCreateView);
            CorrelationViewByParent(cdeTable.PkgName, cdeTable.PanelSplitData.AllPopupView);
        }

        private static void CorrelationViewByParent(string pkgName, List<RectTransform> parentList)
        {
            foreach (var viewParent in parentList)
            {
                if (viewParent == null) continue;

                var viewName = viewParent.name.Replace(UISetting.UIParentName, "");

                var viewPath =
                    $"{UISettingConfigEditor.Instance.UIProjectPackageResPath}/{pkgName}/{UISettingConfigEditor.Instance.UIPrefabs}/{viewName}.prefab";

                var childView = viewParent.FindChildByName(viewName);
                if (childView != null)
                {
                    Object.DestroyImmediate(childView.gameObject);
                }

                AddView(viewPath, viewParent);
            }
        }

        //吧其他view 关联上
        private static void AddView(string loadPath, Transform parent)
        {
            var loadView = (GameObject)AssetDatabase.LoadAssetAtPath(loadPath, typeof(Object));
            if (loadView == null)
            {
                UnityTipsHelper.ShowError($"未知错误 没有加载到 请检查 {loadPath}");
                return;
            }

            var prefabInstance = PrefabUtility.InstantiatePrefab(loadView) as GameObject;
            if (prefabInstance == null)
            {
                Debug.LogError($"{loadView.name} 未知错误 得到一个null 对象");
                return;
            }

            prefabInstance.transform.SetParent(parent, false);
        }
    }
}
#endif