using System;

namespace droid.accounts.Authenticator
{
    public interface IServerAuthenticate
    {
        User UserSignUp(String name, String email, String pass, String authType);

        User UserSignIn(String user, String pass, String authType);
    }
}