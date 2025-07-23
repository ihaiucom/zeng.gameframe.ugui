#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;


namespace Zeng.GameFrame.UIS.Editor
{
    internal static class UICommonMenuItem
    {
        private static void CreateTarget(string targetName)
        {
            var activeObject = Selection.activeObject as GameObject;
            if (activeObject == null)
            {
                UnityTipsHelper.ShowError($"请选择一个对象 右键创建");
                return;
            }

            var path = $"{UISetting.UIFrameworkPath}/UIEditor/PrefabTemplate/UI/{targetName}.prefab";
            Selection.activeObject = UIMenuItemHelper.CloneGameObjectByPath(path, activeObject.transform);
        }

        [MenuItem("GameObject/UI Frame/Text_NoRaycast", false, 100001)]
        private static void CreateText_NoRaycast()
        {
            CreateTarget("UIText_NoRaycast");
        }

        [MenuItem("GameObject/UI Frame/Text (TMP)", false, 100002)]
        private static void CreateTextTMP()
        {
            CreateTarget("UIText (TMP)");
        }

        [MenuItem("GameObject/UI Frame/Image_NoRaycast", false, 100003)]
        private static void CreateImage_NoRaycast()
        {
            CreateTarget("UIImage_NoRaycast");
        }

        [MenuItem("GameObject/UI Frame/Button", false, 100004)]
        private static void CreateButton()
        {
            CreateTarget("UIButton");
        }

        [MenuItem("GameObject/UI Frame/Button_NoText", false, 100005)]
        private static void CreateButton_NoText()
        {
            CreateTarget("UIButton_NoText");
        }
    }
}
#endif