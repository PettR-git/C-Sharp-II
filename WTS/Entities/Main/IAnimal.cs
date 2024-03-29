using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WTS.Entities.Enums;

namespace WTS.Entities.Main
{
    public interface IAnimal
    {
        string Name { get; set; }
        string Id { get; set; }
        int Age { get; set; }
        Enum Gender { get; set; }
        bool Domesticated {  get; set; }
        string getExtraInfo();
    }
}
