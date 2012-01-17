using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using MonoTouchCore;
using SatchmobileCore;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

using System.Drawing;

namespace MonoTouchDemo
{
	public abstract class CatalogBaseController : UIViewController 
	{
		private static readonly string storeUrl = "http://50.56.79.53";
		public CatalogBaseController (string title, string name, NSBundle bundle) : base (name, bundle)
		{
			Title = title;
		}
		
		public class TableViewDataSource : UITableViewDataSource
		{
			static NSString cellIdentifier = new NSString ("productCell");
			private List<Product> list;
		
			public TableViewDataSource (List<Product> list)
			{
				this.list = list;
			}
		
			public override int RowsInSection (UITableView tableview, int section)
			{
				return list == null ? 0 : list.Count;
			}
		
			public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
			{
				UITableViewCell cell = tableView.DequeueReusableCell (cellIdentifier);
				if (cell == null)
					cell = new UITableViewCell (UITableViewCellStyle.Default, cellIdentifier);
				
				var product = list[indexPath.Row];
				var img = GetImage (product.ThumbnailImageURL);
				
				cell.ImageView.Image = img;
				cell.TextLabel.Text = product.Name;
				return cell;
			}
			
			private UIImage GetImage(string url)
			{
				NSUrl imageUrl = NSUrl.FromString(string.Format ("{0}/{1}", storeUrl, url));
				NSData imageData = NSData.FromUrl(imageUrl);
				return new UIImage(imageData);
			}
		}
		
		public override void ViewDidLoad()
		{
			base.ViewDidLoad ();
		}
	}
}