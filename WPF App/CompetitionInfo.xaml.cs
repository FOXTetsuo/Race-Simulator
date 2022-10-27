using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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
