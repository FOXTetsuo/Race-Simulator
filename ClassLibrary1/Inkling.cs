﻿using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Model
{
	public class Inkling : IParticipant
	{
		private int _distanceCovered;
		private int _loopsPassed;
		private int _points;
		private double _lapTime;
		public string Name { get; set; }
		public IEquipment Equipment { get; set; }
		public TeamColors TeamColor { get; set; }
		public string ImageSource { get; set; }
		public string BrokeImageSource { get; set; }
		public Section CurrentSection { get; set; }
		public double LapTime { get { return _lapTime; } set { _lapTime = value; OnPropertyChanged(); } }
		public int Points { get { return _points; } set { _points = value; OnPropertyChanged(); } }
		public int DistanceCovered { get { return _distanceCovered; } set { _distanceCovered = value; OnPropertyChanged(); } }
		public int LoopsPassed { get { return _loopsPassed; } set { _loopsPassed = value; OnPropertyChanged(); } }
		public Random Random { get; set; }
		public string FunFact { get; set; }
		public string ImageSourceWinner { get; set; }

		public static List<String> FunFacts = new List<string>
		{
			"Loves the color purple",
			"Believes in a secret government conspiracy",
			"Is part of a secret government conspiracy",
			"Believes in UFO's",
			"Discovered the earth was flat",
			"Loves marmalade",
			"Their favourite game is Splatoon 2",
			"Pretends to be a duck in their spare time",
			"Has a garden where they grow hopes and dreams",
			"Isn't actually an inkling...",
			"Has a pet cat named Binky",
			"Doesn't understand the difference between a skiff and a rowboat",
			"Believes in fairies",
			"Is a secret agent",
			"Is a secret agent for the secret government conspiracy",
			"Was the first inkling on the moon",
			"Was the first inkling to discover the earth was round",
			"Once fell off of the moon",
			"Abducted by an UFO as a child",
			"Read a book once",
			"Has a pet squid named Squidward",
			"Has a pet octopus named Octavius",
			"Has a pet sea anemone named Anemone",
			"Uses a lot of conditioner",
			"Decided to turn their life around",
			"Once ate a whole bag of chips",
			"Around 80% of their diet is chips",
			"Has a pet dog named Dog",
		};

		public Inkling(string name, IEquipment equipment, TeamColors teamcolor)
		{
			Name = name;
			Points = 0;
			Equipment = equipment;
			TeamColor = teamcolor;
			DistanceCovered = 0;
			LoopsPassed = 0;
			LapTime = 0;
			Random = new Random();
			FunFact = FunFacts[Random.Next(FunFacts.Count)];
		}

		public event PropertyChangedEventHandler? PropertyChanged;

		private void OnPropertyChanged([CallerMemberName] string name = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
		}
	}
}
