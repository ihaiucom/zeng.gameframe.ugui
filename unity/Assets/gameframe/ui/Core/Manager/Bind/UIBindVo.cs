using System;

namespace Zeng.GameFrame.UIS
{
    public struct UIBindVo
    {
        //基类
        public Type CodeType; //Window/SubPanel/View

        //当前继承类
        public Type BaseType; //他继承的类 就是 当前 + Base

        //当前类
        public Type CreatorType;

        //包名/模块名
        public string PkgName;

        //资源名
        public string ResName;
    }
}