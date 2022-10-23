using Model;
using System.ComponentModel;
using System.Runtime.CompilerServices;
namespace Controller
{
	public class DataContext_CompetitionInfoWindow : INotifyPropertyChanged
	{
		private BindingList<IParticipant> _inklingData { get; set; }
		public BindingList<IParticipant> InklingData { get { return _inklingData; } set { _inklingData = value; OnPropertyChanged(); } }

		public Queue<Track> Tracks { get; set; } // get / set are ESSENTIAL
		public event PropertyChangedEventHandler? PropertyChanged;

		protected void OnPropertyChanged([CallerMemberName] string name = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
			//ReOrderLeaderboard();
		}
		public DataContext_CompetitionInfoWindow()
		{
			Data.CurrentRace.RaceFinished += OnRaceFinished;
			//Bind tracks and inklingdata
			Tracks = new Func<Queue<Track>>(() => Data.Competition.Tracks)();

			List<IParticipant> UnsortedInklingData = new List<IParticipant>();
			//TODO: verangen met LINQ
			foreach (IParticipant participant in Data.CurrentRace.Participants)
			{
				UnsortedInklingData.Add(participant);
			}
			InklingData = new BindingList<IParticipant>(UnsortedInklingData.OrderBy(x => x.Name).ToList());
		}

		private void OnRaceFinished(object? sender, EventArgs e)
		{
			ReOrderLeaderboard();
		}

		public void ReOrderLeaderboard()
		{
			List<IParticipant> UnsortedInklingData = new List<IParticipant>();
			foreach (IParticipant participant in Data.CurrentRace.Participants)
			{
				UnsortedInklingData.Add(participant);
			}
			InklingData = new BindingList<IParticipant>(UnsortedInklingData.OrderByDescending(x => x.Points).ToList());
		}
	}
}
