using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;

namespace Spectrum
{
    /// <summary>
    /// User class to hold the data
    /// </summary>
    public class User
    {
        private string _userName;
        private string _password;
        public User(string userName, string password)
        {
            this.Username = userName;
            this.Password = password;
        }
        
        public User() { }

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [NotNull]
        public string Username
        {
            get => _userName;
            set => _userName = value;
        }

        [NotNull]
        public string Password
        {
            get => _password;
            set => _password = value;
        }
    }
}