using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Controller
{
	public static class Data
	{
		static Competition? Competition;
		public static Race? CurrentRace;
		public static void Initialize()
		{
			Competition  =  new(); // maybe competition toevoegen asls alles breekt XDDD
			AddParticipants();
			AddTracks();
		}
		public static void AddParticipants()
		{
			Competition.Participants.Add(new Driver("Joe", 2, new Car(10,10,10,false), TeamColors.Blue));
            Competition.Participants.Add(new Driver("Aapie", 2, new Car(10, 10, 10, false), TeamColors.Red));
            Competition.Participants.Add(new Driver("Jaapie", 2, new Car(10, 10, 10, false), TeamColors.Yellow));
			Competition.Participants.Add(new Driver("Green", 2, new Car(10, 10, 10, false), TeamColors.Green));
		}
		public static void AddTracks()
		{
			Competition.Tracks.Enqueue(new Track("Van zanten-voort", new LinkedList<Section>()));
			Competition.Tracks.Enqueue(new Track("Spacebase", new LinkedList<Section>()));
			Competition.Tracks.Enqueue(new Track("Vroemvroem-in-da-rondje", new LinkedList<Section>()));
		}
		public static void NextRace()
		{
			Track newTrack = Competition.NextTrack();
			if (newTrack is not null )
			{
				CurrentRace = new Race(newTrack, Competition.Participants);
			};
		}

	}

	
}