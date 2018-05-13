using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Foundation;
using Trails.iOS;
using UIKit;
using static Trails.App;

[assembly: Xamarin.Forms.Dependency(typeof(FileImplementation))]
namespace Trails.iOS
{
    public class FileImplementation : IFile
    {
        public FileImplementation() { }

        public string GetPath(string path, string fileName)
        {
            string path1 = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal); // app directory
            string path2 = path + "/" + fileName; // custom folder and file name
            string finalPath = Path.Combine(path1, path2);

            return finalPath;
        }

        public string GetPath(string fileName)
        {
            string path1 = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal); // app directory
            string path2 = fileName; // custom file name
            string finalPath = Path.Combine(path1, path2);

            return finalPath;
        }

        public void WriteFile(string path, string fileName, string data)
        {
            //create the directory if it doesn't exist
            if (!Directory.Exists(GetPath(path)))
            {
                Directory.CreateDirectory(GetPath(path));
            }

            File.WriteAllText(GetPath(path, fileName), data);
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

        public string ReadFile(string fileName, int lineCount)
        {
            StreamReader fileReader = File.OpenText(GetPath(fileName));

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

            webClient.DownloadDataCompleted += (s, e) =>
            {
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

            webClient.DownloadDataCompleted += (s, e) =>
            {
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
            StreamReader fileReader = File.OpenText(Path.Combine(NSBundle.MainBundle.BundlePath, fileName));

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
    }
}