using Controller;
using Model;
using Race_Simulator;
using static Race_Simulator.Visualize;

namespace RaceSimulatorTest
{

	[TestFixture]
	public class RaceSimulator_Visualisatie_Test
	{
		[SetUp]
		public void Setup()
		{
			Console.BackgroundColor = ConsoleColor.DarkGreen;

			try
			{
				Data.Initialize();
				Data.NextRace();

				Visualize.Initialize(Data.CurrentRace);
				Data.CurrentRace.PlaceContestants(Data.CurrentRace.Track, Data.CurrentRace.Participants);
				Data.CurrentRace.Start();
			}
			catch (ImproperCompetitionException e)
			{
				Console.WriteLine(e.Message);
			}
		}

		[Test]
		public void DataGetsInitialized()
		{
			Assert.NotNull(Data.CurrentRace);
		}
		
		[Test]
		public void VisualisationGetsInitialized()
		{
			Assert.That(15, Is.EqualTo(Visualize.ypos));
			Assert.That(20, Is.EqualTo(Visualize.xpos));
		}
		
		[TestCase(Direction.East, SectionTypes.CornerNE, Direction.South)]
		[TestCase(Direction.North, SectionTypes.CornerNE, Direction.West)]
		[TestCase(Direction.West, SectionTypes.CornerNW, Direction.South)]
		[TestCase(Direction.North, SectionTypes.CornerNW, Direction.East)]
		[TestCase(Direction.West, SectionTypes.CornerSW, Direction.North)]
		[TestCase(Direction.South, SectionTypes.CornerSW, Direction.East)]
		[TestCase(Direction.East, SectionTypes.CornerSE, Direction.North)]
		[TestCase(Direction.South, SectionTypes.CornerSE, Direction.West)]
		public void DirectionCorrect(Direction direction, SectionTypes sectionType, Direction ExpectedDirection)
		{
			_direction = direction;
			DetermineDirection(sectionType, _direction);
			Assert.That(ExpectedDirection, Is.EqualTo(_direction));
		}

	}

}
