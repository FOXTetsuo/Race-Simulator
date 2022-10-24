using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
	public class Genetics : IEquipment
	{
		private int _quality;
		private int _performance;
		private int _speed;
		private bool isBroken;

		public event PropertyChangedEventHandler? PropertyChanged;

		public int Quality { get { return _quality; } set { _quality = value; OnPropertyChanged(); } }
		public int Performance { get { return _performance; } set { _performance = value; OnPropertyChanged(); } }
		public int Speed { get { return _speed; } set { _speed = value; OnPropertyChanged(); } }
		public bool IsBroken { get { return isBroken; } set { isBroken = value; OnPropertyChanged(); } }

		protected void OnPropertyChanged([CallerMemberName] string name = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
		}
	}
}
