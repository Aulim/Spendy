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
    public class Transaction
    {
        public string name;
        public int value;
        public string type;
        public int recurrenceDay;

        public Transaction(string name, int value, string type, int recurrenceDay)
        {
            this.name = name;
            this.value = value;
            this.type = type;
            this.recurrenceDay = recurrenceDay;
        }

        public override string ToString()
        {
            return name + "," + value.ToString() + "," + type + "," + recurrenceDay.ToString();
        }
    }
}