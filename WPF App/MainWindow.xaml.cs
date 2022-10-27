using Controller;
using Model;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace WPF_App
{
	public partial class MainWindow : Window
	{
		private CompetitionInfoWindow CompetitionInfoWindow;
		private RaceInfoWindow RaceInfoWindow;
		public MainWindow()
		{
			// initialize window components and set the datacontext
			StartCompetition();

			//Create windows, which are hidden by default
			RaceInfoWindow = new RaceInfoWindow();
			CompetitionInfoWindow = new CompetitionInfoWindow();
		}

		public void StartCompetition()
		{
			Data.Initialize();
			Data.NextRace();
			Data.CurrentRace.PlaceContestants(Data.CurrentRace.Track, Data.CurrentRace.Participants);

			//Subscribe to OnCompetitionOver
			Data.Competition.CompetitionFinished += OnCompetitionOver;
			InitializeComponent();
			InitializeRace();
			RaceNameLabel.Visibility = Visibility.Visible;
		}

		private void CurrentRace_RaceFinished(object? sender, EventArgs e)
		{
			// Clear image cache
			ImageHandler.Clear();
			InitializeRace();
		}

		private void OnCompetitionOver(object? sender, EventArgs e)
		{
			//Open competition info when race is over
			this.Dispatcher.Invoke(
			DispatcherPriority.Normal,
			new Action(() =>
			{
				CompetitionInfoWindow.Show();
			}));

			//Delete trackimage, replace with winner screen
			this.TrackImage.Dispatcher.BeginInvoke(
			DispatcherPriority.Render,
			new Action(() =>
			{
				this.TrackImage.Source = null;
				this.WinnerImage.Source = WPFVisualizer.DrawWinnerFrame(Data.Competition.Winner.ImageSourceWinner);
				CurtainWindow.Visibility = Visibility.Visible;
				MainWindowGrid.Background = new SolidColorBrush(Colors.CadetBlue);
				RaceNameLabel.Visibility = Visibility.Hidden;
				Victorylabel.Visibility = Visibility.Visible;
			}));
		}

		private void CurrentRace_DriversChanged(object? sender, DriversChangedEventArgs e)
			//Replaces the TrackImage when the drivers change.
		{
			this.TrackImage.Dispatcher.BeginInvoke(
			DispatcherPriority.Render,
			new Action(() =>
			{
				this.TrackImage.Source = WPFVisualizer.DrawTrack(Data.CurrentRace.Track);
			}));

		}

		private void MenuItem_Exit_Click(object sender, RoutedEventArgs e)
			//Calls shutdown for the app, so that all windows close (even the hidden ones)
		{
			this.Close();
			Application.Current.Shutdown();
		}

		private void OpenCompetitionInfoWindow(object sender, RoutedEventArgs e)
		{
			CompetitionInfoWindow.Show();
		}

		private void OpenRaceInfoWindow(object sender, RoutedEventArgs e)
		{
			RaceInfoWindow.Show();
		}

		private void InitializeRace()
		{
			//Resubscribe to events and initialize visualizer
			Data.CurrentRace.DriversChanged += CurrentRace_DriversChanged;
			Data.CurrentRace.RaceFinished += CurrentRace_RaceFinished;
			WPFVisualizer.Initialize(Data.CurrentRace);

			//Draw the initial image. If this function is missing, the first "tick"
			//of the race won't be displayed.
			this.TrackImage.Dispatcher.BeginInvoke(
			DispatcherPriority.Render,
			new Action(() =>
			{
				this.TrackImage.Source = WPFVisualizer.DrawTrack(Data.CurrentRace.Track);

			}));
			//Start race
			Data.CurrentRace.Start();
		}

		private void OnClose(object sender, System.ComponentModel.CancelEventArgs e)
			//Calls shutdown when main window is closed via X-button in topright corner.
			//(or through any other means)
		{
			Application.Current.Shutdown();
		}
	}
}
