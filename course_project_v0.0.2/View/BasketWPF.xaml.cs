using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
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
			try
			{
				this.Close();
				MainWindow mainWindow = new MainWindow(ADMIN, LOGIN);
				mainWindow.Show();
			}
			catch(Exception)
			{
				MessageBox.Show("Нет подключения к интернету");
			}
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
		public static DateTime GetNetworkDateTime()
		{
			using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
			{

				socket.Connect("time.nist.gov", 13);
				using (StreamReader rstream = new StreamReader(new NetworkStream(socket)))
				{
					string value = rstream.ReadToEnd().Trim();
					MatchCollection matches = Regex.Matches(value, @"((\d*)-(\d*)-(\d*))|((\d*):(\d*):(\d*))");
					string[] dd = matches[0].Value.Split('-');
					return DateTime.Parse($"{matches[1].Value} {dd[2]}.{dd[1]}.{dd[0]}");
				}

			}
		}
		private void Button_Del_Click(object sender, RoutedEventArgs e)
		{
			try
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
			catch(Exception)
			{

			}
		}
	}
}
