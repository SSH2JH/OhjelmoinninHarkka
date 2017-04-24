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
using TyoajanSeuranta.Classes.ViewModels;

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
			//TODO take this into account and remove debug code

			/*if (string.IsNullOrWhiteSpace(Properties.Settings.Default.Server) == true || string.IsNullOrWhiteSpace(Properties.Settings.Default.Catalog) == true ||
				string.IsNullOrWhiteSpace(Properties.Settings.Default.User) == true || string.IsNullOrWhiteSpace(Properties.Settings.Default.Password) == true) {
				MySqlLogin whee = new MySqlLogin();
				whee.ShowDialog();
			}*/

			

			next.ShowDialog();
			if (string.IsNullOrWhiteSpace(Properties.Settings.Default.UserID) == false) {
				txtb_WellcomeUser.Text = String.Format("Hei {0}!", Properties.Settings.Default.LoggedInUser);
				sp_MainControls.IsEnabled = true;
				SignIn.IsEnabled = false;
			}
		}

		private void SignOff_Click(object sender, RoutedEventArgs e)
		{
			Properties.Settings.Default.UserID = "";
			Properties.Settings.Default.LoggedInUser = "";
			sp_MainControls.IsEnabled = false;
			btn_SignStart.IsEnabled = true;
			btn_SignEnd.IsEnabled = true;
			SignIn.IsEnabled = true;
			txtb_IsOk1.Text = "";
			txtb_IsOk2.Text = "";
			txtb_WellcomeUser.Text = "Hei";
		}

		private void btn_SignStart_Click(object sender, RoutedEventArgs e)
		{
			try {
				AddFirstEntry controls = new AddFirstEntry();
				controls.AddNewEntry();
				txtb_IsOk1.Text = "Ok!";
				btn_SignStart.IsEnabled = false;
			}
			catch (Exception ex) {
				MessageBox.Show(ex.Message);
			}
		}

		private void btn_SignEnd_Click(object sender, RoutedEventArgs e)
		{
			try {
				AddFirstEntry controls = new AddFirstEntry();
				controls.AddLastEntry();
				txtb_IsOk2.Text = "Ok!";
				btn_SignEnd.IsEnabled = false;
			}
			catch (Exception ex) {
				MessageBox.Show(ex.Message);
			}
		}

		private void EditWorkHours_Click(object sender, RoutedEventArgs e)
		{
			OwnWorkDays next = new OwnWorkDays();
			next.Show();
		}
	}
}
