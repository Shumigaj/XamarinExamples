using Android.App;
using Android.Content;
using Android.OS;
using Android.Preferences;
using Android.Runtime;
using System.Collections.Generic;

namespace droid.examples.Preferences
{
    [Activity(Label = "Settings with headers activity", Theme = "@style/AppTheme")]
    [IntentFilter(new string[] { "android.intent.action.APPLICATION_PREFERENCES" })]
    public class SettingsWithHeadersActivity : PreferenceActivity
    {
        public override void OnBuildHeaders(IList<Header> target)
        {
            base.OnBuildHeaders(target);
            LoadHeadersFromResource(Resource.Xml.preference_headers, target);
        }

        protected override bool IsValidFragment(string fragmentName)
        {
            return fragmentName == "droid.examples.preferences.SettingsWithHeadersActivity$SettingsForHeaderFragment";
        }

        [Register("droid.examples.preferences.SettingsWithHeadersActivity$SettingsForHeaderFragment")]
        public class SettingsForHeaderFragment : PreferenceFragment
        {
            public override void OnCreate(Bundle savedInstanceState)
            {
                base.OnCreate(savedInstanceState);
                AddPreferencesFromResource(Resource.Xml.app_preferences_for_header);
            }
        }
    }
}