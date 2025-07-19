using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Zeng.GameFrame.UIs
{
    [Serializable]
    [LabelText("UI CDE总表")]
    [AddComponentMenu("UI Code/★★★★★UI CDE Table 总表★★★★★")]
    public partial class UICode : MonoBehaviour
    {
        
        [LabelText("UI包名")]
        [ReadOnly]
        public string PkgName;

        [LabelText("UI资源名")]
        [ReadOnly]
        public string ResName;
        
        
    }
}