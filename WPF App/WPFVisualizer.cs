using Controller;
using Model;
using System;
using System.Drawing;
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
		public static void Initialize(Race race)
		{
			XSize = 10;
			YSize = 10;
			xpos = 0;
			ypos = 0;
			Race = race;
			Data.CurrentRace.RaceFinished += OnRaceFinished;
			//DetermineTrackWidthAndHeight();
		}
		public static Direction _direction { get; set; }
		public static int XSize { get; set; }
		public static int YSize { get; set; }
		public static int xpos { get; set; }
		public static int ypos { get; set; }
		public const int imageSize = 128;
		public static Race Race { get; set; }
		public static BitmapSource DrawTrack(Track track)
		{
			string sectiontypename;
			Bitmap bitmap = new Bitmap(XSize * imageSize, YSize * imageSize);
			Graphics graphics = Graphics.FromImage(bitmap);

			foreach (Section section in Race.Track.Sections)
			{
				switch (section.SectionType)
				{
					case SectionTypes.StartGrid:
						sectiontypename = StartGrid;
						graphics.DrawImage(ImageHandler.DrawImage(sectiontypename), xpos * imageSize, ypos * imageSize);
						break;
					case SectionTypes.Finish:
						sectiontypename = Finish;
						graphics.DrawImage(ImageHandler.DrawImage(sectiontypename), xpos * imageSize, ypos * imageSize);
						break;
					case SectionTypes.Straight:
						sectiontypename = Straight;
						graphics.DrawImage(ImageHandler.DrawImage(sectiontypename), xpos * imageSize, ypos * imageSize);
						break;
					case SectionTypes.StraightVertical:
						sectiontypename = StraightVertical;
						graphics.DrawImage(ImageHandler.DrawImage(sectiontypename), xpos * imageSize, ypos * imageSize);
						break;
					case SectionTypes.CornerNE:
						sectiontypename = CornerNE;
						graphics.DrawImage(ImageHandler.DrawImage(sectiontypename), xpos * imageSize, ypos * imageSize);
						break;
					case SectionTypes.CornerNW:
						sectiontypename = CornerNW;
						graphics.DrawImage(ImageHandler.DrawImage(sectiontypename), xpos * imageSize, ypos * imageSize);
						break;
					case SectionTypes.CornerSE:
						sectiontypename = CornerSE;
						graphics.DrawImage(ImageHandler.DrawImage(sectiontypename), xpos * imageSize, ypos * imageSize);
						break;
					case SectionTypes.CornerSW:
						sectiontypename = CornerSW;
						graphics.DrawImage(ImageHandler.DrawImage(sectiontypename), xpos * imageSize, ypos * imageSize);
						break;
				}
				DetermineDirection(section.SectionType, _direction);
			}

			return (ImageHandler.CreateBitmapSourceFromGdiBitmap(bitmap));
		}


		public static void DetermineTrackWidthAndHeight()
		{
			int XCurrent = 0;
			int YCurrent = 1;

			foreach (Section section in Race.Track.Sections)
			{

				if (_direction == Direction.East)
				{
					XCurrent += 1;
					if (XCurrent >= XSize)
					{
						XSize = XCurrent;
					}
				}
				if (_direction == Direction.West)
				{
					XCurrent -= 1;
				}
				if (_direction == Direction.North)
				{
					YCurrent += -1;
				}
				if (_direction == Direction.South)
				{
					YCurrent += 1;
					if (YCurrent >= YSize)
					{
						YSize = YCurrent;
					}
				}
				DetermineDirection(section.SectionType, _direction);
			}
		}
		
		public static void DetermineDirection(SectionTypes type, Direction dir)
		// hier gaat fout xDDDDDDDDD
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
				case SectionTypes.StartGrid:
					_direction = Direction.East;
					break;
			}

			switch(_direction)
			{
				case Direction.East:
					xpos += 1;
					break;
				case Direction.South:
					ypos += 1;
					break;
				case Direction.West:
					xpos += -1;
					break;
				case Direction.North:
					ypos += -1;
						break;
			}

			//xpos += 1;
		}

		public static void OnRaceFinished(object sender, EventArgs args)
		{
			//Initialize(Data.CurrentRace);
			//DrawTrack(Data.CurrentRace.Track);
		}

		#region Graphics
		// Change to local reference?
		//private const String StartGrid = "C:\\Users\\Pownu\\source\\repos\\Race Simulator\\WPF App\\WPF Images\\Road\\HorizontalXL.png";
		//private const String CornerNE = "C:\\Users\\Pownu\\source\\repos\\Race Simulator\\WPF App\\WPF Images\\Road\\CornerNEXL.png";
		//private const String CornerNW = "C:\\Users\\Pownu\\source\\repos\\Race Simulator\\WPF App\\WPF Images\\Road\\CornerNWXL.png";
		//private const String CornerSE = "C:\\Users\\Pownu\\source\\repos\\Race Simulator\\WPF App\\WPF Images\\Road\\CornerSEXl.png";
		//private const String CornerSW = "C:\\Users\\Pownu\\source\\repos\\Race Simulator\\WPF App\\WPF Images\\Road\\CornerSWXL.png";
		//private const String Straight = "C:\\Users\\Pownu\\source\\repos\\Race Simulator\\WPF App\\WPF Images\\Road\\HorizontalXL.png";
		//private const String StraightVertical = "C:\\Users\\Pownu\\source\\repos\\Race Simulator\\WPF App\\WPF Images\\Road\\VerticalXL.png";
		//private const String Finish = "C:\\Users\\Pownu\\source\\repos\\Race Simulator\\WPF App\\WPF Images\\Road\\FinishLineXL.png";
		#endregion

		#region Graphics_Large
		// Change to local reference?
		private const String StartGrid = "C:\\Users\\Pownu\\source\\repos\\Race Simulator\\WPF App\\WPF Images\\Road_L\\StraightL.png";
		private const String CornerNE = "C:\\Users\\Pownu\\source\\repos\\Race Simulator\\WPF App\\WPF Images\\Road_L\\CornerNEL.png";
		private const String CornerNW = "C:\\Users\\Pownu\\source\\repos\\Race Simulator\\WPF App\\WPF Images\\Road_L\\CornerNWL.png";
		private const String CornerSE = "C:\\Users\\Pownu\\source\\repos\\Race Simulator\\WPF App\\WPF Images\\Road_L\\CornerSEl.png";
		private const String CornerSW = "C:\\Users\\Pownu\\source\\repos\\Race Simulator\\WPF App\\WPF Images\\Road_L\\CornerSWL.png";
		private const String Straight = "C:\\Users\\Pownu\\source\\repos\\Race Simulator\\WPF App\\WPF Images\\Road_L\\StraightL.png";
		private const String StraightVertical = "C:\\Users\\Pownu\\source\\repos\\Race Simulator\\WPF App\\WPF Images\\Road_L\\VerticalL.png";
		private const String Finish = "C:\\Users\\Pownu\\source\\repos\\Race Simulator\\WPF App\\WPF Images\\Road_L\\FinishLineL.png";
		#endregion
	}
}