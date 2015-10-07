using NUnit.Framework;
using System;
using Xamarin.UITest;
using Xamarin.UITest.Android;
using Xamarin.UITest.Queries;
using System.Threading;
using System.Linq;

namespace SkystoreTest
{
	[TestFixture]
	public class HomePageTest
	{

		//private string path = "skystore-main-PDApi-internal.apk";
		private string path = "/Users/aliwarsame/Projects/SkystoreTest/SkystoreTest/bin/Debug/mobile-PDApi-internal.apk";
		//private string path = "/Users/aliwarsame/Projects/SkystoreTest/SkystoreTest/bin/Debug/mobile-TSApi-internal.apk";
		private AndroidApp app;
		//private string ApiKey = "949b75ec-4b22-43c0-ac8b-bc5ce9b32195";

		[SetUp]
		public void Setup()
		{
			//app = ConfigureApp.Android.ApkFile (path).DeviceSerial("3230490c748290e1").StartApp(); --- to run on a physical device
			app = ConfigureApp.Android.ApkFile (path).StartApp();
			//app = ConfigureApp.Android.ApkFile (path).

		}
			

		[Test]
		public void flashPDP()
		{
			//app.Repl ();

			//var searchTitle = searchTitleMethod ();
			app.Screenshot ("Given as User Has opened the app");
			Func<AppQuery, AppQuery> img_packshot = e => e.Id ("img_packshot");
			app.WaitForElement (img_packshot, "img_packshot element not found, timed out", new TimeSpan (0, 0, 1, 30, 0));
			app.Flash ("img_packshot");
	
			Func<AppQuery, AppQuery> searchButton = e => e.Id ("img_action_search");
			app.WaitForElement (searchButton, "searchButton element not found, timed out", new TimeSpan (0, 0, 1, 30, 0));
		
			app.Screenshot ("And the User can see the Search Icon");

			enterText ();

			searchElementAndClick ();
		
			assertPdpIsTitleSearchedOn ();

			cancelSearch ();
			//add a function to strip out any white space

			//clickBuy ();

			//login ();

			//completeTransaction ();

			//enterPin ();

			//confirmOrder ();

		}

		public void enterText()
		{
			//var searchText = searchTextMethod();

			//app.Flash("img_action_search");
			//app.EnterText (c => c.Marked ("img_action_search"), "Unfriended");
			app.EnterText (c => c.Marked ("img_action_search"), "Run All Night");
			Func<AppQuery, AppQuery> list_suggest = e => e.Id ("list_suggest");
			app.Tap (list_suggest);
			//return "dal";
			app.Screenshot ("When the User enters text within the Search Icon and taps the 1st result");
		}

		public void searchElementAndClick()
		{
			//var checkPDP = checkPDPMethod();
			//assert pdp says unfriended
			Func<AppQuery, AppQuery> searchResult = e => e.Id ("txt_catalog_title");
			app.WaitForElement (searchResult, "searchResult element not found, timed out", new TimeSpan (0, 0, 1, 30, 0));

			//app.WaitForElement (c => c.Marked ("txt_catalog_title").Text ("Unfriended"));
			app.WaitForElement (c => c.Marked ("txt_catalog_title").Text ("Run All Night"));

			//click on search result to get to the pdp
			app.Tap ("img_alert");
			//assert we are on the pdp page for the title unfriended
			app.Screenshot ("Then the User lands on the PDP page");
		}

		public void assertPdpIsTitleSearchedOn()
		{

			Func<AppQuery, AppQuery> pdpTitle = e => e.Id ("txt_title");
			app.WaitForElement (pdpTitle, "pdpTitle element not found, timed out", new TimeSpan (0, 0, 1, 30, 0));
			//Thread.Sleep (1000);
			//app.WaitForElement (c => c.Marked ("txt_title").Text ("Unfriended  "));

			app.WaitForElement(e => e.Id("txt_title"));
			var elementInfo = app.Query(e => e.Id("txt_title")).First();
			app.Screenshot ("And that PDP is the same title the User Searched for");

		}

		public void cancelSearch()
		{
			Func<AppQuery, AppQuery> cancelButton = e => e.Id ("img_action_cancel");
			app.Tap (cancelButton);	
			app.Screenshot ("And then User cancels the page to return back to the homepage");
		}

		public void clickBuy()
		{
			Func<AppQuery, AppQuery> buyButton = e => e.Id ("btn_action_buy");
			app.Tap (buyButton);	
			app.Screenshot ("And then User clicks Buy");
		}

		public void login()
		{
			app.EnterText (c => c.WebView ().Css ("#username"), "t_keep_p");
			app.EnterText (c => c.WebView ().Css ("#password"), "aaaaaaaa");

			app.WaitForElement (c => c.Marked ("txt_title").Text ("Run All Night"));

			app.Screenshot ("Login and Assert Order Summary Page");

		}

		public void completeTransaction()
		{
			app.Tap ("btn_continue");
			app.Screenshot ("Complete Transact from Order Summary Page");
			
		}

		public void enterPin()
		{
			app.EnterText (c => c.Marked ("txt_pin_one"), "1");
			app.EnterText (c => c.Marked ("txt_pin_two"), "1");
			app.EnterText (c => c.Marked ("txt_pin_three"), "1");
			app.EnterText (c => c.Marked ("txt_pin_four"), "1");

			app.Tap ("btn_ok");

			app.Screenshot ("Enter Payment Pin Successfully");
		}
	
		public void confirmOrder()
		{
			Func<AppQuery, AppQuery> playButton = e => e.Id ("transact_button_container");
			app.WaitForElement (playButton, "playButton element not found, timed out", new TimeSpan (0, 0, 1, 30, 0));

			app.Screenshot ("Confirm Order Success");
		}

		public void checkTitleAppearsWithinMyLibrary ()
		{
			Func<AppQuery, AppQuery> cancelButton = e => e.Id ("img_action_cancel");
			app.Tap (cancelButton);

			Func<AppQuery, AppQuery> back = e => e.Id ("img_action_back");
			app.Tap (back);

			Func<AppQuery, AppQuery> burgerMenu = e => e.Id ("img_action_burger");
			app.Tap (burgerMenu);

			Func<AppQuery, AppQuery> myLibrary = e => e.Id ("txt_title");
			app.Tap (myLibrary);

			Func<AppQuery, AppQuery> purchasedTitle = e => e.Id ("txt_item");
			app.WaitForElement (purchasedTitle, "purchasedTitle element not found, timed out", new TimeSpan (0, 0, 1, 30, 0));

			app.Flash(purchasedTitle);

			app.Screenshot ("Title Confirmed within My Library");
		}

	}

}


