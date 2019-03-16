namespace EquipmentRental.Util.Repository.Exception
{
    public class KeyNotFoundException : System.Exception
    {
        public KeyNotFoundException(string key) : base($"Value with given key {key} not found on Redis database.")
        {
            
        }
    }
}