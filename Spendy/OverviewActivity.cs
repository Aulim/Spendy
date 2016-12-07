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
using Android.Animation;

namespace Spendy
{
    [Activity(Label = "OverviewActivity")]
    public class OverviewActivity : Activity
    {
        List<Transaction> transaction = new List<Transaction>();
        IList<string> items;
        List<OverviewData> data = new List<OverviewData>();
        //OverviewAdapter adapter;

        LinearLayout mLinearLayoutM;
        LinearLayout mLinearLayoutW;
        LinearLayout mLinearLayoutD;

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
            items = Intent.Extras.GetStringArrayList("data");
            StringToData();
            OverviewData monthlyData = new OverviewData();
            OverviewData weeklyData = new OverviewData();
            OverviewData dailyData = new OverviewData();
            monthlyData.getMonthlyOverview(transaction);
            data.Add(monthlyData);
            weeklyData.getWeeklyOverview(transaction);
            data.Add(weeklyData);
            dailyData.getDailyOverview(transaction);
            data.Add(dailyData);
            

            
            TextView title = FindViewById<TextView>(Resource.Id.PageTitleText);
            title.Text = "Overview";

            mLinearLayoutM = FindViewById<LinearLayout>(Resource.Id.expandableM);
            mLinearLayoutW = FindViewById<LinearLayout>(Resource.Id.expandableW);
            mLinearLayoutD = FindViewById<LinearLayout>(Resource.Id.expandableD);

            var mLinearHeaderM = FindViewById<LinearLayout>(Resource.Id.headerM);
            var mLinearHeaderW = FindViewById<LinearLayout>(Resource.Id.headerW);
            var mLinearHeaderD = FindViewById<LinearLayout>(Resource.Id.headerD);

            FindViewById<TextView>(Resource.Id.headerTextM).Text = data[0].timeRange;
            FindViewById<TextView>(Resource.Id.headerTextW).Text = data[1].timeRange;
            FindViewById<TextView>(Resource.Id.headerTextD).Text = data[2].timeRange;

            FindViewById<TextView>(Resource.Id.incomesTextM).Text = "Total incomes for 30 days:\t " + data[0].totalIncomes.ToString();
            FindViewById<TextView>(Resource.Id.incomesTextW).Text = "Total incomes for 7 days:\t " + data[1].totalIncomes.ToString();
            FindViewById<TextView>(Resource.Id.incomesTextD).Text = "Total incomes for 1 day:\t " + data[2].totalIncomes.ToString();

            FindViewById<TextView>(Resource.Id.expensesTextM).Text = "Total expenses for 30 days:\t " + data[0].totalExpenses.ToString();
            FindViewById<TextView>(Resource.Id.expensesTextW).Text = "Total expenses for 7 days:\t " + data[1].totalExpenses.ToString();
            FindViewById<TextView>(Resource.Id.expensesTextD).Text = "Total expenses for 1 day:\t " + data[2].totalExpenses.ToString();

            FindViewById<TextView>(Resource.Id.savingsTextM).Text = "Savings:\t " + data[0].totalSaving.ToString();
            FindViewById<TextView>(Resource.Id.savingsTextW).Text = "Savings:\t " + data[1].totalSaving.ToString();
            FindViewById<TextView>(Resource.Id.savingsTextD).Text = "Savings:\t " + data[2].totalSaving.ToString();
        }
    }
}