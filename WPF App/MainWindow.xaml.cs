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
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			Data.Initialize();
			Data.NextRace();
			Data.CurrentRace.PlaceContestants(Data.CurrentRace.Track, Data.CurrentRace.Participants);
			Data.CurrentRace.DriversChanged += CurrentRace_DriversChanged;
			Data.CurrentRace.RaceFinished += CurrentRace_RaceFinished;
			WPFVisualizer.Initialize(Data.CurrentRace);
			Data.CurrentRace.Start();

			//TrackImage.HorizontalAlignment = HorizontalAlignment.Left;
			//TrackImage.VerticalAlignment = VerticalAlignment.Top;
			
			TrackImage.Width = WPFVisualizer.TrackWidth * WPFVisualizer.imageSize;
			TrackImage.Height = WPFVisualizer.TrackHeight * WPFVisualizer.imageSize;
			this.TrackImage.Source = null;
			this.TrackImage.Source = WPFVisualizer.DrawTrack(Data.CurrentRace.Track);
		}
		//somewhere, the positioning for the racers is going wrong, plms fix
		private void CurrentRace_RaceFinished(object? sender, EventArgs e)
		{
			ImageHandler.Clear();
			Data.Initialize();
			// y u no next race :(
			Data.CurrentRace.PlaceContestants(Data.CurrentRace.Track, Data.CurrentRace.Participants);
			Data.CurrentRace.DriversChanged += CurrentRace_DriversChanged;
			Data.CurrentRace.RaceFinished += CurrentRace_RaceFinished;
			InitializeComponent();
			WPFVisualizer.Initialize(Data.CurrentRace);

			this.TrackImage.Dispatcher.BeginInvoke(
			DispatcherPriority.Render,
			new Action(() =>
			{
				this.TrackImage.Source = null;
				this.TrackImage.Source = WPFVisualizer.DrawTrack(Data.CurrentRace.Track);

			}));

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

				//if (!ImageHandler._trackImageCache.ContainsKey(e.Track.Name))
				//{

				//}

				//else this.TrackImage.Source = ImageHandler._trackImageCache[e.Track.Name];

			}));

		}
	}
}
