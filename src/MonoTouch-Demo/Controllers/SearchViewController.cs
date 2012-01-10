using System.Drawing;
using System;
using System.Collections.Generic;
using System.Threading;

using MonoTouch.UIKit;
using MonoTouch.Foundation;

namespace MonoTouchDemo
{
	public partial class SearchViewController : CatalogBaseController
	{
		string _searchText = null;
        WaitingView waiting = new WaitingView();
        
        public SearchViewController (string searchText) : base ("Search Results", "SearchViewController", null)
        { 
                _searchText = searchText;
        }
		
		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}
		
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			
			//any additional setup after loading the view, typically from a nib.
		}
		
		public override void ViewDidUnload ()
		{
			base.ViewDidUnload ();
		
			catalogList.BackgroundView = _backgroundImage;
            waiting.Show ("Searching...Please wait.");
            
            ThreadPool.QueueUserWorkItem (state =>
            {
                    //var result  = new CatalogRepository().SearchByKeywords ("http://50.56.79.53", _searchText);     
                    var result = new List<Product>();
                    InvokeOnMainThread (() => 
                    {
                            catalogList.Source = new CatalogTableViewSource(this, result);
                            catalogList.ReloadData ();
                            waiting.Hide ();
                    });
            });
			
			// Release any retained subviews of the main view.
			// e.g. myOutlet = null;
		}
		
		public override bool ShouldAutorotateToInterfaceOrientation (UIInterfaceOrientation toInterfaceOrientation)
		{
			// Return true for supported orientations
			return (toInterfaceOrientation != UIInterfaceOrientation.PortraitUpsideDown);
		}
	}
}

