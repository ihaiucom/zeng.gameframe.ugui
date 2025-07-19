using UnityEngine;

namespace Zeng.GameFrame.UIs
{
    public class DontDestroyOnLoadSelf : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(this);
        }
    }
}