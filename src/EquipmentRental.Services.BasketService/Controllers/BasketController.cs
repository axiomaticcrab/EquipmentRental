using System;
using System.Collections.Generic;
using EquipmentRental.Services.BasketService.Repository;
using Microsoft.AspNetCore.Mvc;
using KeyNotFoundException = EquipmentRental.Util.Repository.Exception.KeyNotFoundException;

namespace EquipmentRental.Services.BasketService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _basketRepository;

        public BasketController(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }

        [HttpGet("{id}")]
        public ActionResult<Basket> GetBasketById(int id)
        {
            try
            {
                return _basketRepository.ById(id);
            }
            catch (KeyNotFoundException)
            {
                return _basketRepository.Save(new Basket { EntityId = id, LineItems = new List<LineItem>() });
            }


        }

        [HttpPut]
        public Basket UpdateBasket([FromBody] Basket basket)
        {
            return _basketRepository.Save(basket);
        }
    }
}
