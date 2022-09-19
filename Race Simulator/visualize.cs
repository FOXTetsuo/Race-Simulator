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
			direction = 90;
			xpos = 0;
			ypos = 0;
		}
		// graphics voor alles afmaken
		#region graphics
		private static string[] _finishHorizontal = { "-----", "  #  ", "  #  ", "  #  ", "-----" };
		private static string[] _straightPath = { "-----", "     ", "     ", "     ", "-----" };
		private static string[] _straightPathVertical = { "-   -", "-   -", "-   -", "-   -", "-   -" };
		private static string[] _rightCorner = { "-----", "    -", "    -", "    -", "    -" };
		private static string[] _rightCornerReversed = { "-----", "-    ", "-    ", "-    ", "-    " };
		private static string[] _leftCorner = { "    -", "    -", "    -", "    -", "-----" };
		private static string[] _leftCornerReversed = { "-    ", "-    ", "-    ", "-    ", "-----" };
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
					case SectionTypes.RightCorner:
						direction = 180;
						PrintTrack(_rightCorner);
						break;
					case SectionTypes.RightCornerReversed:
						direction = 90;
						PrintTrack(_rightCornerReversed);
						break;
					case SectionTypes.LeftCorner:
						direction = 270;
						PrintTrack(_leftCorner);
						break;
					case SectionTypes.LeftCornerReversed:
						direction = 0;
						PrintTrack(_leftCornerReversed);
						break;
					case SectionTypes.StartGrid:
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
	}
}