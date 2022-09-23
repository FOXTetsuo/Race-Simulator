using Controller;
using Race_Simulator;

namespace RaceSimulatorTest
{

	[TestFixture]
	public class RaceSimulator_Visualisatie
	{
		[SetUp]
		public void Setup()
		{
			Console.BackgroundColor = ConsoleColor.DarkGreen;
			Data.Initialize();
			Data.NextRace();
			Visualize.Initialize(Data.CurrentRace);
			Data.CurrentRace.PlaceContestants(Data.CurrentRace.Track, Data.CurrentRace.Participants);
			Visualize.DrawTrack(Data.CurrentRace.Track.Sections);
		}

		[Test]
		public void DataGetsInitialized()
		{
			//Assert.That.
		}
	}
}