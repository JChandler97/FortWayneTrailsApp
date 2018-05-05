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
    public partial class TabbedFormat : TabbedPage
    {
        public TabbedFormat ()
        {
            InitializeComponent();
            Children.Add(new MainPage());
            Children.Add(new Maps());
            Children.Add(new Media());
        }
    }
}