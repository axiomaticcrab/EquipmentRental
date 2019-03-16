using Microsoft.AspNetCore.Mvc;

namespace EquipmentRental.Ui.Controllers
{
    public class BasketController : Controller
    {
        public IActionResult AddToBasket(AddToBasketViewModel model)
        {
            return RedirectToAction("ListEquipments", "Listing");
        }
    }
}