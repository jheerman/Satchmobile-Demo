using MonoTouch.UIKit;
using System.Drawing;
using System;
using MonoTouch.Foundation;

namespace MonoTouchDemo
{
	public partial class MainViewController : UIViewController
	{
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
		{ }
		
		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
		}
		
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			UIView backgroundView = new UIView(View.Frame);
			backgroundView.BackgroundColor = UIColor.GroupTableViewBackgroundColor;
			View.AddSubview (backgroundView);
			
			NavigationItem.BackBarButtonItem = new UIBarButtonItem("Home", UIBarButtonItemStyle.Plain, null);
			NavigationItem.Title = "Satchmobile - Demo";
			var searchBar = new UISearchBar(new RectangleF(0, 0, View.Bounds.Size.Width, 40))
			{
				BarStyle = UIBarStyle.Default,
				Placeholder = "Search the catalog...",
				ShowsCancelButton = true,
				Delegate = new SearchDelegate(this)
			};
			
			View.AddSubview (searchBar);
			
			//Add featured products button
			var featured = UIButton.FromType (UIButtonType.RoundedRect);
			featured.SetTitleColor(UIColor.Blue, UIControlState.Normal);
			featured.Frame = new RectangleF(25, 100, View.Frame.Width - 50, 40);
			featured.SetTitle ("Featured Products", UIControlState.Normal);
			featured.TouchDown += delegate {
				NavigationController.PushViewController (new FeaturedViewController(), true);
			};
			View.AddSubview (featured);
			
			//Add recent products button
			var recent = UIButton.FromType (UIButtonType.RoundedRect);
			recent.SetTitleColor(UIColor.Blue, UIControlState.Normal);
			recent.Frame = new RectangleF(25, 175, View.Frame.Width - 50, 40);
			recent.SetTitle ("Recent Products", UIControlState.Normal);
			recent.TouchDown += delegate {
				NavigationController.PushViewController (new RecentProductViewController(), true);
			};
			View.AddSubview (recent);
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