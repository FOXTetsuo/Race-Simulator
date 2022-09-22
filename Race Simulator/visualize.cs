﻿using Controller;
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
			//direction init moet zo weg kunnen
			xpos = 20;
			ypos = 5;
			Race = race;
		}
		// graphics voor alles afmaken
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
		private static string[] _NW = { 
			"-----", 
			"    -", 
			"1   -", 
			"   2-", 
			"    -" };
		private static string[] _SW = { 
			"-----", 
			"-   2", 
			"-    ", 
			"- 1  ", 
			"-    " };
		private static string[] _NE = { 
			"    -", 
			"  2 -", 
			"1   -", 
			"    -",
			"-----" };
		private static string[] _SE = { 
			"-1   ", 
			"-    ", 
			"-  2 ", 
			"-    ", 
			"-----" };
		private static string[] _start = { 
			"-----", 
			"  1 >",  
			"    >", 
			"  2 >", 
			"-----" };
		#endregion
		public static void DrawTrack(LinkedList<Section> sectionList)
		{
			foreach (Section section in sectionList)
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
						PrintTrack(_NW, Race.GetSectionData(section));
						break;
					case SectionTypes.CornerSW:
						DetermineDirection(SectionTypes.CornerSW, Direction);
						PrintTrack(_SW, Race.GetSectionData(section));
						break;
					case SectionTypes.CornerSE:
						DetermineDirection(SectionTypes.CornerSE, Direction);
						PrintTrack(_NE, Race.GetSectionData(section));
						break;
					case SectionTypes.CornerNW:
						DetermineDirection(SectionTypes.CornerNW, Direction);
						PrintTrack(_SE, Race.GetSectionData(section));
						break;
					case SectionTypes.StartGrid:
						Direction = 90;
						PrintTrack(_start, Race.GetSectionData(section));
						break;
				}
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
				// replace leftover 1/2 with space
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
				case SectionTypes.CornerNW:
					if (dir == 270)
					{
						Direction = 0;
					}
					else if (dir == 180)
					{
						Direction = 90;
					}
					break;
				case SectionTypes.CornerSW:
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

		public static string ReplaceString(string stringmetnummer, IParticipant participant1, IParticipant participant2)
		{
			return(stringmetnummer.Replace("1", participant1.Name[0].ToString()).Replace("2", participant2.Name[0].ToString()));
		}

	}
}