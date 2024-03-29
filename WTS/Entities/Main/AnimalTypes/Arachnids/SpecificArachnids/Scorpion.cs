using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WTS.Entities.Main.Arachnids.AnimalTypes;
using WTS.Entities.Main.Enums;

namespace WTS.Entities.Main.AnimalTypes.Arachnids.SpecificArachnids
{
    public class Scorpion : Arachnid
    {
        private int stingerLength;
        private string clawType;
        public Scorpion()
        {
            Species = Species.Scorpion;
            EaterType = EaterType.Carnivore;
        }

        public int StingerLength
        {
            get { return stingerLength; }
            set { stingerLength = value; }
        }

        public string ClawType
        {
            get { return clawType; }
            set { clawType = value; }
        }

        public override string getExtraInfo()
        {
            string strOut = string.Empty;

            strOut =  string.Format("{0,-20} {1,-30}", "Animal:", Species.ToString()) + "\n" + base.getExtraInfo() + string.Format("{0,-20} {1,-30}", "Stinger length(mm):", stingerLength) + "\n" +
                string.Format("{0,-20} {1,-30}", "Claw charachteristics:", clawType) + "\n" + string.Format("{0,-20} {1,-30}", "Food type:", EaterType);

            return strOut;
        }
    }
}
