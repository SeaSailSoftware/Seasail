﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Seasail.Data
{
    /// <summary>
    /// 定义一个指定类型的单例，该实例的生命周期将跟随整个应用程序。
    /// </summary>
    /// <typeparam name="T">要创建单例的类型。</typeparam>
    public class Singleton<T> : Singleton
    {
        private static T _instance;

        private static object obj = new object();
        /// <summary>
        /// 获取指定类型的单例实例
        /// </summary>
        public static T Instance
        {
            get
            {
                lock (obj)
                {
                    if (_instance == null)
                    {
                        var contrustor = typeof(T).GetConstructor(new Type[0]);
                        _instance = (T)contrustor.Invoke(new object[0]);
                    }
                    return _instance;
                }
            }
        }
    }


    /// <summary>
    /// 提供一个字典容器，按类型装载所有<see cref="Singleton{T}"/>的单例实例
    /// </summary>
    public class Singleton
    {
        static Singleton()
        {
            if (AllSingletons == null)
            {
                AllSingletons = new Dictionary<Type, object>();
            }
        }

        /// <summary>
        /// 获取 单例对象字典
        /// </summary>
        public static IDictionary<Type, object> AllSingletons { get; }
    }
}
