﻿using Model;
using Section = Model.Section;

namespace Controller
{
	public class Race
	{
		const int SectionLength = 100;
		public event EventHandler<ParticipantsChangedEventArgs> ParticipantsChanged;

		public event EventHandler<EventArgs> RaceFinished;
		public int PointIndex { get; set; }
		public int AmountOfLaps { get; set; }
		private System.Timers.Timer Timer { get; set; }
		private Random _random { get; set; }
		public Track Track { get; set; }
		public List<IParticipant>? Participants { get; set; }
		public DateTime StartTime { get; set; }
		private Dictionary<Section, SectionData> _positions { get; set; }
		public SectionData GetSectionData(Section section)
		// returns sectiondata from a section, if there isn't any, a new sectiondata is made first.
		{
			if (!_positions.ContainsKey(section))
			{
				_positions.Add(section, new SectionData());
			}
			return _positions[section];
		}
		public Race(Track track, List<IParticipant>? participants)
		//Determines how many laps in each race
		{
			PointIndex = 1;
			AmountOfLaps = 1;
			Timer = new System.Timers.Timer(500);
			Timer.Elapsed += OnTimedEvent;
			_random = new Random(DateTime.Now.Millisecond);
			Track = track;
			Participants = participants;
			StartTime = DateTime.Now;
			_positions = new Dictionary<Section, SectionData>();
			RandomizeEquipment();
		}
		private void UpdateSpeed(IParticipant participant)
		//Updates the speed of the participant
		{
			participant.Equipment.Speed = new Func<int>(() => participant.Equipment.Quality * participant.Equipment.Performance)();
		}

		public void RandomizeEquipment()
		// randomizes the equipment of the racers
		{
			foreach (IParticipant participant in Participants)
			{
				participant.Equipment.Quality = _random.Next(5, 11);
				participant.Equipment.Performance = _random.Next(4, 11);
				UpdateSpeed(participant);
			}
		}

		public void PlaceContestants(Track track, List<IParticipant> participants)
		// plaats beginposities voor alle contestants
		{
			//Randomizes the list of participants
			participants = RandomizeList(participants);
			
			// houdt bij waar in de lijst de foreach is
			// index = -1 zodat het plaatsen 1 plek begint voor de start/finish
			int index = -1;
			foreach (Section section in track.Sections)
			{
				// als de start grid is gevonden begint het plaatsen
				if (section.SectionType == SectionTypes.Finish)
				{
					for (int i = 0; i < participants.Count; i += 1)
					{
						// zorgt dat er geen out of bounds error kan komen, en zet de index naar de laatste track.	
						// track.Sections.Count - 1 omdat dit 1 groter is dan de maximale array
						// vervolgens + (i/2), omdat dit vervolgens in GetSectionData gebruikt wordt bij de volgende stap.
						// zo garandeer je dat je altijd het laatste stuk section pakt.

						if (index - (i / 2) < 0)
						{
							index = track.Sections.Count - 1 + (i / 2);
						}

						// maak / get sectiondata om aan te vullen
						// i gedeeld door 2 zodat er 2 mensen per section geplaatst worden
						// 0/2 == 0, 1/2 == 0;

						SectionData sectionData = GetSectionData(track.Sections.ElementAt(index - (i / 2)));
						//checkt of sectionData al gevuld zijn. zo niet -> plaats dan de participant(s)
						//bij 1 participant wordt er 1 geplaatst, bij 2 participants 2.

						PlaceParticipantOnSectionData(sectionData, participants[i]);
						
						// onthoudt waar de participant is op dit moment
						participants[i].CurrentSection = track.Sections.ElementAt(index - (i / 2));

					}
					return;
				}
				index++;
			}
		}

		public void OnTimedEvent(object sender, EventArgs args)
		{
			//Timer.stop to prevent desynchronization issues in case of
			//software hanging / drawing taking longer than expected.
			Timer.Stop();
			BreakAndRecover();
			CheckWhetherToMoveParticipants();
			ParticipantsChanged?.Invoke(this, new ParticipantsChangedEventArgs(Track));
			Timer.Start();
		}

		public void Start()
		{
			Timer.Start();
		}

		// kijkt of elke participant verder is dan de lengte van de track
		// zo ja, haal de lengte van de track van de distanceCovered af,
		// en beweeg de participant naar het volgende stuck track
		public void CheckWhetherToMoveParticipants()
		{
			foreach (IParticipant participant in Participants)
			{
				if (participant.Equipment.IsBroken == false && participant.Equipment.Performance > 0)
				{
					participant.DistanceCovered += (participant.Equipment.Speed);
					if (participant.DistanceCovered >= SectionLength)
					{
						AdvanceParticipantIfPossible(participant);
					}
				}
			}
		}

		public void BreakAndRecover()
		{
			foreach (IParticipant participant in Data.CurrentRace.Participants)
			{
				// 1 op 64 kans dat auto kapot gaat
				if (participant.Equipment.IsBroken == false && (_random.Next(64) == 13))
				{
					BreakCar(participant);
				}
				// recovery afhankelijk van quality
				else if (participant.Equipment.Quality + _random.Next(30) >= 20)
				{
					participant.Equipment.IsBroken = false;
				}
			}
		}

