using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using EquipmentRental.Ui.Models.Basket;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EquipmentRental.Ui.Services
{
    public class BasketService
    {
        private readonly HttpClient _client;

        public BasketService(HttpClient client)
        {
            client.BaseAddress = new Uri("http://equipmentrental.services.basketservice");
            _client = client;
        }

        public async Task<BasketModel> GetBasketById(int basketId)
        {
            var response = await _client.GetAsync($"/api/basket/{basketId}");

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<BasketModel>();
        }

        public async Task<BasketModel> UpdateBasket(BasketModel basketModel)
        {
            var response=
            await _client.PutAsync("/api/basket",
                new StringContent(JsonConvert.SerializeObject(basketModel), Encoding.UTF8, "application/json"));

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<BasketModel>();
        }
    }
}
