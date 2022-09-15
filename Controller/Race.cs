using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controller
{
	public class Race
	{
		private Random _random;

		public Track Track;
		List<IParticipant>? Participants;
		public DateTime StartTime;

		private Dictionary<Section, SectionData> _positions;

		public SectionData GetSectionData(Section section)
		{
			if (!_positions.ContainsKey(section))
			{
				_positions.Add(section, new SectionData());
			}
			return _positions[section];
		}

		public Race(Track track, List<IParticipant>? participants)
		{
			_random = new Random(DateTime.Now.Millisecond);
			Track = track;
			Participants = participants;
			StartTime = new DateTime();
			_positions = new Dictionary<Section, SectionData>();
		}

		public void RandomizeEquipment()
		{
			foreach(IParticipant participant in Participants)
			{
				participant.Equipment.Quality = _random.Next(10);
				participant.Equipment.Performance = _random.Next(10);
			}
		}

	}
}
