using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Inkling : IParticipant 
    {
		private int _distanceCovered;
		private int _loopsPassed;
		private int _points;
		public string Name { get; set; }
        public int Points { get { return _points; } set { _points = value; OnPropertyChanged(); } }
		public IEquipment Equipment { get; set; }
		public TeamColors TeamColor { get; set; }
		public Section CurrentSection { get; set; }
		
		public int DistanceCovered { get { return _distanceCovered; } set { _distanceCovered = value;  OnPropertyChanged(); } }
		public int LoopsPassed { get { return _loopsPassed; } set { _loopsPassed = value; OnPropertyChanged(); } }

		public Inkling(string name, IEquipment equipment, TeamColors teamcolor)
        {
            Name = name;
            Points = 0;
            Equipment = equipment;
            TeamColor = teamcolor;
			DistanceCovered = 0;
			LoopsPassed = 0;
        }

		public event PropertyChangedEventHandler? PropertyChanged;

		protected void OnPropertyChanged([CallerMemberName] string name = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
		}
	}
}
