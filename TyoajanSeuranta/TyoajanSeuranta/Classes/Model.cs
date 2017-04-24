using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TyoajanSeuranta.Classes {
	/// <summary>
	/// This class handles Employee's workday model
	/// </summary>
	class WorkDayItem {
		public int EmployeeId { get; set; }
        public DateTime date;

        public string Date
        {
            get {
                string[] s;
                s = date.ToString().Split(' ');
                return s[0];
            }
        }

        public DateTime StartTime { get; set; }
		public DateTime EndTime { get; set; }
		public float WorkHours { get; set; }
	}
	/// <summary>
	/// This class holds employee's personal data
	/// </summary>
	class Employee {
		public int ID { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public int Passwd { get; set; }
		public bool IsAdministrator { get; set; }
	}
	/// <summary>
	/// Here are all the multipliers
	/// </summary>
	class HourMultipliers {
		public int MultiplierID { get; set; }
		public string Name { get; set; }
		private float multiplier;

		public float Multiplier
		{
			// Multiplier probably has no reason to be under 1
			get { return multiplier; }
			set {
				multiplier = value;
				if (multiplier < 1) {
					multiplier = 1;
				}
			}
		}

	}
}
