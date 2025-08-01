﻿using Cysharp.Threading.Tasks;

namespace Zeng.GameFrame.UIS
{
    public delegate UniTask UITaskEventDelegate();

    public delegate UniTask UITaskEventDelegate<in P1>(P1 p1);

    public delegate UniTask UITaskEventDelegate<in P1, in P2>(P1 p1, P2 p2);

    public delegate UniTask UITaskEventDelegate<in P1, in P2, in P3>(P1 p1, P2 p2, P3 p3);

    public delegate UniTask UITaskEventDelegate<in P1, in P2, in P3, in P4>(P1 p1, P2 p2, P3 p3, P4 p4);

    public delegate UniTask UITaskEventDelegate<in P1, in P2, in P3, in P4, in P5>(P1 p1, P2 p2, P3 p3, P4 p4, P5 p5);
}