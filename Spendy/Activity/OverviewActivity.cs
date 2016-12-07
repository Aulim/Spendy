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
    [Activity(Label = "OverviewActivity")]
    public class OverviewActivity : Activity
    {
        List<Transaction> transaction;
        List<string> items;

        private Transaction StringToTransaction(string input)
        {
            string[] param = input.Split(',');
            return new Transaction(param[0], int.Parse(param[1]), param[2], int.Parse(param[3]));
        }

        private void StringToData()
        {
            for(int i = 0; i < items.Count; i++)
            {
                transaction.Add(StringToTransaction(items[i]));
            }
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Overview);
            items = (List<string>)Intent.GetStringArrayListExtra("data");
            StringToData();

            LinearLayout view = FindViewById<LinearLayout>(Resource.Id.OverviewLayout);
            TextView title = FindViewById<TextView>(Resource.Id.PageTitleText);
            title.Text = "Overview";


        }
    }
}