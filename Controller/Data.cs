using Model;

namespace Controller
{
	public static class Data
	{
		static Competition? Competition { get; set; }
		public static Race? CurrentRace { get; set; }
		public static void Initialize()
		{
			Competition = new(); // maybe competition toevoegen asls alles breekt XDDD
			AddParticipants();
			AddTracks();
		}
		public static void AddParticipants()
		{
			Competition.Participants.Add(new Driver("Joe", 2, new Car(10, 10, 10, false), TeamColors.Blue));
			Competition.Participants.Add(new Driver("Aapie", 2, new Car(10, 10, 10, false), TeamColors.Red));
			Competition.Participants.Add(new Driver("Jaapie", 2, new Car(10, 10, 10, false), TeamColors.Yellow));
			Competition.Participants.Add(new Driver("Green", 2, new Car(10, 10, 10, false), TeamColors.Green));
		}
		public static void AddTracks()
		{
			Competition.Tracks.Enqueue(new Track("Rainbow Spaceroad", TrackBuilder("Rainbow Spaceroad")));
			Competition.Tracks.Enqueue(new Track("Spacebase", TrackBuilder("Spacebase")));
			Competition.Tracks.Enqueue(new Track("Vroemvroem-in-da-rondje", TrackBuilder("Vroemvroem-in-da-rondje")));
		}

		public static SectionTypes[] TrackBuilder(string trackName)
		{
			if (trackName.Equals("Van zanten-voort"))
			{
				SectionTypes[] build = new SectionTypes[]
				{
						SectionTypes.StartGrid,
						SectionTypes.RightCorner,
						SectionTypes.Straight,
						SectionTypes.RightCorner,
						SectionTypes.Straight,
						SectionTypes.RightCorner,
						SectionTypes.Straight,
						SectionTypes.RightCorner

				};
				return build;
			}
			if (trackName.Equals("Spacebase"))
			{
				SectionTypes[] build = new SectionTypes[]
				{
						SectionTypes.StartGrid,

				};
				return build;
			}
			else return null;
		}

		public static void NextRace()
		{
			Track newTrack = Competition.NextTrack();
			if (newTrack is not null)
			{
				CurrentRace = new Race(newTrack, Competition.Participants);
			};
		}

	}


}