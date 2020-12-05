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
    public partial class Page1 : ContentPage
    {
        public SqlConnection conn;
        int chose=0;
        public Page1()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            label1.IsVisible = false;
            SqlConnectionStringBuilder scsb = new SqlConnectionStringBuilder();
            scsb.DataSource = "ip2.shiningball.cn,2433";
            scsb.InitialCatalog = "CSB";
            scsb.UserID = "accountuser";
            scsb.Password = "ACCOUNT";

            //创建连接
            conn = new SqlConnection(scsb.ToString());
           

        }

        private void Button_Clicked(object sender, EventArgs e)
        {
             //打开连接
            //判断是否已经有连接
            if (conn.State == ConnectionState.Closed)
                conn.Open();
            string sql1 = "SELECT * FROM 个人 WHERE 账号='" + phone.Text + "'";
            SqlCommand com1 = new SqlCommand(sql1, conn);
            com1.CommandType = CommandType.Text;
            SqlDataReader sdd;
            sdd = com1.ExecuteReader();
            if (sdd.Read())
            {
                chose = 1;
            }
            else
            {
                chose = 0;
            }
            conn.Close();

            int k = 0;
            if (System.Text.RegularExpressions.Regex.IsMatch(phone.Text, "^[0-9]+$"))
            {
                k = 0;
            }
            else
            {
                k = 1;
            }
            string sqlstr = "INSERT INTO 个人(账号,密码,昵称,性别) VALUES('" + phone.Text+"','"+first.Text+"','"+name.Text+"','男')";
            if (conn.State == ConnectionState.Closed)
                conn.Open();
            
            if (phone.Text.Equals("") )
            {
                DisplayAlert("警告", "手机号不能为空", "确认");

            }
            else if ( name.Text.Equals("") )
            {
                DisplayAlert("警告", "昵称不能为空", "确认");

            }
            else if ( first.Text.Equals("") || second.Text.Equals(""))
            {
                DisplayAlert("警告", "密码不能为空", "确认");

            }
            else if (first.Text != second.Text)
            {
                DisplayAlert("警告", "两次密码输入不一致，请重新输入", "确认");
                first.Text ="";
                second.Text = "";
                
            }
            else if (phone.Text.Length != 11||k==1)
            {
                DisplayAlert("警告", "手机号输入格式错误，请输入11位数字", "确认");
                first.Text = "";
                second.Text = "";
                phone.Text = "";
            }
            else if (chose == 1)
            {
                DisplayAlert("警告", "该手机号已被注册，请移至登陆界面", "确认");
                Navigation.PushAsync(new MainPage());
            }
            else
            {
                SqlCommand comm = new SqlCommand(sqlstr, conn);//参数1：sql语句字符串，参数2：已经打开的数据库连接变量
                comm.CommandType = CommandType.Text;
                comm.ExecuteNonQuery();
                Navigation.PushAsync(new MainPage());
            }
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MainPage());
        }

        private void phone_Completed(object sender, EventArgs e)
        {
            int k = 0;
            if (System.Text.RegularExpressions.Regex.IsMatch(phone.Text, "^[0-9]+$"))
            {
                k = 0;
            }
            else
            {
                k = 1;
            }
            if (phone.Text.Length != 11||k==1)
            {
                label1.Text = "请输入11位手机号";
                label1.IsVisible = true;
            }
            else
            {
                label1.Text = "";
                label1.IsVisible = false;
            }
        }

        private void second_Completed(object sender, EventArgs e)
        {
            if (first.Text != second.Text)
            {
                label1.Text = "两次密码输入不一致";
                label1.IsVisible = true;

            }
            else
            {
                label1.Text = "";
                label1.IsVisible = false;
            }
        }
    }
}