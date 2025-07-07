using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Reflection;

namespace PhasmophobiaMenuExternal.Language
{
    public class Language
    {
        public int Rank { get; set; }
        public string Name { get; private set; }
        public string Translator { get; private set; }

        private Dictionary<string, string> language;

        public Language(string name, string translator, Dictionary<string, string> language)
        {
            Name = name;
            Translator = translator;
            this.language = language;
        }

        public string Localize(string key) => language.ContainsKey(key) ? language[key] : key;
        public bool Has(string key) => language.ContainsKey(key);
        public int Count() => language.Count;
    }

    public class Localization
    {
        private static Language Language { get; set; }
        public static string language
        {
            get => Language.Name;
            set => Language = languages.ContainsKey(value) ? languages[value] : languages["English"];
        }
        private static Dictionary<string, Language> languages = new Dictionary<string, Language>();

        public static void Initialize()
        {
            LoadLanguage();
            language = "English";
        }

        private static void LoadLanguage()
        {
            Assembly.GetExecutingAssembly().GetManifestResourceNames().Where(x => x.Contains(".Languages.") && x.EndsWith(".json")).ToList().ForEach(x =>
            {
                string jsonStr = null;
                try
                {
                    using (StreamReader reader = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream(x))) jsonStr = reader.ReadToEnd();
                    JObject json = JObject.Parse(jsonStr);
                    Dictionary<string, string> localization = new Dictionary<string, string>();
                    if (!json.ContainsKey("LANGUAGE") || !json.ContainsKey("TRANSLATOR"))
                    {
                        Console.WriteLine($"Failed to load localization file => {x}");
                        return;
                    }
                    string language = json["LANGUAGE"].ToString();
                    string translator = json["TRANSLATOR"].ToString();
                    json.Properties().ToList().ForEach(p => localization.Add(p.Name, p.Value.ToString()));
                    languages.Add(language, new Language(language, translator, localization));
                    Console.WriteLine($"Loaded Language {language} by {translator}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Json Error: Loading language file {x} because {ex.Message}");
                    int lineNumber = 0;
                    using (StringReader reader = new StringReader(jsonStr))
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            lineNumber++;
                            if (line.Contains(ex.Message))
                            {
                                Console.WriteLine($"Error occurred at line {lineNumber} in file {x}");
                                break;
                            }
                        }
                    }
                    if (ex is JsonReaderException jsonEx) Console.WriteLine($"Json parsing error: Line {jsonEx.LineNumber}, Position {jsonEx.LinePosition} [{jsonEx.Path}]");
                }
            });
            int englishCount = languages["English"].Count();
            languages.Values.ToList().ForEach(x =>
            {
                if (x.Count() != englishCount)
                {
                    int languageCount = x.Count();
                    Console.WriteLine($"Language {x.Name} is missing {englishCount - languageCount} keys");
                    if ((languageCount / (double)englishCount) * 100 < 80.00)
                    {
                        languages.Remove(x.Name);
                        Console.WriteLine($"{x.Name} is too far behind. Unloading it.");
                    }
                }
            });
        }

        public static string[] GetLanguages()
        {
            languages.Values.ToList().ForEach(x => x.Rank = Language == x ? 1 : 999);
            return languages.Values.OrderBy(x => x.Rank).ThenBy(x => x.Name).Select(x => x.Name).ToArray();
        }

        public static string Localize(string key) => Language.Has(key) ? Language.Localize(key) : languages["English"].Localize(key);
        public static string Localize(string[] keys, bool newLine = false) => keys.Aggregate("", (current, key) => current + (newLine ? "\n" : " ") + Localize(key)).Substring(1);
    }
}