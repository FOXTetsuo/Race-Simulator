using Model;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace Controller
{
	public class DataContext_CompetitionInfoWindow : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler? PropertyChanged;
		private BindingList<IParticipant> _inklingData;
		private BindingList<Track> _tracks;
		private string _winnerString = "The winner is: ";
		public BindingList<IParticipant> InklingData { get { return _inklingData; } set { _inklingData = value; OnPropertyChanged(); } }
		public BindingList<Track> Tracks { get { return _tracks; } set { _tracks = value; OnPropertyChanged(); } }
			
		public string WinnerString { get { return _winnerString; } set { _winnerString = value; OnPropertyChanged(); } }
		protected void OnPropertyChanged([CallerMemberName] string name = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
		}
		public DataContext_CompetitionInfoWindow()
		{
			Data.CurrentRace.RaceFinished += OnRaceFinished;
			Data.Competition.CompetitionFinished += OnCompetitionFinished;
			
			Tracks = new BindingList<Track>();
			foreach (Track track in Data.Competition.Tracks)
			{
				Tracks.Add(track);
			}

			ReOrderLeaderboard();
		}
		
		private void OnRaceFinished(object? sender, EventArgs e)
		{
			BindingList<Track> newTracks = new BindingList<Track>();
			//Tracks = new BindingList<Track>();
			foreach (Track track in Data.Competition.Tracks)
			{
				Tracks.Add(track);
			}
			//Tracks = newTracks;
			ReOrderLeaderboard();
		}

		private void OnCompetitionFinished(object? sender, EventArgs e)
		{
			ReOrderLeaderboard();
			WinnerString += InklingData.First().Name;
			WinnerString += "!";
			Tracks = null;
		}

		public void ReOrderLeaderboard()
		{
			List<IParticipant> UnsortedInklingData = new List<IParticipant>();
			Data.CurrentRace.Participants.ForEach((item) => UnsortedInklingData.Add(item));
			//LINQ statement om data te orderen
			InklingData = new BindingList<IParticipant>(UnsortedInklingData.OrderByDescending(x => x.Points).ToList());
		}
	}
}
