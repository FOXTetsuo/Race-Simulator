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
			Data.CurrentRace.Start();

			//TrackImage.HorizontalAlignment = HorizontalAlignment.Left;
			//TrackImage.VerticalAlignment = VerticalAlignment.Top;
			//TrackImage.Width = WPFVisualizer.XSize * WPFVisualizer.imageSize;
			//TrackImage.Height = WPFVisualizer.YSize * WPFVisualizer.imageSize;
			//this.TrackImage.Source = null;
			//this.TrackImage.Source = WPFVisualizer.DrawTrack(Data.CurrentRace.Track);

		}

		private void CurrentRace_DriversChanged(object? sender, DriversChangedEventArgs e)
		{

			this.TrackImage.Dispatcher.BeginInvoke(
			DispatcherPriority.Render,
			new Action(() =>
			{
				TrackImage.HorizontalAlignment = HorizontalAlignment.Left;
				TrackImage.VerticalAlignment = VerticalAlignment.Top;
				TrackImage.Width = WPFVisualizer.TrackWidth * WPFVisualizer.imageSize;
				TrackImage.Height = WPFVisualizer.TrackHeight * WPFVisualizer.imageSize;
				this.TrackImage.Source = null;

				//if (!ImageHandler._trackImageCache.ContainsKey(e.Track.Name))
				//{
					this.TrackImage.Source = WPFVisualizer.DrawTrack(Data.CurrentRace.Track);
				//}

				//else this.TrackImage.Source = ImageHandler._trackImageCache[e.Track.Name];

			}));

		}
	}
}
