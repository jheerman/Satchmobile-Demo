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
	[Activity (Label = "Satchmobile", MainLauncher=true, Theme="@style/Theme.Splash", NoHistory = true)]
	public class SplashActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			
			Intent intent = new Intent();
			intent.SetClass (this, typeof(MainActivity));
			StartActivity(intent);
		}
	}
}

