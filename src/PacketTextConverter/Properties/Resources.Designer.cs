﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.8784
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PacketTextConverter.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "2.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("PacketTextConverter.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to addfont; Arial;9;Bold;Group;
        ///addfont; Microsoft Sans Serif;8;Regular;Names;
        ///addfont; Arial;14;Bold;Slogan;
        ///
        ///scene;20;
        ///addstring;Пакетный перекодировщик текста;Group;
        ///addstring;v 0.0.2b;Group;
        ///addstring;Распространяется бесплатно;Names;
        ///addstring;                      ;Names;
        ///addstring;Developers;Group;
        ///addstring;[Wildsoft/ChaosSoft];Group;
        ///addstring;                      ;Names;
        ///addstring;Idea by;Group;
        ///addstring;PunkArr[];Names;
        ///addstring;Code by;Group;
        ///addstring;D. Larin;Names;
        ///addstring;C [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string about {
            get {
                return ResourceManager.GetString("about", resourceCulture);
            }
        }
        
        internal static System.Drawing.Bitmap logo {
            get {
                object obj = ResourceManager.GetObject("logo", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Пакетный перекодировщик текстовых файлов v 0.0.2b
        ///
        ///Изначально писалось для товарищей в 2016 г., поскольку почему-то все пакетные конвертеры/перекодировщики текстовых файлов были исключительно платными, во всяком случае на тот момент и под Windows.
        ///
        ///Основные возможности:
        ///
        ///— Пакетная перекодировка текстовых файлов
        ///— Создание полного дерева каталогов для перекодированных файлов
        ///— Доступен расширенный список кодировок (все кодировки, поддерживаемые .NET Framework)
        ///
        ///Изменения в версии v 0.0.2b:
        ///
        ///- До [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string readme {
            get {
                return ResourceManager.GetString("readme", resourceCulture);
            }
        }
    }
}
