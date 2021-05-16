using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using course_project_v0._0._2.DataBase;

namespace course_project_v0._0._2.View
{
	public partial class BasketWPF : Window
	{
		public BasketWPF(string _login, bool _admin)
		{
			LOGIN = _login;
			ADMIN = _admin;
			InitializeComponent();
			InfoForListBox();
			LoginUser.Text = LOGIN;
		}
		private void Button_Click_Back(object sender, RoutedEventArgs e)
		{
			this.Close();
			MainWindow mainWindow = new MainWindow(ADMIN, LOGIN);
			mainWindow.Show();
		}

		public string LOGIN;
		public bool ADMIN;
		private ObservableCollection<AppViewTickets> infoforTickets;
		public void InfoForListBox()
		{
			using (course_work cw = new course_work())
			{
				var info = cw.Ticket.ToList();
				infoforTickets = new ObservableCollection<AppViewTickets>();
				var forBD = cw.Database.SqlQuery<UsersBD>($"select * from UsersBD where UsersBD.login = '{LOGIN}'");
				foreach (var check in forBD)
				{
					foreach (var i in info)
					{
						if (check.userID == i.userID)
						{
							var forFilm = cw.Database.SqlQuery<Film>($"select * from Film where Film.filmName = '{i.filmName}'");
							foreach (var n in forFilm)
							{
								if (n.filmName == i.filmName)
								{
									AppViewTickets allTicket = new AppViewTickets();
									allTicket.InfoForTickets(i.ticketID, i.sessionID, i.userID, i.filmName, i.price, i.date, i.time, i.row, i.place, n.poster);
									infoforTickets.Add(allTicket);
								}
								
							}
						}
					}
				}
				ListBoxTicket.ItemsSource = infoforTickets;
			}
		}
		private void Button_Del_Click(object sender, RoutedEventArgs e)
		{
			var contentListBox = ListBoxTicket.SelectedItem as AppViewTickets;
			if (contentListBox != null)
			{

				course_work context = new course_work();
				Ticket customer = context.Ticket
				 .Where(c => c.ticketID == contentListBox.TicketID)
				 .FirstOrDefault();

				context.Ticket.Remove(customer);
				context.SaveChanges();
			}
			InfoForListBox();
		}
	}
}
