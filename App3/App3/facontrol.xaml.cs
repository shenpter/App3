using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using System.Drawing;

namespace App3
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class facontrol : ContentPage
    {
        string da;
        int renwunum = 0;
        public string phonen;
        public SqlConnection conn;
        string tuanduihao;
        int k = 0;
        int sleepsecond = 0;
        int waitsecond = 2;
        public facontrol()
        {
            InitializeComponent();
            //string thisday = DateTime.Today.ToString("yyyy-mm-dd hh:mm:ss");
            
            picker.MinimumDate = DateTime.Today;

            NavigationPage.SetHasNavigationBar(this, false);

            phonen = Class1.phonenum.phonenumber;
            SqlConnectionStringBuilder scsb = new SqlConnectionStringBuilder();
            scsb.DataSource = "ip2.shiningball.cn,2433";
            scsb.InitialCatalog = "CSB";
            scsb.UserID = "accountuser";
            scsb.Password = "ACCOUNT";


            //创建连接
            conn = new SqlConnection(scsb.ToString());

        }

        private void DatePicker_DateSelected(object sender, DateChangedEventArgs e)
        {
            da = picker.Date.ToString().Replace("/","-").Substring(0,10);
        }
        private void fanhui(object sender, EventArgs e)
        {
            Navigation.PushAsync(new shouye());
        }

        private void name_Completed(object sender, EventArgs e)
        {
            k = 0;
            if (conn.State == ConnectionState.Closed)
                conn.Open();
            string sql = "SELECT * FROM 团队 WHERE 团队名称='"+name.Text+"' AND 首领账号='"+phonen+"'";
            SqlCommand comm = new SqlCommand(sql, conn);
            comm.CommandType = CommandType.Text;
            SqlDataReader sdr;
            sdr = comm.ExecuteReader();
            if (name.Text.Equals(""))
                {
                    fac.Text = "团队名不得为空";
                    fac.TextColor = Xamarin.Forms.Color.Red;
                }
            else if(sdr.Read())
            {
                tuanduihao = sdr["团队号"].ToString();
                k = 1;
                fac.Text = "";
            }
            else
            {
                fac.Text = "该团队并非您创建或并未创建，您没有权限发布任务";
                fac.TextColor = Xamarin.Forms.Color.Red;
            }
            conn.Close();

        }

        private void content_Completed(object sender, EventArgs e)
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
            renwunum = 0;
            if (conn.State == ConnectionState.Closed)
                conn.Open();
            string sql = "SELECT * FROM 任务";
            SqlCommand comm = new SqlCommand(sql, conn);
            comm.CommandType = CommandType.Text;
            SqlDataReader sdr;
            sdr = comm.ExecuteReader();
            while(sdr.Read())
            {
                renwunum++;
            }
            renwunum++;
            conn.Close();

            if (conn.State == ConnectionState.Closed)
                conn.Open();
            string sql1 = "INSERT INTO 任务 VALUES('" + renwunum + "','" + content.Text + "','" + tuanduihao + "');INSERT INTO 发布任务 VALUES('"+renwunum+ "',GETDATE(),'" + da+"')" ;//now出问题
            SqlCommand comm1 = new SqlCommand(sql1, conn);
            comm1.CommandType = CommandType.Text;
            if (k == 1)
            {
                fac.Text = "";
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
                DisplayAlert("通知", "成功发布任务", "确定");
                Navigation.PushAsync(new facontrol());
            }
            else
                DisplayAlert("警告", "该团队并非您创建或并未创建，您没有权限发布任务", "确认");
        }
    }
}