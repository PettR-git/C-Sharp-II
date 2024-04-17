using System;
using System.Collections.Generic;
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
using WTS.Entities;
using WTS.Entities.Enums;
using System.Diagnostics;
using WTS.Entities.Main.Enums;
using Microsoft.Win32;
using WTS.Entities.Main;
using System.Reflection;
using System.Net;
using WTS.Entities.Main.Animals.Reptiles.SpecificReptiles;
using WTS.Entities.Main.Animals.Mammals;
using WTS.Entities.Main.Animals.Mammals.SpecificMammals;
using WTS.Entities.Main.Animals.Arachnids.SpecificArachnids;
using WTS.Entities.Main.Animals.Amphibians.SpecificAmphibians;
using JsonSubTypes;
using Newtonsoft.Json;
using System.IO;

namespace WTS
{
    /// <summary>
    /// To register wildlife in a zoo
    /// Name: Petter Rignell
    /// Date: 2024-01-25
    /// Updated: 2024-04-07
    /// </summary>
    public partial class MainWindow : Window
    {
        private AnimalManager animalManager;    
        private FoodManager foodManager;
        private AnimalSpecifics specifics;  //Form for attribute/specifics
        private FoodItemForm foodItemForm;  //Form for fooditems
        private string fileName;
        public MainWindow()
        {
            InitializeComponent();
            NewApp();
            InitializeGUI();
        }

        #region INITIALIZE APP
        //To fill the comboboxes with enum values
        private void NewApp()
        {
            animalManager = new AnimalManager();
            foodManager = new FoodManager();
            updateMainAnimalUI();
        }
        private void InitializeGUI()
        {
            FontFamily fontFamily = new FontFamily("Courier New");
            lvwAnimals.FontFamily = fontFamily;
            lvwAnimals.FontSize = 10;
            lvwAnimals.SelectionMode = SelectionMode.Multiple;
            lvwAnimalInfo.FontFamily = fontFamily;
            lvwAnimalInfo.FontSize = 10;
            lvwFood.FontFamily = fontFamily;
            lvwFood.FontSize = 10;
            lbxAttributes.FontFamily = fontFamily;
            lbxAttributes.FontSize = 10;

            rbtnSortByName.IsChecked = true;

            foreach (var gender in Enum.GetValues(typeof(Gender)))
            {
                cmbGender.Items.Add(gender);
            }

            foreach (var animalType in Enum.GetValues(typeof(AnimalType)))
            {
                lbxAnimalTypes.Items.Add(animalType);
            }
        }

        #endregion

        #region IMAGE RELATED
        //Add image to an animal (not correlated with animal object yet)
        private void btnAddImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp|All Files|*.*";
            openFileDialog.FilterIndex = 1;

            if (openFileDialog.ShowDialog() == true)
            {
                imgAnimal.Source = new BitmapImage(new Uri(openFileDialog.FileName));
            }
        }

        #endregion

        #region HANDLE ANIMAL TYPE AND SPECIES FROM LISTBOXES
        //To add correct species correlated with animal type
        private void lbxAnimalTypes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<Species> species = new List<Species>();

