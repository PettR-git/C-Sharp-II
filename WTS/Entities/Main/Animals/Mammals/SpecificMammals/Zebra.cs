using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WTS.Entities.Main.Enums;
using WTS.Entities.Main.Animals.Mammals;

namespace WTS.Entities.Main.Animals.Mammals.SpecificMammals
{
    [Serializable]
    public class Zebra : Mammal
    {
        private int nmbrOfStripes;
        private int kickForce;
        public Zebra()
        {
            Species = Species.Zebra;
            EaterType = EaterType.Herbivore;
        }

        public int NmbrOfStripes
        {
            get { return nmbrOfStripes; }
            set { nmbrOfStripes = value; }
        }

        public int KickForce
        {
            get { return kickForce; }
            set { kickForce = value; }
        }

        public override string getExtraInfo()
        {
            string strOut = string.Empty;

            strOut = string.Format("{0,-20} {1,-30}", "Animal:", Species.ToString()) + "\n" + base.getExtraInfo() + string.Format("{0,-20} {1,-30}", "Number of stripes:", nmbrOfStripes) + "\n" +
                string.Format("{0,-20} {1,-30}", "Kick Force(N):", kickForce) + "\n" + string.Format("{0,-20} {1,-30}", "Food type:", EaterType);

            return strOut;
        }
    }       
}
