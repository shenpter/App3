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
    public partial class Group : ContentPage
    {
        public string phonen;
        public SqlConnection conn;
        public IList<Actor> Actor1 { get; set; }
        public IList<Actor> Actor2 { get; set; }
        string name;
        public Group()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            Actor1 = new List<Actor>();
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

            string sql1 = "SELECT 个人管理团队.团队号,团队名称 FROM 团队,个人管理团队 WHERE 账号='"+phonen+ "' AND 团队.团队号=个人管理团队.团队号 AND 个人管理团队.职位='创建者' ";
            SqlCommand com = new SqlCommand(sql1, conn);
            com.CommandType = CommandType.Text;
            SqlDataReader renwu;
            renwu = com.ExecuteReader();
            while (renwu.Read())
            {

                string tt = renwu["团队名称"].ToString();
                string aa = renwu["团队号"].ToString();
                Actor1.Add(new Actor
                {
                    groupname = tt,
                    content = "创建者",
                    data=tt,
                    rennum=aa

                }) ;

            }
            BindingContext = this;
            conn.Close();


            if (conn.State == ConnectionState.Closed)
                conn.Open();

            string sql2 = "SELECT 个人管理团队.团队号,团队名称 FROM 团队,个人管理团队 WHERE 账号='" + phonen + "' AND 团队.团队号=个人管理团队.团队号 AND 个人管理团队.职位='成员'";
            SqlCommand com1 = new SqlCommand(sql2, conn);
            com1.CommandType = CommandType.Text;
            SqlDataReader renwu1;
            renwu1 = com1.ExecuteReader();
            while (renwu1.Read())
            {

                string tt = renwu1["团队名称"].ToString();
                string aa = renwu1["团队号"].ToString();
                Actor2.Add(new Actor
                {
                    groupname = tt,
                    content = "成员",
                    data = tt,
                    rennum = aa

                });


            }
            BindingContext = this;
            conn.Close();
        }
        private void person(object obj, EventArgs args)
        {
            Navigation.PushAsync(new Person());
        }
        private void history(object obj, EventArgs args)
        {
            Navigation.PushAsync(new History());
        }
        private void shouye(object obj, EventArgs args)
        {
            Navigation.PushAsync(new shouye());
        }

        async void list_m_Refreshing(object sender, System.EventArgs e)
        {
            await Task.Delay(1000);
            list_m.IsRefreshing = false;

        }

        private void list_n_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            list_n.SelectedItem = null;
        }

        private void list_m_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            list_m.SelectedItem = null;
        }


        async void list_n_Refreshing(object sender, System.EventArgs e)
        {
            await Task.Delay(2000);
            list_n.IsRefreshing = false;
        }

        private async void list_m_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var actor = e.Item as Actor;
            string zhanghao;
            string action = await DisplayActionSheet("选择", "取消", null, "解散该团队", "转让该团队创建人位置");
            if (action == "解散该团队")
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                string sql1 = "DELETE FROM 个人管理团队 WHERE 团队号='" + actor.rennum + "';DELETE FROM 团队 WHERE 团队号='" + actor.rennum + "'";
                SqlCommand comm1 = new SqlCommand(sql1, conn);
                comm1.CommandType = CommandType.Text;
                comm1.ExecuteNonQuery();
                await DisplayAlert("通知", "成功解散该团队", "确定");
                conn.Close();
                await Navigation.PushAsync(new Group());
            }
            else if(action== "转让该团队创建人位置")
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                string sql1 = "SELECT * FROM 个人管理团队 WHERE 团队号='"+actor.rennum+"' AND 职位='成员'";
                SqlCommand comm1 = new SqlCommand(sql1, conn);
                comm1.CommandType = CommandType.Text;
                SqlDataReader sdr;
                sdr = comm1.ExecuteReader();
                
                if (sdr.Read())
                {
                    string result = await DisplayPromptAsync("问题", "将创建人位置转让给？");
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    string sql2 = "SELECT 个人.账号 FROM 个人,个人管理团队 WHERE 昵称='"+result+"' AND 团队号='"+actor.rennum+ "' AND 个人.账号=个人管理团队.账号 AND 职位='成员'";
                    SqlCommand comm2 = new SqlCommand(sql2, conn);
                    comm2.CommandType = CommandType.Text;
                    SqlDataReader sdr1;
                    sdr1 = comm2.ExecuteReader();
                    if (sdr1.Read())
                    {
                        zhanghao = sdr1["账号"].ToString();
                        if (conn.State == ConnectionState.Closed)
                            conn.Open();
                        string sql3 = "UPDATE 个人管理团队 SET 职位='成员' WHERE 团队号='" + actor.rennum + "' AND 账号='" + Class1.phonenum.phonenumber + "';UPDATE 个人管理团队 SET 职位='创建者' WHERE 团队号 = '" + actor.rennum + "' AND 账号 = '" + zhanghao + "'";
                        SqlCommand comm3 = new SqlCommand(sql3, conn);
                        comm3.CommandType = CommandType.Text;
                        comm3.ExecuteNonQuery();
                        conn.Close();
                        await DisplayAlert("通知", "成功转让创建人", "确定");
                        await Navigation.PushAsync(new Group());
                    }
                    else
                    {
                        await DisplayAlert("通知", "您的团队中不存在该成员", "确定");
                        conn.Close();
                    }


                }
                else
                {
                    await DisplayAlert("通知", "您的团队中不存在成员，无法转让，请选择解散团队", "确定");
                    conn.Close();
                }

                
            }
        }

        private async void list_n_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var actor = e.Item as Actor;
            string action = await DisplayActionSheet("选择", "取消", null, "退出该团队");
            if (action == "退出该团队")
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                string sql1 = "DELETE FROM 个人管理团队 WHERE 团队号='" + actor.rennum + "' AND 账号='"+Class1.phonenum.phonenumber+ "';UPDATE 团队 SET 人数 =人数-1 WHERE 团队号='" + actor.rennum + "'";
                SqlCommand comm1 = new SqlCommand(sql1, conn);
                comm1.CommandType = CommandType.Text;
                comm1.ExecuteNonQuery();
                await DisplayAlert("通知", "成功退出团队", "确定");
                await Navigation.PushAsync(new Group());
                conn.Close();
            }
        }
    }
}