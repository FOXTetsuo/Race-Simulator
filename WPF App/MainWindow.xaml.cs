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
			WPFVisualizer.Initialize(Data.CurrentRace);
			WPFVisualizer.DetermineTrackWidthAndHeight();
			Data.CurrentRace.Start();
		}

		private void CurrentRace_DriversChanged(object? sender, DriversChangedEventArgs e)
		{
			this.TrackImage.Dispatcher.BeginInvoke(
			DispatcherPriority.Render,
			new Action(() =>
			{
				TrackImage.Width = WPFVisualizer.XSize*WPFVisualizer.imageSize;
				TrackImage.Height = WPFVisualizer.YSize * WPFVisualizer.imageSize;
				this.TrackImage.Source = null;
				this.TrackImage.Source = WPFVisualizer.DrawTrack(Data.CurrentRace.Track);
			}));
		}
	}
}
