using Model;
using NUnit.Framework.Internal;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controller
{
	public class DataContext_CompetitionInfoWindow : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler? PropertyChanged;
		// get / set are ESSENTIAL
		public ObservableCollection<IParticipant> InklingData { get; set; }
		public DataContext_CompetitionInfoWindow()
		{
			InklingData = new ObservableCollection<IParticipant>();
			foreach (IParticipant participant in Data.CurrentRace.Participants)
			{
				InklingData.Add(participant);
			}
			//TODO: Order data by position
			//InklingData.Order<
			PropertyChanged += OnPropertyChanged;
			Data.CurrentRace.RaceFinished += OnRaceFinished;
		}

		private void OnRaceFinished(object? sender, EventArgs e)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("InklingData"));
		}

		public void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			switch (e.PropertyName)
			{
				case ("InklingData"):
					//TODO: properly implement observablecollection
					InklingData = null;
					InklingData = new ObservableCollection<IParticipant>();
					foreach (IParticipant participant in Data.CurrentRace.Participants)
					{
						InklingData.Add(participant);
					}
					break;
			}

		}
		public void OnCollectionChanged(object sender, NotifyCollectionChangedEventHandler e)
		{

		}
	}
}
