using EquipmentRental.Services.PricingService.Domain.ReadModel;
using EquipmentRental.Services.PricingService.Domain.ReadModel.Repository.Contract;
using Microsoft.AspNetCore.Mvc;

namespace EquipmentRental.Services.PricingService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeeController : ControllerBase
    {
        private readonly IFeeRepository _repository;

        public FeeController(IFeeRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("{id}")]
        public FeeReadModel Get(int id)
        {
            return _repository.GetById(id);
        }
    }
}
