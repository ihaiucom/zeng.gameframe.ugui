#if UNITY_EDITOR
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Zeng.GameFrame.UIS
{
    public sealed partial class UIPanelSplitData
    {
        [OdinSerialize]
        [LabelText("生成通用界面枚举")]
        [ShowIf("ShowCreatePanelViewEnum")]
        internal bool CreatePanelViewEnum = true;

        internal bool ShowCreatePanelViewEnum()
        {
            return (AllCommonView.Count + AllCreateView.Count) >= 1;
        }

        private bool ShowCheckBtn()
        {
            if (Panel == null)
            {
                return false;
            }

            if (!Panel.name.EndsWith(UISetting.UISource))
            {
                return false;
            }

            return true;
        }

        [GUIColor(0, 1, 1)]
        [Button("检查拆分数据", 30)]
        [ShowIf("ShowCheckBtn")]
        private void AutoCheckBtn()
        {
            AutoCheck();
        }

        internal bool AutoCheck()
        {
            if (!ResetParent()) return false;

            if (!CheckPanelName()) return false;

            CheckViewName(AllCommonView);
            CheckViewName(AllCreateView);
            CheckViewName(AllPopupView);

            CheckViewParent(AllCommonView, AllViewParent);
            CheckViewParent(AllCreateView, AllViewParent);
            CheckViewParent(AllPopupView, AllPopupViewParent);

            var hashList = new HashSet<RectTransform>();
            CheckRepetition(ref hashList, AllCommonView);
            CheckRepetition(ref hashList, AllCreateView);
            CheckRepetition(ref hashList, AllPopupView);

            return true;
        }

        private bool ResetParent()
        {
            if (Panel == null)
            {
                Debug.LogError($"没有找到 Panel");
                return false;
            }

            if (AllViewParent == null || AllViewParent.name != UISetting.UIAllViewParentName)
            {
                AllViewParent = Panel.transform.FindChildByName(UISetting.UIAllViewParentName)
                                     ?.GetComponent<RectTransform>();
            }

            if (AllViewParent == null)
            {
                Debug.LogError($"没有找到 {Panel.name} {UISetting.UIAllViewParentName}  这是必须存在的组件 你可以不用 但是不能没有");
                return false;
            }


            if (AllPopupViewParent == null || AllPopupViewParent.name != UISetting.UIAllPopupViewParentName)
            {
                AllPopupViewParent = Panel.transform.FindChildByName(UISetting.UIAllPopupViewParentName)
                                          .GetComponent<RectTransform>();
            }

            if (AllPopupViewParent == null)
            {
                Debug.LogError($"没有找到 {Panel.name} {UISetting.UIAllPopupViewParentName}  这是必须存在的组件 你可以不用 但是不能没有");
                return false;
            }

            return true;
        }

        private bool CheckPanelName()
        {
            var qualifiedName = NameUtility.ToFirstUpper(Panel.name);
            if (Panel.name != qualifiedName)
            {
                Panel.name = qualifiedName;
            }

            if (Panel.name == UISetting.UIUIPanelSourceName)
            {
                Debug.LogError($"当前是默认名称 请手动修改名称 Xxx{UISetting.UIPanelSourceName}");
                return false;
            }

            // if (!Panel.name.EndsWith($"{UISetting.UIPanelSourceName}"))
            // {
            //     Debug.LogError($"{Panel.name} 命名必须以 {UISetting.UIPanelSourceName} 结尾 请勿随意修改");
            //     return false;
            // }

            return true;
        }

        //命名检查
        private void CheckViewName(List<RectTransform> list)
        {
            for (var i = list.Count - 1; i >= 0; i--)
            {
                var current = list[i];
                if (current == null)
                {
                    list.RemoveAt(i);
                    continue;
                }

                var qualifiedName = NameUtility.ToFirstUpper(current.name);
                if (current.name != qualifiedName)
                {
                    current.name = qualifiedName;
                }

                if (current.name == UISetting.UIUIViewParentName)
                {
                    Debug.LogError($"当前是默认名称 请手动修改名称 Xxx{UISetting.UIViewParentName}");
                    list.RemoveAt(i);
                    continue;
                }

                if (!current.name.EndsWith(UISetting.UIViewParentName))
                {
                    Debug.LogError($"{current.name} 命名必须以 {UISetting.UIViewParentName} 结尾 请勿随意修改");
                    list.RemoveAt(i);
                    continue;
                }

                var viewName = current.name.Replace(UISetting.UIParentName, "");
                var viewCde  = current.GetComponentInChildren<UIBindCDETable>();
                
                if (viewCde == null)
                {
                    //如果这个子物体被隐藏了
                    if (current.transform.childCount >= 1)
                    {
                        var firstChild = current.transform.GetChild(0);
                        viewCde = firstChild.GetComponent<UIBindCDETable>();
                    }
                }
                
                if (viewCde == null)
                {
                    Debug.LogError($" {current.name} 父物体下必须有View  但是未找到View 请使用 右键 UI/Create UIView 创建符合要求的结构");
                    list.RemoveAt(i);
                    continue;
                }

                viewCde.gameObject.name = viewName;
            }
        }

        //检查null / 父级
        private void CheckViewParent(List<RectTransform> list, RectTransform parent)
        {
            for (var i = list.Count - 1; i >= 0; i--)
            {
                if (list[i] == null)
                {
                    list.RemoveAt(i);
                    continue;
                }

                var current = list[i];

                var parentP = current.parent;
                if (parentP == null)
                {
                    list.RemoveAt(i);
                    continue;
                }

                if (parentP != parent)
                {
                    list.RemoveAt(i);

                    //因为只有2个父级 所以如果不是这个就会自动帮你移动到另外一个上面
                    //如果多了还是不要自动了
                    var currentParentName = parentP.name;
                    if (currentParentName == UISetting.UIAllViewParentName)
                    {
                        AllCreateView.Add(current);
                    }
                    else if (currentParentName == UISetting.UIAllPopupViewParentName)
                    {
                        AllPopupView.Add(current);
                    }
                }
            }
        }

        //检查重复
        private void CheckRepetition(ref HashSet<RectTransform> hashList, List<RectTransform> list)
        {
            for (var i = list.Count - 1; i >= 0; i--)
            {
                var current = list[i];
                if (hashList.Contains(current))
                {
                    list.RemoveAt(i);
                    Debug.LogError($"{Panel.name} / {current.name} 重复存在 已移除 请检查");
                }
                else
                {
                    hashList.Add(current);
                }
            }
        }

        [Button("重置拆分列表数据", 30)]
        public void Reset()
        {
            // AllCommonView.Clear();
            AllCreateView.Clear();
            AllPopupView.Clear();

            if (Panel != null)
            {
                AllViewParent = Panel.transform.FindChildByName(UISetting.UIAllViewParentName)
                    ?.GetComponent<RectTransform>();
                AllPopupViewParent = Panel.transform.FindChildByName(UISetting.UIAllPopupViewParentName)
                    ?.GetComponent<RectTransform>();
            }

            
            ResetViewList(AllCreateView, AllViewParent);
            ResetViewList(AllPopupView, AllPopupViewParent);
        }

        private void ResetViewList(List<RectTransform> list, RectTransform parent )
        {
            list.Clear();
            if(parent == null) return;
            var childCount = parent.childCount;
            for (var i = 0; i < childCount; i++)
            {
                var child = (RectTransform) parent.GetChild(i);
                if (child != null && child.name.EndsWith(UISetting.UIViewParentName))
                {
                    list.Add(child);
                }
            }
        }
    }
}
#endif