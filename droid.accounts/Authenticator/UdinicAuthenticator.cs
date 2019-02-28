using Android.Accounts;
using Android.Content;
using Android.OS;
using Android.Text;
using System;

namespace droid.accounts.Authenticator
{
    public class UdinicAuthenticator : AbstractAccountAuthenticator
    {
        private readonly Context mContext;
        public UdinicAuthenticator(Context context)
            : base(context)
        {
            Console.WriteLine("UdinicAuthenticator: ctor with context");
            mContext = context;
        }

        public override Bundle AddAccount(AccountAuthenticatorResponse response, string accountType, string authTokenType, string[] requiredFeatures, Bundle options)
        {
            Console.WriteLine("UdinicAuthenticator: AddAccount");

            Intent intent = new Intent(mContext, typeof(AuthenticatorActivity));
            intent.PutExtra(AuthenticatorActivity.ARG_ACCOUNT_TYPE, accountType);
            intent.PutExtra(AuthenticatorActivity.ARG_AUTH_TYPE, authTokenType);
            intent.PutExtra(AuthenticatorActivity.ARG_IS_ADDING_NEW_ACCOUNT, true);
            intent.PutExtra(AccountManager.KeyAccountAuthenticatorResponse, response);

            Bundle bundle = new Bundle();
            bundle.PutParcelable(AccountManager.KeyIntent, intent);
            return bundle;
        }

        public override Bundle GetAuthToken(AccountAuthenticatorResponse response, Account account, string authTokenType, Bundle options)
        {
            Console.WriteLine("UdinicAuthenticator: GetAuthToken");

            // If the caller requested an authToken type we don't support, then
            // return an error
            if (!authTokenType.Equals(AccountGeneral.AUTHTOKEN_TYPE_READ_ONLY) && !authTokenType.Equals(AccountGeneral.AUTHTOKEN_TYPE_FULL_ACCESS))
            {
                Bundle result = new Bundle();
                result.PutString(AccountManager.KeyErrorMessage, "invalid authTokenType");
                return result;
            }

            // Extract the username and password from the Account Manager, and ask
            // the server for an appropriate AuthToken.
            AccountManager am = AccountManager.Get(mContext);

            String authToken = am.PeekAuthToken(account, authTokenType);
            String userId = null; //User identifier, needed for creating ACL on our server-side

            //Log.d("udinic", TAG + "> peekAuthToken returned - " + authToken);

            // Lets give another try to authenticate the user
            if (TextUtils.IsEmpty(authToken))
            {
                String password = am.GetPassword(account);
                if (password != null)
                {
                    try
                    {
                        //Log.d("udinic", TAG + "> re-authenticating with the existing password");
                        User user = AccountGeneral.sServerAuthenticate.UserSignIn(account.Name, password, authTokenType);
                        if (user != null)
                        {
                            authToken = user.getSessionToken();
                            userId = user.getObjectId();
                        }
                    }
                    catch (Exception e)
                    {
                        //e.printStackTrace();
                    }
                }
            }

            // If we get an authToken - we return it
            if (!TextUtils.IsEmpty(authToken))
            {
                Bundle result = new Bundle();
                result.PutString(AccountManager.KeyAccountName, account.Name);
                result.PutString(AccountManager.KeyAccountType, account.Type);
                result.PutString(AccountManager.KeyAuthtoken, authToken);
                return result;
            }

            // If we get here, then we couldn't access the user's password - so we
            // need to re-prompt them for their credentials. We do that by creating
            // an intent to display our AuthenticatorActivity.
            Intent intent = new Intent(mContext, typeof(AuthenticatorActivity));
            intent.PutExtra(AccountManager.KeyAccountAuthenticatorResponse, response);
            intent.PutExtra(AuthenticatorActivity.ARG_ACCOUNT_TYPE, account.Type);
            intent.PutExtra(AuthenticatorActivity.ARG_AUTH_TYPE, authTokenType);
            Bundle bundle = new Bundle();
            bundle.PutParcelable(AccountManager.KeyIntent, intent);
            return bundle;
        }

        public override string GetAuthTokenLabel(string authTokenType)
        {
            Console.WriteLine("UdinicAuthenticator: GetAuthTokenLabel");

            if (AccountGeneral.AUTHTOKEN_TYPE_FULL_ACCESS.Equals(authTokenType))
            {
                return AccountGeneral.AUTHTOKEN_TYPE_FULL_ACCESS_LABEL;
            }
            else if (AccountGeneral.AUTHTOKEN_TYPE_READ_ONLY.Equals(authTokenType))
            {
                return AccountGeneral.AUTHTOKEN_TYPE_READ_ONLY_LABEL;
            }
            else
            {
                return authTokenType + " (Label)";
            }
        }

        public override Bundle HasFeatures(AccountAuthenticatorResponse response, Account account, string[] features)
        {
            Console.WriteLine("UdinicAuthenticator: HasFeatures");

            Bundle result = new Bundle();
            result.PutBoolean(AccountManager.KeyBooleanResult, false);
            return result;
        }
        
        public override Bundle ConfirmCredentials(AccountAuthenticatorResponse response, Account account, Bundle options)
        {
            Console.WriteLine("UdinicAuthenticator: ConfirmCredentials");
            return null;
        }

        public override Bundle EditProperties(AccountAuthenticatorResponse response, string accountType)
        {
            Console.WriteLine("UdinicAuthenticator: EditProperties");
            return null;
        }

        public override Bundle UpdateCredentials(AccountAuthenticatorResponse response, Account account, string authTokenType, Bundle options)
        {
            Console.WriteLine("UdinicAuthenticator: UpdateCredentials");
            return null;
        }
    }
}