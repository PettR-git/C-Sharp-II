using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WTS.Entities.Main.Animals.Arachnids;
using WTS.Entities.Main.Enums;

namespace WTS.Entities.Main.Animals.Arachnids.SpecificArachnids
{
    [Serializable]
    public class Spider : Arachnid
    {
        private int silkMass;
        private int nmbrOfEyes;
        public Spider()
        {
            Species = Species.Spider;
            EaterType = EaterType.Carnivore;
        }

        [JsonProperty]
        public int SilkMass
        {
            get { return silkMass; }
            set { silkMass = value; }
        }

        [JsonProperty]
        public int NmbrOfEyes
        {
            get { return nmbrOfEyes; }
            set { nmbrOfEyes = value; }
        }

        public override string getExtraInfo()
        {
            string strOut = string.Empty;

            strOut = string.Format("{0,-20} {1,-30}", "Animal:", Species.ToString()) + "\n" + base.getExtraInfo() + string.Format("{0,-20} {1,-30}", "Silk mass(mg):", silkMass) + "\n" +
                string.Format("{0,-20} {1,-30}", "Number of eyes:", nmbrOfEyes) + "\n" + string.Format("{0,-20} {1,-30}", "Food type:", EaterType);
        
            return strOut;
        }
    }
}
