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
    public partial class chuangjian : ContentPage
    {
        public string phonen;
        public SqlConnection conn;
        string num, name, peo;
        int t = -1;
        int ch = 0;
        int groupnum = 0;
        int sleepsecond = 0;
        int waitsecond = 2;
        public chuangjian()
        {
            InitializeComponent();
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

        private void fanhui(object sender, EventArgs e)
        {
            Navigation.PushAsync(new shouye());
        }

        private void chuangbutton(object sender, EventArgs e)
        {
            groupnum = 0;
            if (conn.State == ConnectionState.Closed)
                conn.Open();
            string sql = "SELECT * FROM 团队";
            SqlCommand comm = new SqlCommand(sql, conn);
            comm.CommandType = CommandType.Text;
            SqlDataReader sdr;
            sdr = comm.ExecuteReader();
            while (sdr.Read())
            {
                groupnum++;
            }
            groupnum++;
            conn.Close();

            if (conn.State == ConnectionState.Closed)
                conn.Open();

            if (conn.State == ConnectionState.Closed)
                conn.Open();
            string sql1 = "INSERT INTO 团队 VALUES('"+groupnum+"','"+create.Text+"','"+Class1.phonenum.phonenumber+ "',1);INSERT INTO 个人管理团队 VALUES('"+groupnum+"','"+Class1.phonenum.phonenumber+"','创建者')";
            SqlCommand comm1 = new SqlCommand(sql1, conn);
            comm1.CommandType = CommandType.Text;
            if(create.Text.Equals(""))
                DisplayAlert("警告", "团队号不得为空", "确认");
            else if (ch == 0)
            {
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
            }
            else if (ch == 1)
            {
                DisplayAlert("警告", "该名称已有团队使用，请换一个名称", "确认");
                return;
            }
            else
                return;
            DisplayAlert("通知", "成功创建团队", "确认");
            Navigation.PushAsync(new chuangjian());
            conn.Close();
        }
        private void jiabutton(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Closed)
                conn.Open();
            string sql1;

            sql1 = "INSERT INTO 个人管理团队 VALUES('" + num + "','" + phonen + "','成员');UPDATE 团队 SET 人数 =人数+1 WHERE 团队号='" + num + "'";
            SqlCommand comm1 = new SqlCommand(sql1, conn);
            comm1.CommandType = CommandType.Text;
            if (t == 0)
            {
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
            }
            else if (t == 1)
            {
                DisplayAlert("警告", "已加入该团队", "确认");
                return;
            }
            else
                return;
            DisplayAlert("通知", "成功加入该团队", "确认");
            Navigation.PushAsync(new chuangjian());
            conn.Close();
        }

        private void create_Completed(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Closed)
                conn.Open();
            string sql = "SELECT * FROM 团队 WHERE 团队名称='"+create.Text+"'";
            SqlCommand comm = new SqlCommand(sql, conn);
            comm.CommandType = CommandType.Text;
            SqlDataReader sdr;
            sdr = comm.ExecuteReader();
            if(sdr.Read())
            {
                chuang.Text = "该名称已有团队使用，请换一个名称";
                chuang.TextColor = Xamarin.Forms.Color.Red;
                ch = 1;
            }
            else
            {
                chuang.Text = "该名称可以使用";
                chuang.TextColor = Xamarin.Forms.Color.Green;
                ch = 0;
            }


            conn.Close();

            
        }

        private void join_Completed(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Closed)
                conn.Open();
            string sql = "SELECT * FROM 团队 WHERE 团队号='" + join.Text + "' OR 团队名称='" + join.Text + "'";
            SqlCommand comm = new SqlCommand(sql, conn);
            comm.CommandType = CommandType.Text;
            SqlDataReader sdr;
            sdr = comm.ExecuteReader();
            if (sdr.Read())
            {
                num = sdr["团队号"].ToString();
                name = sdr["团队名称"].ToString();
                peo = sdr["人数"].ToString();
                jiaru.Text = "团队号：" + num + " 团队名称:" + name + " 人数:" + peo;
                jiaru.TextColor = Xamarin.Forms.Color.Green;
            }
            else
            {
                jiaru.Text = "未查询到您输入的团队，请重新输入";
                jiaru.TextColor = Xamarin.Forms.Color.Red;
            }

                conn.Close();


            if (conn.State == ConnectionState.Closed)
                conn.Open();
            string sql2 = "SELECT * FROM 个人管理团队 WHERE 账号='" + phonen + "'AND 团队号='" + num + "'";
            SqlCommand comm2 = new SqlCommand(sql2, conn);
            comm2.CommandType = CommandType.Text;
            SqlDataReader sdr2;
            sdr2 = comm2.ExecuteReader();
            if (sdr2.Read())
            {
                t = 1;
                jiaru.Text += "(已加入)";
            }
            else
                t = 0;

            conn.Close();
        }
    }
}