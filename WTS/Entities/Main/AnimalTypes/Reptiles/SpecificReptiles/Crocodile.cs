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
    public class Crocodile : Reptile
    {
        private int gapLength;
        private int nmbrOfScaleSpikes;
        public Crocodile()
        {
            Species = Species.Crocodile;
            EaterType = EaterType.Carnivore;
        }

        public int GapLength
        {
            get { return gapLength; }
            set { gapLength = value; }
        }

        public int NmbrOfScaleSpikes
        {
            get { return nmbrOfScaleSpikes; }
            set { nmbrOfScaleSpikes = value; }
        }

        public override string getExtraInfo()
        {
            string strOut = string.Empty;

            strOut = string.Format("{0,-20} {1,-30}", "Animal:", Species.ToString()) + "\n" + base.getExtraInfo() + string.Format("{0,-20} {1,-30}", "Mouth gap(cm):", gapLength) + "\n" +
                string.Format("{0,-20} {1,-30}", "Number of spikes:", nmbrOfScaleSpikes) + "\n" + string.Format("{0,-20} {1,-30}", "Food type:", EaterType);

            return strOut;
        }
    }
}
