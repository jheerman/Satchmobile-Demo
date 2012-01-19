using System.Drawing;
using System;
using System.IO;
using System.Net;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using SatchmobileCore;
using MonoTouch.UIKit;
using MonoTouch.Foundation;

namespace MonoTouchDemo
{
	public partial class SearchViewController : CatalogBaseController
	{
		string _searchText = null;
		ProductRepository<Product> _productRepository = new ProductRepository<Product>();
		WaitingView waiting = new WaitingView();
		
		public SearchViewController (string searchText) : base ("Search Results", "SearchViewController", null)
		{
			_searchText = searchText;
		}
		
		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
		}
		
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			
			waiting.Show ("Searching.  Please wait...");
			
			ThreadPool.QueueUserWorkItem (state =>
			{
				List<Product> result =_productRepository.Search(_searchText);
				InvokeOnMainThread (() => 
				{
					catalogList.DataSource = new TableViewDataSource(result);
					catalogList.ReloadData ();
					waiting.Hide ();
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