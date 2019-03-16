using System.Collections.Generic;
using EquipmentRental.Services.EquipmentService.Model;
using Microsoft.AspNetCore.Mvc;

namespace EquipmentRental.Services.EquipmentService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquipmentController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<Equipment>> Get()
        {
            return new List<Equipment>
            {
                new Equipment {Id = 1, Name = "Caterpillar bulldozer", EquipmentType = EquipmentType.Heavy},
                new Equipment {Id = 2, Name = "KamAZ truck", EquipmentType = EquipmentType.Regular},
                new Equipment {Id = 3, Name = "Komatsu crane", EquipmentType = EquipmentType.Heavy},
                new Equipment {Id = 4, Name = "Volvo steamroller", EquipmentType = EquipmentType.Regular},
                new Equipment {Id = 5, Name = "Bosh jackhammer", EquipmentType = EquipmentType.Specialized}
            };
        }
    }
}
