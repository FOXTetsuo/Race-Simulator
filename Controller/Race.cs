using Model;
using System.Linq.Expressions;
using static System.Collections.Specialized.BitVector32;
using Section = Model.Section;

namespace Controller
{
	public class Race
	{
		public event EventHandler<DriversChangedEventArgs> DriversChanged;
		
		public event EventHandler<EventArgs> RaceFinished;
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
				participant.Equipment.Quality = _random.Next(1, 11);
				participant.Equipment.Performance = _random.Next(3, 11);
			}
		}

		// plaats beginposities voor alle contestants
		//TODO: Fix dat er per 1 mensen geplaced worden
		public void PlaceContestants(Track track, List<IParticipant> participants)
		{
			// houdt bij waar in de lijst de foreach is
			// index = -1 zodat het plaatsen begint 1 plek voor de start/finish
			int index = -1;
			foreach (Section section in track.Sections)
			{
				// als de start grid is gevonden begint het plaatsen
				if (section.SectionType == SectionTypes.Finish)
				{
					// tempsection aanmaken die constant aangepast wordt
					for (int i = 0; i < participants.Count; i += 2)
					{
						// zorgen dat er geen fucking outofbounds error komt :DDD
						if ((index - i / 2) < 0)
						{
							index = track.Sections.Count;
						}
						// maak / get sectiondata om aan te vullen
						SectionData sectionData = GetSectionData(track.Sections.ElementAt(index - (i / 2)));
						//checkt of dingen al gevuld zijn

						if (sectionData.Left == null)
						{
							//eerste participant op linkerbaan
							sectionData.Left = participants[i];

							// onthoudt waar de participant is op dit moment
							participants[i].CurrentSection = track.Sections.ElementAt(index - (i / 2));
						}
						// checkt of er wel nog twee participants zijn om toe te voegen m.b.v modulo
						if (sectionData.Right == null && participants.Count % 2 == 0)
						{
							//tweede participant op rechterbaan
							sectionData.Right = participants[i + 1];

							// onthoudt waar de participant is op dit moment
							participants[i + 1].CurrentSection = track.Sections.ElementAt(index - (i / 2));
						}
					}
					return;
				}
				index++;
			}
		}

		public void OnTimedEvent(object sender, EventArgs args)
		{
			// kijk naar speed / tracklength etc, als een driver over de lengte heen is
			// dan advance je deze naar de volgende tracksection
			
			//Comment in case you're trying to fix bugs :')
			DetermineIfCarShouldBreak();
			CheckWhetherToMoveParticipants();
			DriversChanged?.Invoke(this, new DriversChangedEventArgs(Track));
		}

		//start de timer
		public void Start()
		{
			Timer.Start();
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
				participant.DistanceCovered += (participant.Equipment.Performance * participant.Equipment.Speed);
				if (participant.DistanceCovered >= 100 && participant.Equipment.IsBroken == false)
				{
					participant.DistanceCovered += -100 ;
					AdvanceParticipant(participant);
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
		{
			participant.Equipment.IsBroken = true;
			int determinePenalty = _random.Next(2);
			switch (determinePenalty)
			{
				case 0:
					if (participant.Equipment.Performance > 2)
					{
						participant.Equipment.Performance += -1;
					}
					break;
				case 1:
					if (participant.Equipment.Speed > 2)
					{
						participant.Equipment.Speed += -1;
					}
					break;
			}
		}

		public void AdvanceParticipant(IParticipant participant)
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
						SectionData sectData = GetSectionData(section);
						RemoveParticipantFromSectionData(sectData, participant);
						PlaceParticipantOnSectionData(newData, participant);

						participant.CurrentSection = Track.Sections.ElementAt(i + 1);

						if (CheckFinish(participant) == true)
						{
							participant.CurrentSection = null;
							
							RemoveParticipantFromSectionData(newData, participant);

							if (CheckRaceFinished() == true)
							{
								PrepareNextRace();
							}
						}
						return;
					}

					else participant.DistanceCovered = 100;
				}
				i++;
				
			}
		}

		private void PrepareNextRace()
		{
			Cleaner();
			Data.NextRace();
			Data.CurrentRace.PlaceContestants(Data.CurrentRace.Track, Data.CurrentRace.Participants);
			RaceFinished.Invoke(this, new EventArgs());
			Data.CurrentRace.Start();
		}

		public void RemoveParticipantFromSectionData(SectionData sectionData, IParticipant participant)
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
		public void PlaceParticipantOnSectionData(SectionData sectionData, IParticipant participant)
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

		public Boolean CheckRaceFinished()
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

		// mogelijk parameter Track toevoegen, zodat elke race ander aantal rondjes heeft
		public Boolean CheckFinish(IParticipant participant)
		{
			if (participant.CurrentSection.SectionType == SectionTypes.Finish)
			{
				participant.LoopsPassed += 1;
				if (participant.LoopsPassed == 2)
				{
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
			//unsubscribe, timer = null might be unnesessacary 
			Timer = null;
			DriversChanged = null;
			GC.Collect(0);
		}

	}
}
