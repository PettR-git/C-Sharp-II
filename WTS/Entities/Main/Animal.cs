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
    public abstract class Animal : IAnimal, IComparable<Animal>
    {
        public Animal() 
        { 
        }

        //All animals have these datapoints
        public string Id { get; set; }
        public string Name { get; set; }
        public bool Domesticated { get; set; }
        public int Age { get; set; }
        public Enum Gender { get; set; }
        public AnimalType AnimalType { get; set; }
        public Species Species { get; set; }
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
