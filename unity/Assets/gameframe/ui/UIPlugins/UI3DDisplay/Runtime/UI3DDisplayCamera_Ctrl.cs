using UnityEngine;

namespace Zeng.GameFrame.UIS
{
    [ExecuteInEditMode]
    public partial class UI3DDisplayCamera
    {
        public bool setPosition = true; // 是否设置摄像机位置
        public Vector3 targetOffset = Vector3.zero; // 相对于目标的位置偏移
        // 摄像机参数
        public float distance = 3.0f; // 摄像机到目标的距离（沿 -Z 轴）
        public float yaw = 0.0f;       // 偏航角（绕 Y 轴）
        public float pitch = 20.0f;    // 俯仰角（绕 X 轴）
        
        void Update()
        {
            if (setPosition)
            {
                UpdatePositionAngle();
            }
        }
        void UpdatePositionAngle()
        {
            // 如果目标物体未指定，默认使用原点
            Vector3 targetPosition = ShowObject != null     ?    ShowObject.transform.position     :    transform.parent != null ? transform.parent.position : Vector3.zero;
            targetPosition += targetOffset;

            // 根据 yaw 和 pitch 计算摄像机的旋转
            Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);

            // 计算摄像机位置：从目标位置沿着旋转的反方向移动 distance
            // 因为摄像机要“看向”目标，所以位置是目标位置 - 旋转方向的距离
            Vector3 direction = rotation * Vector3.forward; // 注意：这是局部空间中的方向
            Vector3 cameraPosition = targetPosition - direction * distance;

            // 设置摄像机位置
            transform.position = cameraPosition;

            // 让摄像机始终看向目标
            transform.LookAt(targetPosition);
        }
    }
}