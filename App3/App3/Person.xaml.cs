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
    public partial class Person : ContentPage
    {
        public string phonen;
        public SqlConnection conn;
        public Person()
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
            //打开连接
            //判断是否已经有连接
            if (conn.State == ConnectionState.Closed)
                conn.Open();
            string sql = "SELECT * FROM 个人 WHERE 账号 = '" + phonen + "'";
            SqlCommand comm = new SqlCommand(sql, conn);
            comm.CommandType = CommandType.Text;
            SqlDataReader sdr;
            sdr = comm.ExecuteReader();
            if (sdr.Read())
            {
                name.Text = sdr["昵称"].ToString();
                password.Text = sdr["密码"].ToString();
                sex.Text = sdr["性别"].ToString();
            }
            user.Text = name.Text;
            conn.Close();


        }
        private void group(object obj, EventArgs args)
        {
            Navigation.PushAsync(new Group());
        }
        private void history(object obj, EventArgs args)
        {
            Navigation.PushAsync(new History());
        }
        private void shouye(object obj, EventArgs args)
        {
            Navigation.PushAsync(new shouye());
        }
        private void Button_Clicked(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Closed)
                conn.Open();
            string sql = "UPDATE 个人 SET 昵称='"+name.Text+"',密码 = '"+password.Text+"',性别='"+sex.Text+"' WHERE 账号 = '" + phonen + "'";
            SqlCommand comm = new SqlCommand(sql, conn);
            comm.CommandType = CommandType.Text;
            if (name.Text.Equals(""))
            {
                DisplayAlert("警告", "昵称不能为空", "确认");

            }
            else if (password.Text.Equals(""))
            {
                DisplayAlert("警告", "密码不能为空", "确认");

            }
            else if (sex.Text.Equals(""))
            {
                DisplayAlert("警告", "性别不能为空", "确认");

            }
            else
            {

                comm.ExecuteNonQuery();
                DisplayAlert("通知", "成功修改个人信息", "确认");
            }
            conn.Close();
            Navigation.PushAsync(new Person());
        }
    }
}