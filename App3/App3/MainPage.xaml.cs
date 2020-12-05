using Android.Content;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Core;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using System.Drawing;

namespace App3
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public SqlConnection conn;
        public MainPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            SqlConnectionStringBuilder scsb = new SqlConnectionStringBuilder();
            scsb.DataSource = "ip2.shiningball.cn,2433";
            scsb.InitialCatalog = "CSB";
            scsb.UserID = "accountuser";
            scsb.Password = "ACCOUNT";
            scsb.MultipleActiveResultSets = true;
            
            //创建连接
            conn = new SqlConnection(scsb.ToString());
            //打开连接
            //判断是否已经有连接
            if(conn.State == ConnectionState.Closed)
                conn.Open();
        }


        private void Loginin(object sender, EventArgs e)
        {
            string sqlstr = "SELECT * FROM 个人 WHERE 账号='" + phone.Text + "' AND 密码 = '" + password.Text + "'";
            int chose = 0;
            int k = 0;
            if (System.Text.RegularExpressions.Regex.IsMatch(phone.Text, "^[0-9]+$"))
            {
                k = 0;
            }
            else
            {
                k = 1;
            }

            if (phone.Text.Equals("") || password.Text.Equals(""))
            {

                DisplayAlert("警告", "手机号或密码不能为空", "确认");
            }
            else if (phone.Text.Length != 11||k==1)
            {

                DisplayAlert("警告", "手机号输入格式错误，请输入11位数字", "确认");
                phone.Text = "";
                password.Text = "";
            }
            else
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                //Console.WriteLine(sqlstr); 写出
                //创建用于执行sql语句的对象
                SqlCommand comm = new SqlCommand(sqlstr, conn);//参数1：sql语句字符串，参数2：已经打开的数据库连接变量
                comm.CommandType = CommandType.Text;
                //执行comm对象
                //接收查询到的sql结果
                SqlDataReader sdr;

                sdr = comm.ExecuteReader();
                if (sdr.Read())
                {
                    chose = 1;
                }
                else
                {
                    chose = 0;
                }

                

                //读取数据
                if (chose != 1)
                {

                    DisplayAlert("警告", "输入的手机号与密码不符，请重新输入", "确认");
                    phone.Text = "";
                    password.Text = "";
                }
                else
                {
                    Class1.phonenum.phonenumber = phone.Text;
                    conn.Close();
                    Navigation.PushAsync(new wait());
                }
            }
            
        }

        private void Register(object sender, EventArgs e)
        {

            Navigation.PushAsync(new Page1());

        }
    }

}
