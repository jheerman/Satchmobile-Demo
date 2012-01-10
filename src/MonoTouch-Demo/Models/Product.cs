using System;
using Newtonsoft.Json;
using MonoTouch.Foundation;

namespace MonoTouchDemo
{
	[Preserve (AllMembers=true)]
	public class Product: CatalogItem
	{ 
		[JsonProperty("price_range")]
		public string PriceRange { get; set; }
		[JsonProperty("image")]
		public string ImageURL { get; set; }
		[JsonProperty("thumb")]
		public string ThumbnailImageURL { get; set; }
		public decimal Price { get; set; }
		public string SKU { get; set; }
		[JsonIgnore]
		public int Rating { get; set; }
	}
}

