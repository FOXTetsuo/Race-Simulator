using Controller;
using Model;

namespace RaceSimulatorTest
{
	[TestFixture]
	public class RaceTests
	{
		//[Test]
		//public void GetSectionData_ReturnData()
		//{
		//	// Arrange
		//	Data.Initialize();
		//	Data.NextRace();
		//	Data.CurrentRace.PlaceContestants(Data.CurrentRace.Track, Data.CurrentRace.Participants);


		//	// Act
		//	var result = Data.CurrentRace.GetSectionData(
		//	;

		//	// Assert
		//	Assert.Fail();
		//}

		[Test]
		public void RandomizeEquipment_EquipmentRandomized()
		{
			// Arrange
			Data.Initialize();
			Data.NextRace();
			Data.CurrentRace.PlaceContestants(Data.CurrentRace.Track, Data.CurrentRace.Participants);

			foreach (IParticipant participant in Data.CurrentRace.Participants)
			{
				participant.Equipment.Performance = 0;
				participant.Equipment.Quality = 0;
				participant.Equipment.Speed = 0;
			}
			Boolean randomized = true;

			// Act
			Data.CurrentRace.RandomizeEquipment();

			// Assert
			foreach (IParticipant participant in Data.CurrentRace.Participants)
			{
				if (participant.Equipment.Performance == 0 || participant.Equipment.Quality == 0 || participant.Equipment.Speed == 0)
				{
					randomized = false;
				}
			}

			Assert.That(randomized == true);
		}

		[Test]
		public void PlaceContestants_ContestantsOnTrack()
		{
			// Arrange
			Data.Initialize();
			Data.NextRace();


			// Act
			Data.CurrentRace.PlaceContestants(Data.CurrentRace.Track, Data.CurrentRace.Participants);

			// Assert
			if (Data.CurrentRace.Participants.Count > 0)
				Assert.Pass();
			else Assert.Fail();

		}

		//	[Test]
		//	public void DetermineBroken_StateUnderTest_ExpectedBehavior()
		//	{
		//		// Arrange
		//		var race = new Race(TODO, TODO);

		//		// Act
		//		race.DetermineBroken();

		//		// Assert
		//		Assert.Fail();
		//	}

		//	[Test]
		//	public void AdvanceParticipant_StateUnderTest_ExpectedBehavior()
		//	{
		//		// Arrange
		//		var race = new Race(TODO, TODO);
		//		IParticipant participant = null;

		//		// Act
		//		race.AdvanceParticipant(
		//			participant);

		//		// Assert
		//		Assert.Fail();
		//	}

		//	[Test]
		//	public void CheckRaceFinished_StateUnderTest_ExpectedBehavior()
		//	{
		//		// Arrange
		//		var race = new Race(TODO, TODO);

		//		// Act
		//		var result = race.CheckRaceFinished();

		//		// Assert
		//		Assert.Fail();
		//	}

		//	[Test]
		//	public void CheckFinish_StateUnderTest_ExpectedBehavior()
		//	{
		//		// Arrange
		//		var race = new Race(TODO, TODO);
		//		IParticipant participant = null;

		//		// Act
		//		var result = race.CheckFinish(
		//			participant);

		//		// Assert
		//		Assert.Fail();
		//	}

		//	[Test]
		//	public void OverTake_StateUnderTest_ExpectedBehavior()
		//	{
		//		// Arrange
		//		var race = new Race(TODO, TODO);
		//		Section section = null;

		//		// Act
		//		race.OverTake(
		//			section);

		//		// Assert
		//		Assert.Fail();
		//	}

		//	[Test]
		//	public void Cleaner_StateUnderTest_ExpectedBehavior()
		//	{
		//		// Arrange
		//		var race = new Race(TODO, TODO);

		//		// Act
		//		race.Cleaner();

		//		// Assert
		//		Assert.Fail();
		//	}
		//}
	}
}
