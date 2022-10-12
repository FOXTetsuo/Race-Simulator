using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Driver : IParticipant
    {
        public string Name { get; set; }
        public int Points { get; set; }
		public IEquipment Equipment { get; set; }
		public TeamColors TeamColor { get; set; }
		public Section CurrentSection { get; set; }
		public int DistanceCovered { get; set; }
		public int LoopsPassed { get; set; }

		public Driver(string name, int points, IEquipment equipment, TeamColors teamcolor)
        {
            Name = name;
            Points = points;
            Equipment = equipment;
            TeamColor = teamcolor;
			DistanceCovered = 0;
			LoopsPassed = 0;
        }
    }
}
