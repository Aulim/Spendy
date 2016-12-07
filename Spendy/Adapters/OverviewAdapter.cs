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
    class OverviewAdapter : BaseAdapter<OverviewData>
    {
        List<OverviewData> data;
        Activity context;
        LinearLayout mLinearItems;

        public OverviewAdapter(Activity ctx, List<OverviewData> datas)
        {
            context = ctx;
            data = datas;
        }

        public override OverviewData this[int position]
        {
            get
            {
                return data[position];
            }
        }

        public override int Count
        {
            get
            {
                return data.Count;
            }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = data[position];
            var view = convertView;
            if (view == null)
                view = context.LayoutInflater.Inflate(Resource.Layout.ListGroup, null);

            mLinearItems = view.FindViewById<LinearLayout>(Resource.Id.expandable);
            mLinearItems.Visibility = ViewStates.Gone;
            var mLinearHeader = view.FindViewById<LinearLayout>(Resource.Id.header);
            mLinearHeader.Click += (s, e) =>
            {
                if (mLinearItems.Visibility.Equals(ViewStates.Gone))
                {
                    mLinearItems.Visibility = ViewStates.Visible;
                    int widthSpec = View.MeasureSpec.MakeMeasureSpec(0, MeasureSpecMode.Unspecified);
                    int heightSpec = View.MeasureSpec.MakeMeasureSpec(0, MeasureSpecMode.Unspecified);
                    mLinearItems.Measure(widthSpec, heightSpec);

                    ValueAnimator mAnimator = slideAnimator(0, mLinearItems.MeasuredHeight);
                    mAnimator.Start();
                }
                else
                {
                    int finalHeight = mLinearItems.Height;

                    ValueAnimator mAnimator = slideAnimator(finalHeight, 0);
                    mAnimator.AnimationEnd += (object o,EventArgs ev) =>
                    {
                        mLinearItems.Visibility = ViewStates.Gone;
                    };
                }
            };

            view.FindViewById<TextView>(Resource.Id.headerText).Text = item.timeRange;
            view.FindViewById<TextView>(Resource.Id.expensesText).Text = item.totalExpenses.ToString();
            view.FindViewById<TextView>(Resource.Id.incomesText).Text = item.totalExpenses.ToString();
            view.FindViewById<TextView>(Resource.Id.savingsText).Text = item.totalSaving.ToString();

            return view;
        }

        private ValueAnimator slideAnimator(int start, int end)
        {
            ValueAnimator animator = ValueAnimator.OfInt(start, end);
            animator.Update += (object sender, ValueAnimator.AnimatorUpdateEventArgs e) =>
            {
                var value = (int)animator.AnimatedValue;
                ViewGroup.LayoutParams layoutParams = mLinearItems.LayoutParameters;
                layoutParams.Height = value;
                mLinearItems.LayoutParameters = layoutParams;
            };

            return animator;
        }
    }
}