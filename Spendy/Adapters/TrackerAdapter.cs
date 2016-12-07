using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.Support.V4.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Lang;

namespace Spendy
{
    public class TrackerAdapter : BaseAdapter<Expense>
    {
        public List<Expense> expenses;
        public FragmentActivity context;

        public TrackerAdapter(Fragment ctx, List<Expense> items)
        {
            expenses = items;
            context = ctx.Activity;
        }

        public override Expense this[int position]
        {
            get
            {
                return expenses[position];
            }
        }

        public override int Count
        {
            get
            {
                return expenses.Count;
            }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = expenses[position];
            var view = convertView;
            if(view == null)
                view = context.LayoutInflater.Inflate(Resource.Layout.ListTrackerItems, null);

            view.FindViewById<TextView>(Resource.Id.ExpenseNameText).Text = item.name;
            view.FindViewById<TextView>(Resource.Id.ExpenseDateText).Text = item.date.ToString();
            view.FindViewById<TextView>(Resource.Id.ExpenseTimePassageText).Text = item.elapsedTime;
            view.FindViewById<TextView>(Resource.Id.ExpenseAmountText).Text = item.value;

            return view;

        }
    }
}