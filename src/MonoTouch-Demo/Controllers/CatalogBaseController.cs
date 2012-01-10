using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

using System.Drawing;

namespace MonoTouchDemo
{
	public abstract class CatalogBaseController : SatchmobileBaseViewController
	{
		private static readonly string storeUrl = "http://50.56.79.53";
		
		//loads the CatalogBaseController.xib file and connects it to this object
		public CatalogBaseController (string title, string name, NSBundle bundle) : base (name, bundle)
		{ 
			Title = title;
		}
		
		public class CatalogItemGroup
		{
			public string Name { get; set; }
			public string Footer { get; set; }
			public List<Product> Items 
			{
				get { return _items; } 
				set {_items = value; } 
			}
			protected List<Product> _items = new List<Product>();
		}
		
		public class CatalogTableViewSource : UITableViewSource
		{
			static NSString key = new NSString("productCell");
			protected List<CatalogItemGroup> _items = new List<CatalogItemGroup>();
			protected CatalogBaseController _hostController;

			public CatalogTableViewSource (CatalogBaseController hostController, List<Product> items)
			{
				_hostController = hostController;
				
				if (items != null && items.Count > 0)
					_items.Add (
						new CatalogItemGroup {
							Items = items,
							Name = "Products"
						}
					);
			}
			
			public override string TitleForHeader (UITableView tableView, int section)
			{
				return _items[section].Name;
			}
			
			public override UIView GetViewForHeader(UITableView tableView, int section)
			{
				var headerView = new UIView(new RectangleF(0, 0, tableView.Bounds.Size.Width, 50));
				var headerLabel = new UILabel(new RectangleF(15, 0, headerView.Frame.Width - 15, headerView.Frame.Height));
				
				headerLabel.Text = _items[section].Name;
				headerLabel.BackgroundColor = UIColor.Clear;
				headerLabel.TextColor = UIColor.White;
				headerLabel.ShadowColor = UIColor.LightGray;
				
				headerView.AddSubview(headerLabel);
				
				return headerView;
			}

			public override int RowsInSection(UITableView tableview, int section)
			{
				return _items[section].Items.Count;
			}

			public override int NumberOfSections(UITableView tableView)
			{
				return _items.Count;
			}
			
			public override UITableViewCell GetCell(UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath)
			{
				var selectedItem = _items[indexPath.Section].Items[indexPath.Row];
				
				if (selectedItem is Product)
				{
					var product = selectedItem as Product;
					
					var cell = tableView.DequeueReusableCell(key)
							?? new UITableViewCell(UITableViewCellStyle.Subtitle, key);
					
					var img = GetImage (product.ThumbnailImageURL);
					cell.ImageView.Image = img;
					cell.ImageView.Layer.MasksToBounds = true;
					cell.ImageView.Layer.CornerRadius = 8;
					
					cell.TextLabel.Text = product.Name;
					cell.DetailTextLabel.Text = product.PriceRange;
					cell.Accessory = UITableViewCellAccessory.DetailDisclosureButton;

					return cell;
				}
				else
				{
					var cell = tableView.DequeueReusableCell(key)
							?? new UITableViewCell(UITableViewCellStyle.Subtitle, key);
					
					cell.TextLabel.Text = selectedItem.Name;
					cell.Accessory = UITableViewCellAccessory.DisclosureIndicator;
					return cell;
				}
			}
			
			public override float GetHeightForRow(UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath)
			{
				return 55;
			}
			
			public override float GetHeightForHeader(UITableView tableView, int section)
			{
				return 50;
			}
			
			private UIImage GetImage(string url)
			{
				NSUrl imageUrl = NSUrl.FromString(string.Format ("{0}/{1}", storeUrl, url));
				NSData imageData = NSData.FromUrl(imageUrl);
				return new UIImage(imageData);
			}
			
			public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
			{ }
			
			public override void AccessoryButtonTapped (UITableView tableView, NSIndexPath indexPath)
			{ }
		}
		
		public override void ViewDidLoad()
		{
			base.ViewDidLoad ();
		}
	}
}