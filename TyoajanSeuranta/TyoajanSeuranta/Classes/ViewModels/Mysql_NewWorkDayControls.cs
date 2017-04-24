using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TyoajanSeuranta.Classes.ViewModels {
	class AddFirstEntry {
		/// <summary>
		/// This method creates a new row and fills it with current date and time
		/// </summary>
		public void AddNewEntry()
		{
			try {
				string connStr = Properties.Settings.Default.MySqlLoginString;
				string sql = string.Format("INSERT INTO WorkDays (EmployeeID, WorkStartTime) VALUES ({0}, '{1}')", Properties.Settings.Default.UserID,
					DateTime.Now.ToString("yyyy-MM-dd hh:mm"));
				using (MySqlConnection conn = new MySqlConnection(connStr)) {
					conn.Open();
					MySqlCommand cmd = new MySqlCommand(sql, conn);
					cmd.ExecuteNonQuery();
				}
			}
			catch { 

                throw;
			}
		}

		/// <summary>
		/// This method updates the row and fills it with current date and time for employee's end of workday
		/// </summary>
		public void AddLastEntry()
		{
			try {
				string connStr = Properties.Settings.Default.MySqlLoginString;
				string sql = string.Format("UPDATE WorkDays SET WorkEndTime='{0}' WHERE EmployeeID LIKE {1} AND WorkEndTime IS NULL ORDER BY WorkEndTime DESC LIMIT 1",
					DateTime.Now.ToString("yyyy-MM-dd hh:mm"), Properties.Settings.Default.UserID);
				using (MySqlConnection conn = new MySqlConnection(connStr)) {
					conn.Open();
					MySqlCommand cmd = new MySqlCommand(sql, conn);
					cmd.ExecuteNonQuery();
				}
			}
			catch {
				throw;
			}
		}
	}
}
