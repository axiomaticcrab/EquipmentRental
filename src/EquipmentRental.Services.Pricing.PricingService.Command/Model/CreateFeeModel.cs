using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EquipmentRental.Services.Pricing.PricingService.Command.Model
{
    public class CreateFeeModel
    {
        public int FeeId { get; set; }
        public string Tag { get; set; }
        public int Cost { get; set; }
    }

    public class UpdateFeeModel
    {
        public int FeeId { get; set; }
        public int Cost { get; set; }
    }
}
