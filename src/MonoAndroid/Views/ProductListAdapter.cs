using System.Collections.Generic;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace SatchmobileDemo
{
	public class ProductListAdapter : BaseAdapter  
	{
		private Context _context;
		private List<Product> _products;
		
		public ProductListAdapter (Context context, List<Product> products) : base ()
		{
			_context = context;
			_products = products;
		}
		
		public override Java.Lang.Object GetItem (int position)
		{
			return 0;
		}
		
		public Product GetCatalogItem (int position)
		{
			return _products[position];
		}
		
		public override long GetItemId (int position)
		{
			return _products[position].Id;
		}
		
		public override int Count 
		{
			get { return _products.Count; }
		}
		
		public override View GetView (int position, View convertView, ViewGroup parent)
		{
			return convertView ??
				new ProductListView(_context, _products[position])
				{
					Id = _products[position].Id
				};
		}
	}
}