using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
	public class Competition
	{
		public event EventHandler<EventArgs> CompetitionFinished;
		public Queue<Track> Tracks { get; set; }
		public List<IParticipant>? Participants { get; set; }
		public IParticipant Winner {get; set;}

		// Gets the next track in the queue
		public Track NextTrack()
		{
			if (Tracks.Count > 0)
			{
				return Tracks.Dequeue();
			}
			return null;
		}

		public void EndCompetition()
		{
			CompetitionFinished.Invoke(this, new EventArgs());
		}

		public Competition()
		{
			Participants = new List<IParticipant>();
			Tracks = new Queue<Track>();
		}
	}
}
