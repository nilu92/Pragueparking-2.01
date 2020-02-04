using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pragueparking2._01
{
    public class Menu
    {
        private Registry registry;
        private FileController fileController;
        public Menu(Registry registry, FileController fileController)
        {
            this.registry = registry;
            this.fileController = fileController;
        }
        public void MainMenu()
        {
            Console.Clear();
            DateTime localDate = DateTime.Now;
            DayOfWeek wk = DateTime.Today.DayOfWeek;
            Console.WriteLine("******************************\n" + "******************************\n"
               + "*                             *\n"
               + "* Prague Parking System 2.0.  *\n" + "*                             *\n" + "{0},{1}\n" + "*******************************", localDate, wk);
            Console.WriteLine("*******************************");
            Console.WriteLine("*******************************");
            Console.WriteLine("*What do you want to do?*******");
            Console.WriteLine("*(1)-{Add new Vehicle}\n" + "*(2)-{Search Vehicle}\n" + "*(3)-{Collect Vehicle}\n" + "*(4)-{ParkingList}\n" + "*(5)-{MoveVehicle}\n" + "*(6)-{Save File}\n" + "*(7)-{Read File}\n" + "*(8)-{Exit Application}");
            string Action = Console.ReadLine();
            Action.ToLower();
            switch (Action)
            {
                case "1":
                    Console.Clear();
                    AddVehicle();
                    break;
                case "2":
                    SearchVehicle();
                    break;
                case "3":
                    CollectVehicle();
                    break;
                case "4":
                    VehicleList();
                    break;
                case "5":
                    Move();
                    break;
                case "6":
                    Console.Clear();
                    fileController.SaveToFile();
                    Console.WriteLine("Saving to file..");
                    System.Threading.Thread.Sleep(1000);
                    Console.WriteLine("File saved...");
                    Console.ReadKey();
                    MainMenu();
                    break;
                case "7":
                    Console.Clear();
                    fileController.Read();
                    Console.WriteLine("Loading..");
                    System.Threading.Thread.Sleep(1000);
                    Console.WriteLine("File loaded...");
                    Console.ReadKey();
                    MainMenu();
                    break;
                case "8":
                    Environment.Exit(1);
                    break;
                default:
                    Console.WriteLine("Invalid input");
                    MainMenu();
                    break;
            }
        }
        public void AddVehicle()
        {
            DateTime TimeWhenParked = DateTime.Now;
            Console.Clear();
            Console.WriteLine("*******************************************\n" +
                "Please enter the type of vehicle\n" + "{Car}\n" + "or\n" + "{MC}\n" + "*******************************************");
            string type = Console.ReadLine();
            type.ToLower();
            if (type == "car" || type == "mc" || type == "m" || type == "c")
            {
                Console.Clear();
                Console.WriteLine("Enter registration number: ");
                string regNumb = Console.ReadLine();
                regNumb.ToLower();
                while (regNumb.Length != 6)
                {
                    Console.WriteLine("Invalid registration number.");
                    regNumb = Console.ReadLine();
                    regNumb.ToLower();
                    Console.Clear();
                }
                if (registry.CheckForDup(regNumb) == false)
                {
                    Console.Clear();
                    Console.WriteLine("Choose parkingspot.");
                    int parkSpot = 0;
                    string chosenspot = Console.ReadLine();
                    int.TryParse(chosenspot, out parkSpot);
                    while (parkSpot == 0 || parkSpot > 100 || parkSpot < 0)
                    {
                        Console.WriteLine("Invalid spot, choose a parkingspot between spot 1 - 100 ");
                        int.TryParse(Console.ReadLine(), out parkSpot);
                        Console.Clear();
                    }
                    while (registry.CheckIfSpotIsTaken(parkSpot, type))
                    {
                        Console.WriteLine("The chosen spot {0} is  already taken\n" + "Please try again... ", parkSpot);
                        int.TryParse(Console.ReadLine(), out parkSpot);
                        while (parkSpot == 0 || parkSpot > 100 || parkSpot < 0)
                        {
                            Console.WriteLine("Invalid spot, choose a parkingspot between spot 1 - 100 ");
                            int.TryParse(Console.ReadLine(), out parkSpot);
                            Console.Clear();
                        }
                    }
                    Console.Clear();
                    registry.RegisterVehicle(type, regNumb, parkSpot, TimeWhenParked);
                    Console.WriteLine("Your {0} with regNumber {1} has been parked at spot {2}.\n" + " At the current time {3}\n" + "Press enter to continue...", type, regNumb, parkSpot, TimeWhenParked);
                    Console.ReadKey();
                    MainMenu();
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("{0} already exist in the system.\n" + "Press any key to continue", regNumb);
                    Console.ReadKey();
                    AddVehicle();
                }
            }
            else
            {
                AddVehicle();
            }
        }
        public void SearchVehicle()
        {
            Console.Clear();
            Console.WriteLine("Enter registration number: ");
            string regNumb = Console.ReadLine();
            regNumb.ToLower();
            Vehicle vehicle = registry.SearchWithregNumber(regNumb);
            if (vehicle == null)
            {
                Console.Clear();
                Console.WriteLine("Your vehicle with regNumber {0} does not exist in the system.\n" +
                    " Press enter to continue...", regNumb);
                Console.WriteLine("Or press {M} to return to the menu");
                string action = Console.ReadLine();
                action.ToLower();
                if (action == "M")
                {
                    MainMenu();
                }
                else
                {
                    SearchVehicle();
                }
            }
            else
            {
                double Currentcost = registry.CalculateTheCost(vehicle);
                Console.WriteLine("Your {0} is parked at parkingspot {1} and was parked there {2}, current price is {3}kr\n" + "What do you wish to do with this vehicle? \n" +
                    "1 - Collect\n" + "2 - Move vehicle\n" + "3- Return to MainMenu", vehicle.TypeOfVehicle, vehicle.ParkingSpot, vehicle.DateAndTimeParked, Currentcost);
                string Action = Console.ReadLine();
                Action.ToLower();
                Console.ReadKey();
                if (Action == "1" || Action == "C")
                {
                    registry.Collect(vehicle);
                    double cost = registry.Collect(vehicle);
                    Console.WriteLine(" Your {0} has been collectect from parkingspot {1}, totalprice is {2}kr", vehicle.TypeOfVehicle, vehicle.ParkingSpot, cost);
                    Console.ReadKey();
                    MainMenu();

                }
                if (Action == "2" || Action == "Move")
                {
                    Console.Clear();
                    Console.WriteLine("Choose new parkingspot.");
                    int parkSpot = 0;
                    string type = vehicle.TypeOfVehicle;
                    DateTime TimeWhenParked = vehicle.DateAndTimeParked;
                    string chosenspot = Console.ReadLine();
                    int.TryParse(chosenspot, out parkSpot);
                    while (parkSpot == 0 || parkSpot > 100 || parkSpot < 0)
                    {
                        Console.WriteLine("Invalid spot, choose a parkingspot between spot 1 - 100 ");
                        int.TryParse(Console.ReadLine(), out parkSpot);
                        Console.Clear();
                    }
                    while (registry.CheckIfSpotIsTaken(parkSpot, type))
                    {
                        Console.WriteLine("The chosen spot {0} is  already taken\n" + "Please try again... ", parkSpot);
                        int.TryParse(Console.ReadLine(), out parkSpot);
                        while (parkSpot == 0 || parkSpot > 100 || parkSpot < 0)
                        {
                            Console.WriteLine("Invalid spot, choose a parkingspot between spot 1 - 100 ");
                            int.TryParse(Console.ReadLine(), out parkSpot);
                            Console.Clear();
                        }
                    }
                    Console.Clear();
                    registry.RemoveVehicle(vehicle);
                    registry.RegisterVehicle(type, regNumb, parkSpot, TimeWhenParked);
                    Console.WriteLine("{0} with registration number {1} has been moved to parkingspot {2}. The current time is {3} \n" +
                        "Press any key to return to menu...", type, regNumb, parkSpot, TimeWhenParked);
                    Console.ReadKey();
                    MainMenu();
                }
                if (Action == "3" || Action == "M")
                {
                    MainMenu();
                }
            }
        }
        public void Move()
        {
            Console.Clear();
            Console.WriteLine("Enter registration number: ");
            string regNumb = Console.ReadLine();
            regNumb.ToLower();
            Vehicle vehicle = registry.SearchWithregNumber(regNumb);

            if (vehicle == null)
            {
                Console.Clear();
                Console.WriteLine("Your vehicle with regNumber {0} does not exist in the system.\n" +
                    " Press enter to continue...", regNumb);
                Console.WriteLine("Or press {M} to return to the menu");
                string action = Console.ReadLine();
                action.ToLower();
                if (action == "M")
                {
                    MainMenu();
                }
                else
                {
                    Move();
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Choose new parkingspot.");
                int parkSpot = 0;
                string type = vehicle.TypeOfVehicle;
                DateTime TimeWhenParked = vehicle.DateAndTimeParked;
                string chosenspot = Console.ReadLine();
                int.TryParse(chosenspot, out parkSpot);
                while (parkSpot == 0 || parkSpot > 100 || parkSpot < 0)
                {
                    Console.WriteLine("Invalid spot, choose a parkingspot between spot 1 - 100 ");
                    int.TryParse(Console.ReadLine(), out parkSpot);
                    Console.Clear();
                }
                while (registry.CheckIfSpotIsTaken(parkSpot, type))
                {
                    Console.WriteLine("The chosen spot {0} is  already taken\n" + "Please try again... ", parkSpot);
                    int.TryParse(Console.ReadLine(), out parkSpot);
                    while (parkSpot == 0 || parkSpot > 100 || parkSpot < 0)
                    {
                        Console.WriteLine("Invalid spot, choose a parkingspot between spot 1 - 100 ");
                        int.TryParse(Console.ReadLine(), out parkSpot);
                        Console.Clear();
                    }
                }
                Console.Clear();
                registry.RemoveVehicle(vehicle);
                registry.RegisterVehicle(type, regNumb, parkSpot, TimeWhenParked);
                Console.WriteLine("{0} with registration number {1} has been moved to parkingspot {2}. The current time is {3} \n" +
                    "Press any key to return to menu...", type, regNumb, parkSpot, TimeWhenParked);
                Console.ReadKey();
                MainMenu();
            }
        }
        public void CollectVehicle()
        {
            Console.Clear();
            Console.WriteLine("Enter registration number: ");
            string regNumb = Console.ReadLine();
            regNumb.ToLower();
            Vehicle vehicle = registry.SearchWithregNumber(regNumb);
            if (vehicle == null)
            {
                Console.Clear();
                Console.WriteLine("Your vehicle with regNumber {0} does not exist in the system.\n" +
                    " Press enter to continue...", regNumb);
                Console.WriteLine("Or press {M} to return to the menu");
                string action = Console.ReadLine();
                action.ToLower();
                if (action == "M")
                {
                    MainMenu();
                }
                else
                {
                    CollectVehicle();
                }
            }
            else
            {
                registry.RemoveVehicle(vehicle);
                double cost = registry.CalculateTheCost(vehicle);
                Console.WriteLine(" Your {0} has been collectect from parkingspot {1}, totalprice is {2}", vehicle.TypeOfVehicle, vehicle.ParkingSpot, cost);
                Console.ReadKey();
                MainMenu();
            }
        }
        public void VehicleList() 
        {
            Console.Clear();
            foreach (Vehicle vehicle in registry.Vehicles) 
            {
                registry.Vehicles.OrderBy(vehicle => vehicle.ParkingSpot);
                Console.WriteLine("**********************************************************");
                Console.WriteLine($"Parkingspot Nr {vehicle.ParkingSpot} contains: {vehicle.TypeOfVehicle} with registrationnumber: {vehicle.regNumber} Parked: {vehicle.DateAndTimeParked}");
                Console.WriteLine("**********************************************************");
            }
            Console.ReadKey();
            MainMenu();
        }   
    }
}

