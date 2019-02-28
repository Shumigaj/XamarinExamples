using Android.Annotation;
using Android.App;
using Android.Content;
using Android.Media;
using Android.OS;
using Android.Preferences;
using Android.Runtime;
using Android.Text;
using Android.Views;
using System.Collections.Generic;

namespace droid.examples.Preferences
{
    [Activity(Label = "Grouped settings", Theme = "@style/AppTheme")]
    [IntentFilter(new string[] { "android.intent.action.APPLICATION_PREFERENCES" })]
    public class GroupedSettingsActivity : BasePreferenceActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetupActionBar();
            // Create your application here
        }

        // Set up the android.app.ActionBar, if the API is available.
        private void SetupActionBar()
        {
            var actionBar = GetSupportActionBar();
            if (actionBar != null)
            {
                // Show the Up button in the action bar.
                actionBar.SetDisplayHomeAsUpEnabled(true);
            }
        }

        [TargetApi(Value = 11)] // BuildVersionCodes.Honeycomb || Build.VERSION_CODES.
        public override void OnBuildHeaders(IList<Header> target)
        {
            LoadHeadersFromResource(Resource.Xml.pref_group_headers, target);
        }

        //This method stops fragment injection in malicious applications.
        //Make sure to deny any unknown fragments here./
        protected override bool IsValidFragment(string fragmentName)
        {
            return typeof(GeneralPreferenceFragment).FullName.Replace('+', '$').Equals(fragmentName)
                || typeof(NotificationsPreferenceFragment).FullName.Replace('+', '$').Equals(fragmentName)
                || typeof(AccountsPreferenceFragment).FullName.Replace('+', '$').Equals(fragmentName);
        }

        public static bool OnPreferenceChange(Preference preference, object newValue)
        {
            var stringValue = newValue.ToString();

            if (preference is ListPreference)
            {
                // For list preferences, look up the correct display value in
                // the preference's 'entries' list.
                ListPreference listPreference = (ListPreference)preference;
                int index = listPreference.FindIndexOfValue(stringValue);

                // Set the summary to reflect the new value.
                preference.Summary = index >= 0 ? listPreference.GetEntries()[index] : null;

            }
            else if (preference is RingtonePreference)
            {
                // For ringtone preferences, look up the correct display value
                // using RingtoneManager.
                if (TextUtils.IsEmpty(stringValue))
                {
                    // Empty values correspond to 'silent' (no ringtone).
                    preference.Summary = Application.Context.Resources.GetString(Resource.String.pref_group_ringtone_silent);
                }
                else
                {
                    Ringtone ringtone = RingtoneManager.GetRingtone(preference.Context, Android.Net.Uri.Parse(stringValue));

                    if (ringtone == null)
                    {
                        // Clear the summary if there was a lookup error.
                        preference.Summary = null;
                    }
                    else
                    {
                        // Set the summary to reflect the new ringtone display
                        // name.
                        preference.Summary = ringtone.GetTitle(preference.Context);
                    }
                }

            }
            else
            {
                // For all other preferences, set the summary to the value's
                // simple string representation.
                preference.Summary = stringValue;
            }
            return true;
        }

        // Determines if the device has an extra-large screen. For example, 10" tablets are extra-large.
        /*private static bool IsXLargeTablet(Context context)
        {
            return (context.Resources.Configuration.ScreenLayout
                    & Configuration.SCREENLAYOUT_SIZE_MASK)
                    >= Configuration.SCREENLAYOUT_SIZE_XLARGE;
        }*/

        private static void BindPreferenceSummaryToValue(Preference preference)
        {
            // Set the listener to watch for value changes.
            preference.PreferenceChange += (object sender, Preference.PreferenceChangeEventArgs e) =>
            {
                // Trigger the listener immediately with the preference's current value.
                var newValue = PreferenceManager.GetDefaultSharedPreferences(preference.Context).GetString(preference.Key, "");
                OnPreferenceChange(preference, newValue);
            };
        }

        [TargetApi(Value = 11)]
        [Register("droid.examples.Preferences.GroupedSettingsActivity$GeneralPreferenceFragment")]
        public class GeneralPreferenceFragment : PreferenceFragment
        {
            public override void OnCreate(Bundle savedInstanceState)
            {
                base.OnCreate(savedInstanceState);
                AddPreferencesFromResource(Resource.Xml.pref_group_general);
                SetHasOptionsMenu(true);

                // Bind the summaries of EditText/List/Dialog/Ringtone preferences to their values.
                // When their values change, their summaries are updated to reflect the new value, per the Android Design guidelines.
                BindPreferenceSummaryToValue(FindPreference("example_text"));
                BindPreferenceSummaryToValue(FindPreference("example_list"));
            }

            // use android.support.v7 then it will be called
            public override bool OnOptionsItemSelected(IMenuItem item)
            {
                int id = item.ItemId;
                if (id == Android.Resource.Id.Home)
                {
                    StartActivity(new Intent(Activity, typeof(GroupedSettingsActivity)));
                    return true;
                }
                return base.OnOptionsItemSelected(item);
            }
        }

        [TargetApi(Value = 11)]
        [Register("droid.examples.Preferences.GroupedSettingsActivity$NotificationsPreferenceFragment")]
        public class NotificationsPreferenceFragment : PreferenceFragment
        {
            public override void OnCreate(Bundle savedInstanceState)
            {
                base.OnCreate(savedInstanceState);
                AddPreferencesFromResource(Resource.Xml.pref_group_notifications);
                SetHasOptionsMenu(true);

                // Bind the summaries of EditText/List/Dialog/Ringtone preferences
                // to their values. When their values change, their summaries are
                // updated to reflect the new value, per the Android Design
                // guidelines.
                BindPreferenceSummaryToValue(FindPreference("notifications_new_message_ringtone"));
            }

            public override bool OnOptionsItemSelected(IMenuItem item)
            {
                int id = item.ItemId;
                if (id == Android.Resource.Id.Home)
                {
                    StartActivity(new Intent(Activity, typeof(GroupedSettingsActivity)));
                    return true;
                }
                return base.OnOptionsItemSelected(item);
            }
        }

        [TargetApi(Value = 11)]
        [Register("droid.examples.Preferences.GroupedSettingsActivity$AccountsPreferenceFragment")]
        public class AccountsPreferenceFragment : PreferenceFragment
        {
            public override void OnCreate(Bundle savedInstanceState)
            {
                base.OnCreate(savedInstanceState);
                AddPreferencesFromResource(Resource.Xml.pref_group_accounts);
                SetHasOptionsMenu(true);

                // Bind the summaries of EditText/List/Dialog/Ringtone preferences
                // to their values. When their values change, their summaries are
                // updated to reflect the new value, per the Android Design
                // guidelines.
                BindPreferenceSummaryToValue(FindPreference("sync_frequency"));
            }

            public override bool OnOptionsItemSelected(IMenuItem item)
            {
                int id = item.ItemId;
                if (id == Android.Resource.Id.Home)
                {
                    StartActivity(new Intent(Activity, typeof(GroupedSettingsActivity)));
                    return true;
                }
                return base.OnOptionsItemSelected(item);
            }
        }
    }
}