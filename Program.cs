using System;
using System.Collections.Generic;
using System.Threading;
//Noa Denise Ishac NET23

namespace Lab2Threading
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Race race = new Race();

            for (int i = 1; i <= 3; i++)
            {
                string carName = $"Car {i}";
                Car car = new Car(carName);
                race.AddCar(car);
            }

            race.StartRace();

            Console.ReadLine();
        }
    }
}


