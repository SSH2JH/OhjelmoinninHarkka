using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TyoajanSeuranta.Classes;

namespace TyoajanSeuranta.Classes.ViewModels {
	class Mysql_UserLoadWorkdays {
		public List<WorkDayItem> UserWorkDays { get; set; }
		/// <summary>
		/// This class probably holds too little stuff but it's good to be here just to hold the list property
		/// Aside from that the next method populates Workdayitem object for further analysis, such as datagrids
		/// </summary>
		public void LoadFromMysql()
		{
			try {
				List<WorkDayItem> userworkdays = new List<WorkDayItem>();
				string connStr = Properties.Settings.Default.MySqlLoginString;
				string sql = string.Format("SELECT WorkStartTime, WorkEndTime, TIMESTAMPDIFF(HOUR, WorkStartTime, WorkEndTime) FROM WorkDays WHERE EmployeeID LIKE {0}", Properties.Settings.Default.UserID);
				using (MySqlConnection conn = new MySqlConnection(connStr)) {
					conn.Open();
					using (MySqlCommand cmd = new MySqlCommand(sql, conn))
					using (MySqlDataReader reader = cmd.ExecuteReader()) {
						while (reader.Read()) {
							WorkDayItem s = new WorkDayItem();
							s.StartTime = reader.GetDateTime(0);
							s.EndTime = reader.GetDateTime(1);
							s.HoursTogether = reader.GetInt16(2);
							userworkdays.Add(s);
						}
						UserWorkDays = userworkdays;
					}
				}
			}
			catch {
				throw;
			}
		}
	}
}
