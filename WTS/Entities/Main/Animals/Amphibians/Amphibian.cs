using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WTS.Entities.Enums;
using WTS.Entities.Main.Enums;

namespace WTS.Entities.Main.Animals.Amphibians
{
    //Specific attributes of type amphibian are stored
    [Serializable]
    public class Amphibian : Animal
    {
        private int hindLegsLength;
        private int eyeDiameter;
        public Amphibian() 
        {
            AnimalType = AnimalType.Amphibian;
        }

        [JsonProperty]
        public int HindLegsLength 
        { 
            get { return hindLegsLength; }     
            set { hindLegsLength = value; }
        }

        [JsonProperty]
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
