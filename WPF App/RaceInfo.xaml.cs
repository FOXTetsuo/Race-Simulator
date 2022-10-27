using System.Windows;
using System.Windows.Controls;

namespace WPF_App
{
	public partial class RaceInfoWindow : Window
	{
		public RaceInfoWindow()
		{
			InitializeComponent();
		}
		private void Grid_OnLoadingRow(object sender, DataGridRowEventArgs e)
		//For each row, when it loads, show a row-header with index
		{
			e.Row.Header = (e.Row.GetIndex() + 1).ToString();
		}

		private void WindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			e.Cancel = true;
			this.Hide();
		}
	}
}
