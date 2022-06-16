namespace Garage.Entities
{
    internal interface IGarageHandler
    {
        bool IsFull { get; }

        IEnumerable<IVehicle> FilterVehicles(string typeOfVehicle = "", string colorOfVehicle = "", int wheelCountOfVehicle = -1);
        IEnumerable<IVehicle> GetVehicles();
        //List<IVehicle> ListVehicles();
        bool CheckRegPlate(string input);
        void Park(IVehicle item);
        void UnPark(string input);
    }
}