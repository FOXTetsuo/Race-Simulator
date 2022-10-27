using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Controller
{
	public class DataContext_MainWindow : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler? PropertyChanged;
		private string _winnerString;
		private string _raceTrackName;
		public string WinnerString { get { return _winnerString; } set { _winnerString = value; OnPropertyChanged(); } }
		public string RaceTrackName { get { return _raceTrackName; } set { _raceTrackName = value; OnPropertyChanged(); } }
		public DataContext_MainWindow()
		{
			RaceTrackName = new Func<string>(() => Data.CurrentRace.Track.Name)();
			Data.CurrentRace.RaceFinished += OnRaceFinished;
			Data.Competition.CompetitionFinished += OnCompetitionFinished;
		}

		private void OnRaceFinished(object? sender, EventArgs e)
		{
			RaceTrackName = new Func<string>(() => Data.CurrentRace.Track.Name)();
			//Rebind onRaceFinished, because there is a new race
			Data.CurrentRace.RaceFinished += OnRaceFinished;
		}

		private void OnCompetitionFinished(object? sender, EventArgs e)
		{
			//set the winnerstring when the competition ends
			WinnerString = new Func<string>(() => "The winner is: " + Data.Competition.Winner.Name)();
		}

		private void OnPropertyChanged([CallerMemberName] string name = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
		}
	}
}
