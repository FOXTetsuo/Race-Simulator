using Controller;
using Model;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace WPF_App
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	/// 


	public partial class MainWindow : Window
	{
		private CompetitionInfoWindow CompetitionInfoWindow;
		private RaceInfoWindow RaceInfoWindow;
		public MainWindow()
		{
			// initialize window components and set the datacontext
			StartCompetition();

			this.Dispatcher.Invoke(
			DispatcherPriority.Normal,
			new Action(() =>
			{
				RaceInfoWindow = new RaceInfoWindow();
				CompetitionInfoWindow = new CompetitionInfoWindow();
			}));
		}

		public void StartCompetition()
		{

			Data.Initialize();
			Data.NextRace();
			Data.CurrentRace.PlaceContestants(Data.CurrentRace.Track, Data.CurrentRace.Participants);

			//Subscribe to OnCompetitionOver
			Data.Competition.CompetitionFinished += OnCompetitionOver;

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
		{
			this.TrackImage.Dispatcher.BeginInvoke(
			DispatcherPriority.Render,
			new Action(() =>
			{
				this.TrackImage.Source = null;
				this.TrackImage.Source = WPFVisualizer.DrawTrack(Data.CurrentRace.Track);
			}));

		}

		private void MenuItem_Exit_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
			Application.Current.Shutdown();
		}

		private void OpenCompetitionInfoWindow(object sender, RoutedEventArgs e)
		{
			//CompetitionInfoWindow = new CompetitionInfoWindow();
			CompetitionInfoWindow.Show();
		}

		private void OpenRaceInfoWindow(object sender, RoutedEventArgs e)
		{
			//RaceInfoWindow = new RaceInfoWindow();
			RaceInfoWindow.Show();
		}

		private void InitializeRace()
		{
			InitializeComponent();
			//Resubscribe to events and initialize visualizer
			Data.CurrentRace.DriversChanged += CurrentRace_DriversChanged;
			Data.CurrentRace.RaceFinished += CurrentRace_RaceFinished;
			WPFVisualizer.Initialize(Data.CurrentRace);

			//Draw initial image
			this.TrackImage.Dispatcher.BeginInvoke(
			DispatcherPriority.Render,
			new Action(() =>
			{
				this.TrackImage.Source = null;
				this.TrackImage.Source = WPFVisualizer.DrawTrack(Data.CurrentRace.Track);

			}));
			//Start race
			Data.CurrentRace.Start();
		}

		private void Grid_OnLoadingRow(object sender, DataGridRowEventArgs e)
		{
			e.Row.Header = (e.Row.GetIndex() + 1).ToString();
		}

		private void OnClose(object sender, System.ComponentModel.CancelEventArgs e)
		{
			Application.Current.Shutdown();
		}
	}
}
