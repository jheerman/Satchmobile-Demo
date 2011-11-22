using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using PM = Android.Content.PM;

using Newtonsoft.Json;
using System.Net;
using System.IO;
using Android.Graphics;

namespace SatchmobileDemo
{
	[Activity (Label="Search Results", LaunchMode=PM.LaunchMode.SingleTask, Theme="@style/Theme.Default")]
	[IntentFilter (new [] { "android.intent.action.SEARCH" })]
	[MetaData ("android.app.searchable", Resource="@xml/searchable")]
	public class SearchActivity : Activity
	{
		ProgressDialog _progressDialog;
		
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			_progressDialog = new ProgressDialog(this);
			_progressDialog.SetMessage("Searching.  Please wait...");
			_progressDialog.Show();
			
			var intent = this.Intent;
			
			if (Intent.ActionSearch.Equals(intent.Action)) 
			{
				string query = intent.GetStringExtra(SearchManager.Query);
				
				Task.Factory
					.StartNew(() =>
						ExecuteSearch(query))
					.ContinueWith(task =>
						RunOnUiThread(() => RenderResults(task.Result)));
			}
		}
		private List<Product> ExecuteSearch (string query)
		{
			try
			{
				using (Stream stream =  new WebClient().OpenRead(
							string.Format("{0}/ws/search/?include_categories=0&keywords={1}", 
								GetString(Resource.String.store_url), query)))
					using (StreamReader reader = new StreamReader(stream))
					{
						var response = reader.ReadToEnd().ToString();
						return JsonConvert.DeserializeObject<List<Product>>(response);
					}
			}
			catch (Exception)
			{
				return null;
			}
		}
		private void RenderResults (List<Product> products)
		{
			if (products == null || products.Count == 0)
			{
				_progressDialog.Dismiss ();
				var alertDialog = new AlertDialog.Builder(this).Create();
				alertDialog.SetTitle("Search Results");
				alertDialog.SetMessage("We were unable to find any matches in the catalog.");
				alertDialog.SetIcon(Resource.Drawable.icon);
				alertDialog.SetButton("OK", (o, e) => 
				{
					alertDialog.Dismiss(); 
					Finish(); 
				});
				alertDialog.Show();
			}
			else
			{
				SetContentView (Resource.Layout.SearchResults);
				
				var listView = FindViewById<ListView>(Resource.Id.searchResultList);
				listView.SetBackgroundColor (Color.Transparent.ToArgb());
				listView.Adapter = new ProductListAdapter(this, products);
				_progressDialog.Dismiss ();
			}
		}
	}
}