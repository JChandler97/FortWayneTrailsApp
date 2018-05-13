using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Foundation;
using Trails.iOS;
using UIKit;
using static Trails.App;

[assembly: Xamarin.Forms.Dependency(typeof(BaseUrlImplementation))]
namespace Trails.iOS
{
    public class BaseUrlImplementation : IBaseUrl
    {
        public BaseUrlImplementation() { }

        public string Get()
        {
            return NSBundle.MainBundle.BundlePath;
        }
    }
}