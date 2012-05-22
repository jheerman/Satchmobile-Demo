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

using SatchmobileCore;
using Newtonsoft.Json;
using System.Net;
using System.IO;
using G = Android.Graphics;

namespace SatchmobileDemo
{
	[Activity (Label="Search Results", LaunchMode=PM.LaunchMode.SingleTask)]
	[IntentFilter (new [] { "android.intent.action.SEARCH" })]
	[MetaData ("android.app.searchable", Resource="@xml/searchable")]
	public class SearchActivity : SatchmobileListActivity
	{
		ProgressDialog _progressDialog;
		ProductRepository<Product> _productRepository = new ProductRepository<Product>();
		
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			_progressDialog = new ProgressDialog(this);
			_progressDialog.SetMessage("Searching.  Please wait...");
			_progressDialog.Show();
			
			if (Intent.ActionSearch.Equals(Intent.Action)) 
			{
				string query = Intent.GetStringExtra(SearchManager.Query);
				
				Task.Factory
					.StartNew(() =>
						_productRepository.Search (query))
					.ContinueWith(task =>
						RunOnUiThread(() => RenderResults(task.Result)));
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
				ListView.SetBackgroundColor (G.Color.Transparent);
				ListAdapter = new ProductListAdapter(this, products);
				_progressDialog.Dismiss ();
			}
		}
	}
}