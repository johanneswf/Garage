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
            SeedGarage();
            do
            {
                Menu();
                _ui.Read();
            } while (true);
        }

        private void Menu()
        {
            _ui.Print("Menu");
        }

        private void SeedGarage()
        {
            Garage<Vehicle> garage = new(10);
            garage.Add(new Boat(0, "white", "båtbåten", false));
            foreach (var vehicle in garage)
            {
                _ui.Print(vehicle.GetType().Name);
            }
        }
    }
}
