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

namespace Spectrum
{
    /// <summary>
    /// Custom adapter for User ListView
    /// </summary>
    public class UserAdapter : BaseAdapter<User>
    {
        private Context userContext;//variable to hold the context of the UserAdapter
        private List<User> Users;//List of Users sent to UserAdapter

        public UserAdapter(Context context, List<User> listOfUsers)
        {
            userContext = context;
            Users = listOfUsers;            
        }

        /// <summary>
        /// Get the count of number of users in the list view
        /// </summary>
        public override int Count => Users.Count;    

        /// <summary>
        /// Get the user at a specific position in the list
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public override User this[int position]=>Users[position];

        public override long GetItemId(int position) => position;

        /// <summary>
        /// Get the specific view for each row in ListView
        /// </summary>
        /// <param name="position">position of the user in the ListView</param>
        /// <param name="convertView">The view to be shown for each row in ListView for User</param>
        /// <param name="parent">The parent of each view</param>
        /// <returns></returns>
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View rowInListView = convertView;

            try
            {
                if (rowInListView == null)
                {
                    rowInListView = LayoutInflater.From(userContext).Inflate(Resource.Layout.UserView, null, false);
                }

                TextView textView = rowInListView.FindViewById<TextView>(Resource.Id.UserName);
                textView.Text = Users[position].Username;
            }
            catch (Exception ex)
            {
                Log.Info("UserAdapter", ex.Message);
            }          

            return rowInListView;
        }
    }
}