		public void BreakCar(IParticipant participant)
		//sets participant to broken, and then deducts from their stats
		//unless the stats would become so low that the race would barely progress.
		{
			participant.Equipment.IsBroken = true;

			if (participant.Equipment.Performance > 4)
			{
				participant.Equipment.Performance += -1;
				UpdateSpeed(participant);
			}
		}

		public void AdvanceParticipantIfPossible(IParticipant participant)
		{
			// keeps track of the position of the current track in Track.Sections
			int index = 0;
			foreach (Section section in Track.Sections)
			{
				//loop through the sections to find the current section
				if (section == participant.CurrentSection)
				{
					//reset index to prevent indexOutofBounds
					if (Track.Sections.Count <= (index + 1))
					{
						index = -1;
					}
					//grab the sectiondata of the section one step further than the current
					SectionData newData = GetSectionData(Track.Sections.ElementAt(index + 1));

					//IF either of these is empty, then advance the participant.
					if (newData.Left == null || newData.Right == null)
					{
						//remove the participant from the old sectiondata
						//and place the participant on the new sectiondata
						participant.DistanceCovered -= SectionLength;
						SectionData sectData = GetSectionData(section);
						RemoveParticipantFromSectionData(sectData, participant);
						PlaceParticipantOnSectionData(newData, participant);
						//set their new currentsection 
						participant.CurrentSection = Track.Sections.ElementAt(index + 1);

						//if the participant has finished,
						//set currentsection to null, remove them from sectiondata
						//set performance to 0 to stop them from moving
						//and check if the race is finished.

						if (CheckFinish(participant) == true)
						{
							//Stops participants from moving.
							participant.Equipment.Performance = 0;
							participant.CurrentSection = null;

							RemoveParticipantFromSectionData(newData, participant);

							if (CheckRaceFinished() == true)
							{
								PrepareNextRace();
							}
						}
						return;
					}
					//TODO: Decide if behavior below is wanted. As of right now 
					//momentum is conserved.
					// participant distancecovered back to what it was before function was called
					//else participant.DistanceCovered = Sectionlength;
				}
				index++;
			}
		}

		//TODO: methods hieronder private, maar dan kan je ze natuurlijk ook niet meer testen
		private void PrepareNextRace()
		{
			Cleaner();
			if (Data.NextRace() == true)
			{
				Data.CurrentRace.PlaceContestants(Data.CurrentRace.Track, Data.CurrentRace.Participants);
				RaceFinished.Invoke(this, new EventArgs());
				Data.CurrentRace.Start();
			}

		}

		private void RemoveParticipantFromSectionData(SectionData sectionData, IParticipant participant)
		{
			if (sectionData.Left == participant)
			{
				sectionData.Left = null;
			}
			else if (sectionData.Right == participant)
			{
				sectionData.Right = null;
			}
		}

		private void PlaceParticipantOnSectionData(SectionData sectionData, IParticipant participant)
		{
			if (sectionData.Left == null)
			{
				sectionData.Left = participant;
			}
			else if (sectionData.Right == null)
			{
				sectionData.Right = participant;
			}
		}

		private Boolean CheckRaceFinished()
		{
			foreach (Section section in Track.Sections)
			{
				if (GetSectionData(section).Left != null || (GetSectionData(section).Right != null))
				{
					return false;
				}
			}
			return true;
		}

		private Boolean CheckFinish(IParticipant participant)
		{
			if (participant.CurrentSection.SectionType == SectionTypes.Finish)
			{
				participant.LoopsPassed += 1;
				if (participant.LoopsPassed == AmountOfLaps + 1)
				// +1 omdat participants voor de finish beginnen en dus altijd 1 loop passen aan het begin van de race
				{
					if (((6 / PointIndex) - 1) > 0)
					{
						participant.Points += ((6 / PointIndex) - 1);
					}
					// First to finish gets 5 points
					// Second to finish gets 2 points
					// Third gets 1 points
					// Anything after gets nothing
					PointIndex++;
					participant.LapTime = Math.Truncate((DateTime.Now - StartTime).TotalSeconds * 100) / 100;
					return true;
				}
				else return false;
			}
			else return false;
		}

		public void Cleaner()
		{
			foreach (IParticipant participant in Participants)
			{
				participant.CurrentSection = null;
				participant.DistanceCovered = 0;
				participant.LoopsPassed = 0;
				participant.LapTime = 0;
			}
			//stop timer, unsubscribe to events
			Timer.Stop();
			Timer.Elapsed -= OnTimedEvent;
			ParticipantsChanged = null;
			GC.Collect(0);
		}

		public List<P> RandomizeList<P>(List<P> list)
		{
			List<bool> Used = new List<bool>();
			for (int i = 0; i < list.Count; i++)
			{
				Used.Add(false);
			}
			List<P> newList = new List<P>();
			for (int i = 0; i < list.Count; i++)
			{
				int randomIndex = _random.Next(0, list.Count);
				if (Used.ElementAt(randomIndex) == true)
				{
					i--;
				}
				else
				{
					Used[randomIndex] = true;
					newList.Add(list[randomIndex]);
				}
			}
			return newList;
		}
	}
}
