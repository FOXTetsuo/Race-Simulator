using Controller;

Data.Initialize();
Data.NextRace();
Console.WriteLine(Data.CurrentRace.Track.Name);

for (; ; )
{
	Thread.Sleep(100);
}