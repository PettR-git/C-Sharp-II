using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WTS.Entities.Main
{
    //Connect fooditem to animal id and handle all fooditems
    public class FoodManager : ListManager<FoodItem>
    {
        private Dictionary<FoodItem, List<string>> m_foodToIds;
        public FoodManager() {     
            m_foodToIds = new Dictionary<FoodItem, List<string>>();
        }

        //To connect fooditem and ids of animals, creating a non-objective link to Animal id
        public void connectIdAndFoodItem(FoodItem foodItem, string id)
        {
            if (m_foodToIds.ContainsKey(foodItem))
            {
                List<string> ids = m_foodToIds[foodItem];
                ids.Add(id);
            }
            else
            {
                m_foodToIds[foodItem] = new List<string> { id };
            }
            Console.WriteLine(m_foodToIds.Keys.ToString());
        }

        //Retrieve all fooditems for an id
        public List<FoodItem> getFoodItemsAtId(string id)
        {
            List<FoodItem> foodItems = new List<FoodItem>();

            foreach(var pair in m_foodToIds)
            {
                if (pair.Value.Contains(id))
                {
                    foodItems.Add(pair.Key);
                }
            }

            return foodItems;
        }
    }
}
