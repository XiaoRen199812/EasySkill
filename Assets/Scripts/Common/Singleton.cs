/*
日期：
功能：不继承Mono的C#单例（懒加载、线程不安全)
作者：小人
版本号：
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton
{
    private Singleton() { }

    private static Singleton _instance;

    public static Singleton Instance
    {
        get { if (_instance == null) { _instance = new Singleton(); } return _instance; }

    }

    public void Test()
    {

    }
}
