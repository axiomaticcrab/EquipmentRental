using System.Collections.Generic;

namespace EquipmentRental.Services.BasketService.Controllers
{
    public class Basket
    {
        public int Id { get; set; }
        public List<LineItem> LineItems { get; set; }
    }
}