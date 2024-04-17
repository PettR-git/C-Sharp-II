using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WTS.Entities.Enums;
using WTS.Entities.Main.Animals.Amphibians.SpecificAmphibians;
using WTS.Entities.Main.Animals.Amphibians;
using WTS.Entities.Main.Animals.Arachnids.SpecificArachnids;
using WTS.Entities.Main.Animals.Arachnids;
using WTS.Entities.Main.Enums;
using WTS.Entities.Main.Animals.Mammals.SpecificMammals;
using WTS.Entities.Main.Animals.Reptiles.SpecificReptiles;
using WTS.Entities.Main.Animals.Amphibians;
using WTS.Entities.Main.Animals.Mammals;
using WTS.Entities.Main.Animals.Arachnids;
using WTS.Entities.Main.Animals.Reptiles;
using System.Windows;
using WTS.Entities.Main;
using System.Reflection;
using Newtonsoft.Json;
using JsonSubTypes;

namespace WTS.Entities
{
    public class AnimalManager : ListManager<Animal>
    {
        private Dictionary<string, int> ids;
        private JsonSerializerSettings settings;
        public AnimalManager() 
        { 
            InitializeIDs();
        }

        //For initializing Ids: MA0001, AR0001....
        private void InitializeIDs()
        {
            ids = new Dictionary<string, int>();
            foreach (AnimalType animaltype in Enum.GetValues(typeof(AnimalType)))
            {
                ids.Add(animaltype.ToString().Substring(0, 2).ToUpper(), 0);
            }
        }

