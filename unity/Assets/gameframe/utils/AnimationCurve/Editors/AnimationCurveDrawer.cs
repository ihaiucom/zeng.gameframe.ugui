#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

namespace Zeng.Utils.Editor
{
    /// <summary>
    /// AnimationCurve的属性绘制器
    /// </summary>
    [CustomPropertyDrawer(typeof(AnimationCurve))]
    public class AnimationCurveDrawer : PropertyDrawer
    {
        private const float CurveFieldHeight = 80f; // 曲线显示区域高度
        private const float ButtonWidth = 100f; // 按钮宽度
        private UnityEngine.AnimationCurve _unityCurve; // Unity曲线对象

        /// <summary>
        /// 在Inspector中绘制属性
        /// </summary>
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            // 获取实际的AnimationCurve对象
            var target = fieldInfo.GetValue(property.serializedObject.targetObject);
            AnimationCurve curve = target as AnimationCurve;

            if (curve == null)
            {
                EditorGUI.LabelField(position, "AnimationCurve is null");
                return;
            }

            // 绘制标签
            position = EditorGUI.PrefixLabel(position, label);

            // 计算曲线区域和按钮区域
            Rect curveRect = new Rect(position.x, position.y, position.width, CurveFieldHeight);
            Rect buttonRect = new Rect(position.x, position.y + CurveFieldHeight + 2, ButtonWidth,
                EditorGUIUtility.singleLineHeight);

            if (_unityCurve == null)
            {
                _unityCurve = curve.ToUnityCurve();
            }
            else
            {
                curve.RefshToUnityCurve(_unityCurve);
            }

            // 1. 显示当前曲线的预览
            _unityCurve = EditorGUI.CurveField(curveRect,_unityCurve);
            // curve.RefshFromUnityCurve(_unityCurve);

            // 2. 添加编辑按钮
            if (GUI.Button(buttonRect, "编辑曲线"))
            {
                // 打开曲线编辑窗口
                AnimationCurveEditorWindow.ShowWindow(curve, property.serializedObject,
                    updatedCurve =>
                    {
                        // 更新曲线数据
                        curve = updatedCurve;
                        fieldInfo.SetValue(property.serializedObject.targetObject, curve);
                        property.serializedObject.ApplyModifiedProperties();
                    });
            }
        }

        /// <summary>
        /// 获取属性高度
        /// </summary>
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return CurveFieldHeight + EditorGUIUtility.singleLineHeight + 4;
        }
    }

}
#endif