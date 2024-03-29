using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WTS.Entities.Enums;

namespace WTS.Entities.Main.Mammals.AnimalTypes
{
    //Specific attributes of type mammal are stored
    public abstract class Mammal : Animal
    {
        private int nmbrOfOffspring;
        private int nmbrOfToes;
        public Mammal()
        {
            AnimalType = AnimalType.Mammal;
        }

        public int NmbrOfOffspring
        {
            get { return nmbrOfOffspring; }
            set { nmbrOfOffspring = value; }
        }

        public int NmbrOfToes
        {
            get { return nmbrOfToes; }
            set { nmbrOfToes = value; }
        }

        //Extra info to picture text
        public override string getExtraInfo()
        {
            string strOut = string.Empty;

            strOut = string.Format("{0,-20} {1,-30}", "Type:", AnimalType) + "\n" + string.Format("{0,-20} {1,-30}", "Number of offspring:", nmbrOfOffspring) + "\n" +
                string.Format("{0,-20} {1,-30}", "Number of toes:", nmbrOfToes) + "\n";

            return strOut;
        }
    }
}
