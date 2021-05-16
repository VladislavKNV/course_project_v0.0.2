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
		public TicketWPF(string _login, string _sessionID)
		{
			LOGIN = _login;
			SESSID = _sessionID;
			InitializeComponent();
			randID();
			Info();
			TicketsSingleton();


		}
		public string SESSID;
		public string LOGIN;
		public string USERID;
		public string TICKETID;
		public DateTime DATE;
		public TimeSpan TIME;
		public int PRICE;
		public int NumberOfFreeSeats;
		public string FILMNAME;
		public bool valid = true;
		public void randID()
		{
			Random rnd = new Random();
			int value = rnd.Next(1000000, 9999999);
			TICKETID = $"{value}";
		}
		public void TicketsSingleton()
		{
			Singleton singleton = new Singleton();
			qwerty.Background = singleton.bgColor;
		}
		public void Info()
		{
			using (course_work cw = new course_work())
			{
				var forBD = cw.Database.SqlQuery<UsersBD>($"select * from UsersBD");
				foreach (var check in forBD)
				{
					if (LOGIN.Trim() == check.login.Trim())
					{
						USERID = check.userID;
					}
				}

				var forBDs = cw.Database.SqlQuery<Session>($"select * from Session where Session.sessionID = '{SESSID}'");
				foreach (var check in forBDs)
				{
					if (SESSID == check.sessionID)
					{

						var forFilm = cw.Database.SqlQuery<Film>($"select * from Film where Film.filmID = '{check.filmID}'");
						foreach (var i in forFilm)
						{
							if (check.filmID == i.filmID)
							{
								FILMNAME = i.filmName.Trim();
								PRICE = check.price_for_place;
								DATE = check.date;
								TIME = check.time;
								NumberOfFreeSeats = check.number_of_free_seats;
							}

						}
					}
				}
			}
		}
		private void Button_Buy_Click(object sender, RoutedEventArgs e)
		{

			using (course_work cw = new course_work())
			{
				var forBD = cw.Database.SqlQuery<Ticket>($"select * from Ticket where Ticket.sessionID = '{SESSID}'");
				foreach (var check in forBD)
				{

						if (check.row == Convert.ToInt32(ComboBoxRow.Text) && check.place == Convert.ToInt32(ComboBoxPlace.Text))
						{
							valid = false;

						}
				}
				if (valid == true)
				{
					NumberOfFreeSeats--;
					Ticket ticket = new Ticket()
					{
						ticketID = TICKETID.Trim(),
						sessionID = SESSID.Trim(),
						userID = USERID.Trim(),
						filmName = FILMNAME.Trim(),
						price = PRICE,
						date = DATE,
						time = TIME,
						row = Convert.ToInt32(ComboBoxRow.Text),
						place = Convert.ToInt32(ComboBoxPlace.Text)

					};
					cw.Ticket.Add(ticket);
					cw.SaveChanges();
					MessageBox.Show("Запись прошла успешно.");
					//
						course_work context = new course_work();
						var customer = context.Session
							.Where(c => c.sessionID == SESSID)
							.FirstOrDefault();
						customer.number_of_free_seats = NumberOfFreeSeats;
						context.SaveChanges();
					//
				}
				else
				{
					MessageBox.Show("Место занято.");
				}	
					
			}
			
		}
	}
	
}
