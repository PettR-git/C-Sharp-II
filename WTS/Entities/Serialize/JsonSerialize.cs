using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using Newtonsoft.Json;
using Formatting = Newtonsoft.Json.Formatting;

namespace WTS.Entities.Serialize.SerializationTypes
{
    public static class JsonSerialize<T>
    {

        public static void SerializeList(string fileName, List<T> list, JsonSerializerSettings options = null)
        {
            try
            {
                var jsonStr = JsonConvert.SerializeObject(list, options);
                File.WriteAllText(fileName, jsonStr);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static List<T> DeserializeList(string fileName, JsonSerializerSettings options = null)
        {
            List<T> deserializedList = null;
            try
            {
                var jsonStr = File.ReadAllText(fileName);
                Console.WriteLine(jsonStr);
                deserializedList = JsonConvert.DeserializeObject<List<T>>(jsonStr, options);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return deserializedList ?? new List<T>();
        }

    }
}
