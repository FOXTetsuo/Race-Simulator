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
			Competition.Participants.Add(new Inkling("Mike", 2, new Car(10, 5, 10, false), TeamColors.Blue));
			Competition.Participants.Add(new Inkling("Chrimst", 2, new Car(1, 5, 10, false), TeamColors.Green));
			Competition.Participants.Add(new Inkling("Bruger", 2, new Car(7, 10, 10, false), TeamColors.Yellow));
			Competition.Participants.Add(new Inkling("Pimpin", 2, new Car(10, 10, 10, false), TeamColors.Red));
			Competition.Participants.Add(new Inkling("Bruger", 2, new Car(7, 10, 10, false), TeamColors.Yellow));
			Competition.Participants.Add(new Inkling("Bruger", 2, new Car(7, 10, 10, false), TeamColors.Yellow));
			Competition.Participants.Add(new Inkling("Bruger", 2, new Car(7, 10, 10, false), TeamColors.Yellow));
		}
		public static void AddTracks()
		{
		//	Competition.Tracks.Enqueue(new Track("STAROFDACOMPETITION", TrackBuilder("STAROFDACOMPETITION")));
			Competition.Tracks.Enqueue(new Track("MEWHENDARACEISBIG", TrackBuilder("MEWHENDARACEISBIG")));
		//	Competition.Tracks.Enqueue(new Track("NYEEEEEEEEEEEEEEEEEEEOM", TrackBuilder("NYEEEEEEEEEEEEEEEEEEEOM")));
		}
		// Takes the tracknname and builds the track. 
		// Tracks are stored here
		public static SectionTypes[] TrackBuilder(string trackName)
		{
			if (trackName.Equals("STAROFDACOMPETITION"))
			{
				SectionTypes[] build = new SectionTypes[]
				{
						SectionTypes.CornerNW,
						SectionTypes.Finish,
						SectionTypes.Straight,
						SectionTypes.CornerNE,
						SectionTypes.StraightVertical,
						SectionTypes.CornerSE,
						SectionTypes.Straight,
						SectionTypes.Straight,
						SectionTypes.CornerSW,
						SectionTypes.StraightVertical,
						
				};
				return build;
			}
			if (trackName.Equals("MEWHENDARACEISBIG"))
			{
				SectionTypes[] build = new SectionTypes[]
				{
						SectionTypes.CornerNW,
						SectionTypes.Straight,
						SectionTypes.Finish,
						SectionTypes.CornerNE,
						SectionTypes.StraightVertical,
						SectionTypes.StraightVertical,
						SectionTypes.CornerSE,
						SectionTypes.Straight,
						SectionTypes.CornerSW,
						SectionTypes.StraightVertical,
						SectionTypes.CornerNE,
						SectionTypes.CornerSW
						
				};
				return build;
			}
			if (trackName.Equals("NYEEEEEEEEEEEEEEEEEEEOM"))
			{
				// width : 6 height : 3
				SectionTypes[] build = new SectionTypes[]
				{
						SectionTypes.CornerNW,
						SectionTypes.Straight,
						SectionTypes.Straight,
						SectionTypes.Straight,
						SectionTypes.Straight,
						SectionTypes.Finish,
						SectionTypes.CornerNE,
						SectionTypes.StraightVertical,
						SectionTypes.CornerSW,
						SectionTypes.CornerNE,
						SectionTypes.CornerSE,
						SectionTypes.Straight,
						SectionTypes.Straight,
						SectionTypes.CornerSW,
						SectionTypes.StraightVertical,
						SectionTypes.CornerNE,
						SectionTypes.CornerNW,
						SectionTypes.StraightVertical,
						SectionTypes.CornerSE,
						SectionTypes.Straight,
						SectionTypes.Straight,
						SectionTypes.CornerSW,
						SectionTypes.StraightVertical,
						SectionTypes.StraightVertical,

				};
				return build;
			}
			else
			{
				SectionTypes[] build = new SectionTypes[]
				{
					SectionTypes.Finish,
				};
				return build;
			}
		}
		// Makes a new race if it didn't exist, otherwise goes to the next race
		public static void NextRace()
		{
			Track newTrack = Competition.NextTrack();
			if (newTrack != null)
			{
				CurrentRace = new Race(newTrack, Competition.Participants);
			}
			else
			{
				//TODO: competition ends, pls call a function for it.
			}
		}
	}
}