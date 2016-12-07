using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.Support.V4.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;

namespace Spendy
{
    public class TrackerFragment : Fragment
    {
        List<Expense> expenses = new List<Expense>();
        TrackerAdapter adapter;


        private void addItem(Expense item)
        {
            expenses.Add(item);
            adapter.NotifyDataSetChanged();
        }

        private void deleteItem(int pos)
        {
            expenses.RemoveAt(pos);
            adapter.NotifyDataSetChanged();
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

            return inflater.Inflate(Resource.Layout.TrackerFragment, container, false);
        }

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
            
            var layout = View.FindViewById<LinearLayout>(Resource.Id.linearLayoutTracker);
            var addingText = View.FindViewById<Button>(Resource.Id.AddExpenseButton);
            addingText.SetBackgroundColor(Android.Graphics.Color.Rgb(129, 218, 247));
            addingText.SetTextAppearance(Android.Resource.Style.TextAppearanceDeviceDefaultWidgetButton);
            addingText.Alpha = 100;
        }
    }
}