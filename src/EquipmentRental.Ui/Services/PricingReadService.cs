using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using EquipmentRental.Ui.Models;
using EquipmentRental.Ui.Models.Basket;
using Newtonsoft.Json;

namespace EquipmentRental.Ui.Services
{
    public class PricingReadService
    {
        private readonly HttpClient _pricingReadService;

        public PricingReadService(HttpClient client)
        {
            client.BaseAddress = new Uri("http://equipmentrental.services.pricingservice");
            _pricingReadService = client;
        }

      
        public async Task<CalculationResult> CalculateBasket(CalculationRequest calculationRequest)
        {
            var response = 
                await _pricingReadService.PostAsync("/api/calculator",
                    new StringContent(JsonConvert.SerializeObject(calculationRequest), Encoding.UTF8, "application/json"));

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<CalculationResult>();
        }
    }
}