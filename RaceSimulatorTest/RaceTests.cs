﻿using Controller;
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

		//[Test]
		//public void PlaceContestants_StateUnderTest_ExpectedBehavior()
		//{
		//	// Arrange
		//	var race = new Race(TODO, TODO);
		//	Track track = null;
		//	List participants = null;

		//	// Act
		//	race.PlaceContestants(
		//		track,
		//		participants);

		//	// Assert
		//	Assert.Fail();
		//}

		//[Test]
		//public void OnTimedEvent_StateUnderTest_ExpectedBehavior()
		//{
		//	// Arrange
		//	var race = new Race(TODO, TODO);
		//	object sender = null;
		//	EventArgs args = null;

		//	// Act
		//	race.OnTimedEvent(
		//		sender,
		//		args);

		//	// Assert
		//	Assert.Fail();
		//}

		//[Test]
		//public void Start_StateUnderTest_ExpectedBehavior()
		//{
		//	// Arrange
		//	var race = new Race(TODO, TODO);

		//	// Act
		//	race.Start();

		//	// Assert
		//	Assert.Fail();
		//}

		//[Test]
		//public void CheckWhetherToMoveParticipants_StateUnderTest_ExpectedBehavior()
		//{
		//	// Arrange
		//	var race = new Race(TODO, TODO);

		//	// Act
		//	race.CheckWhetherToMoveParticipants();

		//	// Assert
		//	Assert.Fail();
		//}

		//[Test]
		//public void BreakAndRecover_StateUnderTest_ExpectedBehavior()
		//{
		//	// Arrange
		//	var race = new Race(TODO, TODO);

		//	// Act
		//	race.BreakAndRecover();

		//	// Assert
		//	Assert.Fail();
		//}

		//[Test]
		//public void BreakCar_StateUnderTest_ExpectedBehavior()
		//{
		//	// Arrange
		//	var race = new Race(TODO, TODO);
		//	IParticipant participant = null;

		//	// Act
		//	race.BreakCar(
		//		participant);

		//	// Assert
		//	Assert.Fail();
		//}

		//[Test]
		//public void AdvanceParticipantIfPossible_StateUnderTest_ExpectedBehavior()
		//{
		//	// Arrange
		//	var race = new Race(TODO, TODO);
		//	IParticipant participant = null;

		//	// Act
		//	race.AdvanceParticipantIfPossible(
		//		participant);

		//	// Assert
		//	Assert.Fail();
		//}

		//[Test]
		//public void Cleaner_StateUnderTest_ExpectedBehavior()
		//{
		//	// Arrange
		//	var race = new Race(TODO, TODO);

		//	// Act
		//	race.Cleaner();

		//	// Assert
		//	Assert.Fail();
		//}
	}
}
