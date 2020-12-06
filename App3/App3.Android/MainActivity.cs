using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace App3.Droid
{
    [Activity(Label = "任务大师", Icon = "@drawable/main", Theme = "@style/MainTheme",  ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    //[Activity(Label = "App3", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            
            //设置顶部状态栏背景

            //LayoutStable表示布局稳定，不随其他变动而变动,LayoutFullscreen表示把布局拓宽到全屏幕

            var uiOpts = SystemUiFlags.LayoutStable ;

            Window.DecorView.SystemUiVisibility = (StatusBarVisibility)uiOpts;

            //把标题栏设置为透明色

            Window.SetStatusBarColor(Android.Graphics.Color.Argb(127, 88, 161, 217));

            base.OnCreate(savedInstanceState);
            global::Xamarin.Forms.Forms.SetFlags("CollectionView_Experimental");
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

    }
}