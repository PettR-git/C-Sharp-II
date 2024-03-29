using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WTS.Entities.Enums;

namespace WTS.Entities.Main.Reptiles.AnimalTypes
{
    //Specific attributes of type reptile are stored
    public abstract class Reptile : Animal
    {
        private int tailLength;
        private int tongueMass;
        public Reptile()
        {
            AnimalType = AnimalType.Reptile;
        }

        public int TailLength
        {
            get { return tailLength; }
            set { tailLength = value; }
        }

        public int TongueMass
        {
            get { return tongueMass; }
            set { tongueMass = value; }
        }

        //Extra info to picture text
        public override string getExtraInfo()
        {
            string strOut = string.Empty;

            strOut = string.Format("{0,-20} {1,-30}", "Type:", AnimalType) + "\n" + string.Format("{0,-20} {1,-30}", "Tail Length(cm):", tailLength) + "\n" +
                string.Format("{0,-20} {1,-30}", "Tongue Mass(g):", tongueMass) + "\n";

            return strOut;
        }
    }
}
