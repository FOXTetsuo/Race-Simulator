using Controller;
using Model;

namespace RaceSimulatorTest
{
	[TestFixture]
	public class RaceTests
	{
		[SetUp]
		public void SetUp()
		{
			Data.Initialize();
			Data.NextRace();
			Data.CurrentRace.PlaceContestants(Data.CurrentRace.Track, Data.CurrentRace.Participants);
		}

	
		[Test]
		public void RandomizeEquipment_EquipmentRandomized()
		{
			// Arrange
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

			// Act
			Data.CurrentRace.PlaceContestants(Data.CurrentRace.Track, Data.CurrentRace.Participants);

			// Assert
			if (Data.CurrentRace.Participants.Count > 0)
				Assert.Pass();
			else Assert.Fail();

		}

		[Test]
		public void IsNextRaceLoaded()
		{
			Race race = Data.CurrentRace;
			Data.NextRace();
			Assert.AreNotEqual(race, Data.CurrentRace);
		}
		
		[Test]
		public void CheckWinnerCorrect()
		{
			IParticipant participant = Data.CurrentRace.Participants[3];
			participant.Points = 100;
			Data.Competition.EndCompetition();
			Assert.That(participant, Is.EqualTo(Data.Competition.Winner));
		}
	}
}
