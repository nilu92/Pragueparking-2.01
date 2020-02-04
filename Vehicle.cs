using System;

namespace Pragueparking2._01
{
    public class Vehicle
    {
       public string TypeOfVehicle { get; set; }
        public string regNumber { get; set; }
        public int ParkingSpot { get; set; }
        public DateTime DateAndTimeParked { get; set; }
         public Vehicle(string type, string regNumb, int parkSpot, DateTime timeWhenParked) 
        {
            TypeOfVehicle = type;
            regNumber = regNumb;
            ParkingSpot = parkSpot;
            DateAndTimeParked = timeWhenParked;
        }
        public string GetTimeParked()
        {
            string s = DateAndTimeParked.ToString("HH:MM:ss");
            return s;
        }

        public string GetDateParked()
        {
            string s = DateAndTimeParked.ToString("dd/mm/yyyy");
            return s;
        }
     }
    }