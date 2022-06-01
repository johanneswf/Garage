namespace Garage.Entities
{
    internal interface IVehicle
    {
        string Color { get; set; }
        string RegPlate { get; set; }
        int WheelCount { get; set; }
    }
}