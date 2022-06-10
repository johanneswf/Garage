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
                ui.WriteLine("Garage Main Menu" +
                    "\n1. List all vehicles parked in the garage" +
                    "\n2. Park vehicle in garage" +
                    "\n3. Take vehicle from garage" + // Has reg plate "search" functionality.
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
                        garage = TakeVehicle(garage);
                        break;
                    case "4":
                        FilterVehicles(garage);
                        break;
                    case "5":
                        run = false;
                        break;
                }
            } while (run);
        }

        private Garage<IVehicle> TakeVehicle(Garage<IVehicle> garage)
        {
            ListVehicles(garage);
            ui.Write("Vehicle to remove (RegPlate): ");
            var input = ui.ReadLine().ToUpper();
            var vehicleArray = garage.Where(x => x.RegPlate != input).ToArray();

            // ToDo: Fix ugly solution
            if (vehicleArray.Count() < garage.Count())
            {
                Garage<IVehicle> newGarage = new Garage<IVehicle>(garage.Size);

                foreach (var vehicle in vehicleArray)
                {
                    newGarage.Add(vehicle);
                }

                ui.WriteLine($"{input} has been removed from the garage.");

                return newGarage;
            }
            else return garage;

        }

        private void ListVehicles(Garage<IVehicle> garage)
        {
            foreach (var vehicle in garage)
            {
                ui.WriteLine($"{vehicle.RegPlate} {vehicle.GetType().Name}");
            }
        }

        private Garage<IVehicle> InitGarage()
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
                    return new Garage<IVehicle>(garageSize)
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
                garage.Add(NewVehicle());
                ui.WriteLine("Your vehicle has been parked.");
            }
        }

        // ToDo: Fix redunant code
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
                        ui.WriteLine("The vehicle must be one of the types mentioned above.");
                        break;
                }
            } while (true);

        }

        private void FilterVehicles(Garage<IVehicle> garage)
        {
            string typeOfVehicle = string.Empty;
            string colorOfVehicle = string.Empty;
            int wheelCountOfVehicle = -1;

            ui.WriteLine("Filter Vehicles" +
                "\nLeave the field empty if you don't want to filter that category.");

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
                    ui.WriteLine("Only whole numbers are allowed. " +
                        "Leave empty if you don't want to filter this category.");
                else if (number < 0) 
                    ui.WriteLine("A vehicle can't have a negative amount of wheels. " +
                    "Leave empty if you don't want to filter this category.");
                else
                {
                    wheelCountOfVehicle = number;
                    break;
                }
            } while (true);

            var filteredVehicles = garage
                .Where(x => (string.IsNullOrWhiteSpace(typeOfVehicle) || x.GetType().Name == typeOfVehicle))
                .Where(x => (string.IsNullOrWhiteSpace(colorOfVehicle) || x.Color == colorOfVehicle))
                .Where(x => (wheelCountOfVehicle == -1 || x.WheelCount == wheelCountOfVehicle));

            foreach (var vehicle in filteredVehicles)
            {
                ui.WriteLine($"{vehicle.RegPlate} {vehicle.GetType().Name} {vehicle.Color} {vehicle.WheelCount}");
            }

        }

        //ToDo: Generalise the method to eliminate repetition with NewVehicle method.
        private string TypeOfVehicle()
        {
            do
            {
                switch (ui.ReadLine().ToLower().Trim())
                {
                    case "airplane":
                        return "Airplane";
                    case "boat":
                        return "Boat";
                    case "bus":
                        return "Bus";
                    case "car":
                        return "Car";
                    case "motorcycle":
                        return "Motorcycle";
                    default:
                        ui.WriteLine("The garage can only contain the following types of vehicles:" +
                            "\nAirplane" +
                            "\nBoat" +
                            "\nBus" +
                            "\nCar" +
                            "\nMotorcycle");
                        break;
                }
            } while (true);
        }

        private string VehicleRegPlate()
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
