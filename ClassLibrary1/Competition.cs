namespace Model
{
	public class Competition
	{
		public event EventHandler<EventArgs> CompetitionFinished;
		public Queue<Track> Tracks { get; set; }
		public List<IParticipant>? Participants { get; set; }
		public IParticipant Winner { get; set; }

		public Track NextTrack()
		// Gets the next track in the queue
		{
			if (Tracks.Count > 0)
			{
				return Tracks.Dequeue();
			}
			return null;
		}

		public void EndCompetition()
		{
			Winner = Participants.OrderByDescending(x => x.Points).ThenBy(x => x.LapTime).First();
			CompetitionFinished?.Invoke(this, new EventArgs());
		}

		public Competition()
		{
			Participants = new List<IParticipant>();
			Tracks = new Queue<Track>();
		}
	}
}
