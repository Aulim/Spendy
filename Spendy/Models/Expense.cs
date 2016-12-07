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
        public string value;

        public Expense()
        {
            elapsedTime = "Today";
        }

        public Expense(string n, DateTime d, string v)
        {
            name = n;
            date = d;
            elapsedTime = "Today";
            value = v;
        }

        public string addCabinetCommas(string value)
        {
            return value;
        }
    }

}