using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WTS.Entities.Enums;
using WTS.Entities.Main.Animals.Amphibians;
using WTS.Entities.Main.Enums;

namespace WTS.Entities.Main.Animals.Amphibians.SpecificAmphibians
{
    [Serializable]
    public class Frog : Amphibian
    {
        private int tongueLength;
        private string color;
        public Frog()
        {
            Species = Species.Frog;
            EaterType = EaterType.Carnivore;
        }

        [JsonProperty]
        public int TongueLength
        {
            get { return tongueLength; }
            set { tongueLength = value; }
        }

        [JsonProperty]
        public string Color
        {
            get { return color; }
            set { color = value; }
        }

        //Extra info to picture text
        public override string getExtraInfo()
        {
            string strOut = string.Empty;

            strOut = string.Format("{0,-20} {1,-30}", "Animal:", Species.ToString()) + "\n" + base.getExtraInfo() + string.Format("{0,-20} {1,-30}", "Tongue Length(cm):", tongueLength) + "\n" +
                string.Format("{0,-20} {1,-30}", "Color:", color) + "\n" + string.Format("{0,-20} {1,-30}", "Food type:", EaterType);

            return strOut;
        }
    }
}

   
