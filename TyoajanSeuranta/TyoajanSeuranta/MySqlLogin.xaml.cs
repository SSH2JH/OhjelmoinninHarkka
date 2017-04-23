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
	/// Interaction logic for MySqlLogin.xaml
	/// </summary>
	public partial class MySqlLogin : Window {
		public MySqlLogin()
		{
			InitializeComponent();
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			if (string.IsNullOrWhiteSpace(txt_ServerIp.Text) == false && string.IsNullOrWhiteSpace(txt_ServerCatalog.Text) == false &&
				string.IsNullOrWhiteSpace(txt_ServerUserName.Text) == false && string.IsNullOrWhiteSpace(txt_ServerPassword.Text) == false) {
				Properties.Settings.Default.Server = txt_ServerIp.Text;
				Properties.Settings.Default.Catalog = txt_ServerCatalog.Text;
				Properties.Settings.Default.User = txt_ServerUserName.Text;
				Properties.Settings.Default.Password = txt_ServerPassword.Text;
				if (checkIfServerIsValid() == true) {
					this.Close();
				}
			} else {
				txtb_Messages.Text = "Please enter the credentials!";
			}
		}
		private bool checkIfServerIsValid()
		{
			string server = Properties.Settings.Default.Server;
			string cat = Properties.Settings.Default.Catalog;
			string un = Properties.Settings.Default.User;
			string pw = Properties.Settings.Default.Password;
			string connstr = string.Format("Data source={0};Initial Catalog={1};user={2};password={3}", server, cat ,un, pw);
			MySqlConnection conn = null;
			try {
				conn = new MySqlConnection(connstr);
				conn.Open();
				conn.Close();
				return true;
			}
			catch (Exception) {
				MessageBox.Show("Incorrect login!");
				return false;
			}
		}
	}
}
