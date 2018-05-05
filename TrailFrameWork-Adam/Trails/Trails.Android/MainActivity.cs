using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using static Trails.App;
using Trails.Droid;
using System.IO;
using System.Collections.Generic;
using Android.Content.Res;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(FileImplementation))]
namespace Trails.Droid
{
    public class FileImplementation : IFile
    {
        public FileImplementation() { }

        public string GetPath(string path, string fileName)
        {
            string path1 = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal); // app directory
            string path2 = path + "/" + fileName; // custom folder and file name
            string finalPath = Path.Combine(path1, path2);

            Console.WriteLine(finalPath);

            return finalPath;
        }

        public string GetPath(string fileName)
        {
            string path1 = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal); // app directory
            string path2 = fileName; // custom folder and file name
            string finalPath = Path.Combine(path1, path2);

            Console.WriteLine(finalPath);

            return finalPath;
        }

        public void WriteFile(string path, string fileName, string data)
        {
            if (!Directory.Exists(GetPath(path)))
            {
                Directory.CreateDirectory(GetPath(path));
            }

            File.WriteAllText(GetPath(path, fileName), data); // writes to local storage
        }

        public string ReadFile(string path, string file, int lineCount)
        {
            StreamReader fileReader = File.OpenText(GetPath(path, file));

            if (fileReader == null)
                return null;
            else
            {
                List<string> result = new List<string>();
                string line = string.Empty;
                int ctr = 0;
                while ((line = fileReader.ReadLine()) != null)
                {
                    result.Add(line);
                    ctr++;
                    if (ctr >= lineCount) break;
                }
                if (line == null)
                {
                    if (fileReader != null)
                    {
                        fileReader.Dispose();
                    }
                }

                fileReader.Close();
                return line;
            }
        }

        public string ReadFile(string path, int lineCount)
        {
            StreamReader fileReader = File.OpenText(GetPath(path));

            if (fileReader == null)
                return null;
            else
            {
                List<string> result = new List<string>();
                string line = string.Empty;
                int ctr = 0;
                while ((line = fileReader.ReadLine()) != null)
                {
                    result.Add(line);
                    ctr++;
                    if (ctr >= lineCount) break;
                }
                if (line == null)
                {
                    if (fileReader != null)
                    {
                        fileReader.Dispose();
                    }
                }

                fileReader.Close();
                return line;
            }
        }

        /*public List<string> ReadFile(string path, string file)
        {
            StreamReader fileReader = File.OpenText(GetPath(path, file));

            if (fileReader == null)
                return null;
            else
            {
                List<string> result = new List<string>();
                string line = string.Empty;
                while ((line = fileReader.ReadLine()) != null)
                {
                    result.Add(line);
                }
                if (line == null)
                {
                    if (fileReader != null)
                    {
                        fileReader.Dispose();
                    }
                }

                fileReader.Close();
                return result;
            }
        }
        */

        public string ReadFile(string path, string file)
        {
            StreamReader fileReader = File.OpenText(GetPath(path, file));

            if (fileReader == null)
                return null;
            else
            {
                string result = "";
                string line = string.Empty;
                while ((line = fileReader.ReadLine()) != null)
                {
                    result += line;
                }
                if (line == null)
                {
                    if (fileReader != null)
                    {
                        fileReader.Dispose();
                    }
                }

                fileReader.Close();
                return result;
            }
        }

        public bool FileExists(string path, string fileName)
        {
            return File.Exists(GetPath(path, fileName));
        }

        public bool FileExists(string fileName)
        {
            return File.Exists(GetPath(fileName));
        }

        public void SaveImage(string path, string fileName, string imageURL)
        {
            var webClient = new System.Net.WebClient();

            webClient.DownloadDataCompleted += (s, e) => {
                try
                {
                    var bytes = e.Result; // get the downloaded data
                    string localPath = GetPath(path, fileName);
                    File.WriteAllBytes(localPath, bytes); // writes to local storage
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.InnerException.Message);
                }

            };

            var url = new Uri(imageURL);

            webClient.DownloadDataAsync(url);
        }

        public void SaveImage(string fileName, string imageURL)
        {
            var webClient = new System.Net.WebClient();

            webClient.DownloadDataCompleted += (s, e) => {
                try
                {
                    var bytes = e.Result; // get the downloaded data
                    string localPath = GetPath("images", fileName);
                    File.WriteAllBytes(localPath, bytes); // writes to local storage
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.InnerException.Message);
                }

            };

            var url = new Uri(imageURL);

            webClient.DownloadDataAsync(url);
        }

        public string GetAsset(string fileName)
        {
            string content;
            AssetManager assets = Forms.Context.Assets;
            using (StreamReader sr = new StreamReader(assets.Open(fileName)))
            {
                content = sr.ReadToEnd();
            }
            return content;
        }
    }

    [Activity(Label = "Fort Wayne Trails", Icon = "@drawable/trailIcon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());
        }
    }
}