        //Creating correct instance of animal and add specific attributes, 
        public Animal createAnimal(AnimalType animalType, Species species, string cateAtt1, string cateAtt2, string spec1, string spec2)
        {
            Animal animal = null;

            switch (animalType)
            {
                case AnimalType.Mammal:
                    switch (species)
                    {
                        case Species.Squirrel:
                            animal = new Squirrel();
                            ((Squirrel)animal).FrontTeethLength = convertStrToInt(spec1);
                            ((Squirrel)animal).CheekVolume = convertStrToInt(spec2);

                            if(!validateInt(((Squirrel)animal).FrontTeethLength, ((Squirrel)animal).CheekVolume))
                                return null;                           
                            break;
                        case Species.Zebra:
                            animal = new Zebra();
                            ((Zebra)animal).NmbrOfStripes = convertStrToInt(spec1);
                            ((Zebra)animal).KickForce = convertStrToInt(spec2);

                            if (!validateInt(((Zebra)animal).NmbrOfStripes,((Zebra)animal).KickForce))
                                return null;
                            break;
                        default:
                            Debug.WriteLine("Could not animal");
                            return null;
                    }
                    ((Mammal)animal).NmbrOfOffspring = convertStrToInt(cateAtt1);
                    ((Mammal)animal).NmbrOfToes = convertStrToInt(cateAtt2);

                    if (!validateInt(((Mammal)animal).NmbrOfOffspring,((Mammal)animal).NmbrOfToes))
                        return null;
                    break;
                case AnimalType.Reptile:
                    switch(species)
                    {
                        case Species.Crocodile:
                            animal = new Crocodile();
                            ((Crocodile)animal).GapLength = convertStrToInt(spec1);
                            ((Crocodile)animal).NmbrOfScaleSpikes = convertStrToInt(spec2);

                            if (!validateInt(((Crocodile)animal).GapLength, ((Crocodile)animal).NmbrOfScaleSpikes))
                                return null;
                            break;
                        case Species.Snake:
                            animal = new Snake();
                            ((Snake)animal).Venemous = convertStrToInt(spec1);
                            ((Snake)animal).ShedFreq = convertStrToInt(spec2);

                            if (!validateInt(((Snake)animal).Venemous,((Snake)animal).ShedFreq))
                                return null;
                            break;
                        default:
                            Debug.WriteLine("Could not animal");
                            return null;
                    }
                    ((Reptile)animal).TailLength = convertStrToInt(cateAtt1);
                    ((Reptile)animal).TongueMass = convertStrToInt(cateAtt2);

                    if (!validateInt(((Reptile)animal).TailLength,((Reptile)animal).TongueMass))
                        return null;
                    break;
                case AnimalType.Arachnid:
                    switch (species)
                    {
                        case Species.Spider:
                            animal = new Spider();
                            ((Spider)animal).SilkMass = convertStrToInt(spec1);
                            ((Spider)animal).NmbrOfEyes = convertStrToInt(spec2);

                            if(!validateInt(((Spider)animal).SilkMass, ((Spider)animal).NmbrOfEyes))
                                return null;
                            break;
                        case Species.Scorpion:
                            animal = new Scorpion();
                            ((Scorpion)animal).StingerLength = convertStrToInt(spec1);
                            ((Scorpion)animal).ClawType = spec2;
                             
                            if(!validateInt(((Scorpion)animal).StingerLength) || string.IsNullOrEmpty(spec2))
                                return null;
                            break;
                        default:
                            Debug.WriteLine("Could not animal");
                            return null;
                    }
                    ((Arachnid)animal).ExoThickness = convertStrToInt(cateAtt1);
                    ((Arachnid)animal).LegLength = convertStrToInt(cateAtt2);

                    if(!validateInt(((Arachnid)animal).ExoThickness, ((Arachnid)animal).LegLength))
                        return null;
                    break;
                case AnimalType.Amphibian:
                    switch (species)
                    {
                        case Species.Frog:
                            animal = new Frog();
                            ((Frog)animal).TongueLength = convertStrToInt(spec1);
                            ((Frog)animal).Color = spec2;

                            if (!validateInt(((Frog)animal).TongueLength) || string.IsNullOrEmpty(spec2))
                                return null; 
                            break;
                        case Species.Salamander:
                            animal = new Salamander();
                            ((Salamander)animal).Weight = convertStrToInt(spec1);
                            ((Salamander)animal).Wellness = convertStrToInt(spec2);

                            if (!validateInt(((Salamander)animal).Weight, ((Salamander)animal).Wellness))
                                return null;
                            break;
                        default:
                            Debug.WriteLine("Could not animal");
                            return null;
                    }
                    ((Amphibian)animal).HindLegsLength = convertStrToInt(cateAtt1);
                    ((Amphibian)animal).EyeDiameter = convertStrToInt(cateAtt2);

                    if(!validateInt(((Amphibian)animal).HindLegsLength,((Amphibian)animal).EyeDiameter))
                        return null;
                    break;
                default:
                    Debug.WriteLine("Could not enter AnimalType.AnimalType");
                    return null;
            }

            return animal;
        }

        //Try convert string -> int
        private int convertStrToInt(string str)
        {
            if (int.TryParse(str, out int length))
            {
                return length;
            }

            return -1;
        }

        //Validate
        private bool validateInt(int num)
        {
            bool ok = true;

            if (num == -1)
                ok = false;

            return ok;
        }

        //Overloaded validation
        private bool validateInt(int num1, int num2)
        {
            bool ok = true;

            if (num1 == -1 || num2 == -1)
                ok = false;

            return ok;
        }

        public void preSetId(Animal animal)
        {
            string idStr = animal.Id;
            bool parse = int.TryParse(idStr.Substring(idStr.Length - 2), out int presetId);
            string currIdStr = idStr.Substring(0, 2);
            int currId = ids[currIdStr];

            if (parse)
                if (presetId >= currId)
                {
                    ids[currIdStr] = presetId;
                }
        }

        //Adding an id to an animal
        public string addId(Animal animal)
        {
            string animalTypeStr = animal.AnimalType.ToString().Substring(0, 2).ToUpper();
            //Saving the ids for animals
            int id = ++ids[animalTypeStr];
            string formatID = id.ToString("D4");
            string strId = $"{animalTypeStr}{formatID}";

            return strId;
        }

