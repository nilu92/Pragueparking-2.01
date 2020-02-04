using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pragueparking2._01
{
    public class Registry
    {
        public List<Vehicle> Vehicles { get; }
        //Constructor
        public Registry()
        {
            Vehicles = new List<Vehicle>();
        }
         public Vehicle SearchWithregNumber(string regNumb)
        {
            foreach (Vehicle vehicle in Vehicles)
            {
                if (vehicle.regNumber == regNumb)
                {
                    return vehicle;
                }
            }
            return null;
        }
        public Vehicle RegisterVehicle(string type, string regNumb, int spot, DateTime timewhenparked)
        {
            Vehicle vehicle = new Vehicle(type, regNumb, spot, timewhenparked);
            Vehicles.Add(vehicle);

            return vehicle;
        }
        public void RemoveVehicle(Vehicle vehicle)
        {
            Vehicles.Remove(vehicle);
        }

        public bool CheckIfSpotIsTaken(int parkSpot, string type)
        {
            if (Vehicles.Count == 0)
            {
                return false;
            }
            foreach (Vehicle vehicles in Vehicles)
            {
                int check = CheckSpot(parkSpot);
                if (check == 0)
                {
                    return false;
                }
                if (type == "mc")
                {
                    if (vehicles.TypeOfVehicle == "mc" && CheckSpot(parkSpot) < 2)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public int CheckSpot(int parkSpot)
        {
            int i = 0;
            foreach (Vehicle vehicles in Vehicles)
            {
                if (vehicles.ParkingSpot == parkSpot)
                {
                    i++;
                }
            }

            return i;
        }

        public bool CheckForDup(string regNumb)
        {
            foreach (Vehicle vehicles in Vehicles)
            {
                if (vehicles.regNumber == regNumb)
                {
                    return true;
                }
            }
            return false;
        }

        public double Collect(Vehicle vehicle)
        {
            RemoveVehicle(vehicle);
            double cost = CalculateTheCost(vehicle);
            return cost;
        }
        public double CalculateTheCost(Vehicle vehicle)
        {
            TimeSpan TimeParked = DateTime.Now - Convert.ToDateTime(vehicle.DateAndTimeParked);
            double TimeSinceParked = Convert.ToInt32(TimeParked.TotalMinutes);
            double TotalCost = 0;
            if (TimeSinceParked > 5 && TimeSinceParked < 120)
            {
                if (vehicle.TypeOfVehicle == "mc")
                {
                    TotalCost = (int)Price.Mc * 2;
                }
                else
                {
                    TotalCost = (int)Price.Car * 2;
                }
            }
            else if (TimeSinceParked >= 120)
            {
                double parkedMinutes = Math.Abs(TimeSinceParked);

                if (vehicle.TypeOfVehicle == "mc")
                {
                    TotalCost = Math.Ceiling((parkedMinutes / 60)) * (int)Price.Mc;
                }
                else
                {
                    TotalCost = Math.Ceiling((parkedMinutes / 60)) * (int)Price.Car;
                }
            }

            return TotalCost;
        }

    }
}