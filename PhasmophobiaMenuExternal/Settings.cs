using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PhasmophobiaMenuExternal.Cheats.Core;
using PhasmophobiaMenuExternal.Language;
using System.Diagnostics;
using System.Numerics;
using System.Reflection;

namespace PhasmophobiaMenuExternal
{
    public class Settings
    {
        // Menu Settings

        public static string Version = "v1.0.0";
        public static bool FirstLaunch = true;

        // Hack Settings

        public static bool GhostInfo = false;
        public static bool LevelInfo = false;
        public static bool PlayerInfo = false;
        public static bool PlayerInfoSeperateWindows = false;
        public static bool Crosshair = false;

        public static int CrosshairSize = 10;
        public static int CrosshairThickness = 1;

        // Colors

        public static Vector4 CrosshairColor = new Vector4(0, 1, 0, 1);

        //

        public class Config
        {
            private static Dictionary<Type, object> CheatInstances = new Dictionary<Type, object>();
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
                        ["FirstLaunch"] = FirstLaunch.ToString()
                    },
                    ["HackSettings"] = new JObject
                    {
                        ["GhostInfo"] = GhostInfo.ToString(),
                        ["LevelInfo"] = LevelInfo.ToString(),
                        ["PlayerInfo"] = PlayerInfo.ToString(),
                        ["PlayerInfoSeperateWindows"] = PlayerInfoSeperateWindows.ToString(),
                        ["Crosshair"] = Crosshair.ToString(),
                        ["CrosshairSize"] = CrosshairSize.ToString(),
                        ["CrosshairThickness"] = CrosshairThickness.ToString(),
                        ["Cheats"] = new JObject(Cheat.instances.Select(c =>
                        {
                            JObject CheatObj = new JObject
                            {
                                ["Enabled"] = c.Enabled.ToString(),
                            };
                            if (c is TaskCheat taskCheat) CheatObj["Delay"] = taskCheat.Delay.ToString();
                            if (c.Value != -1) CheatObj["Value"] = c.Value.ToString();
                            return new JProperty(c.GetType().Name, CheatObj);
                        }))
                    },
                    ["Colors"] = new JObject
                    {
                        ["CrosshairColor"] = JsonConvert.SerializeObject(CrosshairColor)
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
                    Program.Language = Localization.language;
                    FirstLaunch = bool.TryParse(menuSettings.Value<string>("FirstLaunch"), out var firstLaunch) && firstLaunch;
                }

                JObject hackSettings = json["HackSettings"] as JObject;
                if (hackSettings != null)
                {
                    GhostInfo = bool.TryParse(hackSettings.Value<string>("GhostInfo"), out bool ghostInfo) && ghostInfo;
                    LevelInfo = bool.TryParse(hackSettings.Value<string>("LevelInfo"), out bool levelInfo) && levelInfo;
                    PlayerInfo = bool.TryParse(hackSettings.Value<string>("PlayerInfo"), out bool playerInfo) && playerInfo;
                    Crosshair = bool.TryParse(hackSettings.Value<string>("Crosshair"), out bool crosshair) && crosshair;
                    PlayerInfoSeperateWindows = bool.TryParse(hackSettings.Value<string>("PlayerInfoSeperateWindows"), out bool olayerInfoSeperateWindows) && olayerInfoSeperateWindows;

                    CrosshairSize = int.TryParse(hackSettings.Value<string>("CrosshairSize"), out int size) ? size : CrosshairSize;
                    CrosshairThickness = int.TryParse(hackSettings.Value<string>("CrosshairThickness"), out int thickness) ? thickness : CrosshairThickness;

                    JObject cheatsSettings = hackSettings["Cheats"] as JObject;
                    if (cheatsSettings != null)
                    {
                        foreach (Cheat cheat in Cheat.instances)
                        {
                            if (cheatsSettings[cheat.GetType().Name] is JObject cheatObj)
                            {
                                if (bool.TryParse(cheatObj.Value<string>("Enabled"), out bool enabled)) cheat.Enabled = enabled;
                                if (cheat is TaskCheat taskCheat && int.TryParse(cheatObj.Value<string>("Delay"), out int delay)) taskCheat.Delay = delay;
                                if (cheatObj.ContainsKey("Value") && float.TryParse(cheatObj.Value<string>("Value"), out float value)) cheat.Value = value;
                            }
                        }
                    }
                }

                JObject colors = json["Colors"] as JObject;
                if (colors != null && colors.TryGetValue("CrosshairColor", out JToken colorToken))
                {
                    CrosshairColor = JsonConvert.DeserializeObject<Vector4>(colorToken.ToString());
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