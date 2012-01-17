using System.Drawing;
using System;
using System.Net;
using System.Threading;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using SatchmobileCore;
using MonoTouchCore;
using MonoTouch.UIKit;
using MonoTouch.Foundation;

namespace MonoTouchDemo
{
	public partial class RecentProductViewController : CatalogBaseController
	{
		ProductRepository<Product> _productRepository = new ProductRepository<Product>();
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
				List<Product> products = _productRepository.GetRecent();
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
		
		public override bool ShouldAutorotateToInterfaceOrientation (UIInterfaceOrientation toInterfaceOrientation)
		{
			return (toInterfaceOrientation != UIInterfaceOrientation.PortraitUpsideDown);
		}
	}
}