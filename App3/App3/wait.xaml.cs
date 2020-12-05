using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App3
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class wait : ContentPage
    {
        double sleepsecond = 0;
        double waitsecond = 1;
        public wait()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
             {
                 sleepsecond++;
                 if (sleepsecond >= waitsecond)
                 {
                     Navigation.PushAsync(new shouye());
                 }
                 return sleepsecond < waitsecond;
             });
        }
    }
}