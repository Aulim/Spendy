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

        private ValueAnimator slideAnimator(int start, int end, int header)
        {
            ValueAnimator animator = ValueAnimator.OfInt(start, end);
            animator.Update += (object sender, ValueAnimator.AnimatorUpdateEventArgs e) =>
            {
                var value = (int)animator.AnimatedValue;
                if (header == 0)
                {
                    ViewGroup.LayoutParams layoutParams = mLinearLayoutM.LayoutParameters;
                    layoutParams.Height = value;
                    mLinearLayoutM.LayoutParameters = layoutParams;
                }
                else if (header == 1)
                {
                    ViewGroup.LayoutParams layoutParams = mLinearLayoutW.LayoutParameters;
                    layoutParams.Height = value;
                    mLinearLayoutW.LayoutParameters = layoutParams;
                }
                else
                {
                    ViewGroup.LayoutParams layoutParams = mLinearLayoutD.LayoutParameters;
                    layoutParams.Height = value;
                    mLinearLayoutD.LayoutParameters = layoutParams;
                }
                
            };

            return animator;
        }

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
            mLinearLayoutM.Visibility = ViewStates.Gone;
            mLinearLayoutW = FindViewById<LinearLayout>(Resource.Id.expandableW);
            mLinearLayoutW.Visibility = ViewStates.Gone;
            mLinearLayoutD = FindViewById<LinearLayout>(Resource.Id.expandableD);
            mLinearLayoutD.Visibility = ViewStates.Gone;

            var mLinearHeaderM = FindViewById<LinearLayout>(Resource.Id.headerM);
            mLinearHeaderM.Click += MLinearHeaderM_Click;
            var mLinearHeaderW = FindViewById<LinearLayout>(Resource.Id.headerW);
            mLinearHeaderW.Click += MLinearHeaderW_Click;
            var mLinearHeaderD = FindViewById<LinearLayout>(Resource.Id.headerD);
            mLinearHeaderD.Click += MLinearHeaderD_Click;

            FindViewById<TextView>(Resource.Id.headerTextM).Text = data[0].timeRange;
            FindViewById<TextView>(Resource.Id.headerTextW).Text = data[1].timeRange;
            FindViewById<TextView>(Resource.Id.headerTextD).Text = data[2].timeRange;

            FindViewById<TextView>(Resource.Id.incomesTextM).Text = data[0].totalIncomes.ToString();
            FindViewById<TextView>(Resource.Id.incomesTextW).Text = data[1].totalIncomes.ToString();
            FindViewById<TextView>(Resource.Id.incomesTextD).Text = data[2].totalIncomes.ToString();

            FindViewById<TextView>(Resource.Id.expensesTextM).Text = data[0].totalExpenses.ToString();
            FindViewById<TextView>(Resource.Id.expensesTextW).Text = data[1].totalExpenses.ToString();
            FindViewById<TextView>(Resource.Id.expensesTextD).Text = data[2].totalExpenses.ToString();

            FindViewById<TextView>(Resource.Id.savingsTextM).Text = data[0].totalSaving.ToString();
            FindViewById<TextView>(Resource.Id.savingsTextW).Text = data[1].totalSaving.ToString();
            FindViewById<TextView>(Resource.Id.savingsTextD).Text = data[2].totalSaving.ToString();

        }

        private void MLinearHeaderM_Click(object sender, EventArgs e)
        {
            if (mLinearLayoutM.Visibility.Equals(ViewStates.Gone))
            {
                mLinearLayoutM.Visibility = ViewStates.Visible;
                int widthSpec = View.MeasureSpec.MakeMeasureSpec(0, MeasureSpecMode.Unspecified);
                int heightSpec = View.MeasureSpec.MakeMeasureSpec(0, MeasureSpecMode.Unspecified);
                mLinearLayoutM.Measure(widthSpec, heightSpec);

                ValueAnimator mAnimator = slideAnimator(0, mLinearLayoutM.MeasuredHeight, 0);
                mAnimator.Start();
            }
            else
            {
                int finalHeight = mLinearLayoutM.Height;

                ValueAnimator mAnimator = slideAnimator(finalHeight, 0, 0);
                mAnimator.AnimationEnd += (o, ev) =>
                {
                    mLinearLayoutM.Visibility = ViewStates.Gone;
                };
            }
        }

        private void MLinearHeaderW_Click(object sender, EventArgs e)
        {
            if (mLinearLayoutW.Visibility.Equals(ViewStates.Gone))
            {
                mLinearLayoutW.Visibility = ViewStates.Visible;
                int widthSpec = View.MeasureSpec.MakeMeasureSpec(0, MeasureSpecMode.Unspecified);
                int heightSpec = View.MeasureSpec.MakeMeasureSpec(0, MeasureSpecMode.Unspecified);
                mLinearLayoutW.Measure(widthSpec, heightSpec);

                ValueAnimator mAnimator = slideAnimator(0, mLinearLayoutW.MeasuredHeight, 1);
                mAnimator.Start();
            }
            else
            {
                int finalHeight = mLinearLayoutW.Height;

                ValueAnimator mAnimator = slideAnimator(finalHeight, 0, 0);
                mAnimator.AnimationEnd += (o, ev) =>
                {
                    mLinearLayoutW.Visibility = ViewStates.Gone;
                };
            }
        }

        private void MLinearHeaderD_Click(object sender, EventArgs e)
        {
            if (mLinearLayoutD.Visibility.Equals(ViewStates.Gone))
            {
                mLinearLayoutD.Visibility = ViewStates.Visible;
                int widthSpec = View.MeasureSpec.MakeMeasureSpec(0, MeasureSpecMode.Unspecified);
                int heightSpec = View.MeasureSpec.MakeMeasureSpec(0, MeasureSpecMode.Unspecified);
                mLinearLayoutD.Measure(widthSpec, heightSpec);

                ValueAnimator mAnimator = slideAnimator(0, mLinearLayoutD.MeasuredHeight, 2);
                mAnimator.Start();
            }
            else
            {
                int finalHeight = mLinearLayoutD.Height;

                ValueAnimator mAnimator = slideAnimator(finalHeight, 0, 0);
                mAnimator.AnimationEnd += (o, ev) =>
                {
                    mLinearLayoutD.Visibility = ViewStates.Gone;
                };
            }
        }
    }
}