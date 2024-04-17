using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace WTS.Entities.Serialize.SerializationTypes
{
    public static class BinarySerialize<T>
    {
        public static void SerializeList(string fileName, List<T> list)
        {
            FileStream fileStream = null;
            try
            {
                using (fileStream = new FileStream(fileName, FileMode.Create))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    bf.Serialize(fileStream, list);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (fileStream != null)
                    fileStream.Close();
            }
        }

        public static List<T> DeserializeList(string fileName)
        {
            FileStream fileStream = null;
            List<T> objs = new List<T>();

            try
            {
                using (fileStream = new FileStream(fileName, FileMode.Open))
                {
                    BinaryFormatter b = new BinaryFormatter();
                    objs = (List<T>)b.Deserialize(fileStream);
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                if (fileStream != null)
                    fileStream.Close();
            }

            return objs;
        }
    }
}
