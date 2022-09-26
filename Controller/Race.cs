using Model;

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

		public void OnTimedEvent(object sender, EventArgs args)
		{

			DriversChanged.Invoke(this, args);

		}

		public void Start()
		{
			Timer.Start();
		}
	}
}
