namespace Garage
{
    internal class Main
    {
        IUI ui;

        public Main(IUI ui)
        {
            this.ui = ui;
        }

        public void Run()
        {
            var garage = InitGarage();

            bool run = true;
            do
            {
                ui.Clear();
                ui.WriteLine("Garage Main Menu" +
                    "\n1. List all vehicles parked in the garage" +
                    "\n2. Park vehicle in garage" +
                    "\n3. Take vehicle from garage" +
                    "\n4. Filter/search for vehicles in the garage" +
                    "\n5. Quit the application");

                switch (ui.ReadLine())
                {
                    case "1":
                        ListVehicles(garage);
                        break;
                    case "2":
                        ParkVehicle(garage);
                        break;
                    case "3":
                        break;
                    case "4":
                        break;
                    case "5":
                        run = false;
                        break;
                }
            } while (run);
        }

        private void ListVehicles(Garage<IVehicle> garage)
        {
            int count = 1;
            foreach (var vehicle in garage)
            {
                ui.WriteLine($"\t#{count}. {vehicle.GetType().Name} || REG PLATE: {vehicle.RegPlate}");
                count++;
            }
        }

        private Garage<IVehicle> InitGarage()
        {
            int garageSize;
            ui.WriteLine("Create new garage");
            do
            {
                ui.Write("Size of garage: ");

                if (!int.TryParse(ui.ReadLine(), out garageSize)) ui.WriteLine("Size of garage needs to be a number.");
                else if (garageSize < 1) ui.WriteLine("Size of garage needs to be greater than 0.");
                else if (garageSize > 1000) ui.WriteLine("Size of garage needs to be less than 1000.");

                else break;
            } while (true);

            ui.WriteLine("Do you want the garage to be seeded with 4 vehicles for you?");

            do
            {
                ui.Write("Seed Garage? (y/n): ");
                string yn = ui.ReadLine().ToLower();
                if (yn is "y")
                {
                    return new Garage<IVehicle>(garageSize)
                    {
                        new Boat(0, "white", "båtbåten", false),
                        new Airplane(3, "white", "flygflyget", 1),
                        new Motorcycle(2, "white", "mcmcen", 998),
                        new Car(4, "white", "bilbilen", 12)
                    };
                }
                else if (yn is "n")
                {
                    return new Garage<IVehicle>(garageSize);
                }
            } while (true);
        }

        private void ParkVehicle(Garage<IVehicle> garage)
        {
            if (garage.Size <= garage.Count()) ui.WriteLine("The garage is full.");
            else
            {
                ui.WriteLine("Park vehicle in garage");
                garage.Add(NewVehicle());
            }
        }

        private IVehicle NewVehicle()
        {
            string regPlate;
            string color;
            int wheelCount;

            do
            {
                ui.WriteLine("Type of vehicle?" +
                    "\nAirplane" +
                    "\nBoat" +
                    "\nBus" +
                    "\nCar" +
                    "\nMotorcycle");

                ui.Write("Type: ");

                switch (ui.ReadLine().ToLower().Trim())
                {
                    case "airplane":
                        regPlate = RegPlate();
                        color = Color();
                        wheelCount = WheelCount();
                        int airplaneEngineCount = AirplaneEngineCount();
                        return new Airplane(wheelCount, color, regPlate, airplaneEngineCount);
                    case "boat":
                        regPlate = RegPlate();
                        color = Color();
                        wheelCount = WheelCount();
                        bool boatIsSubmarine = BoatIsSubmarine();
                        return new Boat(wheelCount, color, regPlate, boatIsSubmarine);
                    case "bus":
                        regPlate = RegPlate();
                        color = Color();
                        wheelCount = WheelCount();
                        int busSeatCount = BusSeatCount();
                        return new Bus(wheelCount, color, regPlate, busSeatCount);
                    case "car":
                        regPlate = RegPlate();
                        color = Color();
                        wheelCount = WheelCount();
                        int carCylinderCount = CarCylinderCount();
                        return new Car(wheelCount, color, regPlate, carCylinderCount);
                    case "motorcycle":
                        regPlate = RegPlate();
                        color = Color();
                        wheelCount = WheelCount();
                        double motorcycleCylinderVolume = MotorcycleCylinderVolume();
                        return new Motorcycle(wheelCount, color, regPlate, motorcycleCylinderVolume);
                    default:
                        ui.WriteLine("The vehicle must be one of the types mentioned above.");
                        break;
                }
            } while (true);

        }

        private string RegPlate()
        {
            string regPlate = string.Empty;
            do
            {
                ui.Write("RegPlate: ");
                regPlate = ui.ReadLine();
                if (string.IsNullOrWhiteSpace(regPlate)) ui.WriteLine("RegPlates can't be empty.");
                else if (regPlate.Length > 100) ui.WriteLine("RegPlates can't be longer than 100 characters.");
                else return regPlate.ToUpper();
            } while (true);
        }

        private string Color()
        {
            string color = string.Empty;
            do
            {
                ui.Write("Color: ");
                color = ui.ReadLine();
                if (string.IsNullOrWhiteSpace(color)) ui.WriteLine("Color can't be empty.");
                else if (color.Length > 100) ui.WriteLine("Color can't be longer than 100 characters.");
                else return color.ToLower();
            } while (true);
        }

        private int WheelCount()
        {
            int number;
            do
            {
                ui.Write("Number of wheels: ");
                if (!int.TryParse(ui.ReadLine(), out number)) ui.WriteLine("Only whole numbers are allowed.");
                else if (number < 0) ui.WriteLine("A vehicle can't have a negative amount of wheels.");
                else return number;
            } while (true);
        }

        private int AirplaneEngineCount()
        {
            int number;
            do
            {
                ui.Write("Number of airplane engines: ");
                if (!int.TryParse(ui.ReadLine(), out number)) ui.WriteLine("Only whole numbers are allowed.");
                else if (number < 0) ui.WriteLine("A vehicle can't have a negative amount of engines.");
                else return number;
            } while (true);
        }

        private bool BoatIsSubmarine()
        {
            string yn = string.Empty;
            do
            {
                ui.Write("Is the boat a submarine? (y/n): ");
                yn = ui.ReadLine().ToLower();
                if (yn is "y") return true;
                else if (yn is "n") return false;
            } while (true);
        }

        private int BusSeatCount()
        {
            int number;
            do
            {
                ui.Write("Number of bus seats: ");
                if (!int.TryParse(ui.ReadLine(), out number)) ui.WriteLine("Only whole numbers are allowed.");
                else if (number < 0) ui.WriteLine("A bus can't have a negative amount of seats.");
                else return number;
            } while (true);
        }

        private int CarCylinderCount()
        {
            int number;
            do
            {
                ui.Write("Number of cylinders in the engine: ");
                if (!int.TryParse(ui.ReadLine(), out number)) ui.WriteLine("Only whole numbers are allowed.");
                else if (number < 0) ui.WriteLine("An engine can't have a negative amount of cylinders.");
                else return number;
            } while (true);
        }

        private double MotorcycleCylinderVolume()
        {
            double number;
            do
            {
                ui.Write("Cylinder volume of engine: ");
                if (!double.TryParse(ui.ReadLine(), out number)) ui.WriteLine("Only double precision numbers are allowed.");
                else if (number < 0) ui.WriteLine("A motorcycle can't have a negative cylinder volume.");
                else return number;
            } while (true);
        }
    }
}
