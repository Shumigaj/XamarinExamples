using Android.Content.Res;
using Android.Graphics;
using Android.OS;
using Android.Preferences;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Views;
using Java.Lang;

namespace droid.examples.Preferences
{
    public abstract class BasePreferenceActivity : PreferenceActivity
    {
        private AppCompatDelegate mDelegate;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            getDelegate().InstallViewFactory();
            getDelegate().OnCreate(savedInstanceState);
            base.OnCreate(savedInstanceState);
        }

        protected override void OnPostCreate(Bundle savedInstanceState)
        {
            base.OnPostCreate(savedInstanceState);
            getDelegate().OnPostCreate(savedInstanceState);
        }

        public ActionBar GetSupportActionBar()
        {
            return getDelegate().SupportActionBar;
        }

        public void SetSupportActionBar(Toolbar toolbar)
        {
            getDelegate().SetSupportActionBar(toolbar);
        }

        public override MenuInflater MenuInflater
        {
            get { return getDelegate().MenuInflater; }

        }
        
        public override void SetContentView(int layoutResID)
        {
            getDelegate().SetContentView(layoutResID);
        }

        public override void SetContentView(View view)
        {
            getDelegate().SetContentView(view);
        }

        public override void SetContentView(View view, ViewGroup.LayoutParams @params)
        {
            getDelegate().SetContentView(view, @params);
        }

        public override void AddContentView(View view, ViewGroup.LayoutParams @params)
        {
            getDelegate().AddContentView(view, @params);
        }

        protected override void OnPostResume()
        {
            base.OnPostResume();
            getDelegate().OnPostResume();
        }

        protected override void OnTitleChanged(ICharSequence title, Color color)
        {
            base.OnTitleChanged(title, color);
            getDelegate().SetTitle(title);
        }

        public override void OnConfigurationChanged(Configuration newConfig)
        {
            base.OnConfigurationChanged(newConfig);
            getDelegate().OnConfigurationChanged(newConfig);
        }

        protected override void OnStop()
        {
            base.OnStop();
            getDelegate().OnStop();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            getDelegate().OnDestroy();
        }

        public void InvalidateOptionsMenu() => getDelegate().InvalidateOptionsMenu();

        private AppCompatDelegate getDelegate()
        {
            if (mDelegate == null)
            {
                mDelegate = AppCompatDelegate.Create(this, null);
            }
            return mDelegate;
        }
    }
}