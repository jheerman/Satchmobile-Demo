using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using System.IO;

using SatchmobileCore;

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
		ProductRepository<Product> _productRepository = new ProductRepository<Product>();
		
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			
			_progressDialog = new ProgressDialog(this);
			_progressDialog.SetMessage("Loading.  Please wait...");
			_progressDialog.Show();
			
			SetContentView (Resource.Layout.Catalog);
			
			var action = Intent.GetStringExtra ("action");
			
			switch (action)
			{
				case "featured":
					Task.Factory
						.StartNew(() =>
							_productRepository.GetFeatured ())
						.ContinueWith(task =>
							RunOnUiThread(() => RenderProducts(task.Result)));
					break;
				case "recent":
					Task.Factory
						.StartNew(() =>
							_productRepository.GetRecent())
						.ContinueWith(task =>
							RunOnUiThread(() => RenderProducts(task.Result)));
					break;
				default:
					break;
			}
		}
		
		private void RenderProducts (List<Product> products)
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