using System;

namespace droid.accounts.Authenticator
{
    public class AccountGeneral
    {
        //Account type id8
        public static String ACCOUNT_TYPE = "com.shumigaj.accounts.yandex";

        //Account name
        public static String ACCOUNT_NAME = "Udinic";

        //User data fields
        public static String USERDATA_USER_OBJ_ID = "userObjectId";   //Parse.com object id

        //Auth token types
        public static String AUTHTOKEN_TYPE_READ_ONLY = "Read only";
        public static String AUTHTOKEN_TYPE_READ_ONLY_LABEL = "Read only access to an Udinic account";

        public static String AUTHTOKEN_TYPE_FULL_ACCESS = "Full access";
        public static String AUTHTOKEN_TYPE_FULL_ACCESS_LABEL = "Full access to an Udinic account";

        public static IServerAuthenticate sServerAuthenticate = new ParseComServer();
    }
}