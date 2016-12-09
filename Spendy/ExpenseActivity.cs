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
        Expense addedItem;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            //ActionBar.SetHomeButtonEnabled(true);
            //ActionBar.SetDisplayHomeAsUpEnabled(true);

            // Create your application here
            SetContentView(Resource.Layout.Expense);

            Button addButton = FindViewById<Button>(Resource.Id.expenseAddButton);
            EditText nameText = FindViewById<EditText>(Resource.Id.expenseNameText);
            EditText valueText = FindViewById<EditText>(Resource.Id.expenseValueText);
            TextView dateText = FindViewById<TextView>(Resource.Id.expenseDatePicker);
            nameText.Text = "Food";
            valueText.Text = "15000";
            dateText.Text = DateTime.Now.ToString("d MMM yyyy");
            dateText.Click += (o, e) =>
            {
                DatePickerFragment frag = DatePickerFragment.NewInstance(delegate (DateTime time)
                {
                    dateText.Text = time.ToString("d MMM yyyy");
                });
                frag.Show(FragmentManager, DatePickerFragment.TAG);
            };
            addButton.Click += (o, e) =>
            {
                addedItem = new Expense(nameText.Text, DateTime.Parse(dateText.Text), int.Parse(valueText.Text));
                Intent intent = new Intent();
                intent.PutExtra("result", addedItem.ToString());
                SetResult(Result.Ok, intent);
                Finish();
            };

        }
    }
}