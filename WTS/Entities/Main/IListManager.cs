using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WTS.Entities.Main
{
    interface IListManager<T>
    {
        bool addItem(T item);
        bool removeItem(T item);
        bool changeItem(T item, int index);
        T getListItemAt(int index);
        string[] getListToStrings();
    }
}
