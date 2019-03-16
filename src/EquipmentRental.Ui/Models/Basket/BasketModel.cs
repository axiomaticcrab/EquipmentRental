using System.Collections.Generic;

namespace EquipmentRental.Ui.Models.Basket
{
    public class BasketModel
    {
        public int EntityId { get; set; }
        public List<LineItemModel> LineItems { get; set; }

        public BasketModel()
        {
            LineItems = new List<LineItemModel>();
        }
    }
}