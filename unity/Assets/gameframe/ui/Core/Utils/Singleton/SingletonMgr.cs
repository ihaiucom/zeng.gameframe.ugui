﻿using System.Collections.Generic;
using UnityEngine;

namespace Zeng.GameFrame.UIS
{
    /// <summary>
    /// 所有继承了ISingleton的任意类型单例
    /// 主要目的为统计
    /// 不要轻易调用Dispose 一键释放所有单例
    /// </summary>
    public static class SingletonMgr
    {
        private static List<ISingleton> g_Singles = new List<ISingleton>();

        public static bool Disposing { get; private set; } = true;

        public static int  Count      => g_Singles.Count;
        
        public static bool IsQuitting { get; private set; }

        static SingletonMgr()
        {
            Application.quitting -= OnQuitting;
            Application.quitting += OnQuitting;
        }

        private static void OnQuitting()
        {
            //Debug.LogError("OnQuitting");
            IsQuitting = true;
        }

        //只能由一个地方 真的需要彻底清除时调用
        //游戏退出时不必调用 因为游戏都退了 所有都会被清空
        //需要调用的时机 如不退出游戏 但是要重置全部的情况下使用
        //一般情况是不需要使用的
        public static void Dispose()
        {
            if (IsQuitting)
            {
                Debug.Log("正在退出游戏 不必清理");
                return;
            }
            
            Disposing = true;

            Debug.Log($"SingletonMgr.清除所有单例");
            var singles = g_Singles.ToArray();
            for (int i = 0; i < singles.Length; i++)
            {
                var inst = singles[i];

                if (inst == null || inst.Disposed) continue;

                inst.Dispose();
                singles[i] = null;
            }

            g_Singles.Clear();
        }

        //初始化
        public static void Initialize()
        {
            if (IsQuitting)
            {
                Debug.Log("正在退出游戏 禁止初始化");
                return;
            }

            //Debug.Log($"SingletonMgr.初始化");
            Disposing = false;
        }

        internal static void Add(ISingleton single)
        {
            //Debug.Log($"添加{single.GetType().Name}");
            g_Singles.Add(single);
        }

        internal static void Remove(ISingleton single)
        {
            //Debug.Log($"移除{single.GetType().Name}");
            g_Singles.Remove(single);
        }
    }
}