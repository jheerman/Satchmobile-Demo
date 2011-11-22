using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using System.IO;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using PM = Android.Content.PM;

using Newtonsoft.Json;

namespace SatchmobileDemo
{
	[Activity (Label = "CatalogActivity", Theme="@style/Theme.Default", LaunchMode=PM.LaunchMode.SingleTask)]
	public class CatalogActivity : Activity
	{
		ProgressDialog _progressDialog;
		
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			
			_progressDialog = new ProgressDialog(this);
			_progressDialog.SetMessage("Loading.  Please wait...");
			_progressDialog.Show();
			
			SetContentView (Resource.Layout.Catalog);
			
			var title = new TextView(this);
			title.Text = "Catalog Items";
			
			var action = Intent.GetStringExtra ("action");
			
			switch (action.ToLower ())
			{
				case "featured":
					Task.Factory
						.StartNew(() =>
							GetFeaturedProducts(GetString(Resource.String.store_url)))
						.ContinueWith(task =>
							RunOnUiThread(() => RenderFeaturedProducts(task.Result)));
					break;
				case "recent":
					break;
				default:
					break;
			}
		}
		
		private List<Product> GetFeaturedProducts(string storeUrl)
		{
			try
			{
				using (Stream stream =  new WebClient().OpenRead(string.Format("{0}/ws/featured", storeUrl)))
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
		
		private void RenderFeaturedProducts (List<Product> products)
		{
			if (products != null)
			{
				var listView = FindViewById<ListView>(Resource.Id.catalogList);
				listView.Adapter = new ProductListAdapter(this, products);
			}
			_progressDialog.Dismiss ();
		}
	}
}