        public bool changeExistingId(Animal prevAnimal, Animal newAnimal)
        {
            if(prevAnimal.Id == string.Empty) return false;

            foreach(var pair in ids)
            {
                if(pair.Key == prevAnimal.Id.Substring(0, 2))
                {
                    ids.Remove(pair.Key);
                    string strId = addId(newAnimal);
                    newAnimal.Id = strId;
                    break;
                }
            }

            return true;
        }

        public bool removeId(Animal animal)
        {
            if(animal.Id != string.Empty || animal != null)
            {
                ids.Remove(animal.Id);
                return true;
            }
            
            return false;
        }

        //Sort by name (A-Z)
        public void sortByName()
        {
            defaultSort();
        }

        //Sort by id/animalType (A-Z)
        public void sortById()
        {
            secondarySort((a,b)=> a.Id.CompareTo(b.Id));
        }

        public JsonSerializerSettings animalJsonSerializerSettings()
        {
            if(settings == null)
            {
                settings = new JsonSerializerSettings();
                settings.Converters.Add(JsonSubtypesWithPropertyConverterBuilder
                    .Of<Animal>()
                    .RegisterSubtypeWithProperty<Mammal>("NmbrOfOffsprings")
                    .RegisterSubtypeWithProperty<Mammal>("NmbrOfToes")
                    .RegisterSubtypeWithProperty<Amphibian>("HindLegsLength")
                    .RegisterSubtypeWithProperty<Amphibian>("EyeDiameter")
                    .RegisterSubtypeWithProperty<Arachnid>("ExoThickness")
                    .RegisterSubtypeWithProperty<Arachnid>("LegLength")
                    .RegisterSubtypeWithProperty<Reptile>("TailLength")
                    .RegisterSubtypeWithProperty<Reptile>("TongueMass")
                    .Build());
                settings.Converters.Add(JsonSubtypesWithPropertyConverterBuilder
                    .Of<Mammal>()
                    .RegisterSubtypeWithProperty<Zebra>("NmbrOfStripes")
                    .RegisterSubtypeWithProperty<Zebra>("KickForce")
                    .RegisterSubtypeWithProperty<Squirrel>("FrontTeethLength")
                    .RegisterSubtypeWithProperty<Squirrel>("CheekVolume")
                    .Build());
                settings.Converters.Add(JsonSubtypesWithPropertyConverterBuilder
                    .Of<Amphibian>()
                    .RegisterSubtypeWithProperty<Frog>("TongueLength")
                    .RegisterSubtypeWithProperty<Frog>("Color")
                    .RegisterSubtypeWithProperty<Salamander>("Weight")
                    .RegisterSubtypeWithProperty<Salamander>("Wellness")
                    .Build());
                settings.Converters.Add(JsonSubtypesWithPropertyConverterBuilder
                    .Of<Arachnid>()
                    .RegisterSubtypeWithProperty<Scorpion>("StingerLength")
                    .RegisterSubtypeWithProperty<Scorpion>("ClawType")
                    .RegisterSubtypeWithProperty<Spider>("SilkMass")
                    .RegisterSubtypeWithProperty<Spider>("NmbrOfEyes")
                    .Build());
                settings.Converters.Add(JsonSubtypesWithPropertyConverterBuilder
                    .Of<Reptile>()
                    .RegisterSubtypeWithProperty<Crocodile>("GapLength")
                    .RegisterSubtypeWithProperty<Crocodile>("NmbrOfScaleSpikes")
                    .RegisterSubtypeWithProperty<Snake>("Venemous")
                    .RegisterSubtypeWithProperty<Snake>("ScaleFreq")
                    .Build());

                settings.Formatting = Formatting.Indented;
            }

            return settings;
        }
    }
}
