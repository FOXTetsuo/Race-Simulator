using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Model
{
	public class Genetics : IEquipment
	//Inherits from Equipment
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

		// This method is called by the Set accessor of each property.
		// The CallerMemberName attribute that is applied to the optional propertyName
		// parameter causes the property name of the caller to be substituted as an argument.
		protected void OnPropertyChanged([CallerMemberName] string name = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
		}
	}
}
