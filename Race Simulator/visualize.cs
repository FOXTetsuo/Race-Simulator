using Controller;
using Model;

namespace Race_Simulator
{
	public static class Visualize
	{
		static int Direction;
		static int xpos;
		static int ypos;
		static Race Race;
		public static void Initialize(Race race)
		{
			xpos = 20;
			ypos = 5;
			Race = race;
			Data.CurrentRace.DriversChanged += OnDriversChanged;
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
						DetermineDirection(SectionTypes.CornerNE, Direction);
						PrintTrack(NE, Race.GetSectionData(section));
						break;
					case SectionTypes.CornerNW:
						DetermineDirection(SectionTypes.CornerNW, Direction);
						PrintTrack(_NW, Race.GetSectionData(section));
						break;
					case SectionTypes.CornerSE:
						DetermineDirection(SectionTypes.CornerSE, Direction);
						PrintTrack(_SE, Race.GetSectionData(section));
						break;
					case SectionTypes.CornerSW:
						DetermineDirection(SectionTypes.CornerSW, Direction);
						PrintTrack(_SW, Race.GetSectionData(section));
						break;
					case SectionTypes.StartGrid:
						// Hardcoded direction for now, can be changed by adding onto PrintTrack.
						Direction = 90;
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
				if (data.Right != null && data.Left != null)
				{
					temp = ReplaceString(s,data.Left,data.Right);
				}
				Console.SetCursorPosition(xpos, ypos);
				// replace leftover "1"/"2" with space
				Console.Write(temp.Replace("1", " ").Replace("2", " "));
				ypos += 1;
			}
			if (Direction == 90)
			{
				ypos += -5;
				xpos += 5;
			}
			if (Direction == 270)
			{
				ypos += -5;
				xpos += -5;
			}
			if (Direction == 0)
			{
				ypos += -10;
			}
		}

		public static void DetermineDirection(SectionTypes type, int dir )
		{
			switch (type)
			{
				// sets the direction according to where the corner is entered
				case SectionTypes.CornerNE:
					if (dir == 90)
					{
						Direction = 180;
					}
					else if (dir == 0)
					{
						Direction = 270;
					}
					break;
				case SectionTypes.CornerSE:
					if (dir == 180)
					{
						Direction = 270;
					}
					else if (dir == 90)
					{
						Direction = 0;
					}
					break;
				case SectionTypes.CornerSW:
					if (dir == 270)
					{
						Direction = 0;
					}
					else if (dir == 180)
					{
						Direction = 90;
					}
					break;
				case SectionTypes.CornerNW:
					if (dir == 0)
					{
						Direction = 90;
					}
					else if (dir == 270)
					{
						Direction = 180;
					}
					break;
			}
		}

		// replace 1/2's with participants 
		public static string ReplaceString(string stringmetnummer, IParticipant participant1, IParticipant participant2)
		{
			return(stringmetnummer.Replace("1", participant1.Name[0].ToString()).Replace("2", participant2.Name[0].ToString()));
		}

		public static void OnDriversChanged(object sender, DriversChangedEventArgs args)
		{
			DrawTrack(args.Track);
		}
	}
}