using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controller
{
	public class DataContext_MainWindow : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler? PropertyChanged;
		
		public DataContext_MainWindow()
		{
			Data.CurrentRace.DriversChanged += OnDriversChanged;
		}

		public void OnDriversChanged(object? sender, DriversChangedEventArgs e)
		{

		}
	}
}
