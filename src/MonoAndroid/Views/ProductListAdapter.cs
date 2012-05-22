using System.Collections.Generic;
using System.Net;
using System.IO;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

using G = Android.Graphics;
using D = Android.Graphics.Drawables;
using System;

namespace SatchmobileDemo
{
	public class ProductListAdapter : BaseAdapter
	{
		private Activity _context;
		private List<Product> _products;
		
		public ProductListAdapter (Activity context, List<Product> products) : base ()
		{
			_context = context;
			_products = products;
		}
		
		public override Java.Lang.Object GetItem (int position)
		{
			return null;
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
			var product = _products[position];
			
			var view = convertView ??
				_context.LayoutInflater.Inflate(Resource.Layout.ProductList, null);
			
			try
			{
				var imageUrl = string.Format("{0}/{1}", 
							_context.Resources.GetString(Resource.String.store_url), 
							product.ThumbnailImageURL);
				
				HttpWebRequest request = (HttpWebRequest) HttpWebRequest.Create(imageUrl);
				using (HttpWebResponse response = (HttpWebResponse) request.GetResponse())
					using (Stream responseStream = response.GetResponseStream()) 
					{
						MemoryStream memoryStream = new MemoryStream();
						responseStream.CopyTo(memoryStream);
						G.Bitmap bitmap = G.BitmapFactory.DecodeByteArray (
							memoryStream.GetBuffer (), 0, (int) memoryStream.Length);
						
						D.Drawable image = new D.BitmapDrawable(bitmap);
						view.FindViewById<ImageView>(Resource.Id.productListImage).SetImageDrawable(image);
					}
			}
			catch 
			{ }
			
			var rand = new Random();
			view.FindViewById<RatingBar>(Resource.Id.productListRating).Rating = rand.Next (0, 5);
			view.FindViewById<TextView>(Resource.Id.productListName).Text = product.Name;
			view.FindViewById<TextView>(Resource.Id.productListPriceRange).Text = product.PriceRange;
			return view;
		}
	}
}