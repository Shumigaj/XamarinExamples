using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.Accounts;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace droid.accounts.Authenticator
{
    public class AuthenticatorActivity : AccountAuthenticatorActivity
    {
        public static String ARG_ACCOUNT_TYPE = "ACCOUNT_TYPE";
        public static String ARG_AUTH_TYPE = "AUTH_TYPE";
        public static String ARG_ACCOUNT_NAME = "ACCOUNT_NAME";
        public static String ARG_IS_ADDING_NEW_ACCOUNT = "IS_ADDING_ACCOUNT";

        public static String KEY_ERROR_MESSAGE = "ERR_MSG";

        public static String PARAM_USER_PASS = "USER_PASS";

        private int REQ_SIGNUP = 1;

        private String TAG = typeof(AuthenticatorActivity).FullName;

        private AccountManager mAccountManager;
        private String mAuthTokenType;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            Console.WriteLine("AuthenticatorActivity: OnCreate");

            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.act_login);

            mAccountManager = AccountManager.Get(Application.BaseContext);

            String accountName = Intent.GetStringExtra(ARG_ACCOUNT_NAME);
            mAuthTokenType = Intent.GetStringExtra(ARG_AUTH_TYPE);//getIntent() == Intent
            if (mAuthTokenType == null)
                mAuthTokenType = AccountGeneral.AUTHTOKEN_TYPE_FULL_ACCESS;

            if (accountName != null)
            {
                var textView = FindViewById<TextView>(Resource.Id.accountName);
                textView.Text = accountName;
            }

            var submitButton = FindViewById<Button>(Resource.Id.submit);
            submitButton.Click += Submit;
        }

        public void Submit(object sender, EventArgs e)
        {
            String userName = (FindViewById<TextView>(Resource.Id.accountName)).Text;
            String userPass = (FindViewById<TextView>(Resource.Id.accountPassword)).Text;

            String accountType = Intent.GetStringExtra(ARG_ACCOUNT_TYPE);

            /*new AsyncTask<String, Void, Intent>() {

            @Override
            protected Intent doInBackground(String... params)
            {

                Log.d("udinic", TAG + "> Started authenticating");

                Bundle data = new Bundle();
                try
                {
                    User user = sServerAuthenticate.userSignIn(userName, userPass, mAuthTokenType);

                    data.putString(AccountManager.KEY_ACCOUNT_NAME, userName);
                    data.putString(AccountManager.KEY_ACCOUNT_TYPE, accountType);
                    data.putString(AccountManager.KEY_AUTHTOKEN, user.getSessionToken());

                    // We keep the user's object id as an extra data on the account.
                    // It's used later for determine ACL for the data we send to the Parse.com service
                    Bundle userData = new Bundle();
                    userData.putString(USERDATA_USER_OBJ_ID, user.getObjectId());
                    data.putBundle(AccountManager.KEY_USERDATA, userData);

                    data.putString(PARAM_USER_PASS, userPass);

                }
                catch (Exception e)
                {
                    data.putString(KEY_ERROR_MESSAGE, e.getMessage());
                }

                final Intent res = new Intent();
                res.putExtras(data);
                return res;
            }

            @Override
                protected void onPostExecute(Intent intent)
            {
                if (intent.hasExtra(KEY_ERROR_MESSAGE))
                {
                    Toast.makeText(getBaseContext(), intent.getStringExtra(KEY_ERROR_MESSAGE), Toast.LENGTH_SHORT).show();
                }
                else
                {
                    finishLogin(intent);
                }
            }
        }.execute();
    }*/

        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            // The sign up activity returned that the user has successfully created an account
            if (requestCode == REQ_SIGNUP && resultCode == Result.Ok)
            {
                FinishLogin(data);
            }

            base.OnActivityResult(requestCode, resultCode, data);
        }

        private void FinishLogin(Intent intent)
        {
            //Log.d("udinic", TAG + "> finishLogin");

            String accountName = intent.GetStringExtra(AccountManager.KeyAccountName);
            String accountPassword = intent.GetStringExtra(PARAM_USER_PASS);
            Account account = new Account(accountName, intent.GetStringExtra(AccountManager.KeyAccountType));

            if (Intent.GetBooleanExtra(ARG_IS_ADDING_NEW_ACCOUNT, false))
            {
                //Log.d("udinic", TAG + "> finishLogin > addAccountExplicitly");
                String authtoken = intent.GetStringExtra(AccountManager.KeyAuthtoken);
                String authtokenType = mAuthTokenType;

                // Creating the account on the device and setting the auth token we got
                // (Not setting the auth token will cause another call to the server to authenticate the user)
                mAccountManager.AddAccountExplicitly(account, accountPassword, intent.GetBundleExtra(AccountManager.KeyUserdata));
                mAccountManager.SetAuthToken(account, authtokenType, authtoken);
            }
            else
            {
                //Log.d("udinic", TAG + "> finishLogin > setPassword");
                mAccountManager.SetPassword(account, accountPassword);
            }

            SetAccountAuthenticatorResult(intent.Extras);
            SetResult(Result.Ok, intent);
            Finish();
        }
    }
}