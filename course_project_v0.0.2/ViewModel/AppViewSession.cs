using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using System.Threading.Tasks;

namespace course_project_v0._0._2.View
{
	class AppViewSession : ViewModelBase
	{
		public string sessionID { get; set; }
		public string filmID { get; set; }
		public DateTime date { get; set; }
		public TimeSpan time { get; set; }
		public string hall { get; set; }


		public void AddSession(string _sessionID, string _filmID, DateTime _date, TimeSpan _time, string _hall)
		{
			sessionID = _sessionID;
			filmID = _filmID;
			date = _date;
			time = _time;
			hall = _hall;
		}
	


	}
}
