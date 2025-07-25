﻿#if UNITY_EDITOR
using System;
using System.Reflection;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using Zeng.GameFrame.UIS.Editor;

namespace Zeng.GameFrame.UIS
{
    //Editor
    public sealed partial class UIBindCDETable
    {


        private void OnValueChangedEUICodeType()
        {
            if (name.EndsWith(UISetting.UIPanelName) || name.EndsWith(UISetting.UIPanelSourceName))
            {
                if (UICodeType != EUICodeType.Panel)
                {
                    Debug.LogWarning($"{name} 结尾{UISetting.UIPanelName} 必须设定为{UISetting.UIPanelName}类型");
                }

                UICodeType = EUICodeType.Panel;
            }
            else if (name.EndsWith(UISetting.UIViewName))
            {
                if (UICodeType != EUICodeType.View)
                {
                    Debug.LogWarning($"{name} 结尾{UISetting.UIViewName} 必须设定为{UISetting.UIViewName}类型");
                }

                UICodeType = EUICodeType.View;
            }
            else
            {
                if (UICodeType != EUICodeType.Component)
                {
                    Debug.LogWarning($"{name} 想设定为其他类型 请按照规则设定 请勿强行修改");
                }

                UICodeType = EUICodeType.Component;
            }
        }

        private void OnValueChangedEPanelLayer()
        {
            if (PanelLayer >= EPanelLayer.Cache)
            {
                Debug.LogError($" {name} 层级类型 选择错误 请重新选择");
                PanelLayer = EPanelLayer.Panel;
            }
        }
        
        
        
        
        


        private bool ShowAutoCheckBtn()
        {
            if (!UIOperationHelper.CheckUIOperation(false)) return false;
            return true;
        }

        [GUIColor(1, 1, 0)]
        [Button("自动检查所有", 30)]
        [PropertyOrder(-100)]
        [ShowIf("ShowAutoCheckBtn")]
        private void AutoCheckBtn()
        {
            AutoCheck();
        }

        [GUIColor(1, 1, 1)]
        [Button("重置子预制", 20)]
        [PropertyOrder(-100)]
        [ShowIf("ShowAutoCheckBtn")]
        private void RevertPrefabInstance()
        {
            UnityTipsHelper.CallBack("将会重置所有子CDE 还原到预制初始状态 \n(防止嵌套预制修改)",
                () =>
                {
                    UICreateModuleCode.RefreshChildCdeTable(this);
                    foreach (var cdeTable in AllChildCdeTable)
                    {
                        try
                        {
                            PrefabUtility.RevertPrefabInstance(cdeTable.gameObject, InteractionMode.AutomatedAction);
                        }
                        catch (Exception e)
                        {
                            Debug.LogError($"这个CDETable 不是预制体 请检查 {cdeTable.name}\n{e.Message}", cdeTable.gameObject);
                        }
                    }
                });
        }

        [GUIColor(0, 1, 1)]
        [Button("保存选中", 50)]
        [PropertyOrder(-100)]
        [HideIf("ShowCreateBtn")]
        private void SaveSelectSelf()
        {
            var stage = PrefabStageUtility.GetPrefabStage(gameObject);

            if (stage == null)
            {
                Debug.LogError($"未知错误 没有找到预制 {gameObject.name}");
                return;
            }

            var methodInfo = stage.GetType().GetMethod("Save", BindingFlags.Instance | BindingFlags.NonPublic);

            if (methodInfo != null)
            {
                bool result = (bool)methodInfo.Invoke(stage, null);
                if (!result)
                {
                    Debug.LogError("自动保存失败 注意请手动保存");
                }
            }
            else
            {
                Debug.LogError("Save方法不存在 自动保存失败 注意请手动保存");
            }

            var assetObj = AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(stage.assetPath);
            EditorGUIUtility.PingObject(assetObj);
            Selection.activeObject = assetObj;
        }

