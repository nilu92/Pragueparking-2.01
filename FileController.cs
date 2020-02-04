using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Pragueparking2._01
{
    public class FileController
    {
        // private static StreamReader ReadFile;
        //private static StreamWriter WriteFile;
        private Registry registry;

        public FileController(Registry registry)
        {
            this.registry = registry;
        }
        public void SaveToFile()
        {
            Save();
        }
        public StringBuilder Save()
        {
            StringBuilder builtString = new StringBuilder();
            foreach (Vehicle vehicle in registry.Vehicles)
            {
                builtString.Append($"{vehicle.TypeOfVehicle}\n"
                + $"{vehicle.regNumber}\n"
                    + $"{vehicle.DateAndTimeParked}\n"
                    + $"{vehicle.ParkingSpot}\n");
            }
            string s = builtString.ToString();
            File.WriteAllText("ParkingLot.txt", s);
            return builtString;
        }
        public void Read()
        {
            string[] input = File.ReadAllLines("ParkingLot.txt");
            registry.Vehicles.Clear();
            for (int i = 0; i < input.Length; i += 4)
            
            {
                string Type = input[i];
                string regnumb = input[i + 1];
                string TimeWhenParked = input[i + 2];
                string parkSpot = input[i + 3];
                int park = Int32.Parse(parkSpot);
                DateTime time = Convert.ToDateTime(TimeWhenParked);
                registry.RegisterVehicle(Type, regnumb, park, time);
            }
            
        }
    }
}

