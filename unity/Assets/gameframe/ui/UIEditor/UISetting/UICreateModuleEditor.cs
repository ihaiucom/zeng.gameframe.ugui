#if UNITY_EDITOR

using Sirenix.OdinInspector;

namespace Zeng.GameFrame.UIS.Editor
{
    public class UICreateModuleEditor
    {
        private static UICreateModuleEditor instance;
        public static UICreateModuleEditor Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new UICreateModuleEditor();
                }

                return instance;
            }
        }
        
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

            var basePath          = $"{UISettingConfigEditor.Instance.UIProjectPackageResPath}/{createName}";
            var prefabsPath       = $"{basePath}/{UISettingConfigEditor.Instance.UIPrefabs}";
            var spritesPath       = $"{basePath}/{UISettingConfigEditor.Instance.UISprites}";
            var spritesAtlas1Path = $"{basePath}/{UISettingConfigEditor.Instance.UISprites}/{UISettingConfigEditor.Instance.UISpritesAtlas1}";
            var atlasIgnorePath   = $"{basePath}/{UISettingConfigEditor.Instance.UISprites}/{UISettingConfigEditor.Instance.UIAtlasIgnore}";
            var atlasPath         = $"{basePath}/{UISettingConfigEditor.Instance.UIAtlas}";
            var sourcePath        = $"{basePath}/{UISettingConfigEditor.Instance.UISource}";

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
#endif