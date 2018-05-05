using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Trails
{
	public partial class App : Application
	{
        public static double ScreenWidth { get; internal set; }
        public static double ScreenHeight { get; internal set; }

        public interface IFile
        {
            string GetPath(string path, string fileName);
            string GetPath(string fileName);
            void WriteFile(string path, string fileName, string data);
            string ReadFile(string path, string file, int lineCount);
            string ReadFile(string path, int lineCount);
            string ReadFile(string path, string file);
            bool FileExists(string path, string fileName);
            bool FileExists(string fileName);
            void SaveImage(string path, string fileName, string imageURL);
            void SaveImage(string fileName, string imageURL);
            string GetAsset(string fileName);
        }

        public App ()
		{
			InitializeComponent();

            //MainPage = new Trails.MainPage();

            //change the mainpage() to whatever page
            //var tabbedPage = new TabbedPage();
            //tabbedPage.Children.Add(new MainPage());
            //tabbedPage.Children.Add(new Maps());

            //MainPage = new TabbedPage();
            //MainPage = tabbedPage;

            MainPage = new Trails.TabbedFormat();
            
            /*MainPage = new NavigationPage(new MainPage())
            {
                BarBackgroundColor = Color.FromHex("#51A446"),
                BarTextColor = Color.White,
            };*/

        }

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
