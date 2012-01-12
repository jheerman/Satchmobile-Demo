using System.Drawing;
using System;
using System.Net;
using System.IO;
using System.Threading;

using Newtonsoft.Json;

using MonoTouch.UIKit;
using MonoTouch.Foundation;
using System.Collections.Generic;

namespace MonoTouchDemo
{
	public partial class FeaturedViewController : CatalogBaseController
	{
		WaitingView _waiting = new WaitingView();
		
		public FeaturedViewController () : base ("Featured Products", "FeaturedViewController", null)
		{ }
		
		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
		}
		
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			_waiting.Show ("Getting Featured Products.  Please wait...");
			
			ThreadPool.QueueUserWorkItem (state =>
			{
				var products = GetFeatured ();
				InvokeOnMainThread (() => 
				{
					featuredList.DataSource = new TableViewDataSource(products);
					featuredList.ReloadData ();
					_waiting.Hide ();
				});
			});
		}
		
		private List<Product> GetFeatured()
		{
			try
			{
				using (Stream stream =  new WebClient().OpenRead("http://50.56.79.53/ws/featured"))
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
		
		public override void ViewDidUnload ()
		{
			base.ViewDidUnload ();
		}
		
		public override bool ShouldAutorotateToInterfaceOrientation (UIInterfaceOrientation toInterfaceOrientation)
		{
			return (toInterfaceOrientation != UIInterfaceOrientation.PortraitUpsideDown);
		}
	}
}