            if (!cbxListAll.IsChecked.Value)
            {
                if (lbxAnimalTypes.SelectedItem != null)
                {
                    switch ((AnimalType)lbxAnimalTypes.SelectedItem)
                    {
                        case AnimalType.Amphibian:
                            species.Add(Species.Frog);
                            species.Add(Species.Salamander);
                            break;
                        case AnimalType.Arachnid:
                            species.Add(Species.Spider);
                            species.Add(Species.Scorpion);
                            break;
                        case AnimalType.Mammal:
                            species.Add(Species.Squirrel);
                            species.Add(Species.Zebra);
                            break;
                        case AnimalType.Reptile:
                            species.Add(Species.Crocodile);
                            species.Add(Species.Snake);
                            break;
                        default:
                            Debug.WriteLine("A species was not chosen correctly.");
                            break;
                    }

                    lbxSpecies.Items.Clear();

                    foreach (var animal in species)
                    {
                        lbxSpecies.Items.Add(animal);
                    }
                }
            }     
        }

        //Visually highlighting animaltypes based on chosen animal/species
        private void lbxSpecies_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbxSpecies.SelectedItem != null)
            {
                switch ((Species)lbxSpecies.SelectedItem)
                {
                    case Species.Frog:
                    case Species.Salamander:
                        lbxAnimalTypes.SelectedItem = AnimalType.Amphibian;
                        break;
                    case Species.Spider:
                    case Species.Scorpion:
                        lbxAnimalTypes.SelectedItem = AnimalType.Arachnid;
                        break;
                    case Species.Squirrel:
                    case Species.Zebra:
                        lbxAnimalTypes.SelectedItem = AnimalType.Mammal;
                        break;
                    case Species.Crocodile:
                    case Species.Snake:
                        lbxAnimalTypes.SelectedItem = AnimalType.Reptile;
                        break;
                    default:
                        Debug.WriteLine("Species has not been selected correctly.");
                        break;
                }
            }
        }

        //To handle checkbox mechanics when selecting and unselecting
        //And how it should impact listboxes
        private void cbxListAll_CheckboxChanged(object sender, RoutedEventArgs e)
        {
            lbxSpecies.Items.Clear();
            lbxAnimalTypes.SelectedItem = null;

            if (cbxListAll.IsChecked == true)
            {
                //filling all animals/species into the listbox
                foreach (var animal in Enum.GetValues(typeof(Species)))
                {
                    lbxSpecies.Items.Add(animal);
                }

                ApplyListBoxStyle(Brushes.Gray);
            }
            else
            {
                lbxSpecies.Items.Clear();
                ApplyListBoxStyle(SystemColors.ControlTextBrush);
            }
        }

        //Changes color based on functionality (on/off)
        private void ApplyListBoxStyle(Brush foregroundBrush)
        {
            Style listBoxItemStyle = new Style(typeof(ListBoxItem));
            listBoxItemStyle.Setters.Add(new Setter(ListBoxItem.ForegroundProperty, foregroundBrush));

            lbxAnimalTypes.ItemContainerStyle = listBoxItemStyle;
        }

        #endregion

        #region DISPLAY LISTVIEW ITEMS
        //Populate the listview with all strings of foodschedule
        private void lvwAnimals_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = lvwAnimals.SelectedIndex;
            lvwAnimalInfo.Items.Clear();
            lvwFood.Items.Clear();

            if (index == -1)
                return;

            Animal animal = animalManager.getListItemAt(index);
            lvwAnimalInfo.Items.Add(animal.getExtraInfo());

            if (foodManager.getListToStrings().Count() > 0)
            {
                displayFoodItems(animal.Id);
            }
        }

        //Display all animal ToStrings
        private void updateMainAnimalUI()
        {
            lbxAttributes.Items.Clear();
            lvwAnimals.Items.Clear();
            lvwAnimalInfo.Items.Clear();
            lvwFood.Items.Clear();
            tbxAge.Text = string.Empty;
            tbxName.Text = string.Empty;
            cmbGender.SelectedIndex = -1;
            cbxDomesitcated.IsChecked = false;
            cbxListAll.IsChecked = false;

            foreach (string strA in animalManager.getListToStrings())
            {
                lvwAnimals.Items.Add(strA);
            }
        }

        #endregion

        #region SORTING

        //Sort listView by Animaltype/Id
        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            animalManager.sortById();
            updateMainAnimalUI();
        }

        //Sort listView by Name
        private void rbtnSortByName_Checked(object sender, RoutedEventArgs e)
        {
            animalManager.sortByName();
            updateMainAnimalUI();
        }

        private void sortAnimalView()
        {
            //Sort when adding to listView
            if (rbtnSortByName.IsChecked == true)
                animalManager.sortByName();
            else
                animalManager.sortById();
        }
        #endregion

        #region DATA FROM ANIMALSPECIFICS
        //Handling data from animalspecifics form
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (cmbGender.SelectedItem != null && lbxSpecies.SelectedItem != null)
            {
                if (int.TryParse(tbxAge.Text, out int age) && !string.IsNullOrEmpty(tbxName.Text))
                {
                    AnimalType animalType = (AnimalType)lbxAnimalTypes.SelectedItem;
                    Species species = (Species)lbxSpecies.SelectedItem;

                    //imgAnimal.Source = null;
                    specifics = new AnimalSpecifics(animalType, species);

                    //To handle event when animal attributes have been given
                    specifics.AnimalInfoPassed += handleInputData;
                    specifics.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Invalid input!");
                }
            }
            else
            {
                MessageBox.Show("Choose valid gender and/or species!");
            }
        }

        //Deal with all animal info to and save the animal
        private void handleInputData(Dictionary<string, string> attributes)
        {
            lbxAttributes.Items.Clear();

            foreach(var attribute in attributes)
            {
                lbxAttributes.Items.Add(string.Format("{0, -20}{1, -20}",attribute.Key, attribute.Value));
            }
        }
        #endregion

        #region ADDING, CHANGING, DELETING ANIMAL OBJ
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Animal animal = null;
            animal = createAnimal();

            if (animal == null)
                return;
             
            animal.Id = animalManager.addId(animal);
            animalManager.addItem(animal);
            sortAnimalView();
            updateMainAnimalUI();
            lbxSpecies.SelectedItem = null;
        }

        //Change animal object
        private void btnChange_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int index = lvwAnimals.SelectedIndex;
                Animal oldAnimal = null;
                Animal newAnimal = null;

                if (index == -1)
                {
                    MessageBox.Show("Change by choosing animal in list", "Error");
                    return;
                }

                oldAnimal = animalManager.getListItemAt(index);
                newAnimal = createAnimal(); 
                animalManager.changeExistingId(oldAnimal, newAnimal);
                animalManager.changeItem(newAnimal, index);
                sortAnimalView();
                updateMainAnimalUI();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }

        //Delete animal from view and as object
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Animal animal = null;
                int index = lvwAnimals.SelectedIndex;

                if (index == -1)
                {
                    MessageBox.Show("Delete by choosing animal in list", "Error");
                    return;
                }
                    
                animal = animalManager.getListItemAt(index);

                animalManager.removeId(animal);
                animalManager.removeItem(animal);
                sortAnimalView();
                updateMainAnimalUI();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        //Create a new animal object and give attributes
        private Animal createAnimal()
        {
            Animal animal = null;

            try
            {
                if (cmbGender.SelectedItem != null && lbxSpecies.SelectedItem != null)
                {
                    if(lbxAttributes.Items.Count != 0)
                    {
                        bool domesticated = cbxDomesitcated.IsChecked == true;

                        AnimalType animalType = specifics.getAnimalType();
                        Species species = specifics.getSpecies();
                        Gender gender = (Gender)cmbGender.SelectedItem;

                        string cateAtt1 = specifics.AnType1;
                        string cateAtt2 = specifics.AnType2;
                        string specAtt1 = specifics.Att1;
                        string specAtt2 = specifics.Att2;

                        animal = animalManager.createAnimal(animalType, species, cateAtt1, cateAtt2, specAtt1, specAtt2);

                        if (animal != null)
                        {
                            animal.Name = tbxName.Text;
                            animal.Age = int.Parse(tbxAge.Text);
                            animal.Gender = gender;
                            animal.Domesticated = domesticated;
                            specifics.Close();
                        }
                        else
                        {
                            MessageBox.Show("Invalid attribute input");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Choose animal attributes");
                    }                 
                }
                else
                {
                    MessageBox.Show("Choose valid gender and/or species!");
                }          
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                MessageBox.Show("Invalid input!");
            }

            return animal;
        }

        #endregion

        #region FOOD ITEMS
        //To add food item and connect it to correct ids
        private void btnAddFoodItem_Click(object sender, RoutedEventArgs e)
        {
            List<string> ids = new List<string>();
            int index = -1;

            foreach (object selectedItem in lvwAnimals.SelectedItems)
            {
                index = lvwAnimals.Items.IndexOf(selectedItem);
                ids.Add(animalManager.getListItemAt(index).Id);
            }

            if(index != -1)
            {
                foodItemForm = new FoodItemForm();
                foodItemForm.FoodItemEvent += (foodItem) =>
                {
                    foodItem.Ids = new List<string>();
                    foodItem.Ids.AddRange(ids);
                    foodManager.addItem(foodItem);

                    lvwAnimals.SelectedItems.Clear();

                    foreach (string id in foodItem.Ids)
                    {
                        foodManager.connectIdAndFoodItem(foodItem, id);
                    }
                    //lvwAnimals.SelectedIndex = index;
                };

                foodItemForm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Choose one or more animals from the animal list");
            } 
        }

        //Display food item for an id
        private void displayFoodItems(string id)
        {
            List<FoodItem> foodItems = foodManager.getFoodItemsAtId(id);
            lvwFood.Items.Clear();

            foreach (FoodItem item in foodItems)
            {
                lvwFood.Items.Add(item.ToString());
            }
        }
        #endregion

        #region FILE HANDLING
        //In case action was not wanted
        private bool continueWithFileAction()
        {
            bool ready = false;
            MessageBoxResult res = MessageBox.Show("Are you sure you want to proceed?", "Confirmation", MessageBoxButton.YesNo);

            if (res == MessageBoxResult.Yes)
                ready = true;

            return ready;
        }

        //New app
        private void newFile_click(object sender, RoutedEventArgs e)
        {
            if(continueWithFileAction())
                NewApp();
        }

        //Open and read text file and display content
        private void mnuFileOpenTF_click(object sender, RoutedEventArgs e)
        {
            if (!continueWithFileAction())
                return;

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text files (*.txt)|*.txt";

            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    fileName = openFileDialog.FileName;
                    NewApp();

                    if (!animalManager.binaryDeSerialize(fileName))
                        MessageBox.Show("No data provided from file.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error in file handling!");
                    Console.WriteLine(ex.Message);
                    return;
                }

                for(int i = 0; i<animalManager.Count(); i++)
                {
                    animalManager.preSetId(animalManager.getListItemAt(i));
                }

                updateMainAnimalUI();
            }
        }

        //Open and read json file and display content
        private void mnuFileOpenJ_click(object sender, RoutedEventArgs e)
        {
            if (!continueWithFileAction())
                return;

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "JSON files (*.json)|*.json";

            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    fileName = openFileDialog.FileName;
                    NewApp();

                    if (!animalManager.jsonDeSerialize(fileName, animalManager.animalJsonSerializerSettings()))
                        MessageBox.Show("Could not import data.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error in file handling!");
                    Console.WriteLine(ex.Message);
                    return;
                }

                for (int i = 0; i < animalManager.Count(); i++)
                {
                    animalManager.preSetId(animalManager.getListItemAt(i));
                }

                updateMainAnimalUI();
            }
        }

        //Save as Json or Text file depending on current file
        private void mnuFileSave_click(object sender, RoutedEventArgs e)
        {
            if (!continueWithFileAction())
                return;
            else if(fileName == null)
            {
                MessageBox.Show("Save as a new file", "Error");
            }    
            else if(fileName.Substring(fileName.Length - 4) == "json")
            {
                try
                {
                    animalManager.jsonSerialize(fileName, animalManager.animalJsonSerializerSettings());
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Error in writing data to file!");
                    Console.WriteLine(ex.Message);
                }
            }
            else if (fileName.Substring(fileName.Length - 3) == "txt")
            {
                try
                {
                    animalManager.binarySerialize(fileName);
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Error writing data to file!");
                    Console.WriteLine(ex.Message);
                }
            }
        }

        //Save objects as text file
        private void mnuFileSaveAsTF_click(object sender, RoutedEventArgs e)
        {
            if (!continueWithFileAction())
                return;

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text files (*.txt)|*.txt";

            if(saveFileDialog.ShowDialog() == true)
            {
                try
                {
                    fileName = saveFileDialog.FileName;
                    animalManager.binarySerialize(fileName); 
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error in writing data to file!");
                    Console.WriteLine(ex.ToString());   
                }
            }
        }

        //Save objects as json file
        private void mnuFileSaveAsJ_click(object sender, RoutedEventArgs e)
        {
            if (!continueWithFileAction())
                return;

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "JSON files (*.json)|*.json";

            if (saveFileDialog.ShowDialog() == true)
            {
                try
                {
                    fileName = saveFileDialog.FileName;
                    animalManager.jsonSerialize(fileName, animalManager.animalJsonSerializerSettings());
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error in writing data to file!");
                    Console.WriteLine(ex.Message);
                }
            }
        }

        //Export fooditem to XML
        private void mnuFileExportXML_click(object sender, RoutedEventArgs e)
        {
            if (!continueWithFileAction())
                return;

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "XML files (*.xml)|*.xml";

            if (saveFileDialog.ShowDialog() == true)
            {
                try
                {
                    string fileNameXML = saveFileDialog.FileName;
                    foodManager.xmlSerialize(fileNameXML);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error in writing data to file!");
                    Console.WriteLine(ex.Message);
                }
            }
        }

        //Import FoodItems from XML
        private void mnuFileImportXML_click(object sender, RoutedEventArgs e)
        {
            if (!continueWithFileAction())
                return;

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "XML files (*.xml)|*.xml"; 

            if (openFileDialog.ShowDialog() == true)
            {
                string fileNameXML = openFileDialog.FileName;

                try
                {
                    if (!foodManager.xmlDeserialize(fileNameXML))
                        MessageBox.Show("Could not import data");
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Error in file handling!");
                    Console.WriteLine(ex.Message);
                    return;
                }

                for (int i = 0; i < foodManager.Count(); i++)
                {
                    FoodItem foodItem = foodManager.getListItemAt(i);

                    foreach(string id in foodItem.Ids)
                    {
                        foodManager.connectIdAndFoodItem(foodItem, id);
                    }
                }

                updateMainAnimalUI();
            }
        }

        //Exit App
        private void mnuFileExportExit_click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        #endregion

    }
}
