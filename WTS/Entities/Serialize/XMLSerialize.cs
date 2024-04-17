using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;
using System.Xml.Serialization;

namespace WTS.Entities.Serialize.SerializationTypes
{
    public static class XMLSerialize<T>
    {
        public static void SerializeList(string fileName, List<T> list)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<T>));

                using (StreamWriter writer = new StreamWriter(fileName))
                {
                    serializer.Serialize(writer, list);
                    writer.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static List<T> DeserializeList(string fileName)
        {
            List<T> deserializedList = null;
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<T>));

                using (FileStream reader = new FileStream(fileName, FileMode.Open))
                {
                    deserializedList = (List<T>)serializer.Deserialize(reader);
                    reader.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return deserializedList ?? new List<T>();
        }

    }
}
