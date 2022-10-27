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
			Data.Initialize();
			Data.NextRace();

			Visualize.Initialize(Data.CurrentRace);
			Data.CurrentRace.PlaceContestants(Data.CurrentRace.Track, Data.CurrentRace.Participants);
			Data.CurrentRace.Start();
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
		[Test]
		public void DirectionCorrectNE()
		{
			_direction = Direction.East;
			DetermineDirection(SectionTypes.CornerNE, _direction);
			Assert.AreEqual(_direction, Direction.South);
			_direction = Direction.North;
			DetermineDirection(SectionTypes.CornerNE, _direction);
			Assert.AreEqual(_direction, Direction.West);
		}
		[Test]
		public void DirectionCorrectNW()
		{
			_direction = Direction.West;
			DetermineDirection(SectionTypes.CornerNW, _direction);
			Assert.AreEqual(_direction, Direction.South);
			_direction = Direction.North;
			DetermineDirection(SectionTypes.CornerNW, _direction);
			Assert.AreEqual(_direction, Direction.East);
		}
		[Test]
		public void DirectionCorrectSW()
		{
			_direction = Direction.West;
			DetermineDirection(SectionTypes.CornerSW, _direction);
			Assert.AreEqual(_direction, Direction.North);
			_direction = Direction.South;
			DetermineDirection(SectionTypes.CornerSW, _direction);
			Assert.AreEqual(_direction, Direction.East);
		}
		[Test]
		public void DirectionCorrectSE()
		{
			_direction = Direction.East;
			DetermineDirection(SectionTypes.CornerSE, _direction);
			Assert.AreEqual(_direction, Direction.North);
			_direction = Direction.South;
			DetermineDirection(SectionTypes.CornerSE, _direction);
			Assert.AreEqual(_direction, Direction.West);
		}
	}

}
