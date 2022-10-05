using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace WPF_App
{
	public static class WPFVisualizer
	{
		public enum Direction
		{
			North = 0,
			East = 90,
			South = 180,
			West = 270
		}
		public static Direction _direction { get; set; }
		public static int xpos { get; set; }
		public static int ypos { get; set; }
		public static Race Race { get; set; }
		public static BitmapSource DrawTrack(Track track)
		{
			BitmapSource bitmapSource = ImageHandler.CreateBitmapSourceFromGdiBitmap(ImageHandler.DrawImage(0, 0,CornerNW));
			return bitmapSource;

			if (_direction == Direction.East)
			{
				xpos += 192;
			}
			if (_direction == Direction.West)
			{
				xpos += -192;
			}
			if (_direction == Direction.North)
			{
				ypos += -192;
			}
			if (_direction == Direction.South)
			{
				ypos += 192;
			}

		}
		#region Graphics
		// Change to local reference?
		private const String CornerNE = "C:\\Users\\Pownu\\source\\repos\\Race Simulator\\WPF App\\WPF Images\\Road\\CornerNEXL.png";
		private const String CornerNW = "C:\\Users\\Pownu\\source\\repos\\Race Simulator\\WPF App\\WPF Images\\Road\\CornerNWXL.png";
		private const String CornerSE = "C:\\Users\\Pownu\\source\\repos\\Race Simulator\\WPF App\\WPF Images\\Road\\CornerSEXl.png";
		private const String CornerSW = "C:\\Users\\Pownu\\source\\repos\\Race Simulator\\WPF App\\WPF Images\\Road\\CornerSWXL.png";
		private const String Straight = "C:\\Users\\Pownu\\source\\repos\\Race Simulator\\WPF App\\WPF Images\\Road\\HorizontalXL.png";
		private const String StraightVertical = "C:\\Users\\Pownu\\source\\repos\\Race Simulator\\WPF App\\WPF Images\\Road\\VerticalXL.png";
		private const String Finish = "C:\\Users\\Pownu\\source\\repos\\Race Simulator\\WPF App\\WPF Images\\Road\\VerticalXL.png";
		#endregion
	}
}
