﻿using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
	/// Interaction logic for Window2.xaml
	/// </summary>
	public partial class RaceInfoWindow : Window
	{
		public RaceInfoWindow()
		{
			InitializeComponent();



		}
		private void Grid_OnLoadingRow(object sender, DataGridRowEventArgs e)
		{
			e.Row.Header = (e.Row.GetIndex()+1).ToString();
		}

		private void WindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			e.Cancel = true;
			this.Hide();
		}
	}
}
