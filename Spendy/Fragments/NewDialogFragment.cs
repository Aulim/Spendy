using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.Support.V4.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using Android.Support.Design.Widget;

namespace Spendy
{
    public class DialogEventArgs : EventArgs
    {
        public Transaction payload { get; set; }
    }

    public delegate void DialogEventHandler(object sender, DialogEventArgs args);

    public class NewDialogFragment : DialogFragment
    {
        string name;
        string type;
        string value;
        string recDay;
        string[] oldData;
        Transaction transaction;
        List<string> types = new List<string>()
        {
            "Expense",
            "Income"
        };

        private bool isEmpty(string text)
        {
            if (text.Length == 0) return true;
            return false;
        }

        public event DialogEventHandler Dismissed;

        public static NewDialogFragment NewInstance(Bundle bundle)
        {
            NewDialogFragment fragment = new NewDialogFragment();
            fragment.SetStyle(DialogFragment.StyleNormal, Resource.Style.MY_DIALOG);
            fragment.Arguments = bundle;
            return fragment;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            View view = inflater.Inflate(Resource.Layout.DialogFragment, container, false);
            LinearLayout btnContainer = view.FindViewById<LinearLayout>(Resource.Id.ButtonContainer);
            Button cancelButton = btnContainer.FindViewById<Button>(Resource.Id.CancelButton);
            Button addButton = btnContainer.FindViewById<Button>(Resource.Id.AddTransactionButton);
            Spinner transactionTypeSpinner = view.FindViewById<Spinner>(Resource.Id.AddedTransactionType);
            ArrayAdapter<string> spinnerAdapter = new ArrayAdapter<string>(Activity, Android.Resource.Layout.SimpleSpinnerItem, types);
            spinnerAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            transactionTypeSpinner.Adapter = spinnerAdapter;

            if(Arguments != null)
            {
                oldData = Arguments.GetStringArray("editData");
                view.FindViewById<EditText>(Resource.Id.AddedTransactionName).Text = oldData[0];
                transactionTypeSpinner.SetSelection((oldData[1] == "Expense") ? 0 : 1);
                view.FindViewById<EditText>(Resource.Id.AddedTransactionAmount).Text = oldData[2];
                view.FindViewById<EditText>(Resource.Id.AddedTransactionRecurrence).Text = oldData[3];
                addButton.Text = "Edit";
            }

            transactionTypeSpinner.ItemSelected += (sender, e) =>
            {
                Spinner spinner = (Spinner)sender;
                type = string.Format(spinner.GetItemAtPosition(e.Position).ToString());
            };

            addButton.Click += delegate
            {
                name = view.FindViewById<EditText>(Resource.Id.AddedTransactionName).Text;
                value = view.FindViewById<EditText>(Resource.Id.AddedTransactionAmount).Text;
                recDay = view.FindViewById<EditText>(Resource.Id.AddedTransactionRecurrence).Text;
                if (isEmpty(name) || isEmpty(value) || isEmpty(recDay))
                    Toast.MakeText(this.Activity, "Activity name, value, and recurrence day cannot be empty", ToastLength.Long).Show();
                else
                {
                    transaction = new Transaction(name, int.Parse(value), type, int.Parse(recDay));
                    if (null != Dismissed)
                    {
                        Dismissed(this, new DialogEventArgs { payload = transaction });
                        Dismiss();
                    }
                    Toast.MakeText(this.Activity, string.Format("Transaction {0} {1}ed", name, addButton.Text), ToastLength.Long).Show();
                }
            };

            cancelButton.Click += delegate
            {
                Dismiss();
                Toast.MakeText(this.Activity, "Cancel adding transaction", ToastLength.Long);
            };
           

            return view;
        }
    }
}