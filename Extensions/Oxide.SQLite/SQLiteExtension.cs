﻿using Oxide.Core.Extensions;
using System;
using System.IO;
using System.Reflection;

namespace Oxide.Core.SQLite
{
    /// <summary>
    /// The extension class that represents this extension
    /// </summary>
    public class SQLiteExtension : Extension
    {
        internal static Assembly Assembly = Assembly.GetExecutingAssembly();
        internal static AssemblyName AssemblyName = Assembly.GetName();
        internal static VersionNumber AssemblyVersion = new VersionNumber(AssemblyName.Version.Major, AssemblyName.Version.Minor, AssemblyName.Version.Build);
        internal static string AssemblyAuthors = ((AssemblyCompanyAttribute)Attribute.GetCustomAttribute(Assembly, typeof(AssemblyCompanyAttribute), false)).Company;

        /// <summary>
        /// Gets whether this extension is a core extension
        /// </summary>
        public override bool IsCoreExtension => true;

        /// <summary>
        /// Gets the name of this extension
        /// </summary>
        public override string Name => "SQLite";

        /// <summary>
        /// Gets the author of this extension
        /// </summary>
        public override string Author => AssemblyAuthors;

        /// <summary>
        /// Gets the version of this extension
        /// </summary>
        public override VersionNumber Version => AssemblyVersion;

        private Libraries.SQLite sqlite;

        /// <summary>
        /// Initializes a new instance of the MySqlExtension class
        /// </summary>
        public SQLiteExtension(ExtensionManager manager) : base(manager)
        {
            if (Environment.OSVersion.Platform == PlatformID.Unix)
            {
                var configPath = Path.Combine(Interface.Oxide.ExtensionDirectory, "System.Data.SQLite.dll.config");
                if (File.Exists(configPath)) return;

                File.WriteAllText(configPath, $"<configuration>\n<dllmap dll=\"sqlite3\" target=\"x86/libsqlite3.so\" os=\"!windows,osx\" cpu=\"x86\" />\n" +
                    $"<dllmap dll=\"sqlite3\" target=\"x64/libsqlite3.so\" os=\"!windows,osx\" cpu=\"x86-64\" />\n</configuration>");
            }
        }

        /// <summary>
        /// Loads this extension
        /// </summary>
        public override void Load() => Manager.RegisterLibrary("SQLite", sqlite = new Libraries.SQLite());

        /// <summary>
        /// Loads plugin watchers used by this extension
        /// </summary>
        /// <param name="pluginDirectory"></param>
        public override void LoadPluginWatchers(string pluginDirectory)
        {
        }

        /// <summary>
        /// Called when all other extensions have been loaded
        /// </summary>
        public override void OnModLoad()
        {
        }
    }
}
