using Controller;
using Race_Simulator;

try
{
	Console.BackgroundColor = ConsoleColor.DarkGreen;
	Data.Initialize();
	Data.NextRace();

	Visualize.Initialize(Data.CurrentRace);
	Data.CurrentRace.PlaceContestants(Data.CurrentRace.Track, Data.CurrentRace.Participants);
	Visualize.DrawTrack(Data.CurrentRace.Track);
	Data.CurrentRace.Start();
}
catch (ImproperCompetitionException e)
{
	Console.WriteLine(e.Message);
}


for (; ; )
{
	Thread.Sleep(100);
}
