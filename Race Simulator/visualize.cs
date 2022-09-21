using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Race_Simulator
{
	public static class Visualize
	{
		static int Direction;
		static int xpos;
		static int ypos;
		public static void Initialize()
		{
			//direction init moet zo weg kunnen
			xpos = 20;
			ypos = 5;
		}
		// graphics voor alles afmaken
		#region graphics
		private static string[] _finishHorizontal = {
			"-----", 
			"   # ", 
			"   # ",
			"   # ", 
			"-----" };
		private static string[] _straightPath = { 
			"-----", 
			"    1", 
			"     ", 
			"  2  ", 
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
						PrintTrack(_finishHorizontal);
						break;
					case SectionTypes.Straight:
						PrintTrack(_straightPath);
						break;
					case SectionTypes.StraightVertical:
						PrintTrack(_straightPathVertical);
						break;
					case SectionTypes.CornerNE:
						determineDirection(SectionTypes.CornerNE, Direction);
						PrintTrack(_NW);
						break;
					case SectionTypes.CornerSW:
						determineDirection(SectionTypes.CornerSW, Direction);
						PrintTrack(_SW);
						break;
					case SectionTypes.CornerSE:
						determineDirection(SectionTypes.CornerSE, Direction);
						PrintTrack(_NE);
						break;
					case SectionTypes.CornerNW:
						determineDirection(SectionTypes.CornerNW, Direction);
						PrintTrack(_SE);
						break;
					case SectionTypes.StartGrid:
						Direction = 90;
						PrintTrack(_start);
						break;
				}
				Console.SetCursorPosition(xpos, ypos);
			}
		
		}
		public static void PrintTrack(string[] type)
		{
			foreach (string s in type)
			{
				s.Replace('1', ' ');
				s.Replace('2', ' ');
				Console.SetCursorPosition(xpos, ypos);
				Console.Write(s);
				
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

		public static void determineDirection(SectionTypes type, int dir )
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
	}
}