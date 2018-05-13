using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;

namespace Trails.iOS
{
    public class TabController : UITabBarController
    {

        UIViewController tab1, tab2, tab3;

        public TabController()
        {
            tab1 = new UIViewController();
            tab1.Title = "Maps";
            tab1.View.BackgroundColor = UIColor.Green;

            tab2 = new UIViewController();
            tab2.Title = "News";
            tab2.View.BackgroundColor = UIColor.Orange;

            tab3 = new UIViewController();
            tab3.Title = "Media";
            tab3.View.BackgroundColor = UIColor.Red;

            var tabs = new UIViewController[] 
            {
                tab1, tab2, tab3
            };

            ViewControllers = tabs;
        }
    }
}