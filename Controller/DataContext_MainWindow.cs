using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Controller
{
	public class DataContext_MainWindow : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler? PropertyChanged;

		public string RaceTrackName {get; set;}

		// swereld's meest nutteloze lambda statement voor geen enkele goeie reden
		// serieus, benoem me aub 1 reden dat ik niet gewoon
		// public string RaceTrackName = Data.CurrentRace.Track.Name;
		// kan doen
		// en Func is een soort delegate???

		public DataContext_MainWindow()
		{
			RaceTrackName = new Func<string>(() => Data.CurrentRace.Track.Name)();
			Data.CurrentRace.DriversChanged += OnDriversChanged;
			PropertyChanged += OnPropertyChanged;
			Data.CurrentRace.RaceFinished += OnRaceFinished;
		}

		private void OnRaceFinished(object? sender, EventArgs e)
			// Indicate that name has changed && rebind ondriverschanged
		{
			Data.CurrentRace.DriversChanged += OnDriversChanged;
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("RaceTrackName"));
		}

		public void OnDriversChanged(object? sender, DriversChangedEventArgs e)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(""));
			//Lege string om aan te geven dat alles gewijzigd wordt
		}

		public void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			switch (e.PropertyName)
			{
				case ("RaceTrackName"):
					RaceTrackName = new Func<string>(() => Data.CurrentRace.Track.Name)();
					break;
			}
			
		}
	}
}
