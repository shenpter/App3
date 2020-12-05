using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using System.Drawing;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App3
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class change : ContentPage
    {
        string da;
        string re;
        public string phonen;
        public SqlConnection conn;
        string tuanduihao;
        int k = 0;
        int sleepsecond = 0;
        int waitsecond = 1;
        public change(string tdname,string neirong,string date,string rennum)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            picker.MinimumDate = DateTime.Today;
            phonen = Class1.phonenum.phonenumber;
            SqlConnectionStringBuilder scsb = new SqlConnectionStringBuilder();
            scsb.DataSource = "ip2.shiningball.cn,2433";
            scsb.InitialCatalog = "CSB";
            scsb.UserID = "accountuser";
            scsb.Password = "ACCOUNT";
            name.Text = tdname;
            re = rennum;
            content.Text = neirong;
            DateTime dat = DateTime.Parse(date);
            picker.Date = dat;

            //创建连接
            conn = new SqlConnection(scsb.ToString());
            

        }

        private void fanhui(object sender, EventArgs e)
        {
            Navigation.PushAsync(new shouye());
        }


        private void DatePicker_DateSelected(object sender, DateChangedEventArgs e)
        {
            da = picker.Date.ToString().Replace("/", "-").Substring(0, 10);
        }

        private void content_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (content.Text.Equals(""))
            {
                fac.Text = "任务内容不得为空";
                fac.TextColor = Xamarin.Forms.Color.Red;
            }
            else
                fac.Text = "";
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Closed)
                conn.Open();
            string sql1 ="UPDATE 发布任务 SET 截止日期='"+da+"' WHERE 任务号='"+re+"';UPDATE 任务 SET 内容='"+content.Text + "' WHERE 任务号='" + re + "'";
            SqlCommand comm1 = new SqlCommand(sql1, conn);
            comm1.CommandType = CommandType.Text;
            comm1.ExecuteNonQuery();
            overlaper.IsVisible = true;
            activity.IsRunning = true;
            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                sleepsecond++;
                if (sleepsecond >= waitsecond)
                {
                    overlaper.IsVisible = false;
                    activity.IsRunning = false;
                }
                return sleepsecond < waitsecond;
            });
            DisplayAlert("通知", "成功修改任务", "确定");
            conn.Close();
            Navigation.PushAsync(new shouye());
        }
    }
}