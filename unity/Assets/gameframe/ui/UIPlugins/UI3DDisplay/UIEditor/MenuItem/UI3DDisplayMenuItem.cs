﻿#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;


namespace Zeng.GameFrame.UIS.Editor
{
    internal static class UI3DDisplayMenuItem
    {
        [MenuItem("GameObject/UI Frame/3DDisplay", false, 20001)]
        private static void Create3DDisplay()
        {
            var activeObject = Selection.activeObject as GameObject;
            if (activeObject == null)
            {
                UnityTipsHelper.ShowError($"请选择一个对象 右键创建");
                return;
            }

            var path =
                $"{UISettingConfigEditor.Instance.UIFrameworkPath}/UIPlugins/UI3DDisplay/UIEditor/TemplatePrefabs/UI3DDisplay.prefab";

            Selection.activeObject = UIMenuItemHelper.CloneGameObjectByPath(path, activeObject.transform);
        }
    }
}
#endif