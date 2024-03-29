using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WTS.Entities.Enums;
using WTS.Entities.Main.Enums;
using WTS.Entities.Main.Reptiles.AnimalTypes;

namespace WTS.Entities.Main.AnimalTypes.Reptiles.SpecificReptiles
{
    public class Snake : Reptile
    {
        private int venemous;
        private int shedFreq;
        public Snake()
        {
            Species = Species.Snake;
            EaterType = EaterType.Carnivore;
        }

        public int Venemous
        {
            get { return venemous; }
            set { venemous = value; }
        }

        public int ShedFreq
        {
            get { return shedFreq; }
            set { shedFreq = value; }
        }

        public override string getExtraInfo()
        {
            string strOut = string.Empty;

            strOut = string.Format("{0,-20} {1,-30}", "Animal:", Species.ToString()) + "\n" + base.getExtraInfo() + string.Format("{0,-20} {1,-30}", "Venemous(1-10):", venemous) + "\n" +
                string.Format("{0,-20} {1,-30}", "Shed Frequency(months):", shedFreq) + "\n" + string.Format("{0,-20} {1,-30}", "Food type:", EaterType);

            return strOut;
        }
    }
}
