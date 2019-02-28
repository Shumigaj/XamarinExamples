using System;

namespace droid.accounts.Authenticator
{
    public class ParseComServer : IServerAuthenticate
    {
        private static String APP_ID = "iRnc8I1X0du5q6HrJtZW0a5DlB0JcpOQbjA6chha";
        private static String REST_API_KEY = "tv1xCdYKTwI3p205KHCn1yWpbVj2OHldV9cPZuNZ";
               
        public User UserSignUp(String name, String email, String pass, String authType)
        {
            return new User
            {
                sessionToken = "sessionToken SignUp"
            };
        }

        public User UserSignIn(String user, String pass, String authType)
        {
            return new User
            {
                sessionToken = "sessionToken SignIn"
            };

        }
    }    
}