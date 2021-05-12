using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using course_project_v0._0._2.DataBase;
using System.Windows;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Data;
using System.IO;

namespace course_project_v0._0._2.View
{
	public partial class TicketWPF : Window
	{
		public TicketWPF(string _login)
		{
			InitializeComponent();
			//FILMID = _filmID;
			LOGIN = _login;
		}
		public string FILMID;
		public string FILMNAME;
		public string SESSID;
		public string LOGIN;
		public string USERID;
		public void randID()
		{
			Random rnd = new Random();
			int value = rnd.Next(1000000, 9999999);
			SESSID = $"{value}";
		}
		public void InfoForUser()
		{
			using (course_work cw = new course_work())
			{
				var forBD = cw.Database.SqlQuery<UsersBD>($"select * from UsersBD where UsersBD.login = '{LOGIN}'");
				foreach (var check in forBD)
				{
					if (LOGIN == check.login)
					{
						USERID = check.userID;
					}
				}

				/*var forFilm = cw.Database.SqlQuery<Film>($"select * from Film where v.filmID = '{FILMID}'");
				foreach (var check in forFilm)
				{
					if (FILMID == check.filmID)
					{
						FILMNAME = check.filmName;
					}
				}*/
			}
		}
		private void Button_Buy_Click(object sender, RoutedEventArgs e)
		{
			using (course_work cw = new course_work())
			{
				Ticket ticket = new Ticket()
				{
					sessionID = SESSID.Trim(),
					userID = USERID.Trim(),
					//filmName = FILMNAME.Trim(),
					//price = 
					//date = 
					//time =
					row = Convert.ToInt32(ComboBoxRow.SelectedItem),
					place = Convert.ToInt32(ComboBoxPlace.SelectedItem)

				};
				cw.Ticket.Add(ticket);
				cw.SaveChanges();
			}
			MessageBox.Show("Запись прошла успешно.");
		}
	}
	
}
