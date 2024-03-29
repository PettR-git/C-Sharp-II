using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WTS.Entities.Enums;
using WTS.Entities.Main.Enums;

namespace WTS.Entities.Main.Arachnids.AnimalTypes
{
    //Specific attributes of type arachnid are stored
    public abstract class Arachnid : Animal
    {
        private int exoThickness;
        private int legLength;
        public Arachnid()
        {
            AnimalType = AnimalType.Arachnid;
        }

        public int ExoThickness
        {
            get { return exoThickness; }
            set { exoThickness = value; }
        }

        public int LegLength
        {
            get { return legLength; }
            set { legLength = value; }
        }

        //Extra info for seperate visualization
        public override string getExtraInfo()
        {
            string strOut = string.Empty;

            strOut = string.Format("{0,-20} {1,-30}", "Type:", AnimalType) + "\n" + string.Format("{0,-20} {1,-30}", "Thickness of exo-skeleton(mm):", exoThickness) + "\n" +
                string.Format("{0,-20} {1,-30}", "Leg length(cm):", legLength) + "\n";

            return strOut;
        }
    }
}
