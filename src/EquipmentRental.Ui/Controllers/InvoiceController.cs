using System.Text;
using System.Threading.Tasks;
using EquipmentRental.Ui.Models;
using EquipmentRental.Ui.Services;
using Microsoft.AspNetCore.Mvc;

namespace EquipmentRental.Ui.Controllers
{
    public class InvoiceController : Controller
    {
        private readonly BasketService _basketService;
        private readonly PricingReadService _pricingReadService;

        public InvoiceController(BasketService basketService, PricingReadService pricingReadService)
        {
            _basketService = basketService;
            _pricingReadService = pricingReadService;
        }

        [HttpPost]
        public async Task<IActionResult> GenerateInvoice()
        {
            var basketModel = await _basketService.GetBasketById(1);

            var calculateBasket = await _pricingReadService.CalculateBasket(new CalculationRequest { LineItems = basketModel.LineItems });

            return File(Encoding.UTF8.GetBytes(GenerateInvoiceContent(calculateBasket)),
                "text/plain",
                string.Format("{0}.txt", basketModel.EntityId));
        }

        private string GenerateInvoiceContent(CalculationResult result)
        {
            var builder = new StringBuilder();

            builder.AppendLine("Thank you for your purchase!");

            result.GenerateInvoiceLineItemResults.ForEach(x => { builder.AppendLine($"{x.Name} - {x.Cost}"); });

            builder.AppendLine($"Total : {result.TotalPrice} - Loyalty Points : {result.LoyaltyPoints}");

            return builder.ToString();
        }
    }
}