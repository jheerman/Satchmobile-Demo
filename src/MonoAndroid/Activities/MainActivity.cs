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
	[Activity (Label = "Home")]
	[MetaData ("android.app.default_searchable", Value="satchmobiledemo.SearchActivity")]
	public class MainActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.Main);
			
			var featured = FindViewById<Button>(Resource.Id.cmdFeatured);
			
			featured.Click += (sender, e) => {
				var intent = new Intent();
				intent.SetClass (this, typeof(CatalogActivity));
				intent.PutExtra("action", "featured");
				StartActivity(intent);
			};
			
			var recent = FindViewById<Button>(Resource.Id.cmdRecent);
			
			recent.Click += (sender, e) => {
				var intent = new Intent();
				intent.SetClass (this, typeof(CatalogActivity));
				intent.PutExtra("action", "recent");
				StartActivity(intent);
			};
			
			FindViewById<Button>(Resource.Id.cmdSearch).Click += (sender, e) =>
				{
					StartSearch (null, false, null, false);
				};
		}
	}
}

