#if UNITY_EDITOR && UNITY
using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Zeng.Utils.Editor
{
    
    /// <summary>
    /// 动画曲线编辑窗口
    /// </summary>
    public class AnimationCurveEditorWindow : EditorWindow
    {
        private AnimationCurve _originalCurve; // 原始曲线（用于重置）
        private AnimationCurve _editingCurve; // 正在编辑的曲线
        private UnityEngine.AnimationCurve _unityCurve; // 用于Unity编辑器的曲线
        private SerializedObject _serializedObject; // 序列化对象（用于应用修改）
        private Action<AnimationCurve> _onCurveUpdated; // 曲线更新回调

        /// <summary>
        /// 显示编辑窗口
        /// </summary>
        public static void ShowWindow(AnimationCurve curve, SerializedObject serializedObject,
            Action<AnimationCurve> onCurveUpdated)
        {
            var window = GetWindow<AnimationCurveEditorWindow>();
            window.titleContent = new GUIContent("动画曲线编辑器");
            
            
            if (window._unityCurve == null)
            {
                window._unityCurve = curve.ToUnityCurve();
            }
            else
            {
                curve.RefshToUnityCurve(window._unityCurve);
            }

            window._originalCurve = curve;
            window._editingCurve = new AnimationCurve(curve.Keys); // 创建副本进行编辑
            window._serializedObject = serializedObject;
            window._onCurveUpdated = onCurveUpdated;

            window.Show();
        }

        /// <summary>
        /// 绘制窗口GUI
        /// </summary>
        private void OnGUI()
        {
            if (_editingCurve == null)
            {
                Close();
                return;
            }

            // 1. 显示曲线编辑器
            EditorGUILayout.LabelField("曲线预览", EditorStyles.boldLabel);
            _unityCurve = EditorGUILayout.CurveField(_unityCurve, GUILayout.Height(200));
            _editingCurve.RefshFromUnityCurve(_unityCurve);


            // 2. 控制按钮
            EditorGUILayout.Space();
            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button("添加关键帧"))
            {
                AddNewKeyframe();
            }

            if (GUILayout.Button("重置"))
            {
                _editingCurve = new AnimationCurve(_originalCurve.Keys);
                _originalCurve.RefshToUnityCurve(_unityCurve);
            }

            if (GUILayout.Button("应用"))
            {
                ApplyChanges();
            }

            if (GUILayout.Button("关闭"))
            {
                Close();
            }

            EditorGUILayout.EndHorizontal();
            
            
            // 3. 显示关键帧列表
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("关键帧列表", EditorStyles.boldLabel);

            for (int i = 0; i < _editingCurve.Length; i++)
            {
                DrawKeyframeEditor(i, _editingCurve[i]);
            }
        }

        /// <summary>
        /// 绘制单个关键帧编辑器
        /// </summary>
        private void DrawKeyframeEditor(int index, Keyframe keyframe)
        {
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);

            EditorGUILayout.LabelField($"关键帧 {index}", EditorStyles.boldLabel);

            // 时间/值编辑
            float newTime = EditorGUILayout.FloatField("时间", keyframe.Time);
            float newValue = EditorGUILayout.FloatField("值", keyframe.Value);

            // 切线编辑
            EditorGUILayout.LabelField("切线");
            EditorGUI.indentLevel++;
            float newInTangent = EditorGUILayout.FloatField("进入", keyframe.InTangent);
            float newOutTangent = EditorGUILayout.FloatField("离开", keyframe.OutTangent);
            EditorGUI.indentLevel--;

            // 权重编辑
            EditorGUILayout.LabelField("权重");
            EditorGUI.indentLevel++;
            float newInWeight = EditorGUILayout.FloatField("进入", keyframe.InWeight);
            float newOutWeight = EditorGUILayout.FloatField("离开", keyframe.OutWeight);
            EditorGUI.indentLevel--;

            // 切线控制按钮
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("自动切线"))
            {
                _editingCurve.SmoothTangents(index, 0.5f);
                _editingCurve.RefshToUnityCurve(_unityCurve);
            }

            if (GUILayout.Button("平坦切线"))
            {
                var key = _editingCurve[index];
                _editingCurve[index] = new Keyframe(
                    key.Time, key.Value, 0, 0, key.InWeight, key.OutWeight);
                _editingCurve.RefshToUnityCurve(_unityCurve);
            }

            EditorGUILayout.EndHorizontal();

            // 删除按钮
            if (GUILayout.Button("删除关键帧"))
            {
                _editingCurve.RemoveKey(index);
                _editingCurve.RefshToUnityCurve(_unityCurve);
            }
            else if (newTime != keyframe.Time || newValue != keyframe.Value ||
                     newInTangent != keyframe.InTangent || newOutTangent != keyframe.OutTangent ||
                     newInWeight != keyframe.InWeight || newOutWeight != keyframe.OutWeight)
            {
                // 更新关键帧
                _editingCurve[index] = new Keyframe(
                    newTime, newValue,
                    newInTangent, newOutTangent,
                    newInWeight, newOutWeight);
                _editingCurve.RefshToUnityCurve(_unityCurve);
            }

            EditorGUILayout.EndVertical();
        }

        /// <summary>
        /// 添加新关键帧
        /// </summary>
        private void AddNewKeyframe()
        {
            // 添加一个默认关键帧（时间设为最后一个关键帧时间+1，或0如果没有关键帧）
            float newTime = _editingCurve.Length > 0 ? _editingCurve.Keys.Last().Time + 1 : 0;
            _editingCurve.AddKey(new Keyframe(newTime, 0));
            _editingCurve.RefshToUnityCurve(_unityCurve);
        }

        /// <summary>
        /// 应用修改
        /// </summary>
        private void ApplyChanges()
        {
            _editingCurve.RefshFromUnityCurve(_unityCurve);
            _originalCurve.RefshFromUnityCurve(_unityCurve);
            _onCurveUpdated?.Invoke(_editingCurve);
            _serializedObject?.ApplyModifiedProperties();
            Close();
        }
    }
}

#endif