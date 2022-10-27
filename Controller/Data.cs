 using Model;
using System.Runtime.CompilerServices;

namespace Controller
{
	public static class Data
	{
		public static Competition? Competition { get; set; }
		public static Race? CurrentRace { get; set; }
		public static void Initialize()
		{
			Competition = new();
			AddParticipants();
			AddTracks();
		}
		public static void AddParticipants()
		{
			Competition.Participants.Add(new Inkling("Mike", new Genetics(), TeamColors.Orange));
			Competition.Participants.Add(new Inkling("Chrimst", new Genetics(), TeamColors.Green));
			Competition.Participants.Add(new Inkling("Bruger",  new Genetics(), TeamColors.Purple));
			Competition.Participants.Add(new Inkling("Pimpin", new Genetics(), TeamColors.Red));

		}
		public static void AddTracks()
		{
			Competition.Tracks.Enqueue(new Track("Skifftastic race", TrackBuilder("Skifftastic race")));
			//Competition.Tracks.Enqueue(new Track("Splat city", TrackBuilder("Splat city")));
			//Competition.Tracks.Enqueue(new Track("NYEEEEEEEEEEEEEEEEEEEOM", TrackBuilder("NYEEEEEEEEEEEEEEEEEEEOM")));
		}
		
		public static SectionTypes[] TrackBuilder(string trackName)
		// Takes the tracknname and builds the track.
		{
			if (trackName.Equals("Skifftastic race"))
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
			if (trackName.Equals("Splat city"))
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
		
		public static bool NextRace()
		// Makes a new track from the queue of races. If there are no races to be loaded, end the competition
		{
			Track newTrack = Competition.NextTrack();
			if (newTrack != null)
			{
				CurrentRace = new Race(newTrack, Competition.Participants);
				return true;
			}
			else
			{		
				Competition.EndCompetition();
				return false;
			}
		}

	}
}

