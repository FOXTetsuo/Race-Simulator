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
		}
		public static void AddTracks()
		{
//			Competition.Tracks.Enqueue(new Track("STAROFDACOMPETITION", TrackBuilder("STAROFDACOMPETITION")));
//			Competition.Tracks.Enqueue(new Track("MEWHENDARACEISBIG", TrackBuilder("MEWHENDARACEISBIG")));
			Competition.Tracks.Enqueue(new Track("NYEEEEEEEEEEEEEEEEEEEOM", TrackBuilder("NYEEEEEEEEEEEEEEEEEEEOM")));
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
						SectionTypes.StartGrid,
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
						SectionTypes.Finish,
						SectionTypes.StartGrid,
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
				SectionTypes[] build = new SectionTypes[]
				{
						SectionTypes.CornerNW,
						SectionTypes.StartGrid,
						SectionTypes.CornerNE,
						SectionTypes.CornerSE,
						SectionTypes.Finish,
						SectionTypes.CornerSW,




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
			if (newTrack != null)
			{
				CurrentRace = new Race(newTrack, Competition.Participants);
			}
			else
			{
				// competition eds, pls call a function for it.
			}
		}


	}


}