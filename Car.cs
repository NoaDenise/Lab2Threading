public class Car
{
    public string Name { get; }
    public int Distance { get; private set; }
    public int Speed { get; private set; }
    private readonly Random random = new Random();
    private bool stopped = false;
    private DateTime lastEventTime = DateTime.MinValue; // senaste händelsen
    private TimeSpan eventCooldown = TimeSpan.FromSeconds(30); // 30 sekunders cooldown

    public Car(string name)
    {
        Name = name;
        Distance = 0;
        Speed = 120;
    }

    public void Move(Race race)
    {
        while (!stopped && Distance < 5)
        {
            if (DateTime.Now - lastEventTime >= eventCooldown)
            {
                RandomEvent();
                lastEventTime = DateTime.Now;
            }
            Distance += Speed * 30 / 3600;
            if (Distance >= 5)
            {
                race.AnnounceWinner(Name);
                break;
            }
            Thread.Sleep(30000); // vänta 30 sekunder mellan varje händelse
        }
    }

    public void Stop()
    {
        stopped = true;
    }

    private void RandomEvent()
    {
        int chance = random.Next(1, 51);

        if (chance == 1)
        {
            Console.WriteLine($"{Name} is out of fuel! Stopping for 30 seconds.");
            Thread.Sleep(30000);
        }
        else if (chance <= 3)
        {
            Console.WriteLine($"{Name} has a flat tire! Stopping for 20 seconds.");
            Thread.Sleep(20000);
        }
        else if (chance <= 8)
        {
            Console.WriteLine($"{Name} has a bird on the windshield! Stopping for 10 seconds.");
            Thread.Sleep(10000);
        }
        else if (chance <= 18)
        {
            Console.WriteLine($"{Name} has engine trouble! Reducing speed by 1 km/h.");
            Speed--;
        }
    }
}