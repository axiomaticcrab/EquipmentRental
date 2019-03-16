using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EquipmentRental.Services.EquipmentService.Model
{
    public class Equipment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public EquipmentType EquipmentType { get; set; }
    }

    public enum EquipmentType
    {
        Heavy = 10,
        Regular = 20,
        Specialized = 30
    }
}
