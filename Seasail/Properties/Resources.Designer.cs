﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Seasail.Properties {
    using System;
    
    
    /// <summary>
    ///   一个强类型的资源类，用于查找本地化的字符串等。
    /// </summary>
    // 此类是由 StronglyTypedResourceBuilder
    // 类通过类似于 ResGen 或 Visual Studio 的工具自动生成的。
    // 若要添加或移除成员，请编辑 .ResX 文件，然后重新运行 ResGen
    // (以 /str 作为命令选项)，或重新生成 VS 项目。
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   返回此类使用的缓存的 ResourceManager 实例。
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Seasail.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   重写当前线程的 CurrentUICulture 属性
        ///   重写当前线程的 CurrentUICulture 属性。
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   查找类似 参数中的字符\&quot;{0}\&quot;不是 {1} 进制数的有效字符。 的本地化字符串。
        /// </summary>
        public static string AnyRadixConvert_CharacterIsNotValid {
            get {
                return ResourceManager.GetString("AnyRadixConvert_CharacterIsNotValid", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 进制转换溢出。 的本地化字符串。
        /// </summary>
        public static string AnyRadixConvert_Overflow {
            get {
                return ResourceManager.GetString("AnyRadixConvert_Overflow", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 缓存功能尚未初始化，未找到可用的 ICacheProvider 实现。 的本地化字符串。
        /// </summary>
        public static string Caching_CacheNotInitialized {
            get {
                return ResourceManager.GetString("Caching_CacheNotInitialized", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 标识为“{0}”的项重复定义 的本地化字符串。
        /// </summary>
        public static string ConfigFile_ItemKeyDefineRepeated {
            get {
                return ResourceManager.GetString("ConfigFile_ItemKeyDefineRepeated", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 名称为“{0}”的类型不存在 的本地化字符串。
        /// </summary>
        public static string ConfigFile_NameToTypeIsNull {
            get {
                return ResourceManager.GetString("ConfigFile_NameToTypeIsNull", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 请先初始化依赖注入服务，再使用OSharpContext.IocRegisterServices属性 的本地化字符串。
        /// </summary>
        public static string Context_BuildServicesFirst {
            get {
                return ResourceManager.GetString("Context_BuildServicesFirst", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 上下文初始化类型“{0}”不存在 的本地化字符串。
        /// </summary>
        public static string DbContextInitializerConfig_InitializerNotExists {
            get {
                return ResourceManager.GetString("DbContextInitializerConfig_InitializerNotExists", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 查询条件组中的操作类型错误，只能为And或者Or。 的本地化字符串。
        /// </summary>
        public static string Filter_GroupOperateError {
            get {
                return ResourceManager.GetString("Filter_GroupOperateError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 指定的属性“{0}”在类型“{1}”中不存在。 的本地化字符串。
        /// </summary>
        public static string Filter_RuleFieldInTypeNotFound {
            get {
                return ResourceManager.GetString("Filter_RuleFieldInTypeNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 返回数据处理错误，请重试操作。 的本地化字符串。
        /// </summary>
        public static string Http_Seciruty_Client_DecryptResponse_Failt {
            get {
                return ResourceManager.GetString("Http_Seciruty_Client_DecryptResponse_Failt", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 客户端对发送数据进行加密处理时发生异常。 的本地化字符串。
        /// </summary>
        public static string Http_Security_Client_EncryptRequest_Failt {
            get {
                return ResourceManager.GetString("Http_Security_Client_EncryptRequest_Failt", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 客户端对返回数据签名验证未通过。 的本地化字符串。
        /// </summary>
        public static string Http_Security_Client_VerifyResponse_Failt {
            get {
                return ResourceManager.GetString("Http_Security_Client_VerifyResponse_Failt", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 服务器对请求数据进行解密处理时发生异常。 的本地化字符串。
        /// </summary>
        public static string Http_Security_Host_DecryptRequest_Failt {
            get {
                return ResourceManager.GetString("Http_Security_Host_DecryptRequest_Failt", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 服务器对返回数据进行加密处理时发生异常。 的本地化字符串。
        /// </summary>
        public static string Http_Security_Host_EncryptResponse_Failt {
            get {
                return ResourceManager.GetString("Http_Security_Host_EncryptResponse_Failt", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 无法解析类型“{0}”的构造函数中类型为“{1}”的参数 的本地化字符串。
        /// </summary>
        public static string Ioc_CannotResolveService {
            get {
                return ResourceManager.GetString("Ioc_CannotResolveService", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 OSharp框架尚未初始化，请先初始化 的本地化字符串。
        /// </summary>
        public static string Ioc_FrameworkNotInitialized {
            get {
                return ResourceManager.GetString("Ioc_FrameworkNotInitialized", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 类型“{0}”的实现类型无法找到 的本地化字符串。
        /// </summary>
        public static string Ioc_ImplementationTypeNotFound {
            get {
                return ResourceManager.GetString("Ioc_ImplementationTypeNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 类型“{0}”中找不到合适参数的构造函数 的本地化字符串。
        /// </summary>
        public static string Ioc_NoConstructorMatch {
            get {
                return ResourceManager.GetString("Ioc_NoConstructorMatch", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 实现类型不能为“{0}”，因为该类型与注册为“{1}”的其他类型无法区分 的本地化字符串。
        /// </summary>
        public static string Ioc_TryAddIndistinguishableTypeToEnumerable {
            get {
                return ResourceManager.GetString("Ioc_TryAddIndistinguishableTypeToEnumerable", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 类型“{0}”不是仓储接口“IRepository&lt;,&gt;”的派生类。 的本地化字符串。
        /// </summary>
        public static string IocInitializerBase_TypeNotIRepositoryType {
            get {
                return ResourceManager.GetString("IocInitializerBase_TypeNotIRepositoryType", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 类型“{0}”不是操作单元“IUnitOfWork”的派生类。 的本地化字符串。
        /// </summary>
        public static string IocInitializerBase_TypeNotIUnitOfWorkType {
            get {
                return ResourceManager.GetString("IocInitializerBase_TypeNotIUnitOfWorkType", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 创建名称为“{0}”的日志实例时“{1}”返回空实例。 的本地化字符串。
        /// </summary>
        public static string Logging_CreateLogInstanceReturnNull {
            get {
                return ResourceManager.GetString("Logging_CreateLogInstanceReturnNull", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 MapperExtensions.Mapper不能为空，请先设置值 的本地化字符串。
        /// </summary>
        public static string Map_MapperIsNull {
            get {
                return ResourceManager.GetString("Map_MapperIsNull", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 当前Http上下文中不存在Request有效范围的Mef部件容器。 的本地化字符串。
        /// </summary>
        public static string Mef_HttpContextItems_NotFoundRequestContainer {
            get {
                return ResourceManager.GetString("Mef_HttpContextItems_NotFoundRequestContainer", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 指定对象中不存在名称为“{0}”的属性。 的本地化字符串。
        /// </summary>
        public static string ObjectExtensions_PropertyNameNotExistsInType {
            get {
                return ResourceManager.GetString("ObjectExtensions_PropertyNameNotExistsInType", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 指定名称“{0}”的属性类型不是“{1}”。 的本地化字符串。
        /// </summary>
        public static string ObjectExtensions_PropertyNameNotFixedType {
            get {
                return ResourceManager.GetString("ObjectExtensions_PropertyNameNotFixedType", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 参数“{0}”的值必须在“{1}”与“{2}”之间。 的本地化字符串。
        /// </summary>
        public static string ParameterCheck_Between {
            get {
                return ResourceManager.GetString("ParameterCheck_Between", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 参数“{0}”的值必须在“{1}”与“{2}”之间，且不能等于“{3}”。 的本地化字符串。
        /// </summary>
        public static string ParameterCheck_BetweenNotEqual {
            get {
                return ResourceManager.GetString("ParameterCheck_BetweenNotEqual", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 指定的目录路径“{0}”不存在。 的本地化字符串。
        /// </summary>
        public static string ParameterCheck_DirectoryNotExists {
            get {
                return ResourceManager.GetString("ParameterCheck_DirectoryNotExists", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 指定的文件路径“{0}”不存在。 的本地化字符串。
        /// </summary>
        public static string ParameterCheck_FileNotExists {
            get {
                return ResourceManager.GetString("ParameterCheck_FileNotExists", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 集合“{0}”中不能包含null的项 的本地化字符串。
        /// </summary>
        public static string ParameterCheck_NotContainsNull_Collection {
            get {
                return ResourceManager.GetString("ParameterCheck_NotContainsNull_Collection", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 参数“{0}”的值不能为Guid.Empty 的本地化字符串。
        /// </summary>
        public static string ParameterCheck_NotEmpty_Guid {
            get {
                return ResourceManager.GetString("ParameterCheck_NotEmpty_Guid", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 参数“{0}”的值必须大于“{1}”。 的本地化字符串。
        /// </summary>
        public static string ParameterCheck_NotGreaterThan {
            get {
                return ResourceManager.GetString("ParameterCheck_NotGreaterThan", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 参数“{0}”的值必须大于或等于“{1}”。 的本地化字符串。
        /// </summary>
        public static string ParameterCheck_NotGreaterThanOrEqual {
            get {
                return ResourceManager.GetString("ParameterCheck_NotGreaterThanOrEqual", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 参数“{0}”的值必须小于“{1}”。 的本地化字符串。
        /// </summary>
        public static string ParameterCheck_NotLessThan {
            get {
                return ResourceManager.GetString("ParameterCheck_NotLessThan", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 参数“{0}”的值必须小于或等于“{1}”。 的本地化字符串。
        /// </summary>
        public static string ParameterCheck_NotLessThanOrEqual {
            get {
                return ResourceManager.GetString("ParameterCheck_NotLessThanOrEqual", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 参数“{0}”不能为空引用。 的本地化字符串。
        /// </summary>
        public static string ParameterCheck_NotNull {
            get {
                return ResourceManager.GetString("ParameterCheck_NotNull", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 参数“{0}”不能为空引用或空集合。 的本地化字符串。
        /// </summary>
        public static string ParameterCheck_NotNullOrEmpty_Collection {
            get {
                return ResourceManager.GetString("ParameterCheck_NotNullOrEmpty_Collection", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 参数“{0}”不能为空引用或空字符串。 的本地化字符串。
        /// </summary>
        public static string ParameterCheck_NotNullOrEmpty_String {
            get {
                return ResourceManager.GetString("ParameterCheck_NotNullOrEmpty_String", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 类型“{0}”不是实体类型 的本地化字符串。
        /// </summary>
        public static string QueryCacheExtensions_TypeNotEntityType {
            get {
                return ResourceManager.GetString("QueryCacheExtensions_TypeNotEntityType", resourceCulture);
            }
        }
    }
}
