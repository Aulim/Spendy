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
using Android.Support.V7.App;

namespace Spendy
{
    public class InputFragment : Fragment
    {
        List<Transaction> transaction = new List<Transaction>();
        FloatingActionButton fab;
        ListView listView;
        InputAdapter adapter;
        Button overviewButton;
        TextView overviewPlaceholder;

        private List<string> DataToString()
        {
            List<string> result = new List<string>();
            for (int i = 0; i < transaction.Count; i++)
                result.Add(transaction.ToString());
            return result;
        }

        private void addItem(Transaction trans)
        {
            transaction.Add(trans);
            adapter.NotifyDataSetChanged();
            if(transaction.Count > 0)
            {
                overviewPlaceholder.Visibility = ViewStates.Gone;
                overviewButton.Visibility = ViewStates.Visible;
            }
            else
            {
                overviewButton.Visibility = ViewStates.Gone;
                overviewPlaceholder.Visibility = ViewStates.Visible;
            }
        }

        private void removeItem(int position)
        {
            string transName = transaction[position].name;
            AlertDialog.Builder alert = new AlertDialog.Builder(Activity);
            alert.SetTitle("Confirm delete");
            alert.SetMessage(string.Format("Are you sure you want to delete transaction {0}?", transName));
            alert.SetPositiveButton("Delete", (o, e) =>
            {
                transaction.RemoveAt(position);
                adapter.NotifyDataSetChanged();
                if (transaction.Count > 0)
                {
                    overviewPlaceholder.Visibility = ViewStates.Gone;
                    overviewButton.Visibility = ViewStates.Visible;
                }
                else
                {
                    overviewButton.Visibility = ViewStates.Gone;
                    overviewPlaceholder.Visibility = ViewStates.Visible;
                }
                Toast.MakeText(Activity, string.Format("Transaction {0} deleted", transName), ToastLength.Long).Show();
            });
            alert.SetNegativeButton("Cancel", (o, e) =>
            {

            });

            AlertDialog dialog = alert.Create();
            dialog.Show();
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

            if(savedInstanceState != null)
            {
                transaction = (List<Transaction>)savedInstanceState.GetParcelableArrayList("transactions");
            }

            return inflater.Inflate(Resource.Layout.InputFragment, container, false);
        }

        public override void OnSaveInstanceState(Bundle outState)
        {
            base.OnSaveInstanceState(outState);
        }

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);

            listView = View.FindViewById<ListView>(Resource.Id.listViewInput);
            fab = View.FindViewById<FloatingActionButton>(Resource.Id.fab);
            fab.SetBackgroundColor(Android.Graphics.Color.Rgb(129, 218, 247));
            overviewButton = View.FindViewById<Button>(Resource.Id.OverviewButton);
            overviewButton.Visibility = ViewStates.Gone;
            overviewButton.Click += (o, e) =>
            {
                Intent intent = new Intent(Activity, typeof(OverviewActivity));
                var bundle = DataToString();
                intent.PutStringArrayListExtra("data", bundle);
                StartActivity(intent);
            };
            overviewPlaceholder = View.FindViewById<TextView>(Resource.Id.txtPlaceholderOverview);
            adapter = new InputAdapter(this, transaction);
            listView.Adapter = adapter;
            RegisterForContextMenu(listView);
            fab.Click += (sender, e) =>
            {
                FragmentTransaction ft = FragmentManager.BeginTransaction();
                Fragment prev = FragmentManager.FindFragmentByTag("dialog");
                if (prev != null) ft.Remove(prev);
                ft.AddToBackStack(null);

                NewDialogFragment dialog = NewDialogFragment.NewInstance(null);
                dialog.Show(ft, "dialog");
                dialog.Dismissed += (s, ev) =>
                {
                    if(ev.payload != null)
                    {
                        addItem(ev.payload);
                    }
                };
            };
        }

        public override void OnCreateContextMenu(IContextMenu menu, View vValue, IContextMenuContextMenuInfo menuInfo)
        {
            if(vValue.Id == Resource.Id.listViewInput)
            {
                var info = (AdapterView.AdapterContextMenuInfo) menuInfo;
                var menuItems = Resources.GetStringArray(Resource.Array.menu);
                for (var i = 0; i < menuItems.Length; i++)
                    menu.Add(Menu.None, i, i, menuItems[i]);
            }
        }

        public override bool OnContextItemSelected(IMenuItem item)
        {
            var info = (AdapterView.AdapterContextMenuInfo)item.MenuInfo;
            var menuItemIndex = item.ItemId;
            var menuItems = Resources.GetStringArray(Resource.Array.menu);
            var menuItemName = menuItems[menuItemIndex];
            var listItemPos = info.Position;

            if (menuItemName == "Edit")
            {
                Bundle args = new Bundle();
                string[] bundles = new string[] {
                    transaction[listItemPos].name,
                    transaction[listItemPos].type,
                    transaction[listItemPos].value.ToString(),
                    transaction[listItemPos].recurrenceDay.ToString()
                };
                args.PutStringArray("editData", bundles);
                FragmentTransaction ft = FragmentManager.BeginTransaction();
                Fragment prev = FragmentManager.FindFragmentByTag("dialog");
                if (prev != null) ft.Remove(prev);
                ft.AddToBackStack(null);

                NewDialogFragment dialog = NewDialogFragment.NewInstance(args);
                dialog.Show(ft, "dialog");
                dialog.Dismissed += (s, ev) =>
                {
                    if (ev.payload != null)
                    {
                        transaction[listItemPos] = ev.payload;
                        adapter.NotifyDataSetChanged();
                    }
                };
            }
            else
            {
                removeItem(listItemPos);
            }

            return true;
        }
    }
}