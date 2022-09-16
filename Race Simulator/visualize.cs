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
		public static void Initialize()
		{

		}
		// graphics voor alles afmaken
		#region graphics
		private static string[] _finishHorizontal = { "----------", "  # ", "  # ", "----------" };
		private static string[] _straightPath = {"--------", " ", " ", "--------" };
		private static string[] _rightCorner;
		private static string[] _leftCorner;
		private static string[] _start;
		#endregion
		// misschien het echte printen in de printTrack functie
		public static void DrawTrack(LinkedList<Section> sectionList)
		{
			foreach (Section section in sectionList)
			{
				if (section.SectionType == SectionTypes.Straight)
				{
					foreach (string s in _straightPath)
					Console.WriteLine(s);
				}
				if (section.SectionType == SectionTypes.RightCorner)
				{
					foreach (string s in _rightCorner)
						Console.WriteLine(s);
				}
			}
		
		}
		public static void PrintTrack()
		{

		}
	}
}
