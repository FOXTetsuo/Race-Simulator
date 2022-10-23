using Model;
using Section = Model.Section;

namespace Controller
{
	public class Race
	{

		public event EventHandler<DriversChangedEventArgs> DriversChanged;

		public event EventHandler<EventArgs> RaceFinished;
		public int PointIndex { get; set; }
		public int AmountOfLaps { get; set; }
		private System.Timers.Timer Timer { get; set; }
		private Random _random { get; set; }
		public Track Track { get; set; }
		public List<IParticipant>? Participants { get; set; }
		public DateTime StartTime { get; set; }
		private Dictionary<Section, SectionData> _positions { get; set; }
		// returns sectiondata from a section, if there isn't any, a new sectiondata is made first.
		public SectionData GetSectionData(Section section)
		{
			if (!_positions.ContainsKey(section))
			{
				_positions.Add(section, new SectionData());
			}
			return _positions[section];
		}

		// constructor
		public Race(Track track, List<IParticipant>? participants)
		{
			PointIndex = 1;
			AmountOfLaps = 1;
			Timer = new System.Timers.Timer(500);
			Timer.Elapsed += OnTimedEvent;
			_random = new Random(DateTime.Now.Millisecond);
			Track = track;
			Participants = participants;
			StartTime = new DateTime();
			_positions = new Dictionary<Section, SectionData>();
			RandomizeEquipment();
		}

		// randomizes the equipment of the racers
		public void RandomizeEquipment()
		{
			foreach (IParticipant participant in Participants)
			{
				participant.Equipment.Quality = _random.Next(4, 11);
				participant.Equipment.Performance = _random.Next(5, 11);
				participant.Equipment.Speed = participant.Equipment.Quality * participant.Equipment.Performance;
			}
		}

		public void PlaceContestants(Track track, List<IParticipant> participants)
		// plaats beginposities voor alle contestants
		{
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
						// zo garandeer je dat je altijd het laatste stuck section pakt.

						if (index - (i / 2) < 0)
						{
							index = track.Sections.Count - 1 + (i / 2);
						}

						// maak / get sectiondata om aan te vullen
						// i gedeeld door 2 zodat er 2 mensen per section geplaatst worden
						// 0/2 == 0, 1/2 == 0;

						SectionData sectionData = GetSectionData(track.Sections.ElementAt(index - (i / 2)));
						//checkt of dingen al gevuld zijn

						if (sectionData.Left == null)
						{
							sectionData.Left = participants[i];

							// onthoudt waar de participant is op dit moment
							participants[i].CurrentSection = track.Sections.ElementAt(index - (i / 2));
						}
						// checkt of er wel nog twee participants zijn om toe te voegen m.b.v modulo
						else if (sectionData.Right == null)
						{
							sectionData.Right = participants[i];

							// onthoudt waar de participant is op dit moment
							participants[i].CurrentSection = track.Sections.ElementAt(index - (i / 2));
						}

					}
					return;
				}
				index++;
			}
		}

		public void OnTimedEvent(object sender, EventArgs args)
		{
			//Comment in case you're trying to fix bugs :')
			DetermineIfCarShouldBreak();
			CheckWhetherToMoveParticipants();
			DriversChanged?.Invoke(this, new DriversChangedEventArgs(Track));
		}

		public void Start()
		{
			Timer.Start();
		}

		private void UpdateSpeed(IParticipant participant)
		{
			participant.Equipment.Speed = participant.Equipment.Quality * participant.Equipment.Performance;
		}

		// kijkt of elke participant verder is dan de lengte van de track
		// zo ja, haal de lengte van de track van de distanceCovered af,
		// en beweeg de driver naar het volgende stuck track
		public void CheckWhetherToMoveParticipants()
		{
			//Timer.Stop zodat de thread niet verder gaat in de berekening.
			//Timer.Stop();
			foreach (IParticipant participant in Participants)
			{
				if (participant.Equipment.IsBroken == false)
				{
					participant.DistanceCovered += participant.Equipment.Speed;
					if (participant.DistanceCovered >= 100)
					{
						AdvanceParticipantIfPossible(participant);
					}
				}
			}
			//Timer.Start();
		}

		public void DetermineIfCarShouldBreak()
		{
			foreach (IParticipant participant in Data.CurrentRace.Participants)
			{
				// 1 op 32 kans dat auto kapot gaat
				if (participant.Equipment.IsBroken == false && (_random.Next(32) == 13))
				{
					BreakEquipment(participant);
				}
				// recovery afhankelijk van quality
				else if (participant.Equipment.Quality + _random.Next(30) >= 20)
				{
					participant.Equipment.IsBroken = false;
				}
			}
		}

		public void BreakEquipment(IParticipant participant)
		//sets participants equipment to broken, and then deducts from their stats
		//unless the stats would become so low that the race would barely progress.
		{
			participant.Equipment.IsBroken = true;
			if (participant.Equipment.Performance > 3)
			{
				participant.Equipment.Performance += -1;
				UpdateSpeed(participant);
			}
		}

		public void AdvanceParticipantIfPossible(IParticipant participant)
		{
			int i = 0;
			foreach (Section section in Track.Sections)
			{
				if (section == participant.CurrentSection)
				{
					if (Track.Sections.Count <= (i + 1))
					{
						i = -1;
					}
					SectionData newData = GetSectionData(Track.Sections.ElementAt(i + 1));

					if (newData.Left == null || newData.Right == null)
					{
						participant.DistanceCovered -= 100;
						SectionData sectData = GetSectionData(section);
						RemoveParticipantFromSectionData(sectData, participant);
						PlaceParticipantOnSectionData(newData, participant);

						participant.CurrentSection = Track.Sections.ElementAt(i + 1);

						if (CheckFinish(participant) == true)
						{
							//Stops participants from moving after finishing.
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
					// participant distancecovered terug naar wat hij was voordat de advanceparticipant functie gecallt werd
					else participant.DistanceCovered = 100;
				}
				i++;

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
					participant.Points += ((6 / PointIndex) - 1);
					// First to finish gets 5 points
					// Second to finish gets 2 points
					// Third gets 1 points
					// Anything after gets nothing
					PointIndex++;
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
			}
			//stop timer, unsubscribe to events
			Timer.Stop();
			Timer.Elapsed -= OnTimedEvent;
			DriversChanged = null;
			GC.Collect(0);
		}

	}
}
