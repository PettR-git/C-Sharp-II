using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WTS.Entities.Main.Animals.Amphibians;
using WTS.Entities.Main.Enums;

namespace WTS.Entities.Main.Animals.Amphibians.SpecificAmphibians
{
    [Serializable]
    public class Salamander : Amphibian
    {
        private int weight;
        private int wellness;
        public Salamander()
        {
            Species = Species.Salamander;
            EaterType = EaterType.Carnivore;
        }

        [JsonProperty]
        public int Weight
        {
            get { return weight; }
            set { weight = value; }
        }

        [JsonProperty]
        public int Wellness
        {
            get { return wellness; }
            set { wellness = value; }
        }

        //Extra info to picture text
        public override string getExtraInfo()
        {
            string strOut = string.Empty;

            strOut = string.Format("{0,-20} {1,-30}", "Animal:", Species.ToString()) + "\n" + base.getExtraInfo() + string.Format("{0,-20} {1,-30}", "Weight(g):", weight) + "\n" +
                string.Format("{0,-20} {1,-30}", "Wellness(1-10):", wellness) + "\n" + string.Format("{0,-20} {1,-30}", "Food type:", EaterType);

            return strOut;
        }

    }
}
