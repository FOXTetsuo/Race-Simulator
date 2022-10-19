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
			Competition.Participants.Add(new Inkling("Mike", new Genetics(10, 5, 10, false), TeamColors.Blue));
			Competition.Participants.Add(new Inkling("Chrimst", new Genetics(1, 5, 10, false), TeamColors.Green));
			Competition.Participants.Add(new Inkling("Bruger",  new Genetics(7, 10, 10, false), TeamColors.Yellow));
			Competition.Participants.Add(new Inkling("Pimpin", new Genetics(10, 10, 10, false), TeamColors.Red));
		}
		public static void AddTracks()
		{
			Competition.Tracks.Enqueue(new Track("STAROFDACOMPETITION", TrackBuilder("STAROFDACOMPETITION")));
			Competition.Tracks.Enqueue(new Track("MEWHENDARACEISBIG", TrackBuilder("MEWHENDARACEISBIG")));
			Competition.Tracks.Enqueue(new Track("NYEEEEEEEEEEEEEEEEEEEOM", TrackBuilder("NYEEEEEEEEEEEEEEEEEEEOM")));
		}
		
		public static SectionTypes[] TrackBuilder(string trackName)
		// Takes the tracknname and builds the track. 
		// Tracks are stored here
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
		
		public static void NextRace()
		// Makes a new track from the queue of races. If there are no races to be loaded, end the competition
		{
			Track newTrack = Competition.NextTrack();
			if (newTrack != null)
			{
				CurrentRace = new Race(newTrack, Competition.Participants);
			}
			else
			{
				//TODO: competition ends, pls call a function for it.
				EndCompetition();
			}
		}
		private static void EndCompetition()
		{
			CurrentRace.Cleaner();
			// TODO: remove all tracks, reset all drivers, collect points
			// TODO: put drivers in leaderboard, show instead of other PNG
			// TODO: add button to restart competition
		}
	}
}