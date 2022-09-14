using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
	public interface IEquipment
	{
		private int Quality { get => Quality; set => Quality = value; } // dit gaat goed? denk ik? idunno
		public int Performance { get; set; }
		public int Speed { get; set; }
		public bool IsBroken { get; set; }

	}
}
