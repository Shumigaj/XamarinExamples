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

namespace droid.accounts.Authenticator
{
    //[Service(Name = "com.xamarin.UdinicAuthServices")]
    [Service(Name = "com.shumigaj.accounts.YandexAuthenticatorService")]
    [IntentFilter(new string[] { "android.accounts.AccountAuthenticator" })]
    [MetaData("android.accounts.AccountAuthenticator", Resource = "@xml/authenticator")]
    public class UdinicAuthenticatorService : Service
    {
        private static UdinicAuthenticator _udinicAuthenticator = null;

        public override IBinder OnBind(Intent intent)
        {
            Console.WriteLine("UdinicAuthenticatorService: OnBind");

            IBinder ret = null;
            if (intent.Action == global::Android.Accounts.AccountManager.ActionAuthenticatorIntent)
                ret = GetAuthenticator().IBinder;
            return ret;
        }

        private UdinicAuthenticator GetAuthenticator()
        {
            Console.WriteLine("UdinicAuthenticatorService: GetAuthenticator");

            if (_udinicAuthenticator == null)
                _udinicAuthenticator = new UdinicAuthenticator(this);
            return _udinicAuthenticator;
        }
    }
}