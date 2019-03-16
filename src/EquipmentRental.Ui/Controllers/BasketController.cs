using System.Linq;
using System.Threading.Tasks;
using EquipmentRental.Ui.Models;
using EquipmentRental.Ui.Models.Basket;
using EquipmentRental.Ui.Services;
using Microsoft.AspNetCore.Mvc;

namespace EquipmentRental.Ui.Controllers
{
    public class BasketController : Controller
    {
        private readonly BasketService _basketService;

        public BasketController(BasketService basketService)
        {
            _basketService = basketService;
        }

        public async Task<IActionResult> UpdateBasket(EquipmentViewModel equipmentViewModel)
        {
            var basket = await BuildBasket(equipmentViewModel);

            await _basketService.UpdateBasket(basket);

            return RedirectToAction("ListEquipments", "Listing");
        }

        private async Task<BasketModel> BuildBasket(EquipmentViewModel equipmentViewModel)
        {
            var basket = await _basketService.GetBasketById(1);

            if (basket.LineItems.Any(x => x.EquipmentName == equipmentViewModel.Name))
            {
                basket.LineItems.First(x => x.EquipmentName == equipmentViewModel.Name).RentalDays =
                    equipmentViewModel.RentalDays;
            }
            else
            {
                basket.LineItems.Add(new LineItemModel
                {
                    EquipmentName = equipmentViewModel.Name,
                    RentalDays = equipmentViewModel.RentalDays,
                });
            }

            return basket;
        }
    }
}