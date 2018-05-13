using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static Trails.App;

namespace Trails
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Media : ContentPage
	{
        HtmlWebViewSource screenView = new HtmlWebViewSource();
        //string code = "file:///android_asset/About_us.html";
        string MediaHTML = DependencyService.Get<IFile>().GetAsset("About_us.html");

        public Media ()
		{
			InitializeComponent();
            screenView.Html = MediaHTML;
            screenView.BaseUrl = DependencyService.Get<IBaseUrl>().Get();
            myWebView1.Source = screenView;
		}

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }

        protected override bool OnBackButtonPressed()
        {
            if (myWebView1.CanGoBack)
            {
                myWebView1.GoBack();
                return true;
            }
            return true;
        }
    }
}