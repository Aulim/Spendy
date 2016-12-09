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
    public class Expense
    {
        public string name { get; set; }
        public DateTime date { get; set; }
        public string elapsedTime;
        public int value;

        public Expense()
        {
            elapsedTime = "Today";
        }

        public Expense(string name, DateTime date, int value)
        {
            DateTime today = DateTime.Today;
            int difference = (today - date).Days;
            this.name = name;
            this.date = date;
            elapsedTime = (difference > 0)? (difference > 1)? string.Format("{0} days ago", difference) : "Yesterday" : "Today";
            this.value = value;
        }

        public Expense(string expenseString)
        {
            string[] param = expenseString.Split(',');
            name = param[0];
            date = DateTime.Parse(param[1]);
            elapsedTime = param[2];
            value = int.Parse(param[3]);
        }

        public override string ToString()
        {
            return string.Format("{0},{1},{2},{3}", name, date.ToString(), elapsedTime, value);
        }
    }

}