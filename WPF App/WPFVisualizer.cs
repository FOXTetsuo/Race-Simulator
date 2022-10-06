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
		
		public static Direction _direction { get; set; }
		public static int TrackWidth { get; set; }
		public static int TrackHeight { get; set; }
		public static int xpos { get; set; }
		public static int ypos { get; set; }
		
		public const int imageSize = 128;
		public static Race Race { get; set; }

		public static void Initialize(Race race)
		{
			//TrackWidth = 10;
			//TrackHeight = 10;
			xpos = 0;
			ypos = 0;
			Race = race;
			Data.CurrentRace.RaceFinished += OnRaceFinished;
			DetermineTrackWidthAndHeight();
		}

		public static int CalculateXDraw()
		{
			return xpos * imageSize;
		}

		public static int CalculateYDraw()
		{
			return ypos * imageSize;
		}
		
		public static BitmapSource DrawTrack(Track track)
		{
			Bitmap canvas = new Bitmap(TrackWidth * imageSize, TrackHeight * imageSize);
			Graphics graphics = Graphics.FromImage(canvas);

			foreach (Section section in Race.Track.Sections)
			{
				switch (section.SectionType)
				{
					case SectionTypes.StartGrid:
						graphics.DrawImage(ImageHandler.CloneImageFromCache(StartGrid), CalculateXDraw(), CalculateYDraw());
						break;
					case SectionTypes.Finish:
						graphics.DrawImage(ImageHandler.CloneImageFromCache(Finish), CalculateXDraw(), CalculateYDraw());
						break;
					case SectionTypes.Straight:
						graphics.DrawImage(ImageHandler.CloneImageFromCache(Straight), CalculateXDraw(), CalculateYDraw());
						break;
					case SectionTypes.StraightVertical:
						graphics.DrawImage(ImageHandler.CloneImageFromCache(StraightVertical), CalculateXDraw(), CalculateYDraw());
						break;
					case SectionTypes.CornerNE:
						graphics.DrawImage(ImageHandler.CloneImageFromCache(CornerNE), CalculateXDraw(), CalculateYDraw());
						break;
					case SectionTypes.CornerNW:
						graphics.DrawImage(ImageHandler.CloneImageFromCache(CornerNW), CalculateXDraw(), CalculateYDraw());
						break;
					case SectionTypes.CornerSE:
						graphics.DrawImage(ImageHandler.CloneImageFromCache(CornerSE), CalculateXDraw(), CalculateYDraw());
						break;
					case SectionTypes.CornerSW:
						graphics.DrawImage(ImageHandler.CloneImageFromCache(CornerSW), CalculateXDraw(), CalculateYDraw());
						break;
				}
				DetermineDirection(section.SectionType, _direction);
			}
			//return map from cache
			
			//if (!ImageHandler._trackImageCache.ContainsKey(track.Name))
			//{
			//	ImageHandler._trackImageCache.Add(track.Name, ImageHandler.CreateBitmapSourceFromGdiBitmap(canvas));
			//}
			return (ImageHandler.CreateBitmapSourceFromGdiBitmap(canvas));
		}


		public static void DetermineTrackWidthAndHeight()
		{
			int XCurrent = 0;
			int YCurrent = 1;
			int XMin = 0;
			int YMin = 0;
			int XMax = 0;
			int YMax = 0;

			foreach (Section section in Race.Track.Sections)
			{

				if (_direction == Direction.East)
				{
					XCurrent += 1;
					if (XCurrent >= XMax)
					{
						XMax = XCurrent;
					}
					
				}
				if (_direction == Direction.West)
				{
					XCurrent -= 1;
					if (XCurrent <= XMin)
					{
						XMin = XCurrent;
					}
				}
				if (_direction == Direction.North)
				{
					YCurrent += -1;
					if (YCurrent <= YMin)
					{
						YMin = YCurrent;
					}
				}
				if (_direction == Direction.South)
				{
					YCurrent += 1;
					if (YCurrent >= YMax)
					{
						YMax = YCurrent;
					}
				}

				DetermineDirection(section.SectionType, _direction);
			}
			TrackWidth = (XMax - XMin + 1);
			TrackHeight = (YMax - YMin + 1);
		}
		
		public static void DetermineDirection(SectionTypes type, Direction dir)
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

			moveImageCursor();		

		}

		public static void moveImageCursor()
		{
			switch (_direction)
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