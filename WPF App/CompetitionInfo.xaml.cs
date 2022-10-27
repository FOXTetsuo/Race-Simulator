using System.Windows;
using System.Windows.Controls;

namespace WPF_App
{
	/// <summary>
	/// Interaction logic for Window1.xaml
	/// </summary>
	public partial class CompetitionInfoWindow : Window
	{
		public CompetitionInfoWindow()
		{
			InitializeComponent();
		}

		private void Grid_OnLoadingRow(object sender, DataGridRowEventArgs e)
		//For each row, when it loads, show a row-header with index
		{
			e.Row.Header = (e.Row.GetIndex() + 1).ToString();
		}

		private void WindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
		//Instead of closing the window, just hide it. The window is made when the program starts.
		{
			e.Cancel = true;
			this.Hide();
		}
	}
}
