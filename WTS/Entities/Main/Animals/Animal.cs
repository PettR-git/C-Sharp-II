using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WTS.Entities.Enums;
using WTS.Entities.Main;
using WTS.Entities.Main.Enums;

namespace WTS.Entities
{
    [Serializable]
    public class Animal : IAnimal, IComparable<Animal>
    {
        public Animal() 
        { 
        }

        //All animals have these datapoints
        public string Id { get; set; }
        public string Name { get; set; }
        public bool Domesticated { get; set; }
        public int Age { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public Gender Gender { get; set; }

        [JsonConverter(typeof(StringEnumConverter))] 
        public AnimalType AnimalType { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public Species Species { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public EaterType EaterType { get; set; }

        //Default compare to Name, can be utilized with other sort methods too
        public int CompareTo(Animal animal) =>
            animal == null ? 1 : Name.CompareTo(animal.Name);

        public virtual string getExtraInfo()
        {
            string strOut = string.Empty;

            strOut = string.Format("{0,-15}{1, 10}", "Name: ", Name + ",a/an " + AnimalType);

            return strOut;
        }

        public override string ToString() 
        {
            string outStr = string.Format("{0,-20} {1,-30}", "ID:", Id) + "\n" +
                string.Format("{0,-20} {1,-30}", "Name:", Name) + "\n" +
                string.Format("{0,-20} {1,-30}", "Domesticated:", Domesticated) + "\n" +
                string.Format("{0,-20} {1,-30}", "Age:", Age) + "\n" +
                string.Format("{0,-20} {1,-30}", "Gender:", Gender);

            return outStr;
        }
    }
}
