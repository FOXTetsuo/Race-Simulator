﻿using Model;
using static System.Collections.Specialized.BitVector32;
using Section = Model.Section;

namespace Controller
{
	public class Race
	{
		public event EventHandler<DriversChangedEventArgs> DriversChanged;
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
		}

		// randomizes the equipment of the racers
		public void RandomizeEquipment()
		{
			foreach (IParticipant participant in Participants)
			{
				participant.Equipment.Quality = _random.Next(10);
				participant.Equipment.Performance = _random.Next(10);
			}
		}

		// plaats beginposities voor alle contestants
		public void PlaceContestants(Track track, List<IParticipant> participants)
		{
			// houdt bij waar in de lijst de foreach is
			int index = 0;
			foreach (Section section in track.Sections)
			{
				// als de start grid is gevonden begint het plaatsen
				if (section.SectionType == SectionTypes.StartGrid)
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
						// voegt de participant(s) toe aan _positions.
						//_positions.Add(track.Sections.ElementAt(index - (i)), sectionData);
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
			
			CheckWhetherToMoveParticipants();
			
			DriversChanged.Invoke(this, new DriversChangedEventArgs(Track));
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
			Timer.Stop();
			foreach (IParticipant participant in Participants)
			{
				participant.DistanceCovered += (participant.Equipment.Performance * participant.Equipment.Speed);
				if (participant.DistanceCovered >= 100)
				{
					participant.DistanceCovered += -100 ;
					AdvanceParticipant(participant);
				}
			}
			Timer.Start();
		}

		public void AdvanceParticipant(IParticipant participant)
		{
			int i = 0;
			foreach (Section section in Track.Sections)
			{
				if (section == participant.CurrentSection)
				{
					SectionData sectData = GetSectionData(section);
					
					if (sectData.Left == participant)
					{
						sectData.Left = null;
					}
					else if (sectData.Right == participant)
					{
						sectData.Right = null;
					}
					
					if (Track.Sections.Count <= (i + 1))
					{
						i = -1;
					}

					SectionData NewData = GetSectionData(Track.Sections.ElementAt(i+1));
					if (NewData.Left == null)
					{
						NewData.Left = participant;
					}
					else if (NewData.Right == null)
					{
						NewData.Right = participant;
					}
					participant.CurrentSection = Track.Sections.ElementAt(i + 1);

					if (CheckFinish(participant) == true)
					{
						participant.CurrentSection = null;
						if (NewData.Left == participant)
						{
							NewData.Left = null;
						}
						else if (NewData.Right == participant)
						{
							NewData.Right = null;
						}
					}
					return;
				}
				i++;
				
			}
		}
		// mogelijk parameter Track toevoegen, zodat elke race ander aantal rondjes heeft
		public Boolean CheckFinish(IParticipant participant)
		{
			if (participant.CurrentSection.SectionType == SectionTypes.Finish)
			{
				participant.LoopsPassed += 1;
				if (participant.LoopsPassed == 1)
				{
					return true;
				}
				else return false;
			}
			else return false;
		}

		//mogelijke leuke uitbreiding xdDDdd
		public void OverTake(Section section)
		{
			// geef section mee die overgehaald moet worden
			// pak de sectiondata, remove de racers.
			// snellere racers vervolgens in deze sectiondata
			// daarna de oude racers in het vorige stuck track
		}
	}
}
