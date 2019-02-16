using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Support.V7.Preferences;

namespace droid.examples.Preferences
{
    [Activity(Label = "Settings compat activity", Theme = "@style/AppTheme")]
    public class SettingsCompatActivity : AppCompatActivity
    {
        public static string KEY_PREF_EXAMPLE_SWITCH = "example_switch";

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SupportFragmentManager
               .BeginTransaction()
               .Replace(Android.Resource.Id.Content, new SettingsCompatFragment())
               .Commit();
        }
    }

    public class SettingsCompatFragment : PreferenceFragmentCompat
    {
        /// <summary>
        /// Called during onCreate(Bundle) to supply the preferences for this fragment. 
        /// This calls setPreferenceFromResource to get the preferences from the XML file.
        /// </summary>
        /// <param name="savedInstanceState">If the fragment is being re-created from a previous saved state, this is the state.</param>
        /// <param name="rootKey">If non-null, this preference fragment should be rooted with this key.</param>
        public override void OnCreatePreferences(Bundle savedInstanceState, string rootKey)
        {
            SetPreferencesFromResource(Resource.Xml.preferences_compat, rootKey);
        }
    }
}