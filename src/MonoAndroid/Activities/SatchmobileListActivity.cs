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
using R = Android.Resource;

namespace SatchmobileDemo
{
	public class SatchmobileListActivity : ListActivity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			ActionBar.SetDisplayHomeAsUpEnabled (true);
		}
		
		public override bool OnCreateOptionsMenu (IMenu menu)
		{
			return false;
		}

		public override bool OnOptionsItemSelected (IMenuItem item)
		{
			switch (item.ItemId) {
				case R.Id.Home:
					var intent = new Intent(this, typeof(MainActivity));
					intent.AddFlags (ActivityFlags.ClearTop);
					StartActivity (intent);
					break;
				default:
					break;
			}
			
			return true;
		}
	}
}

