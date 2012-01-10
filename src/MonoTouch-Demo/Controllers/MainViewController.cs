using MonoTouch.UIKit;
using System.Drawing;
using System;
using MonoTouch.Foundation;

namespace MonoTouchDemo
{
	public partial class MainViewController : SatchmobileBaseViewController
	{
		const string _storeUrl = "http://50.56.79.53";
		
		public class SearchDelegate : UISearchBarDelegate 
        {
                UIViewController _hostController;
                
                public SearchDelegate (UIViewController hostcontroller)
                {
                        _hostController = hostcontroller;               
                }
                        
                public override void SearchButtonClicked (UISearchBar bar)
                {
                        bar.ResignFirstResponder ();

                        _hostController.NavigationController.PushViewController(
                                        new SearchViewController(bar.Text), true);
                }

                public override void CancelButtonClicked (UISearchBar bar)
                {
                        bar.ResignFirstResponder ();
                }
        }
		
		public MainViewController () : base ("MainViewController", null)
		{
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
			
			View.AddSubview (_backgroundImage);	
			
			var frame = new RectangleF(0, 0, View.Bounds.Size.Width, 40);
            var searchBar = new UISearchBar(frame)
            {
                    BarStyle = UIBarStyle.Black,
                    Placeholder = "Search the catalog...",
                    ShowsCancelButton = true,
                    Delegate = new SearchDelegate(this)
            };
    
            View.AddSubview (searchBar);
			//any additional setup after loading the view, typically from a nib.
		}
		
		public override void ViewDidUnload ()
		{
			base.ViewDidUnload ();
			
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

