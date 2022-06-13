namespace Garage.Entities
{
    internal class GarageHandler : IGarageHandler
    {
        private readonly Garage<IVehicle> _garage;

        public bool IsFull => _garage.Count > _garage.Size ? true : false;

        public GarageHandler(int capacity) => _garage = new Garage<IVehicle>(capacity);

        public void Park(IVehicle item) => _garage.Add(item);

        // ToDo: Fix null warning
        public IVehicle CheckRegPlate(string input)
        {
            return _garage.FirstOrDefault(v => v.RegPlate == input);
        }

        public void UnPark(string input)
        {
            var found = CheckRegPlate(input);
            if (found != null) _garage.Remove(found);
        }

        public IEnumerable<IVehicle> GetVehicles() => _garage.ToArray();

        public IEnumerable<IVehicle> FilterVehicles(string typeOfVehicle = "", string colorOfVehicle = "", int wheelCountOfVehicle = -1)
        {
            return _garage
                .Where(x => (string.IsNullOrWhiteSpace(typeOfVehicle) || x.GetType().Name == typeOfVehicle))
                .Where(x => (string.IsNullOrWhiteSpace(colorOfVehicle) || x.Color == colorOfVehicle))
                .Where(x => (wheelCountOfVehicle == -1 || x.WheelCount == wheelCountOfVehicle));
        }

    }
}
