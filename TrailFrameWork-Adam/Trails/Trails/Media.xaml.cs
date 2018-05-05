using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Trails
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Media : ContentPage
	{
        HtmlWebViewSource screenView = new HtmlWebViewSource();
        string code = "file:///android_asset/About_us.html";

        public Media ()
		{
			InitializeComponent();
            myWebView1.Source = code;
		}

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            myWebView1.Source = code;
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