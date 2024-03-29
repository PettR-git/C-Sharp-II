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

namespace WTS
{
    /// <summary>
    /// To register wildlife in a zoo
    /// Name: Petter Rignell
    /// Date: 2024-01-25
    /// Updated: 2024-03-20
    /// </summary>
    public partial class MainWindow : Window
    {
        private AnimalManager animalManager;    
        private FoodManager foodManager;
        private AnimalSpecifics specifics;  //Form for attribute/specifics
        private FoodItemForm foodItemForm;  //Form for fooditems
        public MainWindow()
        {
            InitializeComponent();
            animalManager = new AnimalManager();
            foodManager = new FoodManager();
            InitializeGUI();
        }

        #region INITIALIZE GUI
        //To fill the comboboxes with enum values
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
                displayFoodItems(animalManager.getListItemAt(index).Id);
            }
        }

        //Display all animal ToStrings
        private void updateMainAnimalUI()
        {
            lbxAttributes.Items.Clear();
            lvwAnimals.Items.Clear();

            foreach (string strA in animalManager.getListToStrings())
            {
                lvwAnimals.Items.Add(strA);
            }
        }

        #endregion

        #region SORTING

        //Sort listView by Species
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
             
            animal.Id = animalManager.addId(animal.AnimalType);
            animalManager.addItem(animal);
            sortAnimalView();
            updateMainAnimalUI();
            lbxSpecies.SelectedItem = null;
        }

        private void btnChange_Click(object sender, RoutedEventArgs e)
        {
            int index = lvwAnimals.SelectedIndex;
            Animal oldAnimal = animalManager.getListItemAt(index);
            Animal newAnimal = createAnimal();
            
            if (index == -1 || newAnimal == null)
                return;

            animalManager.changeExistingId(oldAnimal, newAnimal);
            animalManager.changeItem(newAnimal, index);
            sortAnimalView();
            updateMainAnimalUI();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            int index = lvwAnimals.SelectedIndex;
            Animal animal = animalManager.getListItemAt(index);

            if (index == -1)
                return;

            animalManager.removeId(animal);
            animalManager.removeItem(animal);
            sortAnimalView();
            updateMainAnimalUI();
        }

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
                    retrieveFoodItem(ids, foodItem);
                };

                foodItemForm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Choose one or more animals from the animal list");
            } 
        }

        private void retrieveFoodItem(List<string> ids, FoodItem foodItem)
        {
            foodManager.addItem(foodItem);

            foreach (string id in ids)
            { 
                foodManager.connectIdAndFoodItem(foodItem, id);
            }

            if(ids.Count > 0)
                displayFoodItems(ids[0]);
        }

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

    }
}
