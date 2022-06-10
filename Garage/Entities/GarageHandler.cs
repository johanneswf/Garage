namespace Garage.Entities
{
    internal class GarageHandler
    {
        private readonly Garage<IVehicle> _garage;

        public GarageHandler(int capacity)
        {
            _garage = new Garage<IVehicle>(capacity);
        }

        public void Add(IVehicle item)
        {
            _garage.Add(item);
        }


        // ToDo: Make LINQ queries
        // ToDo: Create standard output method for vehicles
        public List<IVehicle> ListVehicles()
        {
            List<IVehicle> result = new();
            foreach (IVehicle vehicle in _garage)
            {
                result.Add(vehicle);
            }
            return result;
        }

        internal IEnumerable<IVehicle> GetVehicles()
        {
           return _garage.ToList();
        }

        internal void UnPark(string input)
        {
            var found = _garage.FirstOrDefault(v => v.RegPlate == input);
            if (found != null)
            {
                var ok = _garage.Remove(found);

            }
        }

        //public List<string> ListVehicles()
        //{
        //    List<string> result = new();
        //    foreach (IVehicle vehicle in _garage)
        //    {
        //        result.Add(
        //            $"\nType: {vehicle.GetType().Name}" +
        //            $"\nColor: {vehicle.Color}" +
        //            $"\nRegPlate: {vehicle.RegPlate}" +
        //            $"\nWheels: {vehicle.WheelCount}");

        //    }
        //    return result;
        //}
    }
}
