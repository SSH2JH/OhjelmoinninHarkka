using MySql.Data.MySqlClient;
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

namespace TyoajanSeuranta {
	/// <summary>
	/// Interaction logic for SignIn.xaml
	/// </summary>
	public partial class SignIn : Window {
		public SignIn()
		{
			InitializeComponent();
            txt_PinCode.Focusable = true;
            Keyboard.Focus(txt_PinCode);
        }


		/// <summary>
		/// User cancels the login and nothing happens
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void button_Cancel_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}

		/// <summary>
		/// User attempts to login and this will check whether the attempt is successful
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void button_Ok_Click(object sender, RoutedEventArgs e)
		{
            // Checks if the pincode textbox is empty
            if (string.IsNullOrEmpty(txt_PinCode.Text) == true)
            {
                // It was empty!
                txt_signinMessages.Text = "Pin koodi on pakollinen!";
            }
            else if (VerifyUserLogin(txt_PinCode.Text) == true)
            {
                // This is a little bit complicated one. If statement checks from a boolean method wether the user login is valid
                // If it is we return to main window
                this.Close();
            }
        }

		private bool VerifyUserLogin(string pincode)
		{
			// We create array of strings for the information we require from the mysql database for userlogin
			string[] userName = new string[3];
			try {
				// The connection string is preferably stored into application config
				string connStr = Properties.Settings.Default.MySqlLoginString;
				// The sql query
				string sql = string.Format("SELECT ID, FirstName, LastName FROM Employee WHERE Passwd LIKE {0}", pincode);
				// Set up the connection. using statments are only valid in its scope, it will self delete on "upper" scope
				using (MySqlConnection conn = new MySqlConnection(connStr)) {
					conn.Open();
					using (MySqlCommand cmd = new MySqlCommand(sql, conn))
					using (MySqlDataReader reader = cmd.ExecuteReader()) {
						while (reader.Read()) {
							// Reader reads the necessary information and places it to the array
							userName[0] = string.Format("{0}", reader.GetInt16(0));
							userName[1] = reader.GetString(1);
							userName[2] = reader.GetString(2);
						}
					}
				}
				// Check if the values in array are empty.
				if (string.IsNullOrWhiteSpace(userName[1]) == false && string.IsNullOrWhiteSpace(userName[2]) == false) {
					// In this case they are not empty. In that case fill config variables and return true
					Properties.Settings.Default.LoggedInUser = string.Format("{0} {1}", userName[1], userName[2]);
					Properties.Settings.Default.UserID = userName[0];
					return true;
				} else {
					// In this case the userlogin was incorrect and as a safety measure we clear the config variables
					// and lastly return false
					txt_signinMessages.Text = "Väärä pin-koodi!";
					Properties.Settings.Default.UserID = "";
					Properties.Settings.Default.LoggedInUser = "";
					return false;
				}
			}
			catch (Exception ex) {
				// This is meant for random hiccups. I haven't reached this point in a loooong time
				txt_signinMessages.Text = ex.Message;
				Properties.Settings.Default.UserID = "";
				// also, return false for good measure
				return false;
			}
		}
	}
}
