using System.Collections.Generic;

namespace EquipmentRental.Services.PricingService.Model
{
    public class CalculationResult
    {
        public List<LineItemCalculationResult> GenerateInvoiceLineItemResults { get; set; }

        public int TotalPrice { get; set; }
        public int LoyaltyPoints { get; set; }

        public CalculationResult()
        {
            GenerateInvoiceLineItemResults=new List<LineItemCalculationResult>();
        }
    }

    public class CalculationRequest
    {
        public List<LineItemModel> LineItems { get; set; }
    }
}