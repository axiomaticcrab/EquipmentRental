using System;
using EquipmentRental.Services.BasketService.Repository;
using EquipmentRental.Util.Repository.Exception;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet]
        public ActionResult<Basket> GetBasketById(int id)
        {
            try
            {
                return _basketRepository.ById(id);
            }
            catch (KeyNotFoundException)
            {
                return _basketRepository.Save(id);
            }


        }

        [HttpPut]
        public void UpdateBasket([FromBody] Basket basket)
        {
        }
    }
}
