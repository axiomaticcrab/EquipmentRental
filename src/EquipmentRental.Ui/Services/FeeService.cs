using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using EquipmentRental.Ui.Models.Basket;
using Newtonsoft.Json;

namespace EquipmentRental.Ui.Services
{
    public class PricingService
    {
        private readonly HttpClient _pricingCommandService;

        public PricingService(HttpClient client)
        {
            client.BaseAddress = new Uri("http://equipmentrental.services.pricing.pricingservice.command");
            _pricingCommandService = client;
        }

        public async Task InitPricingService()
        {
             await _pricingCommandService.GetAsync($"/api/Initializer");
        }
    }
}
