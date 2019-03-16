using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace EquipmentRental.Ui.Controllers
{
    public class InvoiceController : Controller
    {
        [HttpPost]
        public IActionResult GenerateInvoice()
        {
            return File(Encoding.UTF8.GetBytes("Hello text :))"),
                "text/plain",
                string.Format("{0}.txt", ViewData["BasketId"]));
        }
    }
}