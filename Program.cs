using System;

namespace Pragueparking2._01
{
    class Program
    {
        static void Main(string[] args)
        {
            new Program().Run();
        }
        private Menu menu;
        private Registry registry;
        private FileController fileController;
        private Vehicle vehicle;
        //constructor
        public Program()
        {
            registry = new Registry();
            fileController = new FileController(registry);
            menu = new Menu(registry,fileController);
        }
        private void Run()
        {
            //Test
           /* 
            string reg1 = "123456";
            string reg2 = "654321";
            string reg3 = "666666";

            DateTime now = DateTime.Now;
            var testTime = now.Subtract(TimeSpan.FromHours(5));

            registry.RegisterVehicle("mc", reg1, 1, testTime);
            registry.RegisterVehicle("car", reg2, 2, testTime);
            registry.RegisterVehicle("car", reg3, 3, testTime);

            fileController.SaveToFile();
            fileController.Read();
            */
            menu.MainMenu();

        }
    }
}
