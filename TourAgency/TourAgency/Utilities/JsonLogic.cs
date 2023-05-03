using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace TourAgency
{
    public static class JsonLogic
    {
        private static JsonSerializerOptions options = new JsonSerializerOptions()
        {
            WriteIndented = true,
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        };

        public static bool SaveToJson<T>(List<T> list, string path)
        {
            try
            {
                string jsonString = JsonSerializer.Serialize(list, options);

                File.WriteAllText(path, jsonString);

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static bool ReadFromJson<T>(string path, out List<T> list)
        {
            list = new();

            try
            {
                string jsonString = File.ReadAllText(path);

                if (jsonString != "")
                    list = JsonSerializer.Deserialize<List<T>>(jsonString, options)!;

                return true;
            }
            catch (IOException ex)
            {
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
