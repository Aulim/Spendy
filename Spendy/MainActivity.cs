using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.App;
using Android.Support.V4.View;
using Android.Views;
using Java.Lang;
using System;
using Fragment = Android.Support.V4.App.Fragment;

namespace Spendy
{
    [Activity(Label = "Spendy", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : FragmentActivity
    {
        Fragment[] fragments = new Fragment[]
        {
                new InputFragment(),
                new TrackerFragment()
        };

        ICharSequence[] titles = CharSequence.ArrayFromStringArray(new[]
        {
                "Planner",
                "Tracker"
        });

        TabFragmentAdapter tabAdapter;

        string tabOverview;

        public void setTabOverview(string v)
        {
            tabOverview = v;
        }

        public string getTabOveriew()
        {
            return tabOverview;
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.AppMenu, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.AboutMenu:
                    
                    return true;
            }
            return base.OnOptionsItemSelected(item);
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            
            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);

            tabAdapter = new TabFragmentAdapter(SupportFragmentManager, fragments, titles);

            var viewPager = FindViewById<ViewPager>(Resource.Id.viewpager);
            viewPager.Adapter = tabAdapter;
            viewPager.SetPadding(20, 20, 20, 20);

            var tabLayout = FindViewById<TabLayout>(Resource.Id.sliding_tabs);
            tabLayout.SetupWithViewPager(viewPager);
        }
    }
}

