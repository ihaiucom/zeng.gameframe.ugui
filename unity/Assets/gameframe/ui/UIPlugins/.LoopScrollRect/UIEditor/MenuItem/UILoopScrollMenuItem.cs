#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Zeng.GameFrame.UIS.Editor
{
    internal static class YIUILoopScrollMenuItem
    {
        [MenuItem("GameObject/UI Frame/LoopScroll/Horizontal", false, 10001)]
        private static void CreateLoopScrollHorizontal()
        {
            CreateLoopScroll("LoopScrollHorizontal");
        }

        [MenuItem("GameObject/UI Frame/LoopScroll/Horizontal Reverse", false, 10002)]
        private static void CreateLoopScrollHorizontalReverse()
        {
            CreateLoopScroll("LoopScrollHorizontalReverse");
        }

        [MenuItem("GameObject/UI Frame/LoopScroll/Horizontal Group", false, 10003)]
        private static void CreateLoopScrollHorizontalGroup()
        {
            CreateLoopScroll("LoopScrollHorizontalGroup");
        }

        [MenuItem("GameObject/UI Frame/LoopScroll/Vertical", false, 10011)]
        private static void CreateLoopScrollVertical()
        {
            CreateLoopScroll("LoopScrollVertical");
        }

        [MenuItem("GameObject/UI Frame/LoopScroll/Vertical Reverse", false, 10012)]
        private static void CreateLoopScrollVerticalReverse()
        {
            CreateLoopScroll("LoopScrollVerticalReverse");
        }

        [MenuItem("GameObject/UI Frame/LoopScroll/Vertical Group", false, 10013)]
        private static void CreateLoopScrollVerticalGroup()
        {
            CreateLoopScroll("LoopScrollVerticalGroup");
        }

        private static void CreateLoopScroll(string name)
        {
            var activeObject = Selection.activeObject as GameObject;
            if (activeObject == null)
            {
                UnityTipsHelper.ShowError($"请选择一个对象 右键创建");
                return;
            }

            var path = $"{UISetting.UIFrameworkPath}/UIPlugins/LoopScrollRect/YIUIEditor/TemplatePrefabs/{name}.prefab";

            Selection.activeObject = UIMenuItemHelper.CloneGameObjectByPath(path, activeObject.transform);
        }
    }
}
#endif