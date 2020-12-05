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
    public partial class History : ContentPage
    {
        public string phonen;
        public SqlConnection conn;
        string name;
        public IList<Actor> Actor1 { get; set; }
        public IList<Actor> Actor2 { get; set; }
        public History()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            Actor1 = new List<Actor> ();
            Actor2 = new List<Actor>();
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
            string sql = "SELECT 昵称 FROM 个人 WHERE 账号 = '" + phonen + "'";
            SqlCommand comm = new SqlCommand(sql, conn);
            comm.CommandType = CommandType.Text;
            SqlDataReader sdr;
            sdr = comm.ExecuteReader();
            if (sdr.Read())
            {
                name = sdr["昵称"].ToString();
            }
            user.Text = name;
            conn.Close();


            if (conn.State == ConnectionState.Closed)
                conn.Open();

            string sql1 = "SELECT 团队名称,内容,convert(char(10),发布任务.截止日期, 120)截止日期 FROM 团队,个人管理团队,任务,发布任务 WHERE 发布任务.任务号=任务.任务号 AND 任务.团队号=个人管理团队.团队号 AND 团队.团队号=任务.团队号 AND 个人管理团队.账号='" + phonen + "' AND 个人管理团队.职位='创建者'  ORDER BY 截止日期 DESC";
            SqlCommand com = new SqlCommand(sql1, conn);
            com.CommandType = CommandType.Text;
            SqlDataReader renwu;
            renwu = com.ExecuteReader();
            while (renwu.Read())
            {

                string tt = renwu["团队名称"].ToString();
                string bb = renwu["内容"].ToString();
                string cc = renwu["截止日期"].ToString();
                cc = cc.Substring(0, 10);
                cc += "    ";
                Actor1.Add(new Actor
                {
                    groupname = tt,
                    content = bb,
                    data = cc

                });
            }
            BindingContext = this;
            conn.Close();
            if (conn.State == ConnectionState.Closed)
                conn.Open();
            string sql2 = "SELECT 团队名称,内容,convert(char(10),发布任务.截止日期, 120)截止日期 FROM 团队,个人管理团队,任务,发布任务 WHERE 发布任务.任务号=任务.任务号 AND 任务.团队号=个人管理团队.团队号 AND 团队.团队号=任务.团队号 AND 个人管理团队.账号='" + phonen + "' AND 个人管理团队.职位='成员'  ORDER BY 截止日期 DESC";
            SqlCommand com1 = new SqlCommand(sql2, conn);
            com1.CommandType = CommandType.Text;
            SqlDataReader renwuyi;
            renwuyi = com1.ExecuteReader();
            while (renwuyi.Read())
            {

                string tt = renwuyi["团队名称"].ToString();
                string bb = renwuyi["内容"].ToString();
                string cc = renwuyi["截止日期"].ToString();
                cc = cc.Substring(0, 10);
                cc += "    ";
                Actor2.Add(new Actor
                {
                    groupname = tt,
                    content = bb,
                    data = cc

                });

            }
            BindingContext = this;
            conn.Close();

        }
        private void person(object obj, EventArgs args)
        {
            Navigation.PushAsync(new Person());
        }
        private void shouye(object obj, EventArgs args)
        {
            Navigation.PushAsync(new shouye());
        }
        private void group(object obj, EventArgs args)
        {
            Navigation.PushAsync(new Group());
        }
        async void list_n_Refreshing(object sender, System.EventArgs e)
        {
            await Task.Delay(2000);
            list_n.IsRefreshing = false;
        }

        async void list_m_Refreshing(object sender, System.EventArgs e)
        {
            await Task.Delay(1000);
            list_m.IsRefreshing = false;

        }
        private void list_n_ItemTapped(object sender, ItemTappedEventArgs e)
        {

        }

        private void list_m_ItemTapped(object sender, ItemTappedEventArgs e)
        {

        }


        private void list_n_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            list_n.SelectedItem = null;
        }

        private void list_m_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            list_m.SelectedItem = null;
        }
    }
}