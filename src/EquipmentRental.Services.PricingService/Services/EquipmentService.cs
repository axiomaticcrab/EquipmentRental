using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace EquipmentRental.Services.PricingService.Services
{
    public class EquipmentService
    {
        private readonly HttpClient _client;

        public EquipmentService(HttpClient client)
        {
            client.BaseAddress = new Uri("http://equipmentrental.services.equipmentservice");
            _client = client;
        }

        public async Task<List<EquipmentModel>> GetEquipments()
        {
            var response = await _client.GetAsync("/api/equipment");

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<List<EquipmentModel>>();
        }
    }
}