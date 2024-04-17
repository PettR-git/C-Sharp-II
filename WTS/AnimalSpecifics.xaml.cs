using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WTS.Entities;
using WTS.Entities.Enums;
using WTS.Entities.Main.Animals.Amphibians.SpecificAmphibians;
using WTS.Entities.Main.Animals.Arachnids.SpecificArachnids;
using WTS.Entities.Main.Animals.Mammals.SpecificMammals;
using WTS.Entities.Main.Animals.Reptiles.SpecificReptiles;
using WTS.Entities.Main.Enums;
using WTS.Utilities.Exceptions;

namespace WTS
{
    /// <summary>
    /// Specific inputs of animal category/animaltype and species
    /// </summary>
    public partial class AnimalSpecifics : Window
    {
        public event Action<Dictionary<string, string>> AnimalInfoPassed;
    
        private AnimalType animalType;
        private Species species;
        public AnimalSpecifics(AnimalType animalType, Species species)
        {
            InitializeComponent();
            CenterWindowOnScreen();
            this.animalType = animalType;
            this.species = species;

            displayAttributesText();
        }

        //Initialize UI-text depending on chosen animal
        private void displayAttributesText()
        {
            switch (animalType)
            {
                case AnimalType.Mammal:
                    switch (species)
                    {
                        case Species.Squirrel:
                            lblAtt1.Content = "Front Teeth length(cm)";
                            lblAtt2.Content = "Cheek Volume(cm³)";
                            break;
                        case Species.Zebra:
                            lblAtt1.Content = "Number of stripes:";
                            lblAtt2.Content = "Kick Force(N)";
                            break;
                        default:
                            Debug.WriteLine("Could not animal");
                            break;
                    }
                    lblAnType1.Content = "Number of offspring:";
                    lblAnType2.Content = "Number of toes:";
                    break;
                case AnimalType.Reptile:
                    switch (species)
                    {
                        case Species.Crocodile:
                            lblAtt1.Content = "Mouth gap(cm)";
                            lblAtt2.Content = "Number of scale spikes:";
                            break;
                        case Species.Snake:
                            lblAtt1.Content = "Venemous(1-10):";
                            lblAtt2.Content = "Shed Frequency(months):";
                            break;
                        default:
                            Debug.WriteLine("Could not animal");
                            break;
                    }
                    lblAnType1.Content = "Tail length(cm)";
                    lblAnType2.Content = "Tounge mass(kg)";
                    break;
                case AnimalType.Arachnid:
                    switch (species)
                    {
                        case Species.Spider:
                            lblAtt1.Content = "Silk mass(mg)";
                            lblAtt2.Content = "Number of eyes:";
                            break;
                        case Species.Scorpion:
                            lblAtt1.Content = "Stinger length(mm)";
                            lblAtt2.Content = "Claw charachteristics:";
                            break;
                        default:
                            Debug.WriteLine("Could not animal");
                            break;
                    }
                    lblAnType1.Content = "Thickness of exo-skeleton(mm)";
                    lblAnType2.Content = "Leg length(cm)";
                    break;
                case AnimalType.Amphibian:
                    switch (species)
                    {
                        case Species.Frog:
                            lblAtt1.Content = "Tongue length(cm):";
                            lblAtt2.Content = "Color:";
                            break;
                        case Species.Salamander:
                            lblAtt1.Content = "Weight(g):";
                            lblAtt2.Content = "Wellness(1-10):";
                            break;
                        default:
                            Debug.WriteLine("Could not animal");
                            break;
                    }
                    lblAnType1.Content = "Hind-Legs length(cm):";
                    lblAnType2.Content = "Eye diameter(cm):";
                    break;
                default:
                    Debug.WriteLine("Could not enter AnimalType.AnimalType");
                    break;
            }
        }

        private void CenterWindowOnScreen()
        {
            WindowStartupLocation = WindowStartupLocation.Manual;

            double screenWidth = SystemParameters.PrimaryScreenWidth;
            double screenHeight = SystemParameters.PrimaryScreenHeight;
            double windowWidth = Width;
            double windowHeight = Height;

            Left = (screenWidth - windowWidth) / 2;
            Top = (screenHeight - windowHeight) / 2;
        }

        public AnimalType getAnimalType() { return animalType; }
        public Species getSpecies() { return species; }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Dictionary<string, string> attributes = new Dictionary<string, string>();

            try
            {
                checkGrading();
            }
            catch (NumberOutOfGradingScale err)
            {
                MessageBox.Show(err.Message, "Error");
                return;
            }

            attributes[lblAnType1.Content.ToString()] = tbxAnType1.Text;
            attributes[lblAnType2.Content.ToString()] = tbxAnType2.Text;
            attributes[lblAtt1.Content.ToString()] = tbxAtt1.Text;
            attributes[lblAtt2.Content.ToString()] = tbxAtt2.Text;
            AnType1 = tbxAnType1.Text;
            AnType2 = tbxAnType2.Text;
            Att1 = tbxAtt1.Text;
            Att2 = tbxAtt2.Text;

            foreach (var attribute in attributes)
            {
                if (string.IsNullOrEmpty(attribute.Value))
                {
                    MessageBox.Show("Invalid input");
                    return;
                }       
            }

            AnimalInfoPassed?.Invoke(attributes);
            this.Close();          
        }

        private void checkGrading()
        {
            string labelStr = "";
            const int greaterLimit = 10;
            const int lessLimit = 10;
            bool res = false;

            switch (getSpecies())
            {
                case Species.Salamander:
                    labelStr = tbxAtt2.Text;
                    break;
                case Species.Snake:
                    labelStr = tbxAtt1.Text;
                    break;
                default: 
                    return;
            }

            res = int.TryParse(labelStr, out int val);

            if (res)
                if (val > greaterLimit)
                {
                    string message = string.Format("Value cannot exceed {0} or be less than {0}, given the grading scale.", greaterLimit, lessLimit) + '\n' +
                                     string.Format("The Value given is {0}", val);
                    throw new NumberOutOfGradingScale(message, null, val);
                }
        }

        public string AnType1 { get; private set; }
        public string AnType2 { get; private set;}
        public string Att1 { get; private set;}
        public string Att2 { get; private set;}


    }
}
