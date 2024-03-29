using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WTS.Entities.Main.Arachnids.AnimalTypes;
using WTS.Entities.Main.Enums;

namespace WTS.Entities.Main.AnimalTypes.Arachnids.SpecificArachnids
{
    public class Spider : Arachnid
    {
        private int silkMass;
        private int nmbrOfEyes;
        public Spider()
        {
            Species = Species.Spider;
            EaterType = EaterType.Carnivore;
        }

        public int SilkMass
        {
            get { return silkMass; }
            set { silkMass = value; }
        }

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
