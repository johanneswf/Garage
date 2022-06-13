namespace Garage
{
    internal class Main
    {
        private readonly IUI ui;
        private IGarageHandler garageHandler;

        public Main(IUI ui)
        {
            this.ui = ui;
        }

        public void Run()
        {
            InitGarage();

            dynamic[,] menu =
            {
                { "1", "List all vehicles parked in the garage" },
                { "2", "Park vehicle in garage" },
                { "3", "Take vehicle from garage" }, // Has reg plate "search" functionality.
                { "4", "Filter/search for vehicles in the garage" }, // Also counts types of vehicles if you only filter for that type.
                { "0", "Quit" }
            };

            int menuLength = menu.GetLength(0);

            bool run = true;
            do
            {
                ui.Clear();
                ui.WriteLine("Garage Main Menu");
                for (int i = 0; i < menuLength; i++) ui.WriteLine($"{menu[i, 0]}) {menu[i, 1]}");

                // ToDo: Switch on dynamic array / convert to dict
                switch (ui.ReadLine())
                {
                    case "1":
                        PrintVehicles();
                        break;
                    case "2":
                        ParkVehicle();
                        break;
                    case "3":
                        TakeVehicle();
                        break;
                    case "4":
                        FilterVehicles();
                        break;
                    case "0":
                        run = false;
                        break;
                }
            } while (run);
        }

        private void TakeVehicle()
        {
            PrintVehicles();
            ui.Write("Vehicle to remove (RegPlate): ");
            var input = ui.ReadLine().ToUpper();
            garageHandler.UnPark(input);
        }

        private void PrintVehicles()
        {
            foreach (var vehicle in garageHandler.GetVehicles())
            {
                ui.WriteLine($"{vehicle.RegPlate} {vehicle.GetType().Name}");
            }
        }

        private void InitGarage()
        {
            int garageSize;
            ui.WriteLine("Create new garage");
            do
            {
                ui.Write("Parking spots in garage: ");

                if (!int.TryParse(ui.ReadLine(), out garageSize)) ui.WriteLine("Size of garage needs to be a number.");
                else if (garageSize < 1) ui.WriteLine("Size of garage needs to be greater than 0.");
                else if (garageSize > 1000) ui.WriteLine("Size of garage needs to be less than 1000.");

                else break;
            } while (true);

            do
            {
                ui.Write("Seed Garage? (y/n): ");
                string yn = ui.ReadLine().ToLower();
                if (yn is "y")
                {
                    garageHandler = new GarageHandler(garageSize);
                    foreach (var vehicle in GetSeed()) garageHandler.Park(vehicle);

                    //GetSeed().ForEach(v => garageHandler.Park(v));
                    break;
                }
                else if (yn is "n")
                {
                    garageHandler = new GarageHandler(garageSize);
                    break;
                }
            } while (true);
        }

        private static List<IVehicle> GetSeed()
        {
            return new()
                    {
                        new Boat(0, "white", "BÅT1", false),
                        new Boat(0, "purple", "BÅT2", true),
                        new Boat(0, "orange", "BÅT3", false),
                        new Boat(0, "white", "BÅT4", false),
                        new Airplane(3, "white", "FLYG1", 1),
                        new Airplane(5, "pink", "FLYG2", 4),
                        new Airplane(3, "blue", "FLYG3", 2),
                        new Motorcycle(2, "white", "MC1", 998),
                        new Motorcycle(3, "red", "MC2", 666),
                    };
        }

        private void ParkVehicle()
        {
            if (garageHandler.IsFull)
                ui.WriteLine("The garage is full.");
            else
            {
                garageHandler.Park(NewIVehicle());
                ui.WriteLine("Your vehicle has been parked.");
            }
        }

        // ToDo: Elim redunant code
        private IVehicle NewIVehicle()
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
                        //NewAirplane();
                        regPlate = VehicleRegPlate();
                        color = VehicleColor();
                        wheelCount = VehicleWheelCount();
                        int airplaneEngineCount = VehicleAirplaneEngineCount();
                        return new Airplane(wheelCount, color, regPlate, airplaneEngineCount);
                    case "boat":
                        regPlate = VehicleRegPlate();
                        color = VehicleColor();
                        wheelCount = VehicleWheelCount();
                        bool boatIsSubmarine = VehicleBoatIsSubmarine();
                        return new Boat(wheelCount, color, regPlate, boatIsSubmarine);
                    case "bus":
                        regPlate = VehicleRegPlate();
                        color = VehicleColor();
                        wheelCount = VehicleWheelCount();
                        int busSeatCount = BusSeatCount();
                        return new Bus(wheelCount, color, regPlate, busSeatCount);
                    case "car":
                        regPlate = VehicleRegPlate();
                        color = VehicleColor();
                        wheelCount = VehicleWheelCount();
                        int carCylinderCount = CarCylinderCount();
                        return new Car(wheelCount, color, regPlate, carCylinderCount);
                    case "motorcycle":
                        regPlate = VehicleRegPlate();
                        color = VehicleColor();
                        wheelCount = VehicleWheelCount();
                        double motorcycleCylinderVolume = MotorcycleCylinderVolume();
                        return new Motorcycle(wheelCount, color, regPlate, motorcycleCylinderVolume);
                    default:
                        ui.WriteLine("The vehicle must be one of the types mentioned.");
                        break;
                }
            } while (true);

        }


        private Airplane NewAirplane()
        {
            throw new NotImplementedException();
        }

        private void FilterVehicles()
        {

            string typeOfVehicle = string.Empty;
            string colorOfVehicle = string.Empty;
            int wheelCountOfVehicle = -1;

            ui.WriteLine("Filter Vehicles"
                         + "\nLeave the field empty if you don't want to filter that category.");

            ui.Write("Type: ");
            typeOfVehicle = ui.ReadLine();

            ui.Write("Color: ");
            colorOfVehicle = ui.ReadLine();

            do
            {
                ui.Write("Number of Wheels: ");
                string input = ui.ReadLine();
                if (string.IsNullOrWhiteSpace(input)) break;
                else if (!int.TryParse(input, out int number))
                    ui.WriteLine("Only whole numbers are allowed.");
                else if (number < 0)
                    ui.WriteLine("A vehicle can't have a negative amount of wheels.");
                else
                {
                    wheelCountOfVehicle = number;
                    break;
                }
            } while (true);

            var filteredVehicles = garageHandler.FilterVehicles(typeOfVehicle, colorOfVehicle, wheelCountOfVehicle);

            int filteredVehiclesCount = 0;
            foreach (var vehicle in filteredVehicles)
            {
                ui.WriteLine(
                    $"\n#{++filteredVehiclesCount}" +
                    $"\nRegPlate: {vehicle.RegPlate}" +
                    $"\nType: {vehicle.GetType().Name}" +
                    $"\nColor: {vehicle.Color}" +
                    $"\nWheel Count: {vehicle.WheelCount}");
            }

        }


        // Could make one method to contain all the functionality below and throw exceptions as needed.
        private string VehicleRegPlate()
        {
            string regPlate = string.Empty;
            do
            {
                ui.Write("RegPlate: ");
                regPlate = ui.ReadLine().ToUpper();
                if (string.IsNullOrWhiteSpace(regPlate)) ui.WriteLine("RegPlates can't be empty.");
                else if (regPlate.Length > 100) ui.WriteLine("RegPlates can't be longer than 100 characters.");
                else if (garageHandler.CheckRegPlate(regPlate) != null) ui.WriteLine("A vehicle with that RegPlate is already parked in the garage.");
                else return regPlate;
            } while (true);
        }

        private string VehicleColor()
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

        private int VehicleWheelCount()
        {
            int number;
            do
            {
                ui.Write("Number of Wheels: ");
                if (!int.TryParse(ui.ReadLine(), out number)) ui.WriteLine("Only whole numbers are allowed.");
                else if (number < 0) ui.WriteLine("A vehicle can't have a negative amount of wheels.");
                else return number;
            } while (true);
        }

        private int VehicleAirplaneEngineCount()
        {
            int number;
            do
            {
                ui.Write("Number of Airplane Engines: ");
                if (!int.TryParse(ui.ReadLine(), out number)) ui.WriteLine("Only whole numbers are allowed.");
                else if (number < 0) ui.WriteLine("A vehicle can't have a negative amount of engines.");
                else return number;
            } while (true);
        }

        private bool VehicleBoatIsSubmarine()
        {
            string yn = string.Empty;
            do
            {
                ui.Write("Is the Boat a Submarine? (y/n): ");
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