        internal bool AutoCheck()
        {
            if (!UIOperationHelper.CheckUIOperation(this)) return false;
            if (!UICreateModuleCode.InitVoName(this)) return false;
            OnValueChangedEUICodeType();
            OnValueChangedEPanelLayer();
            
            // PanelSplitData.Reset();
            if (UICodeType == EUICodeType.Panel && IsSplitData)
            {
                PanelSplitData.Panel = gameObject;
                if (!PanelSplitData.AutoCheck()) return false;
            }
            

            UICreateModuleCode.RefreshChildCdeTable(this);
            ComponentTable?.AutoCheck();
            DataTable?.AutoCheck();
            EventTable?.AutoCheck();
            return true;
        }

        private bool ShowCreateBtnByHierarchy()
        {
            if (string.IsNullOrEmpty(PkgName) || string.IsNullOrEmpty(ResName)) return false;
            if (ResName.Contains("Source")) return false;
            if (!UIOperationHelper.CheckUIOperation(this, false)) return false;
            return !PrefabUtility.IsPartOfPrefabAsset(this);
        }

        [GUIColor(0f, 0.5f, 1f)]
        [Button("生成", 50)]
        [ShowIf("ShowCreateBtnByHierarchy")]
        internal void CreateUICodeByHierarchy()
        {
            if (!ShowCreateBtnByHierarchy()) return;

            var prefabStage = UnityEditor.SceneManagement.PrefabStageUtility.GetCurrentPrefabStage();
            if (prefabStage == null)
            {
                Debug.LogError($"当前不在预制体编辑器模式下");
                return;
            }

            var path = prefabStage.assetPath;
            var root = prefabStage.prefabContentsRoot;
            PrefabUtility.SaveAsPrefabAsset(root, path, out var success);
            if (!success)
            {
                Debug.LogError("快捷保存失败 请检查");
                return;
            }

            prefabStage.ClearDirtiness();

            var cdeTable = AssetDatabase.LoadAssetAtPath<UIBindCDETable>(path);
            if (cdeTable == null) return;
            cdeTable.CreateUICode();

            AssetDatabase.OpenAsset(cdeTable);
        }

        private bool ShowCreateBtn()
        {
            if (IsSplitData) return false;
            if (!UIOperationHelper.CheckUIOperationAll(this, false)) return false;
            return true;
        }

        [GUIColor(0.7f, 0.4f, 0.8f)]
        [Button("生成", 50)]
        [ShowIf("ShowCreateBtn")]
        internal void CreateUICode()
        {
            if (!UIOperationHelper.CheckUIOperation(this)) return;

            CreateUICode(true, true);
        }

        private bool ShowPanelSourceSplit()
        {
            if (!UIOperationHelper.CheckUIOperationAll(this, false)) return false;
            return IsSplitData;
        }

        [GUIColor(0f, 0.4f, 0.8f)]
        [Button("源数据拆分", 50)]
        [ShowIf("ShowPanelSourceSplit")]
        internal void PanelSourceSplit()
        {
            if (!UIOperationHelper.CheckUIOperation(this)) return;

            if (IsSplitData)
            {
                if (AutoCheck())
                {
                    UIPanelSourceSplit.Do(this);
                }
            }
            else
            {
                UnityTipsHelper.ShowError($"{name} 当前数据不是源数据 无法进行拆分 请检查数据");
            }
        }

        internal void CreateUICode(bool refresh, bool tips)
        {
            UICreateModuleCode.Create(this, refresh, tips);
        }

        private void OnValidate()
        {
            ComponentTable ??= GetComponent<UIBindComponentTable>();
            DataTable      ??= GetComponent<UIBindDataTable>();
            EventTable     ??= GetComponent<UIBindEventTable>();
        }

        private void AddComponentTable()
        {
            if (!UIOperationHelper.CheckUIOperation()) return;

            ComponentTable = gameObject.GetOrAddComponent<UIBindComponentTable>();
        }

        private void AddDataTable()
        {
            if (!UIOperationHelper.CheckUIOperation()) return;

            DataTable = gameObject.GetOrAddComponent<UIBindDataTable>();
        }

        private void AddEventTable()
        {
            if (!UIOperationHelper.CheckUIOperation()) return;

            EventTable = gameObject.GetOrAddComponent<UIBindEventTable>();
        }
    }
}
#endif