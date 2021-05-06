using course_project_v0._0._2.View;
using System;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using course_project_v0._0._2.DataBase;

namespace course_project_v0._0._2
{
	/// <summary>
	/// Логика взаимодействия для MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
            InfoForListBox();
            DataContext = new AppView();
          //  ButtonForAdmin.Visibility = Visibility.Hidden;
            


        }

        private ObservableCollection<AppView> infoforfilm;
   
        public void InfoForListBox()
        {
            using (course_work cw = new course_work())
            {

                var info = cw.Film.ToList();
                infoforfilm = new ObservableCollection<AppView>();
                foreach (var i in info)
                {
                    AppView allFilms = new AppView();

                    allFilms.Add(i.filmName, (int)i.year,i.genres, (float)i.rating,i.countries,i.director, (int)i.duration);
                    infoforfilm.Add(allFilms);
                }
                ListBoxFilms.ItemsSource = infoforfilm;
            }
        }

		private void Button_Click_Admin(object sender, RoutedEventArgs e)
		{
            this.Close();
            AdminAddFilm adminAddFilm = new AdminAddFilm();
            adminAddFilm.Show();
        }
      /* private void Button_Click_Admin2(object sender, RoutedEventArgs e)
        {
            AppView app = new AppView();
            string login;
            login = app.login;

            using (course_work cw = new course_work())
            {
                var forBD = cw.Database.SqlQuery<UsersBD>($"select * from UsersBD where UsersBD.login = '{login}'");
                foreach (var check in forBD)
                {
                    if (check.admin == true)
                    {
                        ButtonForAdmin.Visibility = Visibility.Visible;
                    }
                }
            }
        }*/
    }
}
