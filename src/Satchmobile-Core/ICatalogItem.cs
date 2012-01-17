using System;

namespace SatchmobileCore
{
	public interface ICatalogItem
	{
		int Id { get; set; }
		string Name { get; set; }
		string Description { get; set; }
		string Slug { get; set; }
	}
}