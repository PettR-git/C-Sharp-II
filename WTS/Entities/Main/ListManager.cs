using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WTS.Entities.Main
{
    public class ListManager<T> : IListManager<T>
    {
        private List<T> list;
        public ListManager() 
        { 
            list = new List<T>();
        }

        public bool addItem(T item)
        {
            bool ok = false;
            if (checkIndex(list.IndexOf(item))){
                list.Add(item);
                ok = true;
            }
           
            return ok;
        }

        public bool removeItem(T item)
        {
            bool ok = false;
            if (checkIndex(list.IndexOf(item)))
            {
                list.Remove(item);
                ok = true;
            }
            return ok;
        }

        public bool changeItem(T item, int index)
        {
            bool ok = false;

            if (checkIndex(index))
            {
                list[index] = item;
                ok = true;
            }

            return ok;
        }

        private bool checkIndex(int index)
        {
            bool ok = false;
            if (index < list.Count)
            {
                ok = true;
            }
            return ok;
        }

        //Get item at specific index of list
        public T getListItemAt(int index)
        {
            T item;

            if (!checkIndex(index))
                return default(T);
                
            item = list[index];

            return item;
        }

        //Get all item ToStrings as string array
        public string[] getListToStrings()
        {
            string[] itemArr = new string[list.Count];

            int count = 0;

            foreach (T item in list)
            {
                itemArr[count++] = item.ToString();
            }

            return itemArr;
        }

        public List<T> getList() {return list;}
    }
}
