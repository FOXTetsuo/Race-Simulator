using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Controller
{
	public class DataContext_MainWindow : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler? PropertyChanged;

		private string _raceTrackName;
		public string RaceTrackName { get { return _raceTrackName; } set { _raceTrackName = value; OnPropertyChanged(); } }

		// swereld's meest nutteloze lambda statement voor geen enkele goeie reden
		// serieus, benoem me aub 1 reden dat ik niet gewoon
		// public string RaceTrackName = Data.CurrentRace.Track.Name;
		// kan doen
		// en Func is een soort delegate???

		public DataContext_MainWindow()
		{
			RaceTrackName = new Func<string>(() => Data.CurrentRace.Track.Name)();
			Data.CurrentRace.RaceFinished += OnRaceFinished;
		}

		private void OnRaceFinished(object? sender, EventArgs e)
			// Indicate that name has changed && rebind ondriverschanged
		{
			RaceTrackName = new Func<string>(() => Data.CurrentRace.Track.Name)();
		}

		protected void OnPropertyChanged([CallerMemberName] string name = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
		}
	}
}
