using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WTS.Entities.Main
{
    public class FoodItem
    {
        private ListManager<string> ingredients;
        private string name;
        public FoodItem() 
        { 
            ingredients = new ListManager<string>();
        }

        public ListManager<string> Ingredients { get { return ingredients; } }
        public string Name {  get { return name; } set { name = value; } }

        public override string ToString()
        {
            string strOut = string.Empty;
            string strIngr = string.Empty;

            string[] strArr = Ingredients.getListToStrings();
            strIngr = string.Join(", ", strArr);

            strOut = string.Format("{0,-10}{1,-60}", name, strIngr);

            return strOut;
        }
    }
}
