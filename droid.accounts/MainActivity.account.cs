using Android.Accounts;
using Android.App;
using Android.Content;
using Android.Icu.Util;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Text;
using Android.Views;
using Android.Widget;
using droid.accounts.Authenticator;
using System;

namespace droid.accounts
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity, NavigationView.IOnNavigationItemSelectedListener
    {
        private static String STATE_DIALOG = "state_dialog";
        private static String STATE_INVALIDATE = "state_invalidate";

        private AccountManager mAccountManager;
        private global::Android.App.AlertDialog mAlertDialog;
        private bool mInvalidate;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
            global::Android.Support.V7.Widget.Toolbar toolbar = FindViewById<global::Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            fab.Click += FabOnClick;

            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            ActionBarDrawerToggle toggle = new ActionBarDrawerToggle(this, drawer, toolbar, Resource.String.navigation_drawer_open, Resource.String.navigation_drawer_close);
            drawer.AddDrawerListener(toggle);
            toggle.SyncState();

            NavigationView navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            navigationView.SetNavigationItemSelectedListener(this);



            /*mAccountManager = AccountManager.Get(this);

            if (savedInstanceState != null)
            {
                bool showDialog = savedInstanceState.GetBoolean(STATE_DIALOG);
                bool invalidate = savedInstanceState.GetBoolean(STATE_INVALIDATE);
                if (showDialog)
                {
                    showAccountPicker(AccountGeneral.AUTHTOKEN_TYPE_FULL_ACCESS, invalidate);
                }
            }*/
        }

        public override void OnBackPressed()
        {
            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            if (drawer.IsDrawerOpen(GravityCompat.Start))
            {
                drawer.CloseDrawer(GravityCompat.Start);
            }
            else
            {
                base.OnBackPressed();
            }
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.action_settings)
            {
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        private void FabOnClick(object sender, EventArgs eventArgs)
        {
            View view = (View)sender;
            Snackbar.Make(view, "Replace with your own action", Snackbar.LengthLong)
                .SetAction("Action", (global::Android.Views.View.IOnClickListener)null).Show();
        }

        public bool OnNavigationItemSelected(IMenuItem item)
        {
            int id = item.ItemId;

            /*if (id == Resource.Id.nav_camera)
            {
                addNewAccount(AccountGeneral.ACCOUNT_TYPE, AccountGeneral.AUTHTOKEN_TYPE_FULL_ACCESS);
            }
            else if (id == Resource.Id.nav_gallery)
            {
                showAccountPicker(AccountGeneral.AUTHTOKEN_TYPE_FULL_ACCESS, false);
            }
            else if (id == Resource.Id.nav_slideshow)
            {
                getTokenForAccountCreateIfNeeded(AccountGeneral.ACCOUNT_TYPE, AccountGeneral.AUTHTOKEN_TYPE_FULL_ACCESS);
            }
            else if (id == Resource.Id.nav_manage)
            {
                showAccountPicker(AccountGeneral.AUTHTOKEN_TYPE_FULL_ACCESS, true);
            }
            else if (id == Resource.Id.nav_share)
            {
            }
            else if (id == Resource.Id.nav_send)
            {
            }*/
            
            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            drawer.CloseDrawer(GravityCompat.Start);
            return true;
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
        }

        protected override void OnSaveInstanceState(Bundle outState)
        {
            base.OnSaveInstanceState(outState);
            /*if (mAlertDialog != null && mAlertDialog.IsShowing)
            {
                outState.PutBoolean(STATE_DIALOG, true);
                outState.PutBoolean(STATE_INVALIDATE, mInvalidate);
            }*/
        }

        private void addNewAccount(String accountType, String authTokenType)
        {
        }

        private void showAccountPicker(String authTokenType, bool invalidate)
        {
            /*mInvalidate = invalidate;
            Account[] availableAccounts = mAccountManager.GetAccountsByType(AccountGeneral.ACCOUNT_TYPE);

            if (availableAccounts.Length == 0)
            {
                Toast.MakeText(this, "No accounts", ToastLength.Long).Show();
            }
            else
            {
                String[] name = new String[availableAccounts.Length];
                for (int i = 0; i < availableAccounts.Length; i++)
                {
                    name[i] = availableAccounts[i].Name;
                }

                // Account picker
                //mAlertDialog = new global::Android.App.AlertDialog.Builder(this)
                //    .SetTitle("Pick Account")
                //    .SetAdapter(new ArrayAdapter<String>(Application.BaseContext, android.R.layout.simple_list_item_1, name), new DialogInterface.OnClickListener() {
                //
                //    public void onClick(DialogInterface dialog, int which)
                //    {
                //        if (invalidate)
                //            invalidateAuthToken(availableAccounts[which], authTokenType);
                //        else
                //            getExistingAccountAuthToken(availableAccounts[which], authTokenType);
                //    }
                //}).create();
                //mAlertDialog.show();
            }*/
        }

        private void getExistingAccountAuthToken(Account account, String authTokenType)
        {
            IAccountManagerFuture future = mAccountManager.GetAuthToken(account, authTokenType, null, this, null, null);

            var bnd = future.GetResult(5L, Java.Util.Concurrent.TimeUnit.Seconds);
            bnd.Wait();
            //String authtoken = bnd.GetString(AccountManager.KEY_AUTHTOKEN);

            /*new Thread(new Runnable() {
            @Override
            public void run()
            {
                try
                {
                    Bundle bnd = future.getResult();

                    final String authtoken = bnd.getString(AccountManager.KEY_AUTHTOKEN);
                    showMessage((authtoken != null) ? "SUCCESS!\ntoken: " + authtoken : "FAIL");
                    Log.d("udinic", "GetToken Bundle is " + bnd);
                }
                catch (Exception e)
                {
                    e.printStackTrace();
                    showMessage(e.getMessage());
                }
                }
            }).start();*/
        }

        private void invalidateAuthToken(Account account, String authTokenType)
        {
            /*final AccountManagerFuture<Bundle> future = mAccountManager.getAuthToken(account, authTokenType, null, this, null, null);

            new Thread(new Runnable() {
            @Override
            public void run()
            {
                try
                {
                    Bundle bnd = future.getResult();

                    final String authtoken = bnd.getString(AccountManager.KEY_AUTHTOKEN);
                    mAccountManager.invalidateAuthToken(account.type, authtoken);
                    showMessage(account.name + " invalidated");
                }
                catch (Exception e)
                {
                    e.printStackTrace();
                    showMessage(e.getMessage());
                }
            }
        }).start();*/
        }

        private void getTokenForAccountCreateIfNeeded(String accountType, String authTokenType)
        {
            /*final AccountManagerFuture<Bundle> future = mAccountManager.getAuthTokenByFeatures(accountType, authTokenType, null, this, null, null,
                    new AccountManagerCallback<Bundle>() {
                    @Override
                        public void run(AccountManagerFuture<Bundle> future)
            {
                Bundle bnd = null;
                try
                {
                    bnd = future.getResult();
                    final String authtoken = bnd.getString(AccountManager.KEY_AUTHTOKEN);
                    showMessage(((authtoken != null) ? "SUCCESS!\ntoken: " + authtoken : "FAIL"));
                    Log.d("udinic", "GetTokenForAccount Bundle is " + bnd);

                }
                catch (Exception e)
                {
                    e.printStackTrace();
                    showMessage(e.getMessage());
                }
            }
        }
        , null);*/
        }

        private void showMessage(String msg)
        {
            if (TextUtils.IsEmpty(msg))
                return;

            Toast.MakeText(Application.BaseContext, msg, ToastLength.Short).Show();
            /*runOnUiThread(new Runnable() {
            @Override
            public void run()
            {
                Toast.MakeText(Application.BaseContext, msg, ToastLength.Short).Show();
            }
        });*/
        }
    }
}

