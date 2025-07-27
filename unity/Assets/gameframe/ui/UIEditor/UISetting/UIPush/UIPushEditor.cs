#if UNITY_EDITOR

using System;
using System.Collections.Generic;
using System.IO;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using UnityEngine;

namespace Zeng.GameFrame.UIS.Editor
{
    public class UIPushEditor : ITreeMenu
    {
        private static UIPushEditor instance;
        public static UIPushEditor Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new UIPushEditor();
                }

                return instance;
            }
        }
        
        [GUIColor(0f, 1f, 1f)]
        [Button("UI自动生成绑定替代反射代码", 50)]
        [PropertyOrder(-9999)]
        public static void CreateUIBindProvider()
        {
            CreateUIBindProviderModule.Create();
        }
        
        
        [FolderPath]
        [LabelText("所有模块资源路径")]
        [ReadOnly]
        [ShowInInspector]
        private string m_AllPkgPath = UISetting.UIProjectResPath;
        
        
        
        internal const string MenuName = "发布";
        //所有模块
        private List<UIPublishPackageModule> m_AllUIPublishPackageModule = new List<UIPublishPackageModule>();

        [HideInInspector]
        public OdinMenuTree Tree { get; internal set; }
        [GUIColor(0.4f, 0.8f, 1)]
        [Button("刷新模块列表", 50)]
        [PropertyOrder(-98)]
        private void AddAllPkg()
        {
            m_AllUIPublishPackageModule.Clear();
            EditorHelper.CreateExistsDirectory(m_AllPkgPath);
            var folders = Array.Empty<string>();
            try
            {
                folders = Directory.GetDirectories(EditorHelper.GetProjPath(m_AllPkgPath));
            }
            catch (Exception e)
            {
                Debug.LogError($"获取所有模块错误 请检查 err={e.Message}{e.StackTrace}");
                return;
            }

            foreach (var folder in folders)
            {
                var pkgName   = Path.GetFileName(folder);
                var upperName = NameUtility.ToFirstUpper(pkgName);
                if (upperName != pkgName)
                {
                    Debug.LogError($"这是一个非法的模块[ {pkgName} ]请使用统一方法创建模块 或者满足指定要求");
                    continue;
                }

                var newUIPublishPackageModule = new UIPublishPackageModule(this, pkgName);

                //0 模块
                Tree.AddMenuItemAtPath(MenuName,
                    new OdinMenuItem(Tree, pkgName, newUIPublishPackageModule)).AddIcon(EditorIcons.Folder);
                //
                // //1 图集
                // Tree.AddAllAssetsAtPath($"{m_PublishName}/{pkgName}/{UISetting.UIAtlasCN}",
                //     $"{m_AllPkgPath}/{pkgName}/{UISetting.UIAtlas}", typeof(SpriteAtlas), true, false);

                //2 预制体
                Tree.AddAllAssetsAtPath($"{MenuName}/{pkgName}/{UISetting.UIPrefabsCN}",
                    $"{m_AllPkgPath}/{pkgName}/{UISetting.UIPrefabs}", typeof(UIBindCDETable), true, false);

                // //3 源文件
                // Tree.AddAllAssetsAtPath($"{m_PublishName}/{pkgName}/{UISetting.UISourceCN}",
                //     $"{m_AllPkgPath}/{pkgName}/{UISetting.UISource}", typeof(UIBindCDETable), true, false);
                //
                // //4 精灵
                // Tree.AddAllAssetImporterAtPath($"{m_PublishName}/{pkgName}/{UISetting.UISpritesCN}",
                //     $"{m_AllPkgPath}/{pkgName}/{UISetting.UISprites}", typeof(TextureImporter), true, false);

                m_AllUIPublishPackageModule.Add(newUIPublishPackageModule);
            }
        }

        private float time = 0;
        public void OnSelected()
        {
            if (m_AllUIPublishPackageModule.Count == 0 || DateTime.Now.Second - time > 60 * 5 )
            {
                time = DateTime.Now.Second;
                AddAllPkg();
            }
        }
        
        
        [GUIColor(0.4f, 0.8f, 1)]
        [Button("全部发布", 50)]
        [PropertyOrder(-99)]
        public void PublishAll()
        {
            if (!UIOperationHelper.CheckUIOperation()) return;
            
            AddAllPkg();

            foreach (var module in m_AllUIPublishPackageModule)
            {
                module.PublishCurrent(false); //不要默认重置所有图集设置 有的图集真的会有独立设置
            }

            UnityTipsHelper.CallBackOk("UI全部 发布完毕", UISettingEditor.CloseWindowRefresh);
        }
    }
}
#endif