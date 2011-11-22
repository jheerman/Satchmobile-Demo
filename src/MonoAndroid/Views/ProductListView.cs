using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using PM = Android.Content.PM;
using D = Android.Graphics.Drawables;

namespace SatchmobileDemo
{
	public class ProductListView : RelativeLayout
	{
		public ProductListView (Context context, Product product) : base(context)
		{
			SetBackgroundColor(G.Color.Transparent.ToArgb());
			
			LayoutInflater inflater = (LayoutInflater) context.GetSystemService(Context.LayoutInflaterService);
			inflater.Inflate(Resource.Layout.ProductList, this);
			
			try
			{
				var imageUrl = string.Format("{0}/{1}", 
				context.Resources.GetString(Resource.String.store_url), 
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
							FindViewById<ImageView>(Resource.Id.productListImage).SetImageDrawable(image);
					}
			}
			catch (Exception)
			{
			}
			
			FindViewById<TextView>(Resource.Id.productListName).Text = product.Name;
			FindViewById<TextView>(Resource.Id.productListPriceRange).Text = product.PriceRange;
			FindViewById<RatingBar>(Resource.Id.productListRating).Rating = product.Rating;
		}
	}
}

