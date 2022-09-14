using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
	public class Car : IEquipment
	{
		public int Quality { get; set; }
		public int Performance { get; set; }
		public int Speed { get; set; }
		public bool IsBroken { get; set; }


		public Car(int quality, int perf, int speed, bool isBroken)
		{
			Quality = quality;
			Performance = perf;
			Speed = speed;
			IsBroken = isBroken;
		}
	}
}
