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

		/// <summary>
		/// SignIn_Click the logic for user login. It handles the initial mysql access and user credentials and stores them
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void SignIn_Click(object sender, RoutedEventArgs e)
		{
			SignIn next = new SignIn();

			//TODO take this into account and remove debug code
			// This is debug code for testing and demonstration purposes. It checks whether values in application config are present, if not, ask them before userlogin
			if (string.IsNullOrWhiteSpace(Properties.Settings.Default.Server) == true || string.IsNullOrWhiteSpace(Properties.Settings.Default.Catalog) == true ||
				string.IsNullOrWhiteSpace(Properties.Settings.Default.User) == true || string.IsNullOrWhiteSpace(Properties.Settings.Default.Password) == true) {
				MySqlLogin whee = new MySqlLogin();
				whee.ShowDialog();
			}

			// Login dialog opens and asks for user credentials
			next.ShowDialog();
			// This if statement checks whether the saved UserID property is null or not and return boolean
			if (string.IsNullOrWhiteSpace(Properties.Settings.Default.UserID) == false) {
				// If all is good we enable the UI
				txtb_WellcomeUser.Text = String.Format("Hei {0}!", Properties.Settings.Default.LoggedInUser);
				sp_MainControls.IsEnabled = true;
				SignIn.IsEnabled = false;
			} // If the user pressed cancel in the login screen he wil end up here.
		}

		private void SignOff_Click(object sender, RoutedEventArgs e)
		{
			// This is a cleanup for signing off. This probably cleans up too much stuff but, honestly... you can't be
			// carefull enough
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
			// This button allows employee to start their workday
			try {
				AddFirstEntry controls = new AddFirstEntry();
				// In the next method happens all the sql magic. You can find it under classes and viewmodel
				controls.AddNewEntry();
				// also disable the button for this session because why would you start workday two times per day
				txtb_IsOk1.Text = "Ok!";
				btn_SignStart.IsEnabled = false;
			}
			catch (Exception ex) {
				MessageBox.Show(ex.Message);
			}
		}

		private void btn_SignEnd_Click(object sender, RoutedEventArgs e)
		{
			// This is same as the previous but reverse. This ends the workday
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

		/// <summary>
		/// this for now handles the employee's personal workhours
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void EditWorkHours_Click(object sender, RoutedEventArgs e)
		{
			OwnWorkDays next = new OwnWorkDays();
			next.Show();
		}
	}
}
