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
                
        public App ()
		{
			InitializeComponent();

            MainPage = new TabbedFormat();
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

        public interface INetworkConnection { bool IsConnected(); }

        public interface IBaseUrl { string Get(); }

        public interface IFile
        {
            string GetPath(string path, string fileName);
            string GetPath(string fileName);
            void WriteFile(string path, string fileName, string data);
            string ReadFile(string path, string file, int lineCount);
            string ReadFile(string fileName, int lineCount);
            string ReadFile(string path, string file);
            bool FileExists(string path, string fileName);
            bool FileExists(string fileName);
            void SaveImage(string path, string fileName, string imageURL);
            void SaveImage(string fileName, string imageURL);
            string GetAsset(string fileName);
        }
    }
}
