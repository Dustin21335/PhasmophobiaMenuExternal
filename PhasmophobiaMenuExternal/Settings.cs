using Newtonsoft.Json.Linq;
using PhasmophobiaMenuExternal.Language;
using PhasmophobiaMenuExternal.Menu.Tabs;
using System.Diagnostics;
using System.Reflection;

namespace PhasmophobiaMenuExternal
{
    public class Settings
    {
        // Menu Settings

        public static string Version = "v1.0.8";
        public static bool FirstLaunch = true;

        public class Config
        {
            public static string config = "PhasmophobiaMenuExternal.config.json";
            public static string defaultConf = "PhasmophobiaMenuExternal.default.config.json";

            public static void CreateConfigIfNotExists()
            {
                if (!HasConfig()) SaveConfig();
            }

            public static void SaveDefaultConfig() => SaveConfig(defaultConf);
            public static bool HasConfig() => config != null && File.Exists(config);
            public static void SaveConfig() => SaveConfig(config);

            public static void SaveConfig(string config)
            {
                JObject json = new JObject
                {
                    ["MenuSettings"] = new JObject
                    {
                        ["Language"] = Localization.language,
                        ["FirstLaunch"] = FirstLaunch.ToString(),
                    }
                };
                File.WriteAllText(config, json.ToString());
            }

            public static void LoadConfig()
            {
                CreateConfigIfNotExists();
                JObject json = JObject.Parse(File.ReadAllText(config));

                JObject menuSettings = json["MenuSettings"] as JObject;
                if (menuSettings != null)
                {
                    Localization.language = menuSettings.Value<string>("Language") ?? Localization.language;
                    SettingsTab.Language = Localization.language;
                    FirstLaunch = bool.TryParse(menuSettings.Value<string>("FirstLaunch"), out bool firstLaunch) && firstLaunch;
                }


            }

            public static void RegenerateConfig()
            {
                if (HasConfig()) File.Delete(config);
                File.Copy(defaultConf, config);
                LoadConfig();
            }

            public static void OpenConfig() => Process.Start("explorer.exe", config);
        }

        public class Changelog
        {
            public class Entry
            {
                public string Version { get; set; }
                public string Type { get; set; }
                public string Name { get; set; }
                public string Description { get; set; }
            }

            public static List<Entry> entries = new List<Entry>();

            public static void ReadChanges()
            {
                using Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(Assembly.GetExecutingAssembly().GetManifestResourceNames().FirstOrDefault(n => n.EndsWith("Changelog.json", StringComparison.OrdinalIgnoreCase)));
                JObject changelogs = (JObject)JObject.Parse(new StreamReader(stream).ReadToEnd())["Changelogs"];
                entries = changelogs.Properties().SelectMany(v =>
                {
                    JObject entriesObj = (JObject)v.Value["Entries"];
                    return entriesObj.Properties().SelectMany(t => ((JArray)t.Value).Select(e => new Entry
                    {
                        Version = v.Name,
                        Type = t.Name,
                        Name = e["Name"]?.ToString(),
                        Description = e["Description"]?.ToString()
                    }));
                }).ToList();
            }
        }
    }
}