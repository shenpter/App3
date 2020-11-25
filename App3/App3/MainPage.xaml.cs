using Android.Content;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Core;

namespace App3
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }


        private void Loginin(object sender, EventArgs e)
        {
            Navigation.PushAsync(new shouye()); 
        }

        private void Register(object sender, EventArgs e)
        {
            //await Navigation.PushAsync(new MainPage());
            //await label.RelRotateTo(360, 1000);
            Navigation.PushAsync(new Page1());
            
        }
    }
}
