using UnityEngine;

namespace Zeng.GameFrame.UIS
{
    public static class ApplicationExt
    {
        public static bool IsQuitting { get; private set; }
        static ApplicationExt()
        {
            Application.quitting -= OnQuitting;
            Application.quitting += OnQuitting;
        }

        private static void OnQuitting()
        {
            //Debug.LogError("OnQuitting");
            IsQuitting = true;
        }
    }
}