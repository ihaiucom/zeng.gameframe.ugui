using Sirenix.OdinInspector;
using Sirenix.Utilities;

namespace Zeng.GameFrame.UIS.Editor
{
    public class UICreateModuleEditor : GlobalConfig<UICreateModuleEditor>
    {
        
        [LabelText("新增模块名称")]
        public string Name;

        [GUIColor(0, 1, 0)]
        [Button("创建", 30)]
        private void Create()
        {
            if (!UIOperationHelper.CheckUIOperation()) return;

            Create(Name);
        }
        
        public static void Create(string createName)
        {
            if (string.IsNullOrEmpty(createName))
            {
                UnityTipsHelper.ShowError("请设定 名称");
                return;
            }

            createName = NameUtility.ToFirstUpper(createName);

            var basePath          = $"{UISetting.UIProjectResPath}/{createName}";
            var prefabsPath       = $"{basePath}/{UISetting.UIPrefabs}";
            var spritesPath       = $"{basePath}/{UISetting.UISprites}";
            var spritesAtlas1Path = $"{basePath}/{UISetting.UISprites}/{UISetting.UISpritesAtlas1}";
            var atlasIgnorePath   = $"{basePath}/{UISetting.UISprites}/{UISetting.UIAtlasIgnore}";
            var atlasPath         = $"{basePath}/{UISetting.UIAtlas}";
            var sourcePath        = $"{basePath}/{UISetting.UISource}";

            EditorHelper.CreateExistsDirectory(prefabsPath);
            EditorHelper.CreateExistsDirectory(spritesPath);
            EditorHelper.CreateExistsDirectory(spritesAtlas1Path);
            EditorHelper.CreateExistsDirectory(atlasIgnorePath);
            EditorHelper.CreateExistsDirectory(atlasPath);
            EditorHelper.CreateExistsDirectory(sourcePath);

            MenuItemUIPanel.CreateUIPanelByPath(sourcePath, createName);

            UISettingEditor.CloseWindowRefresh();
        }
    }
}