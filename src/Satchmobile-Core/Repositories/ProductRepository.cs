using System;
using System.IO;
using System.Net;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace SatchmobileCore
{
	public class ProductRepository <T> where T: SatchmobileProduct
	{
		public ProductRepository ()
		{ }
		
		public List<T> GetFeatured()
		{
			try
			{
				using (Stream stream =  new WebClient().OpenRead("http://50.56.79.53/ws/featured"))
					using (StreamReader reader = new StreamReader(stream))
					{
						var response = reader.ReadToEnd().ToString();
						return JsonConvert.DeserializeObject<List<T>>(response);
					}
			}
			catch (Exception)
			{
				return null;
			}
		}
		
		public List<T> GetRecent()
		{
			try
			{
				using (Stream stream =  new WebClient().OpenRead("http://50.56.79.53/ws/product/view/recent"))
					using (StreamReader reader = new StreamReader(stream))
					{
						var response = reader.ReadToEnd().ToString();
						return JsonConvert.DeserializeObject<List<T>>(response);
					}
			}
			catch (Exception)
			{
				return null;
			}
		}
		
		public List<T> Search (string query)
		{
			try
			{
				using (Stream stream =  new WebClient()
					.OpenRead(string.Format("http://50.56.79.53/ws/search/?include_categories=0&keywords={0}", query)))
					using (StreamReader reader = new StreamReader(stream))
					{
						var response = reader.ReadToEnd().ToString();
						return JsonConvert.DeserializeObject<List<T>>(response);
					}
			}
			catch (Exception)
			{
				return null;
			}
		}
	}
}

