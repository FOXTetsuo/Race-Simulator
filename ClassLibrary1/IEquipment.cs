using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
	//sectielengte = 100m
	public interface IEquipment : INotifyPropertyChanged
	{
		public int Quality { get; set; }
		public int Performance { get; set; }
		public int Speed { get; set; }
		public bool IsBroken { get; set; }

	}
}
