using Controller;
using Model;
using System;
using System.Drawing;
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
		public static int Xposition { get; set; }
		public static int Yposition { get; set; }

		public const int ImageSize = 128;
		public static Race Race { get; set; }

		public static void Initialize(Race race)
		{
			DetermineImageSources();
			Xposition = 0;
			Yposition = 0;
			Race = race;
			_direction = Direction.East;
			CalculateTrackSize();
		}

		public static void DetermineImageSources()
		//Gives each participant the correct image sources based on their teamcolor
		{
			foreach (IParticipant participant in Data.CurrentRace.Participants)
			{
				switch (participant.TeamColor)
				{
					case TeamColors.Orange:
						participant.ImageSource = WPFVisualizer.Squid1;
						participant.ImageSourceWinner = WPFVisualizer.Squid1L;
						participant.BrokeImageSource = WPFVisualizer.Squid1_Ink;
						break;
					case TeamColors.Green:
						participant.ImageSource = WPFVisualizer.Squid2;
						participant.ImageSourceWinner = WPFVisualizer.Squid2L;
						participant.BrokeImageSource = WPFVisualizer.Squid2_Ink;
						break;
					case TeamColors.Purple:
						participant.ImageSource = WPFVisualizer.Squid3;
						participant.ImageSourceWinner = WPFVisualizer.Squid3L;
						participant.BrokeImageSource = WPFVisualizer.Squid3_Ink;
						break;
					case TeamColors.Red:
						participant.ImageSource = WPFVisualizer.Squid4;
						participant.ImageSourceWinner = WPFVisualizer.Squid4L;
						participant.BrokeImageSource = WPFVisualizer.Squid4_Ink;
						break;
				}
			}
		}

		public static int CalculateXPosition()
		//Incorporates imagesize in calculations
		{
			return Xposition * ImageSize;
		}

		public static int CalculateYPosition()
		//Incorporates imagesize in calculations
		{
			return Yposition * ImageSize;
		}

		public static BitmapSource DrawTrack(Track track)
		//Draws the entire track by looping through it.
		{
			Bitmap canvas = new Bitmap(TrackWidth * ImageSize, TrackHeight * ImageSize);
			Graphics graphics = Graphics.FromImage(canvas);

			foreach (Section section in Race.Track.Sections)
			{
				switch (section.SectionType)
				{
					case SectionTypes.Finish:
						graphics.DrawImage(ImageHandler.CloneImageFromCache(Finish), CalculateXPosition(), CalculateYPosition());
						break;
					case SectionTypes.Straight:
						graphics.DrawImage(ImageHandler.CloneImageFromCache(Straight), CalculateXPosition(), CalculateYPosition());
						break;
					case SectionTypes.StraightVertical:
						graphics.DrawImage(ImageHandler.CloneImageFromCache(StraightVertical), CalculateXPosition(), CalculateYPosition());
						break;
					case SectionTypes.CornerNE:
						graphics.DrawImage(ImageHandler.CloneImageFromCache(CornerNE), CalculateXPosition(), CalculateYPosition());
						break;
					case SectionTypes.CornerNW:
						graphics.DrawImage(ImageHandler.CloneImageFromCache(CornerNW), CalculateXPosition(), CalculateYPosition());
						break;
					case SectionTypes.CornerSE:
						graphics.DrawImage(ImageHandler.CloneImageFromCache(CornerSE), CalculateXPosition(), CalculateYPosition());
						break;
					case SectionTypes.CornerSW:
						graphics.DrawImage(ImageHandler.CloneImageFromCache(CornerSW), CalculateXPosition(), CalculateYPosition());
						break;
				}
				//Draws all of the participants over the track in the correct places
				DrawParticipantssInSection(graphics, Race, section);
				DetermineDirection(section.SectionType, _direction);
				MoveImageCursor();
			}
			//Returns the entire track
			return (ImageHandler.CreateBitmapSourceFromGdiBitmap(canvas));
		}

		public static BitmapSource DrawWinnerFrame(string winner)
		{
			Bitmap canvas = new Bitmap(400, 400);
			Graphics graphics = Graphics.FromImage(canvas);
			//Draws the frame to place the participant in
			graphics.DrawImage(ImageHandler.CloneImageFromCache(WinnerFrame), 0, 0);

			//Draws the participant in the middle of the frame
			int x = (400 - ImageHandler.CloneImageFromCache(winner).Width) / 2;
			graphics.DrawImage(ImageHandler.CloneImageFromCache(winner), x, x - 9);
			return (ImageHandler.CreateBitmapSourceFromGdiBitmap(canvas));
		}

		private static void DrawParticipantssInSection(Graphics graphics, Race race, Section section)
			//Draws all of the participants in a given section
		{
			if (race.GetSectionData(section).Left is not null || race.GetSectionData(section).Right is not null)
				//Only draws if there are participants in the section.
			{
				//Draws the correct participant for each section
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
			//Draws a single participant, on the side of the track that is given
		{
			int xposition = CalculateXPosition();
			int yposition = CalculateYPosition();

			//The two code blocks below determine where the image of the participant should be drawn
			//To make it look like it is properly moving over the track (instead of over the grass)
			if (_direction == Direction.North || _direction == Direction.South)
			{
				if (side == Side.Left)
				{
					yposition += (ImageSize / 3);
					xposition += (ImageSize / 4);
				}
				else if (side == Side.Right)
				{
					yposition += (ImageSize / 2);
					xposition += (ImageSize / 2);
				}
			}
			
			if (_direction == Direction.East || _direction == Direction.West)
			{
				if (side == Side.Left)
				{
					xposition += (ImageSize / 3);
					yposition += (ImageSize / 4);
				}
				else if (side == Side.Right)
				{
					xposition += (ImageSize / 2);
					yposition += (ImageSize / 2);
				}
			}
			//Depending on the equipment status, a certain image is drawn. The image is rotated to fit the direction the
			//Participants are going in
			if (participant.Equipment.IsBroken)
			{
				graphics.DrawImage(RotateParticipant(ImageHandler.CloneImageFromCache(participant.BrokeImageSource)), xposition, yposition);
			}
			else
			{
				graphics.DrawImage(RotateParticipant(ImageHandler.CloneImageFromCache(participant.ImageSource)), xposition, yposition);
			}

		}

		public static Bitmap RotateParticipant(Bitmap bitmap)
			//Rotates a participant based on their direction.
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
		//Calculate the size of the track with maths.
		//Walks through the track and calculates the highest and lowest X&Y positions
		//Then takes the absolute of Highest X - Lowest X, and Highest Y - Lowest Y
		//and sets the size of the track to them
		{
			int xCurrent = 0;
			int yCurrent = 0;
			int xMin = 0;
			int yMin = 0;
			int xMax = 0;
			int yMax = 0;

			foreach (Section section in Race.Track.Sections)
			{
				if (_direction == Direction.East)
				{
					xCurrent += 1;
					if (xCurrent >= xMax)
					{
						xMax = xCurrent;
					}
				}
				if (_direction == Direction.West)
				{
					xCurrent -= 1;
					if (xCurrent <= xMin)
					{
						xMin = xCurrent;
					}
				}
				if (_direction == Direction.North)
				{
					yCurrent += -1;
					if (yCurrent <= yMin)
					{
						yMin = yCurrent;
					}
				}
				if (_direction == Direction.South)
				{
					yCurrent += 1;
					if (yCurrent >= yMax)
					{
						yMax = yCurrent;
					}
				}
				DetermineDirection(section.SectionType, _direction);
			}
			TrackWidth = (xMax - xMin);
			TrackHeight = (yMax - yMin + 1);
		}
		public static void DetermineDirection(SectionTypes type, Direction dir)
			//Determines the direction of the participant based on the direction
			//that they entered a specific type of corner
		{
			switch (type)
			{
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

		public static void MoveImageCursor()
			//Move the image cursor for the next piece of track with it's matching participants
			// +1 and -1 because it's multiplied by the imagesize
		{
			switch (_direction)
			{
				case Direction.East:
					Xposition += 1;
					break;
				case Direction.South:
					Yposition += 1;
					break;
				case Direction.West:
					Xposition += -1;
					break;
				case Direction.North:
					Yposition += -1;
					break;
			}
		}

		#region Graphics
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

		private const String Squid1L = "WPF Images\\Squid1.png";
		private const String Squid2L = "WPF Images\\Squid2.png";
		private const String Squid3L = "WPF Images\\Squid3.png";
		private const String Squid4L = "WPF Images\\Squid4.png";

		private const String Squid1_Ink = "WPF Images\\Squid1_Ink.png";
		private const String Squid2_Ink = "WPF Images\\Squid2_Ink.png";
		private const String Squid3_Ink = "WPF Images\\Squid3_Ink.png";
		private const String Squid4_Ink = "WPF Images\\Squid4_Ink.png";

		private const String WinnerFrame = "WPF Images\\WinnerFrame.png";
		#endregion
	}
}