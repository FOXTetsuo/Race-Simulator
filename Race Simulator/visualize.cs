using Controller;
using Model;

namespace Race_Simulator
{
	public static class Visualize
	{
		static Direction _direction;
		static int xpos;
		static int ypos;
		static Race Race;
		public static void Initialize(Race race)
		{
			xpos = 20;
			ypos = 15;
			Race = race;
			Data.CurrentRace.DriversChanged += OnDriversChanged;
		}

		public enum Direction
		{
			North = 0,
			East = 90,
			South = 180,
			West = 270
		}

		#region graphics
		private static string[] _finishHorizontal = {
			"-----", 
			"   #2", 
			"   # ",
			"  1# ", 
			"-----" };
		private static string[] _straightPath = { 
			"-----", 
			"   1 ", 
			"     ", 
			" 2   ", 
			"-----" };
		private static string[] _straightPathVertical = { 
			"-1  -", 
			"-   -", 
			"-   -", 
			"-  2-", 
			"-   -" };
		private static string[] NE = { 
			"----\\", 
			"    -", 
			"1   -", 
			"   2-", 
			"\\   -" };
		private static string[] _NW = { 
			"/----", 
			"-   2", 
			"-    ", 
			"- 1  ", 
			"-   /" };
		private static string[] _SE = { 
			"/   -", 
			"  2 -", 
			"1   -", 
			"    -",
			"----/" };
		private static string[] _SW = { 
			"-1  \\", 
			"-    ", 
			"-  2 ", 
			"-    ", 
			"\\----" };
		private static string[] _start = { 
			"-----", 
			"  1 ]",  
			"    ]", 
			"  2 ]", 
			"-----" };
		#endregion
		// function takes a linkedlist of tracks (from currentrace.track.sections)
		// searches which section it's currently dealing with
		// and then draws that section, by calling PrintTrack.
		// sectiondata is given to put drivers on the track in the right place.
		// then when it exits the loop, updated xpos & ypos.
		public static void DrawTrack(Track track)
		{
			foreach (Section section in track.Sections)
			{
				switch (section.SectionType)
				{
					case SectionTypes.Finish:
						PrintTrack(_finishHorizontal, Race.GetSectionData(section));
						break;
					case SectionTypes.Straight:
						PrintTrack(_straightPath, Race.GetSectionData(section));
						break;
					case SectionTypes.StraightVertical:
						PrintTrack(_straightPathVertical, Race.GetSectionData(section));
						break;
					case SectionTypes.CornerNE:
						DetermineDirection(SectionTypes.CornerNE, _direction);
						PrintTrack(NE, Race.GetSectionData(section));
						break;
					case SectionTypes.CornerNW:
						DetermineDirection(SectionTypes.CornerNW, _direction);
						PrintTrack(_NW, Race.GetSectionData(section));
						break;
					case SectionTypes.CornerSE:
						DetermineDirection(SectionTypes.CornerSE, _direction);
						PrintTrack(_SE, Race.GetSectionData(section));
						break;
					case SectionTypes.CornerSW:
						DetermineDirection(SectionTypes.CornerSW, _direction);
						PrintTrack(_SW, Race.GetSectionData(section));
						break;
					case SectionTypes.StartGrid:
						// Hardcoded direction for now, can be changed by adding onto PrintTrack.
						_direction = Direction.East;
						PrintTrack(_start, Race.GetSectionData(section));
						break;
				}
				// set where to draw the next part
				Console.SetCursorPosition(xpos, ypos);
			}
		
		}
		public static void PrintTrack(string[] type, SectionData data)
		{
			foreach (string s in type)
			{
				string temp = s;
				// if drivers exist - replace 1/2 with their initals
				//pakt sectiondata van trackl
				// kijkt naar of de 
				if (data.Right != null && data.Left != null)
				{
					temp = ReplaceString(s,data.Left,data.Right);
				}
				else if (data.Left != null)
				{
					temp = ReplaceString(s, data.Left);
				}
				else if (data.Right != null)
				{
					temp = ReplaceString(s, data.Right);
				}
				Console.SetCursorPosition(xpos, ypos);
				// replace leftover "1"/"2" with space
				Console.Write(temp.Replace("1", " ").Replace("2", " "));
				ypos += 1;
			}
			if (_direction == Direction.East)
			{
				ypos += -5;
				xpos += 5;
			}
			if (_direction == Direction.West)
			{
				ypos += -5;
				xpos += -5;
			}
			if (_direction == Direction.North)
			{
				ypos += -10;
			}
		}

		public static void DetermineDirection(SectionTypes type, Direction dir )
		{
			switch (type)
			{
				// sets the direction according to where the corner is entered
				case SectionTypes.CornerNE:
					if (dir == Direction.East)
					{
						_direction = Direction.South;
					}
					else if (dir == Direction.North)
					{
						_direction = Direction.West;
					}
					break;
				case SectionTypes.CornerSE:
					if (dir == Direction.South)
					{
						_direction = Direction.West;
					}
					else if (dir == Direction.East)
					{
						_direction = Direction.North;
					}
					break;
				case SectionTypes.CornerSW:
					if (dir == Direction.West)
					{
						_direction = Direction.North;
					}
					else if (dir == Direction.South)
					{
						_direction = Direction.East;
					}
					break;
				case SectionTypes.CornerNW:
					if (dir == Direction.North)
					{
						_direction = Direction.East;
					}
					else if (dir == Direction.West)
					{
						_direction = Direction.South;
					}
					break;
			}
		}

		// replace 1/2's with participants 
		public static string ReplaceString(string stringmetnummer, IParticipant participant1, IParticipant participant2)
		{
			return(stringmetnummer.Replace("1", participant1.Name[0].ToString()).Replace("2", participant2.Name[0].ToString()));
		}

		// method ovearload in case only 1 driver needs to be moved
		public static string ReplaceString(string stringmetnummer, IParticipant participant)
		{
			if (Race.GetSectionData(participant.CurrentSection).Left == participant)
			{
				return (stringmetnummer.Replace("1", participant.Name[0].ToString()));
			}
			else if (Race.GetSectionData(participant.CurrentSection).Right == participant)
			{
				return (stringmetnummer.Replace("2", participant.Name[0].ToString()));
			}
			else return null;
		}


		public static void OnDriversChanged(object sender, DriversChangedEventArgs args)
		{
			Console.Clear();
			DrawTrack(args.Track);
		}
	}
}