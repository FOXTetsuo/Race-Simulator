using Model;
using System.ComponentModel;
using System.Runtime.CompilerServices;
namespace Controller
{
	public class DataContext_CompetitionInfoWindow : INotifyPropertyChanged
	{
		private BindingList<IParticipant> _inklingData { get; set; }
		public BindingList<IParticipant> InklingData { get { return _inklingData; } set { _inklingData = value; OnPropertyChanged(); } }
		private BindingList<Track> _tracks { get; set; }
		public BindingList<Track> Tracks { get { return _tracks; } set { _tracks = value; OnPropertyChanged(); } }
		//public Queue<Track> Tracks { get; set; } // get / set are ESSENTIAL
		public event PropertyChangedEventHandler? PropertyChanged;

		protected void OnPropertyChanged([CallerMemberName] string name = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
			//ReOrderLeaderboard();
		}
		public DataContext_CompetitionInfoWindow()
		{
			Data.CurrentRace.RaceFinished += OnRaceFinished;
			Data.Competition.CompetitionFinished += OnRaceFinished;
			//Bind tracks and inklingdata
			
			Tracks = new BindingList<Track>();
			
			//TODO: fix illegale operatie
			foreach (Track track in Data.Competition.Tracks)
			{
				Tracks.Add(track);
			}

			List<IParticipant> UnsortedInklingData = new List<IParticipant>();
			//TODO: verangen met LINQ
			foreach (IParticipant participant in Data.CurrentRace.Participants)
			{
				UnsortedInklingData.Add(participant);
			}
			InklingData = new BindingList<IParticipant>(UnsortedInklingData.OrderBy(x => x.Points).ToList());
		}

		private void OnRaceFinished(object? sender, EventArgs e)
		{
			ReOrderLeaderboard();
		}

		public void ReOrderLeaderboard()
		{
			List<IParticipant> UnsortedInklingData = new List<IParticipant>();
			//TODO: For 
			//foreach (IParticipant participant in Data.CurrentRace.Participants)
			//{
			//	UnsortedInklingData.Add(participant);
			//}
			//lambda van wat hierboven staat
			Data.CurrentRace.Participants.ForEach((item) => UnsortedInklingData.Add(item));
			//LINQ statement om data te orderen
			InklingData = new BindingList<IParticipant>(UnsortedInklingData.OrderByDescending(x => x.Points).ToList());
		}
	}
}
