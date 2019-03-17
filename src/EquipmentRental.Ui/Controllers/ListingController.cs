using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EquipmentRental.Ui.Models;
using EquipmentRental.Ui.Models.Basket;
using EquipmentRental.Ui.Services;
using Microsoft.AspNetCore.Mvc;

namespace EquipmentRental.Ui.Controllers
{
    public class ListingController : Controller
    {
        private readonly BasketService _basketService;
        private readonly EquipmentService _equipmentService;
        private readonly PricingCommandService _pricingCommandService;

        public ListingController(BasketService basketService, EquipmentService equipmentService,PricingCommandService pricingCommandService)
        {
            _basketService = basketService;
            _equipmentService = equipmentService;
            _pricingCommandService = pricingCommandService;
        }

        [HttpGet()]
        public async Task<IActionResult> ListEquipments()
        {
            var basketTask = _basketService.GetBasketById(1); //assuming there is only one user and it's id is set to 1.
            var equipmentsTask = _equipmentService.GetEquipments();

            await Task.WhenAll(basketTask, equipmentsTask);

            var basketModel = await basketTask;
            var equipmentModel = await equipmentsTask;

            return View(BuildEquipmentViewModel(basketModel, equipmentModel));
        }

        private List<EquipmentViewModel> BuildEquipmentViewModel(BasketModel basketModel, List<EquipmentModel> equipmentModels)
        {
            var result = new List<EquipmentViewModel>();
            foreach (var equipmentModel in equipmentModels)
            {
                result.Add(new EquipmentViewModel
                {
                    Name = equipmentModel.Name,
                    EquipmentType = equipmentModel.EquipmentType,
                    RentalDays = basketModel.LineItems.Any(x => x.EquipmentName == equipmentModel.Name) ?
                        basketModel.LineItems.First(x => x.EquipmentName == equipmentModel.Name).RentalDays :
                        0,
                });
            }

            return result;
        }
    }
}
