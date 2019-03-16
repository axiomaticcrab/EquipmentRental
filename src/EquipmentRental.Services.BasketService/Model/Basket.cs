using System.Collections.Generic;
using EquipmentRental.Util.Repository;

namespace EquipmentRental.Services.BasketService.Controllers
{
    public class Basket : Entity
    {
        public List<LineItem> LineItems { get; set; }
    }
}