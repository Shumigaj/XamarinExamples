using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Support.V7.Preferences;
using Android.Views;
using Android.Widget;
using droid.examples.Preferences;
using System;

namespace droid.examples
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            fab.Click += FabOnClick;

            ReadPreferenceSetting();
        }

        private void ReadPreferenceSetting()
        {
            // Sets default values only once, first time this is called.
            // The third argument is a boolean that indicates whether the default values should be set more than once. 
            // When false, the system sets the default values only if this method has never been called in the past.
            PreferenceManager.SetDefaultValues(this, Resource.Xml.preferences_compat, false);

            PreferenceManager.SetDefaultValues(this, Resource.Xml.pref_group_accounts, false);
            PreferenceManager.SetDefaultValues(this, Resource.Xml.pref_group_general, false);
            PreferenceManager.SetDefaultValues(this, Resource.Xml.pref_group_notifications, false);

            // Read the settings from the shared preferences
            var sharedPref = PreferenceManager.GetDefaultSharedPreferences(this);
            var switchPref = sharedPref.GetBoolean(SettingsCompatActivity.KEY_PREF_EXAMPLE_SWITCH, false);
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            Type settingActivityType = null;

            switch (id)
            {
                case Resource.Id.action_settings_compat:
                    {
                        settingActivityType = typeof(SettingsCompatActivity);
                        break;
                    }
                case Resource.Id.action_grouped_settings_compat:
                    {
                        settingActivityType = typeof(GroupedSettingsCompatActivity);
                        break;
                    }
                case Resource.Id.action_settings_with_headers:
                    {
                        settingActivityType = typeof(SettingsWithHeadersActivity);
                        break;
                    }
                case Resource.Id.action_grouped_settings:
                    {
                        settingActivityType = typeof(GroupedSettingsActivity);
                        break;
                    }
            }

            if(settingActivityType != null)
            {
                var settingsIntent = new Intent(this, settingActivityType);
                StartActivity(settingsIntent);
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        private void FabOnClick(object sender, EventArgs eventArgs)
        {
            View view = (View) sender;
            Snackbar.Make(view, "Replace with your own action", Snackbar.LengthLong)
                .SetAction("Action", (Android.Views.View.IOnClickListener)null).Show();
        }
	}
}

