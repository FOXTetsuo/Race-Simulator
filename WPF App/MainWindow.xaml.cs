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
		private Window1 Window1;
		private Window2 Window2;

		public MainWindow()
		{
			// initialize window components and set the datacontext
			InitializeComponent();
			Data.Initialize();
			Data.NextRace();
			// place contestants and subscribe to events
			Data.CurrentRace.PlaceContestants(Data.CurrentRace.Track, Data.CurrentRace.Participants);
			Data.CurrentRace.DriversChanged += CurrentRace_DriversChanged;
			Data.CurrentRace.RaceFinished += CurrentRace_RaceFinished;
			//start the visualizer & the race
			WPFVisualizer.Initialize(Data.CurrentRace);
			Data.CurrentRace.Start();
			
			// initial track drawing.
			TrackImage.Width = WPFVisualizer.TrackWidth * WPFVisualizer.imageSize;
			TrackImage.Height = WPFVisualizer.TrackHeight * WPFVisualizer.imageSize;
			this.TrackImage.Source = null;
			this.TrackImage.Source = WPFVisualizer.DrawTrack(Data.CurrentRace.Track);
		}
		//somewhere, the positioning for the racers is going wrong, plms fix
		private void CurrentRace_RaceFinished(object? sender, EventArgs e)
		{
			// Clear image cache
			ImageHandler.Clear();
			//Resubscribe to events and initialize visualizer
			Data.CurrentRace.DriversChanged += CurrentRace_DriversChanged;
			Data.CurrentRace.RaceFinished += CurrentRace_RaceFinished;
			InitializeComponent();
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

		private void OpenFirstWindow(object sender, RoutedEventArgs e)
		{
			Window1 = new Window1();
			Window1.Show();
		}

		private void OpenSecondWindow(object sender, RoutedEventArgs e)
		{
			Window2 = new Window2();
			Window2.Show();
		}
	}
}
