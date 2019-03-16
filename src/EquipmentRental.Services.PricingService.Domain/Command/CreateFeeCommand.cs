using System;
using System.Collections.Generic;
using System.Text;

namespace EquipmentRental.Services.PricingService.Domain.Command
{
    public class CreateFeeCommand : BaseCommand
    {
        public readonly int FeeId;
        public readonly string Tag;
        public readonly int Cost;

        public CreateFeeCommand(Guid id, int feeId, string tag, int cost)
        {
            Id = id;
            FeeId = feeId;
            Tag = tag;
            Cost = cost;
        }

    }

    public class UpdateFeeCostCommand : BaseCommand
    {
        public readonly int FeeId;
        public readonly int Cost;

        public UpdateFeeCostCommand(Guid id, int feeId, int cost)
        {
            Id = id;
            FeeId = feeId;
            Cost = cost;
        }
    }
}
