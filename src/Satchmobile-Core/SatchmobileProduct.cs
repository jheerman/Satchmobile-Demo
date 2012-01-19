using System;
using Newtonsoft.Json;

namespace SatchmobileCore
{
	public abstract class SatchmobileProduct 
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
		[JsonIgnore]
		public int Rating { get; set; }
	}
}

