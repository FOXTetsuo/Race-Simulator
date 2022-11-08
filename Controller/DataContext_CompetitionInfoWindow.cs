using Model;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Controller
{
    public class DataContext_CompetitionInfoWindow : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private BindingList<Track> _tracks;
        public BindingList<IParticipant> InklingData { get; set; }
        public BindingList<Track> Tracks { get { return _tracks; } set { _tracks = value; OnPropertyChanged(); } }
        public DataContext_CompetitionInfoWindow()
        {
            //Bind to events, make a list of tracks and reload the leaderboard
            Data.CurrentRace.RaceFinished += OnRaceFinished;
            Data.Competition.CompetitionFinished += OnCompetitionFinished;

            Tracks = new BindingList<Track>();
            foreach (Track track in Data.Competition.Tracks)
            {
                Tracks.Add(track);
            }
            RefreshLeaderBoard();
        }

        private void OnRaceFinished(object? sender, EventArgs e)
        {
            //Resub to racefinishedevent, because race has now changed:
            Data.CurrentRace.RaceFinished += OnRaceFinished;

            //Reassign Tracks to newTracks, because you can't change
            //it in this thread (The origin is a UI thread, so it can
            //only be edited from there.)
            
			BindingList<Track> newTracks = new BindingList<Track>();
            foreach (Track track in Data.Competition.Tracks)
            {
                newTracks.Add(track);
            }
            Tracks = newTracks;

            RefreshLeaderBoard();
        }

        private void OnCompetitionFinished(object? sender, EventArgs e)
        {
            RefreshLeaderBoard();
        }

        private void RefreshLeaderBoard()
        //Fill the leaderboard with participants sorted by points
        {
            List<IParticipant> UnsortedInklingData = new List<IParticipant>();
            Data.CurrentRace.Participants.ForEach((item) => UnsortedInklingData.Add(item));

            //LINQ statement om data te orderen. Eerst gesorteerd door punten, daarna door laptime van de laatste race.
            //Stel dat er twee mensen zijn met een even aantal punten, wint dus de snelste racer in de laatste race,
            //en staat deze hier ook bovenaan.
            //de echte winner wordt beslist in Competition
            InklingData = new BindingList<IParticipant>(UnsortedInklingData.OrderByDescending(x => x.Points).ThenBy(x => x.LapTime).ToList());
        }
        private void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
