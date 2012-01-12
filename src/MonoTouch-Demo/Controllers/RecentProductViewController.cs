using System.Drawing;
using System;
using System.Net;
using System.Threading;
using System.IO;
using System.Collections.Generic;

using Newtonsoft.Json;

using MonoTouch.UIKit;
using MonoTouch.Foundation;

namespace MonoTouchDemo
{
	public partial class RecentProductViewController : CatalogBaseController
	{
		WaitingView _waiting = new WaitingView();
		public RecentProductViewController () : base ("Recent Products", "RecentProductViewController", null)
		{ }
		
		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
		}
		
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			
			_waiting.Show ("Getting Recently Added.  Please wait...");
			
			ThreadPool.QueueUserWorkItem (state =>
			{
				var products = GetRecent ();
				InvokeOnMainThread (() => 
				{
					recentList.DataSource = new TableViewDataSource(products);
					recentList.ReloadData ();
					_waiting.Hide ();
				});
			});
		}
		
		public override void ViewDidUnload ()
		{
			base.ViewDidUnload ();
		}
		
		private List<Product> GetRecent()
		{
			try
			{
				using (Stream stream =  new WebClient().OpenRead("http://50.56.79.53/ws/product/view/recent"))
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
		
		public override bool ShouldAutorotateToInterfaceOrientation (UIInterfaceOrientation toInterfaceOrientation)
		{
			return (toInterfaceOrientation != UIInterfaceOrientation.PortraitUpsideDown);
		}
	}
}