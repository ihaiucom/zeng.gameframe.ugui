#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;


namespace Zeng.GameFrame.UIS.Editor
{
    public static class MenuItemUIComponent
    {
        [MenuItem("GameObject/UI Frame/Create UIComponent", false, 2)]
        static void CreateUIComponent()
        {
            var activeObject = Selection.activeObject as GameObject;
            if (activeObject == null)
            {
                UnityTipsHelper.ShowError($"请选择一个目标");
                return;
            }

            //Component
            var componentObject = new GameObject();
            var viewRect        = componentObject.GetOrAddComponent<RectTransform>();
            componentObject.GetOrAddComponent<CanvasRenderer>();
            var cdeTable = componentObject.GetOrAddComponent<UIBindCDETable>();
            cdeTable.UICodeType = EUICodeType.Component;
            viewRect.SetParent(activeObject.transform, false);


            componentObject.SetLayerRecursively(LayerMask.NameToLayer("UI"));
            Selection.activeObject = componentObject;
        }
    }
}
#endif