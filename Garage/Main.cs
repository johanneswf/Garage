using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garage
{
    internal class Main
    {
        private IUI ui;

        public Main(IUI ui)
        {
            this.ui = ui; 
        }

        public void Run()
        {
            SeedGarage();
            do
            {
                Menu();
                ui.Read();
            } while (true);
        }

        private void Menu()
        {
            ui.Print("Menu");
        }

        private void SeedGarage()
        {
            Garage<Vehicle> garage = new(10);
            garage.Add(new Boat(0, "white", "båtbåten", false));
            foreach (var vehicle in garage)
            {
                ui.Print(vehicle.GetType().Name.ToString());
            }
        }
    }
}
