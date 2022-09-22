using Model;

namespace Controller
{
	public class Race
	{
		private Random _random { get; set; }
		public Track Track { get; set; }
		public List<IParticipant>? Participants { get; set; }
		public DateTime StartTime { get; set; }
		private Dictionary<Section, SectionData> _positions { get; set; }

		public SectionData GetSectionData(Section section)
		{
			if (!_positions.ContainsKey(section))
			{
				_positions.Add(section, new SectionData());
			}
			return _positions[section];
		}

		public Race(Track track, List<IParticipant>? participants)
		{
			_random = new Random(DateTime.Now.Millisecond);
			Track = track;
			Participants = participants;
			StartTime = new DateTime();
			_positions = new Dictionary<Section, SectionData>();
		}

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
					for (int i = 0; i < participants.Count; i+=2)
					{
						// zorgen dat er geen fucking outofbounds error komt :DDD
						if ((index - i/2) < 0)
						{
							index = track.Sections.Count;
						}
						// maak / get sectiondata om aan te vullen
						SectionData sectionData = GetSectionData(track.Sections.ElementAt(index - (i/2)));
						//checkt of dingen al gevuld zijn
						if (sectionData.Left == null)
						{
							//eerste participant op linkerbaan
							sectionData.Left = participants[i];
						}
						// checkt of er wel nog twee participants zijn om toe te voegen
						if (sectionData.Right == null && participants.Count % 2 == 0)
						{
							//tweede participant op rechterbaan
							sectionData.Right = participants[i+1];
						}
						// voegt de participant(s) toe aan _positions.
						//_positions.Add(track.Sections.ElementAt(index - (i)), sectionData);
					}
					return;
				}
				index++;
			}
		}

	}
}
