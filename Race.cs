using Lab2Threading;

public class Race
{
    private List<Car> cars = new List<Car>();
    private bool raceOver = false;
    private readonly object raceLock = new object();

    public void AddCar(Car car)
    {
        cars.Add(car);
    }


    //startar racet med denna metod
    public void StartRace()
    {
        Console.WriteLine("The race starts!");

        foreach (var car in cars)
        {
            Thread carThread = new Thread(() => car.Move(this));
            carThread.Start();
        }

        // tråd för att identifiera enter knappen
        Thread userInputThread = new Thread(() =>
        {
            while (!raceOver)
            {
                if (Console.ReadKey(true).Key == ConsoleKey.Enter)
                {
                    DisplayRaceStatus();
                }
            }
        });
        userInputThread.IsBackground = true;
        userInputThread.Start();
    }

    //status för varje bil i racet
    private void DisplayRaceStatus()
    {
        Console.WriteLine("\nRace Status:");
        foreach (var car in cars)
        {
            Console.WriteLine($"{car.Name}: {car.Distance} km - Speed: {car.Speed} km/h");
        }
        Console.WriteLine();
    }

    public void AnnounceWinner(string winnerName)
    {
        lock (raceLock)
        {
            if (!raceOver)
            {
                raceOver = true;
                Console.WriteLine($"\n{winnerName} has won the race!");
                foreach (var car in cars)
                {
                    car.Stop();
                }
            }
        }
    }
}