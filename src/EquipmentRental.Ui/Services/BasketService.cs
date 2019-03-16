using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using EquipmentRental.Ui.Models.Basket;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet]
        public async Task<BasketModel> GetBasketById(int basketId)
        {
            var response = await _client.GetAsync($"/api/basket/{basketId}");

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<BasketModel>();
        }
    }
}
