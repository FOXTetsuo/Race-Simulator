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
using System;  
using System.Collections.Generic;  
using System.ComponentModel;  
using System.Drawing;  
using System.Runtime.CompilerServices;  
namespace Controller
{
	public class DataContext_CompetitionInfoWindow : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler? PropertyChanged;

		protected void OnPropertyChanged([CallerMemberName] string name = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
		}
		// get / set are ESSENTIAL
		public BindingList<IParticipant> InklingData { get; set; }
		public DataContext_CompetitionInfoWindow()
		{
			InklingData = new BindingList<IParticipant>();
			foreach (IParticipant participant in Data.CurrentRace.Participants)
			{
				InklingData.Add(participant);
			}
		}
	}
}
