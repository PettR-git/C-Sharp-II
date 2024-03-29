using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WTS.Entities.Enums;
using WTS.Entities.Main.Enums;
using static System.Net.Mime.MediaTypeNames;

namespace WTS.Entities.Main.Amphibians.AnimalTypes
{
    //Specific attributes of type amphibian are stored
    public abstract class Amphibian : Animal
    {
        private int hindLegsLength;
        private int eyeDiameter;
        public Amphibian() 
        {
            AnimalType = AnimalType.Amphibian;
        }

        public int HindLegsLength 
        { 
            get { return hindLegsLength; }     
            set { hindLegsLength = value; }
        }

        public int EyeDiameter
        {
            get { return eyeDiameter; }
            set { eyeDiameter = value; }
        }

        //Extra info to picture text
        public override string getExtraInfo()
        {
            string strOut = string.Empty;

            strOut = string.Format("{0,-20} {1,-30}", "Type:", AnimalType) + "\n" + string.Format("{0,-20} {1,-30}", "Hind-legs length(cm):", hindLegsLength) + "\n" +
                string.Format("{0,-20} {1,-30}", "Eye diameter(mm):", eyeDiameter) + "\n";

            return strOut;
        }

    }
}
