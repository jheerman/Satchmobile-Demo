using System.Drawing;
using System;
using System.IO;
using System.Net;
using System.Collections.Generic;
using System.Threading;

using Newtonsoft.Json;

using MonoTouch.UIKit;
using MonoTouch.Foundation;

namespace MonoTouchDemo
{
	public partial class SearchViewController : CatalogBaseController
	{
		string _searchText = null;
		WaitingView waiting = new WaitingView();
		
		public SearchViewController (string searchText) : base ("Search Results", "SearchViewController", null)
		{
			_searchText = searchText;
		}
		
		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
		}
		
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			
			waiting.Show ("Searching.  Please wait...");
			
			ThreadPool.QueueUserWorkItem (state =>
			{
				var result = ExecuteSearch (_searchText);
				InvokeOnMainThread (() => 
				{
					catalogList.DataSource = new TableViewDataSource(result);
					catalogList.ReloadData ();
					waiting.Hide ();
				});
			});
		}
		
		public override void ViewDidUnload ()
		{
			base.ViewDidUnload ();
		}
		
		private List<Product> ExecuteSearch (string query)
		{
			try
			{
				using (Stream stream =  new WebClient()
					.OpenRead(string.Format("http://50.56.79.53/ws/search/?include_categories=0&keywords={0}", query)))
					using (StreamReader reader = new StreamReader(stream))
					{
						var response = reader.ReadToEnd().ToString();
						return JsonConvert.DeserializeObject<List<Product>>(response);
					}
			}
			catch (Exception)
			{
				return null;
			}
		}
		
		public override bool ShouldAutorotateToInterfaceOrientation (UIInterfaceOrientation toInterfaceOrientation)
		{
			return (toInterfaceOrientation != UIInterfaceOrientation.PortraitUpsideDown);
		}
	}
}