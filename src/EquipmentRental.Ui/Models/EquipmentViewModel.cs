namespace EquipmentRental.Ui.Models
{
    public class EquipmentViewModel
    {
        public string Name { get; set; }
        public int RentalDays { get; set; }
        public EquipmentType EquipmentType { get; set; }
    }

    public enum EquipmentType
    {
        Heavy = 10,
        Regular = 20,
        Specialized = 30
    }
}