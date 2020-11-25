using App3.Component;
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
    public partial class shouye : ContentPage
    {
        public shouye()
        {
            InitializeComponent();
            InitMenuBtn();
        }

        private void InitMenuBtn()
        {
            // 设置子按钮
            List<SubButton> subs = new List<SubButton>();
            subs.Add(new SubButton { ImageUrl = "jiaru.png", Action = TestMethod1 });
            subs.Add(new SubButton { ImageUrl = "guanli.png", Action = TestMethod2});

            // 设置按钮的直径为40px
            menuBtn.ButtonDiameter = 50;
            // 设置主按钮的按钮图片
            menuBtn.SetMenuImage("jiahao.png");
            // 绑定子按钮
            menuBtn.SetMenuButton(subs);
        }
        private void TestMethod1(object obj, EventArgs args)
        {
            Navigation.PushAsync(new jiaru());
        }
        private void TestMethod2(object obj, EventArgs args)
        {
            Navigation.PushAsync(new manage());
        }
    }
}