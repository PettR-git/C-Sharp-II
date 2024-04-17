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
using System.Windows.Shapes;
using WTS.Entities;
using WTS.Entities.Main;

namespace WTS
{
    /// <summary>
    /// Interaction logic for FoodItemForm.xaml
    /// </summary>
    public partial class FoodItemForm : Window
    {
        public Action<FoodItem> FoodItemEvent;
        public FoodItemForm()
        {
            InitializeComponent();
        }

        //Add ingredient
        private void btnAddItem_Click(object sender, RoutedEventArgs e)
        {
            string ingredient = tbxIngredient.Text;

            if (ingredient == string.Empty)
                return;

            lbxIngredients.Items.Add(ingredient);
        }

        //Change ingredient
        private void btnChangeItem_Click(object sender, RoutedEventArgs e)
        {
            int index = lbxIngredients.SelectedIndex;

            if (index == -1 || tbxIngredient.Text == string.Empty)
                return;

            lbxIngredients.Items[index] = tbxIngredient.Text;
        }

        //Delete ingredient
        private void btnDeleteItem_Click(object sender, RoutedEventArgs e)
        {
            int index = lbxIngredients.SelectedIndex;

            if (index == -1)
                return;

            lbxIngredients.Items.RemoveAt(index);
        }

        //OK -> create object of fooditem with its ingredients
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            FoodItem foodItem = new FoodItem();
            foreach(string strItem in lbxIngredients.Items)
            {
                foodItem.Ingredients.Add(strItem);
            }

            foodItem.Name = tbxIngredientName.Text;

            FoodItemEvent?.Invoke(foodItem);
            this.Close();
        }

        //Cancel -> just close without saving data
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
