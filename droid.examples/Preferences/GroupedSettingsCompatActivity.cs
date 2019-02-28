using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Support.V7.Preferences;
using Android.Views;
using Android.Widget;

namespace droid.examples.Preferences
{
    [Activity(Label = "Grouped compat activity", Theme = "@style/AppTheme")]
    public class GroupedSettingsCompatActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            //SetContentView(Resource.Layout.activity_settings);

            // android.R.id.content is probably for old style activity
            var fragmentTransaction = SupportFragmentManager.BeginTransaction();
            //add a Fragment to an existing container
            //fragmentTransaction.Add(Resource.Id.account_settings_compat, new AccountSettingsCompatFragment());
            fragmentTransaction.Replace(Android.Resource.Id.Content, new GeneralSettingsCompatFragment());
            fragmentTransaction.Commit();
        }
    }

    [Register("droid.examples.Preferences.GeneralSettingsCompatFragment")]
    public class GeneralSettingsCompatFragment : PreferenceFragmentCompat
    {
        /// <summary>
        /// Called during onCreate(Bundle) to supply the preferences for this fragment. 
        /// This calls setPreferenceFromResource to get the preferences from the XML file.
        /// </summary>
        /// <param name="savedInstanceState">If the fragment is being re-created from a previous saved state, this is the state.</param>
        /// <param name="rootKey">If non-null, this preference fragment should be rooted with this key.</param>
        public override void OnCreatePreferences(Bundle savedInstanceState, string rootKey)
        {
            AddPreferencesFromResource(Resource.Xml.pref_group_compat_general);
        }

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
            SetDivider(null);
        }

        public override bool OnPreferenceTreeClick(Preference preference)
        {
            return base.OnPreferenceTreeClick(preference);
        }
    }

    [Register("droid.examples.Preferences.AccountSettingsCompatFragment")]
    public class AccountSettingsCompatFragment : PreferenceFragmentCompat
    {
        public override void OnCreatePreferences(Bundle savedInstanceState, string rootKey)
        {
            SetPreferencesFromResource(Resource.Xml.pref_group_compat_account, rootKey);
        }
    }
}