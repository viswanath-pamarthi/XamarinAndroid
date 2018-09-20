using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace Spectrum
{
    [Activity(Label = "AddUser")]
    public class AddUserActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.AddUser);            

            var cancelButton = FindViewById<Button>(Resource.Id.cancelUserButton);
            var addUserButton = FindViewById<Button>(Resource.Id.createUserButton);

            cancelButton.Click += OnCancelClick;
            addUserButton.Click += OnCreateUserClick;
        }

        private void OnCreateUserClick(object sender, EventArgs e)
        {
            string uname = FindViewById<EditText>(Resource.Id.newUsername).Text;
            string pass = FindViewById<EditText>(Resource.Id.newPassword).Text;

            if (IsPasswordValid(pass) && !string.IsNullOrEmpty(uname))
            {
                var intent = new Intent();

                intent.PutExtra("username", uname);
                intent.PutExtra("password", pass);

                SetResult(Result.Ok, intent);

                Finish();
            }
            else
            {
                StringBuilder errorMessage = new StringBuilder();
                
                if (string.IsNullOrEmpty(uname))
                {
                    errorMessage.AppendLine("Username:");
                    errorMessage.AppendLine("Username is required.");
                    errorMessage.AppendLine(" ");
                }

                errorMessage.AppendLine("Password:");

                if (string.IsNullOrEmpty(pass))
                {
                    errorMessage.AppendLine("- Password is required");
                }

                errorMessage.AppendLine("- Password must consist of a mixture of letters and numerical digits only, with at least one of each. ");
                errorMessage.AppendLine("- Password must be between 5 and 12 characters in length.");
                errorMessage.AppendLine("- Password must not contain any sequence of characters immediately followed by the same sequence");

                AlertDialog.Builder alertDialog = new AlertDialog.Builder(this);
                alertDialog.SetTitle("Alert");
                alertDialog.SetMessage(errorMessage.ToString());
                alertDialog.SetNeutralButton("Ok", delegate { alertDialog.Dispose(); });
                alertDialog.Show();               
            }
        }

        private bool IsPasswordValid(string password)
        {
            var alphaNumbericMinMax = "^[a-zA-Z0-9]{5,12}$";
            var atleastOneCharacter = "[a-zA-Z]{1,}";
            var atleastOneNumber = "[0-9]{1,}";
            bool hasValidSubsequence = CheckSameSequenceSubStrings(password, 2);

            var reg = new Regex(alphaNumbericMinMax);
            var reg1 = new Regex(atleastOneCharacter);
            var reg2 = new Regex(atleastOneNumber);

            if (!reg.IsMatch(password) || !reg1.IsMatch(password) || !reg2.IsMatch(password) || !hasValidSubsequence || string.IsNullOrEmpty(password))
            {
                return false;
            }


            return true;
        }

        private bool CheckSameSequenceSubStrings(string password, int windowSize)
        {
            try
            {
                for (int window = windowSize; window <= password.Length / 2; window++)
                {
                    for (int index = 0; index + window < password.Length; index++)
                    {
                        if (string.Compare(password.Substring(index, window), password.Substring(index + window, window)) == 0)
                        {
                            return false;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Log.Info("AddUserActivity:", ex.Message);
            }

            return true;
        }


        private void OnCancelClick(object sender, EventArgs e)
        {
            Finish();
        }
    }
}
