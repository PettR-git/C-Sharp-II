using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;
using WTS.Entities.Enums;
using WTS.Entities.Main.Amphibians.AnimalTypes;
using WTS.Entities.Main.Enums;

namespace WTS.Entities.Main.AnimalTypes.Amphibians.SpecificAmphibians
{
    public class Frog : Amphibian
    {
        private int tongueLength;
        private string color;
        public Frog()
        {
            Species = Species.Frog;
            EaterType = EaterType.Carnivore;
        }

        public int TongueLength
        {
            get { return tongueLength; }
            set { tongueLength = value; }
        }

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

   
