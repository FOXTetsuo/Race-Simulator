 using Model;

namespace Controller
{
	public static class Data
	{
		static Competition? Competition { get; set; }
		public static Race? CurrentRace { get; set; }
		public static void Initialize()
		{
			Competition = new();
			AddParticipants();
			AddTracks();
		}
		public static void AddParticipants()
		{
			Competition.Participants.Add(new Driver("Mike", 2, new Car(10, 10, 10, false), TeamColors.Blue));
			Competition.Participants.Add(new Driver("Chrimst", 2, new Car(1, 10, 10, false), TeamColors.Green));
			Competition.Participants.Add(new Driver("Bruger", 2, new Car(7, 10, 10, false), TeamColors.Yellow));
			Competition.Participants.Add(new Driver("Pimpin", 2, new Car(10, 10, 10, false), TeamColors.Red));
		}
		public static void AddTracks()
		{
			Competition.Tracks.Enqueue(new Track("Spacebase", TrackBuilder("Spacebase")));
			Competition.Tracks.Enqueue(new Track("Rainbow Spaceroad", TrackBuilder("Rainbow Spaceroad")));
			Competition.Tracks.Enqueue(new Track("Vroemvroem-in-da-rondje", TrackBuilder("Vroemvroem-in-da-rondje")));
		}
		// Takes the tracknname and builds the track. 
		// Tracks are stored here
		public static SectionTypes[] TrackBuilder(string trackName)
		{
			if (trackName.Equals("Rainbow Spaceroad"))
			{
				SectionTypes[] build = new SectionTypes[]
				{
						SectionTypes.Finish,
						SectionTypes.StartGrid,
						SectionTypes.CornerNE,
						SectionTypes.StraightVertical,
						SectionTypes.CornerSE,
						SectionTypes.Straight,
						SectionTypes.Straight,
						SectionTypes.CornerSW,
						SectionTypes.StraightVertical,
						SectionTypes.CornerNW
				};
				return build;
			}
			if (trackName.Equals("Spacebase"))
			{
				SectionTypes[] build = new SectionTypes[]
				{
						SectionTypes.StartGrid,
						SectionTypes.CornerNE,
						SectionTypes.StraightVertical,
						SectionTypes.StraightVertical,
						SectionTypes.CornerSE,
						SectionTypes.Straight,
						SectionTypes.CornerSW,
						SectionTypes.StraightVertical,
						SectionTypes.CornerNE,
						SectionTypes.CornerSW,
						SectionTypes.CornerNW,
						SectionTypes.Finish
				};
				return build;
			}
			if (trackName.Equals("Vroemvroem-in-da-rondje"))
			{
				SectionTypes[] build = new SectionTypes[]
				{
						SectionTypes.StartGrid,
						SectionTypes.CornerNE,
						SectionTypes.Straight,
						SectionTypes.CornerNE,
						SectionTypes.Straight,
						SectionTypes.CornerNE,
						SectionTypes.Straight,
						SectionTypes.CornerNE,
						SectionTypes.Finish

				};
				return build;
			}
			else
			{
				SectionTypes[] build = new SectionTypes[]
				{
					SectionTypes.StartGrid,
				};
				return build;
			}
		}
		// Makes a new race if it didn't exist, otherwise goes to the next race
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