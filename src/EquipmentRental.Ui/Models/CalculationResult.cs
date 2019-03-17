using System.Collections.Generic;
using EquipmentRental.Ui.Models.Basket;
using EquipmentRental.Ui.Services;

namespace EquipmentRental.Ui.Models
{
    public class CalculationResult
    {
        public List<LineItemCalculationResult> GenerateInvoiceLineItemResults { get; set; }

        public int TotalPrice { get; set; }
        public int LoyaltyPoints { get; set; }
    }

    public class CalculationRequest
    {
        public List<LineItemModel> LineItems { get; set; }
    }
}