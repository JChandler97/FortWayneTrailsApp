using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Net;
using Trails;
using Trails.Droid;
using static Trails.App;
using Plugin.Connectivity;

[assembly: Xamarin.Forms.Dependency(typeof(NetworkConnection))]

namespace Trails.Droid
{
    public class NetworkConnection : INetworkConnection
    {

        public bool IsConnected()
        {
            var ConnectivityManager = (ConnectivityManager)Application.Context.GetSystemService(Context.ConnectivityService);
            var ActiveNetworkInfo = ConnectivityManager.ActiveNetworkInfo;
            if (ActiveNetworkInfo != null && ActiveNetworkInfo.IsConnected)
                return true;
            else
                return false;

            //return CrossConnectivity.Current.IsConnected;
        }
    }
}