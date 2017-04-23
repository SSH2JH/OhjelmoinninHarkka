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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TyoajanSeuranta {
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window {
		public MainWindow()
		{
			InitializeComponent();
		}

		private void SignIn_Click(object sender, RoutedEventArgs e)
		{
			SignIn next = new SignIn();
			next.ShowDialog();
			if (string.IsNullOrEmpty(Properties.Settings.Default.UserPin) == false) {
				sp_MainControls.IsEnabled = true;
				SignIn.IsEnabled = false;
			}
		}

		private void SignOff_Click(object sender, RoutedEventArgs e)
		{
			Properties.Settings.Default.UserPin = "";
			sp_MainControls.IsEnabled = false;
			SignIn.IsEnabled = true;
			txtb_IsOk1.Text = "";
			txtb_IsOk2.Text = "";
		}

		private void btn_SignStart_Click(object sender, RoutedEventArgs e)
		{
			txtb_IsOk1.Text = "Ok!";
		}

		private void btn_SignEnd_Click(object sender, RoutedEventArgs e)
		{
			txtb_IsOk2.Text = "Ok!";
		}
	}
}
