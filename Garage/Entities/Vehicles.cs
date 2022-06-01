namespace Garage.Entities
{
    internal class Vehicle
    {
        public int WheelCount { get; set; }
        public string Color { get; set; }
        public string RegPlate { get; set; }

        public Vehicle(int wheelCount, string color, string regPlate)
        {
            WheelCount = wheelCount;
            Color = color;
            RegPlate = regPlate;
        }
    }

    internal class Airplane : Vehicle
    {
        public int EngineCount { get; set; }

        public Airplane(int wheelCount, string color, string regPlate, int engineCount) : base(wheelCount, color, regPlate)
        {
            EngineCount = engineCount;
        }
    }

    internal class Motorcycle : Vehicle
    {
        public int CylinderVolume { get; set; }

        public Motorcycle(int wheelCount, string color, string regPlate, int cylinderVolume) : base(wheelCount, color, regPlate)
        {
            CylinderVolume = cylinderVolume;
        }
    }

    internal class Car : Vehicle
    {
        public int CylinderCount { get; set; }

        public Car(int wheelCount, string color, string regPlate, int cylinderCount) : base(wheelCount, color, regPlate)
        {
            CylinderCount = cylinderCount;
        }
    }

    internal class Bus : Vehicle
    {
        public int SeatCount { get; set; }

        public Bus(int wheelCount, string color, string regPlate, int seatCount) : base(wheelCount, color, regPlate)
        {
            SeatCount = seatCount;
        }
    }

    internal class Boat : Vehicle
    {
        public bool IsSubmarine { get; set; }

        public Boat(int wheelCount, string color, string regPlate, bool isSubmarine) : base(wheelCount, color, regPlate)
        {
            IsSubmarine = isSubmarine;
        }
    }
}
