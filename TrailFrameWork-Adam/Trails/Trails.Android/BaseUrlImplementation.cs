using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Trails.Android;
using Xamarin.Forms;
using static Trails.App;

[assembly: Dependency(typeof(BaseUrlImplementation))]
namespace Trails.Android
{
    public class BaseUrlImplementation : IBaseUrl
    {
        public string Get()
        {
            return "file:///android_asset/";
        }
    }
}