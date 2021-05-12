﻿using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using course_project_v0._0._2.DataBase;
using GalaSoft.MvvmLight;

namespace course_project_v0._0._2.View
{
	class AppViewUsers : ViewModelBase
    {
        public string UserID { get; set; }
        public string login { get; set; }
        public string password { get; set; }
        public string Email { get; set; }
        public bool admin { get; set; }
        public string basket { get; set; }

        public void AddUser(string _UserID, string _login, string _password, string _Email, bool _admin, string _basket)
        {
            UserID = _UserID;
            login = _login;
            password = _password;
            Email = _Email;
            admin = _admin;
            basket = _basket;
        }
    }
}
