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

namespace SatchmobileDemo
{
	[Activity (Label = "MainActivity")]			
	public class MainActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			
			var container = FindViewById<LinearLayout>(Resource.Id.parentContainer);
			
			TextView message = new TextView(this);
			message.Text = "Satchmobile Demo";
			container.AddView (message);
		}
	}
}

