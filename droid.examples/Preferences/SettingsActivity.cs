
using Android.App;
using Android.Content;
using Android.OS;
using Android.Preferences;
using Android.Support.V7.Preferences;
using System.Collections.Generic;

namespace droid.examples.Preferences
{
    [Activity(Label = "Settings activity", Theme = "@style/AppTheme", Name = "droid.examples.Preferences.SettingsActivity")]
    [IntentFilter(new string[] { "android.intent.action.APPLICATION_PREFERENCES" })]
    public class SettingsActivity : PreferenceActivity
    {
        public override void OnBuildHeaders(IList<Header> target)
        {
            base.OnBuildHeaders(target);
            LoadHeadersFromResource(Resource.Xml.preference_headers, target);
        }

        public class SettingsFragment : PreferenceFragmentCompat
        {
            public override void OnCreatePreferences(Bundle savedInstanceState, string rootKey)
            {
                // Load the Preferences from the XML file
                //AddPreferencesFromResource(Resource.Xml.app_preferences);
                SetPreferencesFromResource(Resource.Xml.app_preferences, rootKey);
            }
        }
    }

}