using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.Support.V4.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace Spendy.Fragments
{
    public class ExpenseDialogEventArgs : EventArgs
    {
        public Expense payload { get; set; }
    }

    public delegate void ExpenseDialogEventHandler(object sender, ExpenseDialogEventArgs args);

    public class ExpenseDialogFragment : DialogFragment
    {
        public event ExpenseDialogEventHandler Dismissed;

        public static ExpenseDialogFragment newInstance(Bundle bundle)
        {
            ExpenseDialogFragment fragment = new ExpenseDialogFragment();
            fragment.SetStyle(DialogFragment.StyleNormal, Resource.Style.MY_DIALOG);
            fragment.Arguments = bundle;
            return fragment;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

            View view = inflater.Inflate(Resource.Layout.ExpenseDialogFragment, container, false);

            return view;
        }
    }
}