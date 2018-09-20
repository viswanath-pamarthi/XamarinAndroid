using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using System.Collections.Generic;
using System;
using Android.Content;
using Spectrum.Resources;

namespace Spectrum
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {        
        //List of Users
        public static List<User> Users;        
        private UserAdapter _userAdapter;//Adapter for the Listview        
        Database database;//Database object to handle SQLite operations for User table in SQLite DB

        /// <summary>
        /// Override the OnCreate method of AppCompatActivity
        /// </summary>
        /// <param name="savedInstanceState">the parcelable values</param>
        protected override void OnCreate(Bundle savedInstanceState)
        {            
             base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            database = new Database();
            database.CreateDataBase();

            //Fetch users from Database
            Users = new List<User>(database?.GetTableData());

            //Configure User ListView data
            ListView usersListView = FindViewById<ListView>(Resource.Id.listViewUsers);
            _userAdapter= new UserAdapter(this, Users);
            usersListView.Adapter = _userAdapter;

            //Attach an eevnt handler
            FindViewById<Button>(Resource.Id.AddUserButton).Click += OnAddUserClick;

        }

        /// <summary>
        /// Event handler for Add User button click
        /// </summary>
        /// <param name="sender">sender objec for the event handler</param>
        /// <param name="e">event args for the event handler</param>
        private void OnAddUserClick(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(AddUserActivity));

            StartActivityForResult(intent, 100);
        }

        /// <summary>
        /// Get the new user data after creating the user by AddUser
        /// </summary>
        /// <param name="requestCode">The request code sent to AddUser</param>
        /// <param name="resultCode">resullt code from Add User</param>
        /// <param name="data">INtent from Add user with the Extra/return data</param>
        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            if (requestCode == 100 && resultCode == Result.Ok)
            {
                //Fetch user data from the result intent
                string newUsername = data.GetStringExtra("username");

                //TODO: Can has the password and store in db
                string newPassword = data.GetStringExtra("password");
                User newUser = new User(newUsername, newPassword);

                //Insert data in to SQLIte User table
                if(database.InsertIntoTable(newUser))
                {
                    //Add the user to ListView
                    MainActivity.Users.Add(newUser);
                    _userAdapter.NotifyDataSetChanged();
                }
                
            }
        }
    }
}