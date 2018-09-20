using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using SQLite;
using Spectrum;

namespace Spectrum.Resources
{
    /// <summary>
    /// Class to help with data operations o SQLite or the User table
    /// </summary>
   public class Database
    {
        string folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
       
        /// <summary>
        /// Method to create the SQlite database
        /// </summary>
        /// <returns></returns>
        public bool CreateDataBase()
        {
            try
            {
                using (var connection=new SQLiteConnection(System.IO.Path.Combine(folder, "Spectrum.db3")))
                {
                    connection.CreateTable<User>();
                    return true;
                }
            }
            catch(SQLiteException ex)
            {
                Log.Info("SQLiteEx", ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Method to insert the User record in to User table
        /// </summary>
        /// <typeparam name="User"></typeparam>
        /// <param name="entity">user record to insert</param>
        /// <returns></returns>
        public bool InsertIntoTable<User>(User entity)
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "Spectrum.db3")))
                {
                    connection.Insert(entity);
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEx", ex.Message);
                return false;
            }
        }


        /// <summary>
        /// Method to oget the User table data
        /// </summary>
        /// <returns></returns>
        public List<User> GetTableData()
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "Spectrum.db3")))
                {                    
                    return connection.Table<User>().ToList();
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEx", ex.Message);
                return null;
            }
        }

    }
}