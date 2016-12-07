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
    public class OverviewData
    {
        public string timeRange;
        public double totalExpenses;
        public double totalIncomes;
        public double totalSaving;

        public OverviewData() { }
        public void getMonthlyOverview(List<Transaction> items)
        {
            timeRange = "Monthly";
            totalExpenses = 0;
            totalIncomes = 0;
            for(int i = 0; i < items.Count; i++)
            {
                if (items[i].type == "Expense")
                    totalExpenses += (items[i].value * (30.0 / items[i].recurrenceDay));
                else
                    totalIncomes += (items[i].value * (30.0 / items[i].recurrenceDay));
            }
            totalSaving = ((totalIncomes - totalExpenses) < 0) ? 0 : totalIncomes - totalExpenses;
        }
        public void getWeeklyOverview(List<Transaction> items)
        {
            timeRange = "Weekly";
            totalExpenses = 0;
            totalIncomes = 0;
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].type == "Expense")
                    totalExpenses += (items[i].value * (7.0 / items[i].recurrenceDay));
                else
                    totalIncomes += (items[i].value * (7.0 / items[i].recurrenceDay));
            }
            totalSaving = ((totalIncomes - totalExpenses) < 0) ? 0 : totalIncomes - totalExpenses;
        }
        public void getDailyOverview(List<Transaction> items)
        {
            timeRange = "Daily";
            totalExpenses = 0;
            totalIncomes = 0;
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].type == "Expense")
                    totalExpenses += (items[i].value * (1.0 / items[i].recurrenceDay));
                else
                    totalIncomes += (items[i].value * (1.0 / items[i].recurrenceDay));
            }
            totalSaving = ((totalIncomes - totalExpenses) < 0) ? 0 : totalIncomes - totalExpenses;
        }
    }
}