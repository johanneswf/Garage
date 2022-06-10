namespace Garage
{
    internal class Main
    {
        IUI _ui;

        public Main(IUI ui)
        {
            _ui = ui; 
        }

        public void Run()
        {
            int garageSize = 5; // While testing
            while (garageSize <= 0)
            {
                int.TryParse(_ui.ReadLine(), out garageSize);
            }

            var garage = new Garage<IVehicle>(garageSize);

            var handler = new GarageHandler(garage);

            SeedGarage(handler); // While testing

            do
            {
                MainMenu();
                switch (_ui.ReadLine())
                {
                    case "1":
                        foreach (var vehicle in handler.ListVehicles())
                        {

                        }
                        break;
                    case "2":
                        break;
                }
            } while (true);
        }

        private void MainMenu()
        {
            _ui.WriteLine("Menu" +
                "\n1. List All Vehicles in Garage" +
                "\n2. Add Vehicle to Garage");
        }

        private void SeedGarage(GarageHandler handler)
        {
            handler.Add(new Boat(0, "white", "båtbåten", false));
            handler.Add(new Airplane(3, "white", "flygflyget", 1));
            handler.Add(new Motorcycle(2, "white", "mcmcen", 998));
            handler.Add(new Car(4, "white", "bilbilen", 12));
        }
    }
}
