using System;
using System.Threading.Tasks;
using Android;
using Android.Accounts;
using Android.App;
using Android.Content;
using Android.Gms.Auth;
using Android.Gms.Auth.Api;
using Android.Gms.Auth.Api.SignIn;
using Android.Gms.Common;
using Android.Gms.Drive;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Views;
using Java.Util.Concurrent;

namespace droid.accounts
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity, NavigationView.IOnNavigationItemSelectedListener, IAccountManagerCallback
    {
        private static GoogleManager GoogleManager;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            GoogleManager = new GoogleManager();
            GoogleManager.Initialize(this);

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

            if (id == Resource.Id.nav_camera)
            {
                GoogleManager.Login(this);
            }
            else if (id == Resource.Id.nav_gallery)
            {
            }
            else if (id == Resource.Id.nav_slideshow)
            {
                AccountManager mAccountManager = AccountManager.Get(this);
                Account[] accounts = mAccountManager.GetAccounts();

                // "wise" = Google Spreadheets
                //var amf = mAccountManager.GetAuthToken(accounts[0], "wise", null, this, this, null);//"wise"
                var t = mAccountManager.BlockingGetAuthToken(accounts[0], "wise", true);
                try
                {
                    //Task.Run(async () =>
                    //{
                    //    var result = await amf.GetResultAsync(5L, TimeUnit.Seconds);
                    //    var t = result;
                    //});
                    //var authToken = authTokenBundle.GetString(AccountManager.KEY_AUTHTOKEN);
                }
                catch (System.Exception e)
                {

                    
                }

            }
            else if (id == Resource.Id.nav_manage)
            {
            }
            else if (id == Resource.Id.nav_share)
            {
            }
            else if (id == Resource.Id.nav_send)
            {
                var accountTypes = new string[] { "com.google" };
                var intent = AccountPicker.NewChooseAccountIntent(null, null, null,/*accountTypes,*/ false, null, null, null, null);
                StartActivityForResult(intent, 1000);//REQUEST_CODE_PICK_ACCOUNT
            }

            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            drawer.CloseDrawer(GravityCompat.Start);
            return true;
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            if (requestCode == 1000 && resultCode == Result.Ok)
            {
                var accountName = data.GetStringExtra(AccountManager.KeyAccountName);
                Account accountDetails = new Account(accountName, GoogleAuthUtil.GoogleAccountType);

                //var accountId = GoogleAuthUtil.GetAccountId(this, accountName);
                var token = GoogleAuthUtil.GetToken(this, accountDetails, "oauth2:mail");//https://www.googleapis.com/auth/drive.file


            }

            if (requestCode == 500 && resultCode == Result.Ok)
            {
                GoogleSignInResult result = Auth.GoogleSignInApi.GetSignInResultFromIntent(data);
                GoogleManager.Drive(result.SignInAccount);
                
            }
        }

        public void Logout()
        {
            GoogleManager._googleApiClient.Disconnect();
        }

        public void OnResult(Java.Lang.Object result)
        {
            var contentResults = (result).JavaCast<IDriveApiDriveContentsResult>();
            if (!contentResults.Status.IsSuccess) // handle the error
                return;

        }

        public void Run(IAccountManagerFuture future)
        {
            var authTokenBundle = future.GetResult(5L, TimeUnit.Seconds).JavaCast<Bundle>();
            var authToken = authTokenBundle.GetString(AccountManager.KeyAuthtoken);

            //var authToken = future.GetString(AccountManager.KEY_AUTHTOKEN);
        }
    }
}

