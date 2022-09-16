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

		#region graphics
		private static string[] _finishHorizontal = { "----------", "  # ", "  # ", "----------" };
		private static string[] _straightPath = {"--------", " ", " ", "--------" };
		#endregion

		public static void DrawTrack(Track track)
		{
			Console.WriteLine(_finishHorizontal);
		}
	}
}
