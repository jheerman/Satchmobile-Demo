using System;

using MonoTouch.UIKit;
using MonoTouch.Foundation;

namespace MonoTouchDemo
{
	public class SatchmobileBaseViewController : UIViewController
	{
		public UIImageView _backgroundImage;
		public SatchmobileBaseViewController (string nibName, NSBundle bundle) : base(nibName, bundle)
        { }
		
		public SatchmobileBaseViewController (string title, string nibName, NSBundle bundle) : this(nibName, bundle)
		{ 
			Title = title;
		}
		
		public override void ViewDidLoad()
		{
			base.ViewDidLoad ();
			
			_backgroundImage = new UIImageView(UIImage.FromFile("Images/Backgrounds/retina_aqua.png"));
			_backgroundImage.UserInteractionEnabled = true;
			_backgroundImage.Frame = View.Bounds;
		}
		
		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
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

