using UnityEngine;

namespace Zeng.GameFrame.UIS
{
    public class DontDestroyOnLoadSelf : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(this);
        }
    }
}