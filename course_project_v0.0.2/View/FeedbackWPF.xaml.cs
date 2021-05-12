using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using course_project_v0._0._2.DataBase;
using System.Windows.Controls;
using System.Windows.Data;

using System.Collections.ObjectModel;

namespace course_project_v0._0._2.View
{
	public partial class FeedbackWPF : Window
	{
		public FeedbackWPF(bool admin, string login)
		{
			InitializeComponent();
			InfoForFeedback();
			LOGIN = login;
			IDRand();
			userid();
		}
		public bool ADMIN;
		public string LOGIN;
		public string USERid;
		private string FeedbackID;
		private void Button_Click_Save(object sender, RoutedEventArgs e)
		{
			using (course_work cw = new course_work())
			{
				Feedback feedback = new Feedback()
				{
					feedbackID = FeedbackID.Trim(),
					userID = USERid.Trim(),
					feedback1 = Feedback_TextBox.Text.Trim(),
					login = LOGIN.Trim(),
					dateFeedback = DateTime.Now
				};
				cw.Feedback.Add(feedback);
				cw.SaveChanges();
			}
			MessageBox.Show("Спасибо за отзыв.");
		}

		private ObservableCollection<AppViewFeedback> infoforfeedback;

		public void InfoForFeedback()
		{
			using (course_work cw = new course_work())
			{

				var info = cw.Feedback.ToList();
				infoforfeedback = new ObservableCollection<AppViewFeedback>();
				foreach (var i in info)
				{
					AppViewFeedback allFeedback = new AppViewFeedback();

					allFeedback.AddFeedback(i.login,i.feedback1,i.dateFeedback);
					infoforfeedback.Add(allFeedback);
				}
				ListBoxFeedbacks.ItemsSource = infoforfeedback;
			}
		}
		public void	userid()
		{
			using (course_work cw = new course_work())
			{
				var forenter = cw.Database.SqlQuery<UsersBD>($"select * from UsersBD");
				foreach (var check in forenter)
				{
					if (check.login == LOGIN)
					{
						USERid = check.userID;
					}
				}
			}
		}
		private void IDRand()
		{
			Random rnd = new Random();
			int value = rnd.Next(1000000, 9999999);
			FeedbackID = $"{value}";
		}
	}
}
