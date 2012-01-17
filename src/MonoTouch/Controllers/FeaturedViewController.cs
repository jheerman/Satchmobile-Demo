using System.Drawing;
using System;
using System.Net;
using System.IO;
using System.Threading;
using System.Linq;
using System.Collections.Generic;

using MonoTouchCore;
using SatchmobileCore;

using MonoTouch.UIKit;
using MonoTouch.Foundation;

namespace MonoTouchDemo
{
	public partial class FeaturedViewController : CatalogBaseController
	{
		WaitingView _waiting = new WaitingView();
		ProductRepository<Product> _productRepository = new ProductRepository<Product>();
		
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
				List<Product> products = _productRepository.GetFeatured();
				InvokeOnMainThread (() => 
				{
					featuredList.DataSource = new TableViewDataSource(products);
					featuredList.ReloadData ();
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