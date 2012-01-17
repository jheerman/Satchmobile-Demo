using System;
using Newtonsoft.Json;

namespace SatchmobileCore
{
	public interface IProduct : ICatalogItem
	{
		[JsonProperty("price_range")]
		string PriceRange { get; set; }
		[JsonProperty("image")]
		string ImageURL { get; set; }
		[JsonProperty("thumb")]
		string ThumbnailImageURL { get; set; }
		decimal Price { get; set; }
		string SKU { get; set; }
		[JsonIgnore]
		int Rating { get; set; }
	}
}

