using Controller;
using Model;
using System;
using System.Drawing;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Section = Model.Section;

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

		public enum Side
		{
			Left,
			Right
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
			DetermineImageSources();
			//TrackWidth = 10;
			//TrackHeight = 10;
			xpos = 0;
			ypos = 0;
			Race = race;
			_direction = Direction.East;
			//DetermineTrackWidthAndHeight();
			CalculateTrackSize();
		}

		public static void DetermineImageSources()
		{
			foreach (IParticipant participant in Data.CurrentRace.Participants)
			{
				switch (participant.TeamColor)
				{
					case TeamColors.Orange:
						participant.ImageSource = WPFVisualizer.Squid1;
						participant.BrokeImageSource = WPFVisualizer.Squid1_Ink;
						break;
					case TeamColors.Green:
						participant.ImageSource = WPFVisualizer.Squid2;
						participant.BrokeImageSource = WPFVisualizer.Squid2_Ink;
						break;
					case TeamColors.Purple:
						participant.ImageSource = WPFVisualizer.Squid3;
						participant.BrokeImageSource = WPFVisualizer.Squid3_Ink;
						break;
					case TeamColors.Red:
						participant.ImageSource = WPFVisualizer.Squid4;
						participant.BrokeImageSource = WPFVisualizer.Squid4_Ink;
						break;
				}
			}
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
				DrawDriversInSection(graphics, Race, section);
				DetermineDirection(section.SectionType, _direction);
				moveImageCursor();
			}
			return (ImageHandler.CreateBitmapSourceFromGdiBitmap(canvas));
		}
		private static void DrawDriversInSection(Graphics graphics, Race race, Section section)
		{
			if(race.GetSectionData(section).Left is not null || race.GetSectionData(section).Right is not null)
			{
				foreach (IParticipant participant in race.Participants)
				{
					if (participant.CurrentSection == section)
					{
						if (race.GetSectionData(section).Left == participant)
						{
							DrawParticipant(graphics, participant, Side.Left);
						}
						if (race.GetSectionData(section).Right == participant)
						{
							DrawParticipant(graphics, participant, Side.Right);
						}
					}

				}
			}
			
		}
		private static void DrawParticipant(Graphics graphics, IParticipant participant, Side side)
		{

			int xposition = CalculateXDraw();
			int yposition = CalculateYDraw();
			// match de participant aan de afbeelding

			if (_direction == Direction.North || _direction == Direction.South)
			{
				if (side == Side.Left)
				{
					yposition += (imageSize / 3);
					xposition += (imageSize / 4);
				}
				else if (side == Side.Right)
				{
					yposition += (imageSize / 2);
					xposition += (imageSize / 2);
				}
			}

			if (_direction == Direction.East || _direction == Direction.West)
			{
				if (side == Side.Left)
				{
					xposition += (imageSize / 3);
					yposition += (imageSize / 4);
				}
				else if (side == Side.Right)
				{
					xposition += (imageSize / 2);
					yposition += (imageSize / 2);
				}
			}

			//voeg hier mooie BROKE Squid art toe :DDD
			if (participant.Equipment.IsBroken)
			{
				graphics.DrawImage(rotateDriver(ImageHandler.CloneImageFromCache(participant.BrokeImageSource)), xposition, yposition);
			}
			else
			{
				graphics.DrawImage(rotateDriver(ImageHandler.CloneImageFromCache(participant.ImageSource)), xposition, yposition);
			}
			
		}

		public static Bitmap rotateDriver(Bitmap bitmap)
		{
			switch (_direction) 
			{
				case Direction.East:
					bitmap.RotateFlip(RotateFlipType.Rotate90FlipNone);
					break;
				case Direction.South:
					bitmap.RotateFlip(RotateFlipType.Rotate180FlipNone);
					break;
				case Direction.West:
					bitmap.RotateFlip(RotateFlipType.Rotate270FlipNone);
					break;
			}

			return bitmap;
			
		}

		public static void CalculateTrackSize()
			{
				int XCurrent = 0;
				int YCurrent = 0;
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
			}

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

		#region Graphics_Large_Local
		private const String CornerNE = "WPF Images\\Road_L\\CornerNEL.png";
		private const String CornerNW = "WPF Images\\Road_L\\CornerNWL.png";
		private const String CornerSE = "WPF Images\\Road_L\\CornerSEl.png";
		private const String CornerSW = "WPF Images\\Road_L\\CornerSWL.png";
		private const String Straight = "WPF Images\\Road_L\\StraightL.png";
		private const String StraightVertical = "WPF Images\\Road_L\\VerticalL.png";
		private const String Finish = "WPF Images\\Road_L\\FinishLineL.png";
		private const String Squid1 = "WPF Images\\Squid1_S.png";
		private const String Squid2 = "WPF Images\\Squid2_S.png";
		private const String Squid3 = "WPF Images\\Squid3_S.png";
		private const String Squid4 = "WPF Images\\Squid4_S.png";
		private const String Squid1_Ink = "WPF Images\\Squid1_Ink.png";
		private const String Squid2_Ink = "WPF Images\\Squid2_Ink.png";
		private const String Squid3_Ink = "WPF Images\\Squid3_Ink.png";
		private const String Squid4_Ink = "WPF Images\\Squid4_Ink.png";
		#endregion
	}
}