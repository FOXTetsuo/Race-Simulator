using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controller
{
	internal class Race
	{
		private Random _random { get => _random; set => _random = value; }

		Track Track { get => Track; set => Track = value; }
		List<IParticipant>? Participants { get => Participants; set => Participants = value; }
		DateTime StartTime { get => StartTime; set => StartTime = value; }
	}
}
