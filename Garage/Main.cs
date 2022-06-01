namespace Garage
{
    internal class Main
    {
        private readonly IUI _ui;

        public Main(IUI ui)
        {
            _ui = ui; 
        }

        public void Run()
        {
            do
            {
                Menu();
                SeedGarage();
                _ui.Read();
            } while (true);
        }

        private void Menu()
        {
            _ui.Print("Menu");
        }

        private void SeedGarage()
        {
            Garage<IVehicle> garage = new(10);
            garage.Add(new Boat(0, "white", "båtbåten", false));
            garage.Add(new Airplane(3, "white", "flygflyget", 1));
            garage.Add(new Motorcycle(2, "white", "mcmcen", 998));
            garage.Add(new Car(4, "white", "bilbilen", 12));

            foreach (var vehicle in garage)
            {
                var result =    
                    $"\nType: {vehicle.GetType().Name}" +
                    $"\nColor: {vehicle.Color}" +
                    $"\nRegPlate: {vehicle.RegPlate}";
                if (vehicle.WheelCount > 0) result += 
                    $"\nWheels: {vehicle.WheelCount}";
                _ui.Print(result);
            }
        }
    }
}
