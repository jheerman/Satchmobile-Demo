using System;
using SatchmobileCore;
using MonoTouch.Foundation;
using Newtonsoft.Json;

namespace MonoTouchCore
{
	[Preserve(AllMembers=true)]
	public class Product : ICatalogItem, IProduct
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string Slug { get; set; }
		[JsonProperty("price_range")]
		public string PriceRange { get; set; }
		[JsonProperty("image")]
		public string ImageURL { get; set; }
		[JsonProperty("thumb")]
		public string ThumbnailImageURL { get; set; }
		public decimal Price { get; set; }
		public string SKU { get; set; }
		public int Rating { get; set; }
	}
}