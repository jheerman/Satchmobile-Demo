using System;
using MonoTouch.Foundation;

namespace MonoTouchDemo
{
	[Preserve(AllMembers=true)]
	public class CatalogItem
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string Slug { get; set; }
	}
}