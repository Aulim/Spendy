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
    public class InputAdapter : BaseAdapter<Transaction>
    {
        public List<Transaction> transaction;
        public FragmentActivity context;

        private string recurrenceString(int recurrenceDay)
        {
            return "Every " + recurrenceDay.ToString() + " day" + (recurrenceDay > 1 ? "s" : "");
        }

        public InputAdapter(Fragment ctx, List<Transaction> trans)
        {
            context = ctx.Activity;
            transaction = trans;
        }

        public override Transaction this[int position]
        {
            get
            {
                return transaction[position];
            }
        }

        public override int Count
        {
            get
            {
                return transaction.Count;
            }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = transaction[position];
            var view = convertView;
            if (view == null)
                view = context.LayoutInflater.Inflate(Resource.Layout.ListItems, null);
            view.FindViewById<TextView>(Resource.Id.transactionNameText).Text = item.name;
            view.FindViewById<TextView>(Resource.Id.transactionTypeText).Text = item.type;
            view.FindViewById<TextView>(Resource.Id.transactionValueText).Text = string.Format("{0:n0}", item.value);
            view.FindViewById<TextView>(Resource.Id.transactionRecurrenceText).Text = recurrenceString(item.recurrenceDay);

            return view;
        }
    }
}