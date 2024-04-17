using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WTS.Entities.Main.Animals.Mammals;
using WTS.Entities.Main.Enums;

namespace WTS.Entities.Main.Animals.Mammals.SpecificMammals
{
    [Serializable]
    public class Squirrel : Mammal
    {
        private int frntTeethL;
        private int cheekVol;
        public Squirrel()
        {
            Species = Species.Squirrel;
            EaterType = EaterType.Omnivore;
        }

        [JsonProperty]
        public int FrontTeethLength
        {
            get { return frntTeethL; }
            set { frntTeethL = value; }
        }

        [JsonProperty]
        public int CheekVolume
        {
            get { return cheekVol; }
            set { cheekVol = value; }
        }

        public override string getExtraInfo()
        {
            string strOut = string.Empty;

            strOut = string.Format("{0,-20} {1,-30}", "Animal:", Species.ToString()) + "\n" + base.getExtraInfo() + string.Format("{0,-20} {1,-30}", "Front Teeth length(cm):", frntTeethL) + "\n" +
                string.Format("{0,-20} {1,-30}", "Cheek Volume(cm³):", cheekVol) + "\n" + string.Format("{0,-20} {1,-30}", "Food type:", EaterType);

            return strOut;
        }
    }
}
