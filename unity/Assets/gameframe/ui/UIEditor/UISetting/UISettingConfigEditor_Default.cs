#if UNITY_EDITOR && !UI_ET

using Sirenix.OdinInspector;
using UnityEngine;

namespace Zeng.GameFrame.UIS.Editor
{
    public partial class UISettingConfigEditor
    {
       
        
        [Space(20)]
        [LabelText("UI根目录名称")]
        public  string UIProjectName = "UI";

        [LabelText("UI项目命名空间")]
        public  string UINamespace = "Games.UI"; //所有生成文件的命名空间

        // [LabelText("UI项目编辑器资源路径")]
        // public  string UIProjectEditorPath = "Assets/Editor/UI" ; //编辑器才会用到的资源

        [LabelText("UI项目资源路径")]
        public  string UIProjectResPath = "Assets/GameRes/UI" ; //玩家的预设/图片等资源存放的地方

        [LabelText("YIUI项目指定包资源")]
        public string UIProjectPackageResPath =  "Assets/GameRes/UI"; //指定包的资源存放的地方

        [LabelText("UI项目脚本路径")]
        public  string UIGenerationPath = "Assets/Scripts/UIGeneration"; //自动生成的代码

        [LabelText("UI项目自定义脚本路径")]
        public  string UICodeScriptsPath = "Assets/Scripts/UI"; //玩家可编写的核心代码部分

        [LabelText("UI框架所处位置路径")]
        public  string UIFrameworkPath = "Assets/gameframe/ui";

        [LabelText("UI项目代码模板路径")]
        public  string UITemplatePath = "Assets/gameframe/ui/UIEditor/CodeTemplate";

        public  string UIRootPrefabPath =  "Assets/gameframe/ui/UIEditor/UIRootPrefab/UIRoot.prefab";
        
        
        [Space(20)]
        [ReadOnly]
        public  string UIPanelName               = "Panel";
        [ReadOnly]
        public  string UIViewName                = "View";
        [ReadOnly]
        public  string UIPrefabs                = "Prefabs";
        [ReadOnly]
        public  string UIPrefabsCN              = "预制";
        [ReadOnly]
        public  string UISprites                = "Sprites";
        [ReadOnly]
        public  string UISpritesCN              = "精灵";
        [ReadOnly]
        public  string UIAtlas                  = "Atlas";
        [ReadOnly]
        public  string UIAtlasCN                = "图集";
        [ReadOnly]
        public  string UISource                 = "Source";
        [ReadOnly]
        public  string UISourceCN               = "源文件";
        [ReadOnly]
        public  string UIAtlasIgnore            = "AtlasIgnore"; //图集忽略文件夹名称
        [ReadOnly]
        public  string UISpritesAtlas1          = "Atlas1";      //图集1 不需要华丽的取名 每个包内的自定义图集就按顺序就好 当然你也可以自定义其他
        [ReadOnly]
        public  string UIAllViewParentName      = "AllViewParent";
        [ReadOnly]
        public  string UIAllPopupViewParentName = "AllPopupViewParent";
        [ShowInInspector]
        public  string UIUIPanelName  {  get { return UIProjectName + UIPanelName;} } 
        [ShowInInspector]
        public  string UIUIPanelSourceName  {  get { return UIProjectName + UIPanelName + UISource;} }   
        [ShowInInspector]
        public  string UIPanelSourceName  {  get { return UIPanelName + UISource;} }    
        [ShowInInspector]  
        public  string UIUIViewName   {  get { return UIProjectName + UIViewName;} }        
        [ShowInInspector]  
        public  string UIViewParentName    {  get { return UIViewName + UISetting.UIParentName;} }        
        [ShowInInspector]
        public  string UIUIViewParentName    { get { return UIProjectName + UIViewName + UISetting.UIParentName;} }    
    }
}
#endif