using System;
using System.Collections.Generic;
using EquipmentRental.Ui.Models;
using Microsoft.AspNetCore.Mvc;

namespace EquipmentRental.Ui.Controllers
{
    public class ListingController : Controller
    {

        [HttpGet()]
        public IActionResult ListEquipments()
        {
            ViewData["BasketId"] = Guid.NewGuid();

            var viewModel = new List<EquipmentViewModel>
            {
                    new EquipmentViewModel {Name = "Caterpillar bulldozer",RentalDays=0,EquipmentType = EquipmentType.Heavy},
                    new EquipmentViewModel {Name = "KamAZ truck",RentalDays=3,EquipmentType = EquipmentType.Regular},
                    new EquipmentViewModel {Name = "Komatsu crane",RentalDays=0,EquipmentType = EquipmentType.Heavy},
                    new EquipmentViewModel {Name = "Volvo steamroller",RentalDays=0,EquipmentType = EquipmentType.Regular},
                    new EquipmentViewModel {Name = "Bosh jackhammer",RentalDays=0,EquipmentType = EquipmentType.Specialized},
            };

            return View(viewModel);
        }
    }
}
