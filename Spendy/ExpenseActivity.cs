using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Spendy
{
    [Activity(Label = "ExpenseActivity")]
    public class ExpenseActivity : Activity
    {

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            //ActionBar.SetHomeButtonEnabled(true);
            //ActionBar.SetDisplayHomeAsUpEnabled(true);

            // Create your application here
            SetContentView(Resource.Layout.Expense);

            TextView dateText = FindViewById<TextView>(Resource.Id.expenseDatePicker);
            dateText.Text = DateTime.Now.ToString("d MMM yyyy");
            dateText.Click += (o, e) =>
            {
                DatePickerFragment frag = DatePickerFragment.NewInstance(delegate (DateTime time)
                {
                    dateText.Text = time.ToString("d MMM yyyy");
                });
                frag.Show(FragmentManager, DatePickerFragment.TAG);
            };


        }
    }
}