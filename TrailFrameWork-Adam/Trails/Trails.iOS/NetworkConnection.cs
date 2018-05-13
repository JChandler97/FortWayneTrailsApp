using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Trails;
using Trails.iOS;
using static Trails.App;

[assembly: Xamarin.Forms.Dependency(typeof(NetworkConnection))]

namespace Trails.iOS
{
    public class NetworkConnection : INetworkConnection
    {

        public bool IsConnected()
        {
            return CrossConnectivity.Current.IsConnected;
        }
    }
}