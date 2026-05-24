using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace DigitalUniversity
{

    public static class Messages
    {
        private static Dictionary<string, Dictionary<string, string>> _data = new();
        private static bool _loaded = false;
        private static object _dataJson = new object();

        public static void Load(string jsonPath)
        {
            if (!File.Exists(jsonPath))
            {
                Console.WriteLine($"[Messages] Файл не знайдено: {jsonPath}");
                return;
            }

            var json = File.ReadAllText(jsonPath);

            if (string.IsNullOrWhiteSpace(json))
            {
                Console.WriteLine("[Messages] JSON файл порожній");
                return;
            }

            try
            {
                var root = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(json);
                var data = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, string>>>(json);
                _dataJson = data;

                if (root == null)
                    return;

                foreach (var section in root)
                {
                    var dict = new Dictionary<string, string>();

                    foreach (var kv in section.Value.EnumerateObject())
                        dict[kv.Name] = kv.Value.GetString() ?? "";

                    _data[section.Key] = dict;
                }

                _loaded = true;
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"[Messages] Помилка JSON: {ex.Message}");
            }
        }

        // Get(section, key, args) 
        public static string Get(string section, string key, params object[] args)
        {
            if (_loaded && _data.TryGetValue(section, out var dict) && dict.TryGetValue(key, out var template))
                return args.Length > 0 ? string.Format(template, args) : template;

            return $"[{section}.{key}]";
        }

        public static object GetJsonData()
        {
            return _dataJson;
        }

        public static void Print(string section, string key, params object[] args)
            => Console.WriteLine(Get(section, key, args));
    }
}