using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WTS.Entities.Enums;
using WTS.Entities.Main.Enums;

namespace WTS.Entities.Main
{
    //Animal properties 
    public interface IAnimal
    {
        string Name { get; set; }
        string Id { get; set; }
        int Age { get; set; }
        Gender Gender { get; set; }
        AnimalType AnimalType { get; set; }
        Species Species { get; set; }
        bool Domesticated {  get; set; }
        //virtual method
        string getExtraInfo();
    }
}
