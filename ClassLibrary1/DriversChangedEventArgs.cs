namespace Model
{

	public class ParticipantsChangedEventArgs : EventArgs
	{
		public Track Track { get; set; }

		public ParticipantsChangedEventArgs(Track track)
		{
			Track = track;
		}
	}
}
