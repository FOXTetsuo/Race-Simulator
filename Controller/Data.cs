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
			Competition.Participants.Add(new Driver("Chrimst", 2, new Car(10, 10, 10, false), TeamColors.Green));
			Competition.Participants.Add(new Driver("Stalin", 2, new Car(10, 10, 10, false), TeamColors.Yellow));
			Competition.Participants.Add(new Driver("Ballin baller", 2, new Car(10, 10, 10, false), TeamColors.Red));
		}
		public static void AddTracks()
		{
			Competition.Tracks.Enqueue(new Track("Rainbow Spaceroad", TrackBuilder("Rainbow Spaceroad")));
			Competition.Tracks.Enqueue(new Track("Spacebase", TrackBuilder("Spacebase")));
			Competition.Tracks.Enqueue(new Track("Vroemvroem-in-da-rondje", TrackBuilder("Vroemvroem-in-da-rondje")));
		}

		public static SectionTypes[] TrackBuilder(string trackName)
		{
			// dit hierondder gaat niet goed vanwege de .equals method
			if (trackName.Equals("Rainbow Spaceroad"))
			{
				SectionTypes[] build = new SectionTypes[]
				{
						SectionTypes.StartGrid,
						SectionTypes.CornerNE,
						SectionTypes.StraightVertical,
						SectionTypes.CornerSE,
						SectionTypes.Straight,
						SectionTypes.CornerNW,
						SectionTypes.StraightVertical,
						SectionTypes.CornerSW
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
						SectionTypes.StraightVertical,
						SectionTypes.CornerSE,
						SectionTypes.Straight,
						SectionTypes.CornerNW,
						SectionTypes.StraightVertical,
						SectionTypes.CornerNE,
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
			//TEMPFIX - maak hier fucking switchcases van
			{
				SectionTypes[] build = new SectionTypes[]
				{
					SectionTypes.StartGrid,
				};
				return build;
			}
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