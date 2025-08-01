﻿#if UNITY_EDITOR
using System.IO;
using UnityEditor;
using UnityEngine;


namespace Zeng.GameFrame.UIS.Editor
{
    public static class MenuItemUIPanel
    {
        [MenuItem("Assets/UI Frame/Create UIPanel", false, 0)]
        static void CreateUIPanelByFolder()
        {
            var activeObject = Selection.activeObject as DefaultAsset;
            if (activeObject == null)
            {
                UnityTipsHelper.ShowError(
                    $"请在路径 {UISettingConfigEditor.Instance.UIProjectResPath}/xxx/{UISettingConfigEditor.Instance.UISource} 下右键创建");
                return;
            }

            var path = AssetDatabase.GetAssetPath(Selection.activeObject);

            if ( /*activeObject.name != UISettingConfigEditor.Instance.UISource ||*/ 
                !path.Contains(UISettingConfigEditor.Instance.UIProjectResPath))
            {
                UnityTipsHelper.ShowError(
                    $"请在路径 {UISettingConfigEditor.Instance.UIProjectResPath}/xxx/{UISettingConfigEditor.Instance.UISource} 下右键创建");
                return;
            }
            Debug.Log(path);
            bool isSource = path.Contains(UISettingConfigEditor.Instance.UISource);

            CreateUIPanelByPath(path, null, isSource);
        }

        internal static void CreateUIPanelByPath(string path, string name = null, bool isSource = true)
        {
            if (!path.Contains(UISettingConfigEditor.Instance.UIProjectResPath))
            {
                UnityTipsHelper.ShowError(
                    $"请在路径 {UISettingConfigEditor.Instance.UIProjectResPath}/xxx/{UISettingConfigEditor.Instance.UISource} 下右键创建");
                return;
            }

            var saveName = string.IsNullOrEmpty(name)
                ? UISettingConfigEditor.Instance.UIUIPanelSourceName
                : $"{name}{UISettingConfigEditor.Instance.UIPanelSourceName}";

            if (!isSource)
            {
                saveName = string.IsNullOrEmpty(name)
                    ? $"Xxx{UISettingConfigEditor.Instance.UIPanelName}"
                    : $"{name}{UISettingConfigEditor.Instance.UIPanelName}";
            }

            var savePath = $"{path}/{saveName}.prefab";

            if (AssetDatabase.LoadAssetAtPath(savePath, typeof(Object)) != null)
            {
                UnityTipsHelper.ShowError($"已存在 请先重命名 {saveName}");
                return;
            }

            var createPanel = CreateUIPanel(null, isSource);
            var panelPrefab = PrefabUtility.SaveAsPrefabAsset(createPanel, savePath);
            Object.DestroyImmediate(createPanel);
            Selection.activeObject = panelPrefab;
        }

        [MenuItem("GameObject/UI Frame/Create UIPanel", false, 0)]
        static void CreateUIPanelByGameObject()
        {
            var activeObject = Selection.activeObject as GameObject;
            if (activeObject == null)
            {
                UnityTipsHelper.ShowError($"请选择UIRoot 右键创建");
                return;
            }

            if (activeObject.name != UIRoot.UILayerRootName)
            {
                UnityTipsHelper.ShowError($"只能在指定的 {UIRoot.UILayerRootName} 下使用 快捷创建Panel");
                return;
            }

            Selection.activeObject = CreateUIPanel(activeObject, false);
        }

        static GameObject CreateUIPanel(GameObject activeObject = null, bool isSource = true)
        {
            //panel
            var panelObject = new GameObject();
            var panelRect   = panelObject.GetOrAddComponent<RectTransform>();
            panelObject.GetOrAddComponent<CanvasRenderer>();
            var cdeTable = panelObject.GetOrAddComponent<UIBindCDETable>();
            cdeTable.UICodeType  = EUICodeType.Panel;
            cdeTable.IsSplitData = isSource;

            //cdeTable.PanelOption |= EPanelOption.DisReset; //如果想要都是默认缓存界面可开启
            var panelEditorData = cdeTable.PanelSplitData;
            panelEditorData.Panel = panelObject;
            panelObject.name      = isSource ? UISettingConfigEditor.Instance.UIUIPanelSourceName : UISettingConfigEditor.Instance.UIUIPanelName;
            if (activeObject != null)
                panelRect.SetParent(activeObject.transform, false);
            panelRect.ResetToFullScreen();

            //阻挡图
            var backgroundObject = new GameObject();
            var backgroundRect   = backgroundObject.GetOrAddComponent<RectTransform>();
            backgroundObject.GetOrAddComponent<CanvasRenderer>();
            backgroundObject.GetOrAddComponent<UIBlock>();
            backgroundObject.name = "UIBlockBG";
            backgroundRect.SetParent(panelRect, false);
            backgroundRect.ResetToFullScreen();


            // 添加allView节点
            var allViewObject = new GameObject();
            var allViewRect   = allViewObject.GetOrAddComponent<RectTransform>();
            allViewObject.name = UISettingConfigEditor.Instance.UIAllViewParentName;
            allViewRect.SetParent(panelRect, false);
            allViewRect.ResetToFullScreen();
            panelEditorData.AllViewParent = allViewRect;

            // 添加AllPopupView节点
            var allPopupViewObject = new GameObject();
            var allPopupViewRect   = allPopupViewObject.GetOrAddComponent<RectTransform>();
            allPopupViewObject.name = UISettingConfigEditor.Instance.UIAllPopupViewParentName;
            allPopupViewRect.SetParent(panelRect, false);
            allPopupViewRect.ResetToFullScreen();
            panelEditorData.AllPopupViewParent = allPopupViewRect;

            panelObject.SetLayerRecursively(LayerMask.NameToLayer("UI"));

            return panelObject;
        }
    }
}
#endif