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
		static int direction;
		static int xpos;
		static int ypos;
		public static void Initialize()
		{
			//direction init moet zo weg kunnen
			direction = 90;
			xpos = 10;
			ypos = 10;
		}
		// graphics voor alles afmaken
		#region graphics
		private static string[] _finishHorizontal = { "-----", "  #  ", "  #  ", "  #  ", "-----" };
		private static string[] _straightPath = { "-----", "     ", "     ", "     ", "-----" };
		private static string[] _straightPathVertical = { "-   -", "-   -", "-   -", "-   -", "-   -" };
		private static string[] _NW = { "-----", "    -", "    -", "    -", "    -" };
		private static string[] _SW = { "-----", "-    ", "-    ", "-    ", "-    " };
		private static string[] _NE = { "    -", "    -", "    -", "    -", "-----" };
		private static string[] _SE = { "-    ", "-    ", "-    ", "-    ", "-----" };
		private static string[] _start = { "-----", ">    ",  ">    ", ">    ", "-----" };
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
						determineDirection(SectionTypes.CornerNE, direction);
						PrintTrack(_NW);
						break;
					case SectionTypes.CornerSW:
						determineDirection(SectionTypes.CornerSW, direction);
						PrintTrack(_SW);
						break;
					case SectionTypes.CornerSE:
						determineDirection(SectionTypes.CornerSE, direction);
						PrintTrack(_NE);
						break;
					case SectionTypes.CornerNW:
						determineDirection(SectionTypes.CornerNW, direction);
						PrintTrack(_SE);
						break;
					case SectionTypes.StartGrid:
						direction = 90;
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
				Console.SetCursorPosition(xpos, ypos);
				Console.Write(s);
				ypos += 1;
			}
			if (direction == 90)
			{
				ypos += -5;
				xpos += 5;
			}
			if (direction == 270)
			{
				ypos += -5;
				xpos += -5;
			}
			if (direction == 0)
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
						direction = 180;
					}
					else if (dir == 0)
					{
						direction = 270;
					}
					break;
				case SectionTypes.CornerSE:
					if (dir == 180)
					{
						direction = 270;
					}
					else if (dir == 90)
					{
						direction = 0;
					}
					break;
				case SectionTypes.CornerNW:
					if (dir == 270)
					{
						direction = 0;
					}
					else if (dir == 180)
					{
						direction = 90;
					}
					break;
				case SectionTypes.CornerSW:
					if (dir == 0)
					{
						direction = 90;
					}
					else if (dir == 270)
					{
						direction = 180;
					}
					break;
			}
		}
	}
}