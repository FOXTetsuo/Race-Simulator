using Controller;
using Model;
using System;
using System.Windows;
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
			Data.Initialize();
			Data.NextRace();
			Data.CurrentRace.PlaceContestants(Data.CurrentRace.Track, Data.CurrentRace.Participants);
			InitializeRace();
			RaceNameLabel.FontSize = 30;
			RaceNameLabel.FontFamily = new System.Windows.Media.FontFamily("Informal Roman");
		}
		private void CurrentRace_RaceFinished(object? sender, EventArgs e)
		{
			// Clear image cache
			ImageHandler.Clear();
			InitializeRace();
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
			CompetitionInfoWindow = new CompetitionInfoWindow();
			CompetitionInfoWindow.Show();
		}

		private void OpenRaceInfoWindow(object sender, RoutedEventArgs e)
		{
			RaceInfoWindow = new RaceInfoWindow();
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
				TrackImage.Width = WPFVisualizer.TrackWidth * WPFVisualizer.imageSize;
				TrackImage.Height = WPFVisualizer.TrackHeight * WPFVisualizer.imageSize;
				this.TrackImage.Source = null;
				this.TrackImage.Source = WPFVisualizer.DrawTrack(Data.CurrentRace.Track);

			}));
			//start race
			Data.CurrentRace.Start();
		}
	}
}
