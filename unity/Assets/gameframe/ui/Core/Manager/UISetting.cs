using Sirenix.OdinInspector;

namespace Zeng.GameFrame.UIS
{
    //一个项目不可能随时换项目路径 这里就是强制设置的只可读 初始化项目的时候手动改这个一次就可以了
    /// <summary>
    /// UI静态助手
    /// </summary>
    public static class UISetting
    {
        [LabelText("UI根目录名称")]
        public const string UIProjectName = "UI";

        [LabelText("UI项目命名空间")]
        public const string UINamespace = "Games.UI"; //所有生成文件的命名空间

        [LabelText("UI项目编辑器资源路径")]
        public const string UIProjectEditorPath = "Assets/Editor/" + UIProjectName; //编辑器才会用到的资源

        [LabelText("UI项目资源路径")]
        public const string UIProjectResPath = "Assets/GameRes/" + UIProjectName; //玩家的预设/图片等资源存放的地方

        [LabelText("UI项目脚本路径")]
        public const string UIGenerationPath = "Assets/Scripts/UIGeneration"; //自动生成的代码

        [LabelText("UI项目自定义脚本路径")]
        public const string UICodeScriptsPath = "Assets/Scripts/" + UIProjectName; //玩家可编写的核心代码部分

        [LabelText("UI框架所处位置路径")]
        public const string UIFrameworkPath = "Assets/gameframe/ui";

        [LabelText("UI项目代码模板路径")]
        public const string UITemplatePath = UIFrameworkPath + "/UIEditor/CodeTemplate";

        public const string UIRootPrefabPath =
            UIFrameworkPath + "/UIEditor/UIRootPrefab/" + UIRoot.UIRootName + ".prefab";

        public const string UIBaseName                = nameof(UIBase);
        public const string UIBasePanelName           = nameof(UIPanel);
        public const string UIBaseViewName            = nameof(UIView);
        public const string UIBaseComponentName       = nameof(UIComponent);
        public const string UIPanelName               = "Panel";
        public const string UIViewName                = "View";
        public const string UIParentName             = "Parent";
        public const string UIPrefabs                = "Prefabs";
        public const string UIPrefabsCN              = "预制";
        public const string UISprites                = "Sprites";
        public const string UISpritesCN              = "精灵";
        public const string UIAtlas                  = "Atlas";
        public const string UIAtlasCN                = "图集";
        public const string UISource                 = "Source";
        public const string UISourceCN               = "源文件";
        public const string UIAtlasIgnore            = "AtlasIgnore"; //图集忽略文件夹名称
        public const string UISpritesAtlas1          = "Atlas1";      //图集1 不需要华丽的取名 每个包内的自定义图集就按顺序就好 当然你也可以自定义其他
        public const string UIAllViewParentName      = "AllViewParent";
        public const string UIAllPopupViewParentName = "AllPopupViewParent";
        public const string UIUIPanelName          = UIProjectName + UIPanelName;
        public const string UIUIPanelSourceName    = UIProjectName + UIPanelName + UISource;
        public const string UIPanelSourceName        = UIPanelName + UISource;
        public const string UIUIViewName           = UIProjectName + UIViewName;
        public const string UIViewParentName         = UIViewName + UIParentName;
        public const string UIUIViewParentName     = UIProjectName + UIViewName + UIParentName;
        
        //低品质 将会影响动画等逻辑 也可以根据这个参数自定义一些区别
        public static bool IsLowQuality = false;
        
        [BoxGroup("基础设置", CenterLabel = true)]
        [LabelText("使用老的CDEInspector显示模式")]
        public static bool DisplayOldCDEInspector = false;
        
        
    }
}