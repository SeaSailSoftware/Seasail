﻿using Seasail.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace Seasail.Reflection
{
    /// <summary>
    /// 不注册Com组件的方式加载Com组件
    /// </summary>
    public class ComLibraryLoader : Disposable
    {
        private delegate int DllGetClassObjectInvoker([MarshalAs(UnmanagedType.LPStruct)] Guid clsid,
            [MarshalAs(UnmanagedType.LPStruct)] Guid iid,
            [MarshalAs(UnmanagedType.IUnknown)] out object ppv);
        private static readonly Guid UnknownId = new Guid("00000000-0000-0000-C000-000000000046");
        private IntPtr _lib = IntPtr.Zero;
        private bool _preferURObjects = true;

        /// <summary>
        /// 从Com的dll文件创建Com对象
        /// </summary>
        /// <param name="dllPath">dll文件路径</param>
        /// <param name="clsid">Com组件的clsid</param>
        /// <param name="comFallback">无法加载时是否从注册系统中的Com组件加载</param>
        /// <returns></returns>
        public object CreateObjectFromPath(string dllPath, Guid clsid, bool comFallback)
        {
            return CreateObjectFromPath(dllPath, clsid, false, comFallback);
        }

        /// <summary>
        /// 从Com的dll文件创建Com对象
        /// </summary>
        /// <param name="dllPath">dll文件路径</param>
        /// <param name="clsid">Com组件的clsid</param>
        /// <param name="setSearchPath">是否设置搜索路径</param>
        /// <param name="comFallback">无法加载时是否从注册系统中的Com组件加载</param>
        /// <returns>创建的Com对象</returns>
        public object CreateObjectFromPath(string dllPath, Guid clsid, bool setSearchPath, bool comFallback)
        {
            if (File.Exists(dllPath) && (_preferURObjects || !comFallback))
            {
                if (setSearchPath)
                {
                    NativeMethods.SetDllDirectory(Path.GetDirectoryName(dllPath));
                }
                _lib = NativeMethods.LoadLibrary(dllPath);
                if (setSearchPath)
                {
                    NativeMethods.SetDllDirectory(null);
                }
                if (_lib != IntPtr.Zero)
                {
                    //we need to cache the handle so the COM object will work and we can clean up later
                    IntPtr ptr = NativeMethods.GetProcAddress(_lib, "DllGetClassObject");
                    if (ptr != IntPtr.Zero)
                    {
                        if (Marshal.GetDelegateForFunctionPointer(ptr, typeof(DllGetClassObjectInvoker)) is DllGetClassObjectInvoker invoker)
                        {
                            int hr = invoker(clsid, UnknownId, out object unknow);
                            if (hr >= 0)
                            {
                                if (unknow is IComClassFactory factory)
                                {
                                    factory.CreateInstance(null, UnknownId, out object createdObject);
                                    return createdObject;
                                }
                            }
                        }
                    }
                    else
                    {
                        throw new Win32Exception();
                    }
                }
            }
            if (!comFallback)
            {
                throw new Win32Exception();
            }

            Type type = Type.GetTypeFromCLSID(clsid);
            return Activator.CreateInstance(type);
        }

        protected override void Dispose(bool disposing)
        {
            if (!Disposed)
            {
                NativeMethods.FreeLibrary(_lib);
            }
            base.Dispose(disposing);
        }
    }
}
