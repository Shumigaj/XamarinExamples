using Android.App;
using Android.Content;
using Android.Gms.Auth.Api;
using Android.Gms.Auth.Api.SignIn;
using Android.Gms.Common;
using Android.Gms.Common.Apis;
using Android.Gms.Drive;
using Android.OS;

namespace droid.accounts
{
    public class GoogleManager : Java.Lang.Object, GoogleApiClient.IConnectionCallbacks, GoogleApiClient.IOnConnectionFailedListener
    {
        public static GoogleApiClient _googleApiClient { get; set; }
        public static GoogleManager Instance { get; private set; }//singleton
        
        public void Initialize(Context context)
        {
            GoogleSignInOptions gso = new GoogleSignInOptions.Builder(GoogleSignInOptions.DefaultSignIn)
                    .RequestEmail()
                    .RequestProfile()
                    .Build();

            //_googleApiClient = new GoogleApiClient.Builder(context)
            //    //.AddApi(Auth.GOOGLE_SIGN_IN_API, gso)
            //    .AddConnectionCallbacks(this)
            //    .AddOnConnectionFailedListener(this)
            //    .AddApi(DriveClass.API).AddScope(new Scope(Scopes.DriveFile))
            //    .Build();
            //
            

           //auth
           _googleApiClient = new GoogleApiClient.Builder(context)
                .AddConnectionCallbacks(this)
                .AddOnConnectionFailedListener(this)
                .AddApi(Auth.GOOGLE_SIGN_IN_API, gso)
                .Build();

            if (!_googleApiClient.IsConnected)
                _googleApiClient.Connect();  //logout: GoogleManager._googleApiClient.Disconnect();
        }

        public void Login(Activity activity)//Action<GoogleUser, string> onLoginComplete
        {
            //_onLoginComplete = onLoginComplete;
            Intent signInIntent = Auth.GoogleSignInApi.GetSignInIntent(_googleApiClient);
            /*((MainActivity)Forms.Context)*/activity.StartActivityForResult(signInIntent, 500);
            //_googleApiClient.Connect();
        }

        public void Drive(GoogleSignInAccount account)//Action<GoogleUser, string> onLoginComplete
        {
            //DriveResourceClient client = Drive.getDriveResourceClient(this, account);
            //client.getAppFolder().
        }

        public void OnConnected(Bundle connectionHint)
        {
            //DriveClass.DriveApi.NewDriveContents(_googleApiClient);
        }

        public void OnConnectionFailed(ConnectionResult result)
        {
            
        }

        public void OnConnectionSuspended(int cause)
        {
           
        }
    }
}