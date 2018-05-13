using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using System.Net;
using Xamarin.Forms;
using static Trails.App;
using System.Net.Http;
using System.Globalization;
using Newtonsoft.Json;
//using Java.IO;
//using Android.Content.Res;

namespace Trails
{
    public partial class MainPage : ContentPage
    {
        private RootObject JSONData;
        private string UpdateTime;

        bool LiveMode = false;
        string UpdateTimeLink;
        string SiteContentsLink;

        public string UpdateTimeLinkDev = "https://fwt.codechameleon.com/api-last-update/";
        public string SiteContentsLinkDev = "https://fwt.codechameleon.com/api-content/";

        public string UpdateTimeLinkLive = "https://fwtrails.org/api-last-update/";
        public string SiteContentsLinkLive = "https://fwtrails.org/api-content/";

        HtmlWebViewSource screenView = new HtmlWebViewSource();
        HtmlWebViewSource nextPage = new HtmlWebViewSource();
        string NewsHTML;

        public MainPage()
        {
            InitializeComponent();

            if (LiveMode == true)
            {
                UpdateTimeLink = UpdateTimeLinkLive;
                SiteContentsLink = SiteContentsLinkLive;
            }
            else
            {
                UpdateTimeLink = UpdateTimeLinkDev;
                SiteContentsLink = SiteContentsLinkDev;
            }

            GetHTMLTemplate();
            GetJSON();

            //code = code.Replace("App content here", "New Content here");

        }

        void GetHTMLTemplate()
        {
            /*
            using (WebClient client = new WebClient())
            {
                if (DependencyService.Get<IFile>().FileExists("html", "template.html"))
                {
                    //code = DependencyService.Get<IFile>().ReadFile("html", "template.html");
                    code = DependencyService.Get<IFile>().GetAsset("index.html");
                }
                else
                {
                    //code = client.DownloadString("http://www.asheragencydev.com/fwtrls-29952");                

                    code = DependencyService.Get<IFile>().GetAsset("index.html");

                    //DependencyService.Get<IFile>().WriteFile("html", "template.html", code);
                }
            }
            */

            NewsHTML = DependencyService.Get<IFile>().GetAsset("index.html");

            NewsHTML = NewsHTML.Replace("Main Page", "News");

            nextPage.Html = NewsHTML;
        }

        async void GetJSON()
        {
            var client = new HttpClient();

            bool DownloadJSON = false;
            string JSONDataRaw = "";

            string UpdateFolder = "data";
            string UpdateFile = "update.txt";
            string SiteContentsFile = "SiteContents.json";

            string UpdateFilePath = DependencyService.Get<IFile>().GetPath(UpdateFolder, UpdateFile);
            string SiteContentsFilePath = DependencyService.Get<IFile>().GetPath(UpdateFolder, SiteContentsFile);

            string StoredUpdateTimeRaw = "";
            string UpdateTimeFormat = "yyyy-MM-dd hh:mm:ss";

            CultureInfo provider = CultureInfo.InvariantCulture;

            DateTime StoredUpdateTime;
            DateTime NewUpdateTime;

            if (DependencyService.Get<INetworkConnection>().IsConnected())
            {
                HttpResponseMessage LastUpdateResponse = await client.GetAsync(UpdateTimeLink);

                LastUpdateResponse.EnsureSuccessStatusCode();

                string lastUpdateBody = await LastUpdateResponse.Content.ReadAsStringAsync();
                lastUpdateBody = lastUpdateBody.Substring(0, UpdateTimeFormat.Length);
                UpdateTime = lastUpdateBody;
                NewUpdateTime = DateTime.ParseExact(UpdateTime, UpdateTimeFormat, provider);

                if (DependencyService.Get<IFile>().FileExists(UpdateFolder, UpdateFile))
                {
                    StoredUpdateTimeRaw = DependencyService.Get<IFile>().ReadFile(UpdateFilePath, 1);
                    StoredUpdateTime = DateTime.ParseExact(StoredUpdateTimeRaw, UpdateTimeFormat, provider);

                    if (DateTime.Compare(NewUpdateTime, StoredUpdateTime) > 0) //if JSON is outdated
                    {
                        //write new date to file
                        DependencyService.Get<IFile>().WriteFile(UpdateFolder, UpdateFile, UpdateTime);

                        //update JSON
                        DownloadJSON = true;
                    }
                    else
                    {
                        //read from cached JSON
                        DownloadJSON = false;
                    }
                }
                else
                {
                    DependencyService.Get<IFile>().WriteFile(UpdateFolder, UpdateFile, UpdateTime);
                    DownloadJSON = true;
                }
            }
            if (DownloadJSON == true)
            {
                //download JSON
                HttpResponseMessage ContentsResponse = await client.GetAsync(SiteContentsLink);
                ContentsResponse.EnsureSuccessStatusCode();

                JSONDataRaw = await ContentsResponse.Content.ReadAsStringAsync();
                DependencyService.Get<IFile>().WriteFile(UpdateFolder, SiteContentsFile, JSONDataRaw);
            }
            else
            {
                //read cached JSON
                if (DependencyService.Get<IFile>().FileExists(UpdateFolder, SiteContentsFile))
                {
                    JSONDataRaw = DependencyService.Get<IFile>().ReadFile(UpdateFolder, SiteContentsFile);
                }
            }

            JSONData = JsonConvert.DeserializeObject<RootObject>(JSONDataRaw);

            //DependencyService.Get<IFile>().SaveImage(UpdateFolder, "image.jpg", "http://fwt.codechameleon.com/wp-content/uploads/2018/03/100-Miles-of-Trails-Event-42.jpg");

            string HTMLBody = "";
            
            if (JSONData != null)
            {
                foreach (var post in JSONData.contents.news)
                {
                    HTMLBody += "<h1>" + post.name + "</h1>";
                    //HTMLBody += "<h3>" + post.slug + "</h3>";
                    //HTMLBody += "<h6>" + post.coverImage + "</h6>";
                    HTMLBody += "<p>" + post.summary + "</p>";
                    HTMLBody += "<p>" + post.body + "</p>";
                    HTMLBody += "<br />";
                }
            }

            if (HTMLBody == "")
            {
                NewsHTML = NewsHTML.Replace("<h1>Loading...</h1>", "<h1 style=\"padding: 30vw 0\">No connection. <br> Please try again later.</h1>");
            }
            else
            {
                NewsHTML = NewsHTML.Replace("<h1>Loading...</h1>", HTMLBody);
            }
            

            screenView.Html = NewsHTML;
            screenView.BaseUrl = DependencyService.Get<IBaseUrl>().Get();

            myWebView.Source = screenView;
        }

        // Both functions below are on MainPage and Media
        // When a user goes to another tab, the tab refreshes itself to the mainpage
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            myWebView.Source = screenView;
        }

        // This function is used to use the back button to go to the last page
        // Function could be removed if it induces bugs
        protected override bool OnBackButtonPressed()
        {
            if (myWebView.CanGoBack)
            {
                myWebView.GoBack();
                return true;
            }
            return true;
        }
    }
}
