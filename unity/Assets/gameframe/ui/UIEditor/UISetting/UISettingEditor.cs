#if UNITY_EDITOR
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using UnityEditor;

namespace Zeng.GameFrame.UIS.Editor
{
    public class UISettingEditor  : OdinMenuEditorWindow
    {
        [MenuItem("Tools/UI工具")]
        private static void OpenWindow()
        {
            var window = GetWindow<UISettingEditor>();
            window.Show();
        } 
        
        private static void CloseWindow()
        {
            GetWindow<UISettingEditor>().Close();
        }
        
        

        //关闭后刷新资源
        public static void CloseWindowRefresh()
        {
            CloseWindow();
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        private OdinMenuTree           m_OdinMenuTree;
        protected override OdinMenuTree BuildMenuTree()
        {
            OdinMenuTree tree = new OdinMenuTree(supportsMultiSelect: true)
            {
                { "UI设置", UISettingConfig.Instance, EditorIcons.SettingsCog },
                { "创建模块", UICreateModuleEditor.Instance, EditorIcons.Folder },
                { "多语言", UII2LocalizationEditor.Instance, EditorIcons.Globe },
            };
            return tree;
        }
    }

}
#endif