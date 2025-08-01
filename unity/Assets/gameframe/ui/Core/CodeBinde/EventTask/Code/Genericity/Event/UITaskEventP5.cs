using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;



namespace Zeng.GameFrame.UIS
{
    public class UITaskEventP5<P1, P2, P3, P4, P5> : UIEventBase, IUITaskEventInvoke<P1, P2, P3, P4, P5>
    {
        private LinkedList<UITaskEventHandleP5<P1, P2, P3, P4, P5>> m_UITaskEventDelegates;
        public  LinkedList<UITaskEventHandleP5<P1, P2, P3, P4, P5>> UITaskEventDelegates => m_UITaskEventDelegates;

        public UITaskEventP5()
        {
        }

        public UITaskEventP5(string name) : base(name)
        {
        }

        public async UniTask Invoke(P1 p1, P2 p2, P3 p3, P4 p4, P5 p5)
        {
            if (m_UITaskEventDelegates == null)
            {
                Logger.LogWarning($"{EventName} 未绑定任何事件");
                return;
            }

            var list = UITaskEventListPool.HandlerPool.Get();

            var itr = m_UITaskEventDelegates.First;
            while (itr != null)
            {
                var next  = itr.Next;
                var value = itr.Value;
                if (value.UITaskEventParamDelegate != null)
                    list.Add(value.UITaskEventParamDelegate.Invoke(p1, p2, p3, p4, p5));
                itr = next;
            }

            try
            {
                await UniTask.WhenAll(list);
            }
            catch (Exception e)
            {
                Logger.LogError(e);
            }
            finally
            {
                UITaskEventListPool.HandlerPool.Release(list);
            }
        }

        public override bool IsTaskEvent => true;

        public override bool Clear()
        {
            if (m_UITaskEventDelegates == null) return false;

            var first = m_UITaskEventDelegates.First;
            while (first != null)
            {
                PublicUITaskEventP5<P1, P2, P3, P4, P5>.HandlerPool.Release(first.Value);
                first = m_UITaskEventDelegates.First;
            }

            LinkedListPool<UITaskEventHandleP5<P1, P2, P3, P4, P5>>.Release(m_UITaskEventDelegates);
            m_UITaskEventDelegates = null;
            return true;
        }

        public UITaskEventHandleP5<P1, P2, P3, P4, P5> Add(UITaskEventDelegate<P1, P2, P3, P4, P5> callback)
        {
            m_UITaskEventDelegates ??= LinkedListPool<UITaskEventHandleP5<P1, P2, P3, P4, P5>>.Get();

            if (callback == null)
            {
                Logger.LogError($"{EventName} 添加了一个空回调");
            }

            var handler = PublicUITaskEventP5<P1, P2, P3, P4, P5>.HandlerPool.Get();
            var node    = m_UITaskEventDelegates.AddLast(handler);
            return handler.Init(m_UITaskEventDelegates, node, callback);
        }

        public bool Remove(UITaskEventHandleP5<P1, P2, P3, P4, P5> handle)
        {
            m_UITaskEventDelegates ??= LinkedListPool<UITaskEventHandleP5<P1, P2, P3, P4, P5>>.Get();

            if (handle == null)
            {
                Logger.LogError($"{EventName} UITaskEventParamHandle == null");
                return false;
            }

            return m_UITaskEventDelegates.Remove(handle);
        }
        #if UNITY_EDITOR
        public override string GetEventType()
        {
            return $"UITaskEventP5<{GetParamTypeString(0)},{GetParamTypeString(1)},{GetParamTypeString(2)},{GetParamTypeString(3)},{GetParamTypeString(4)}>";
        }

        public override string GetEventHandleType()
        {
            return $"UITaskEventHandleP5<{GetParamTypeString(0)},{GetParamTypeString(1)},{GetParamTypeString(2)},{GetParamTypeString(3)},{GetParamTypeString(4)}>";
        }
        #endif
    }
}