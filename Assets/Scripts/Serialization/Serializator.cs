using System.IO;
using Newtonsoft.Json;

namespace Serialization
{
    public static class Serializator
    {
        public static void Serializate(object data, string path)
        {
            string serializedFeatures = JsonConvert.SerializeObject(data);
            File.WriteAllText(path,
                serializedFeatures);
        }

        public static T Deserializate<T>(string path)
        {
            if (!File.Exists(path))
            {
                return default;
            }

            string searilazideFeatures = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<T>(searilazideFeatures);
        }
    }
}