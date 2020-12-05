using App3.Component;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public partial class shouye : ContentPage
    {
        public IList<Actor> Actor1 { get;  set; }
        public IList<Actor> Actor2{ get; set; }
        public string phonen;
        public SqlConnection conn;
        string name;
        public shouye()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            InitMenuBtn();
            
            if (Class1.phonenum.control == 0)
            {
                ren.BackgroundColor = Xamarin.Forms.Color.CornflowerBlue;
                ren.BorderColor=Xamarin.Forms.Color.Gold ;
                ren.BorderWidth = 2;
                fa.BackgroundColor = Xamarin.Forms.Color.Beige;
                fa.BorderColor = Xamarin.Forms.Color.Gold;
                fa.BorderWidth = 0;
            }
            else if (Class1.phonenum.control == 1)
            {
                    fa.BackgroundColor = Xamarin.Forms.Color.CornflowerBlue;
                    fa.BorderColor = Xamarin.Forms.Color.Gold;
                    fa.BorderWidth = 2;
                    ren.BackgroundColor = Xamarin.Forms.Color.Beige;
                    ren.BorderColor = Xamarin.Forms.Color.Gold;
                    ren.BorderWidth = 0;
            }
            
            Actor1 = new List<Actor> ();
            Actor2 = new List<Actor> ();
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
            string sql1= "SELECT 团队名称,内容,任务.任务号,convert(char(10),发布任务.截止日期, 120)截止日期 FROM 团队,个人管理团队,任务,发布任务 WHERE 发布任务.任务号=任务.任务号 AND 任务.团队号=个人管理团队.团队号 AND 团队.团队号=任务.团队号 AND 个人管理团队.账号='" + phonen+ "' AND 个人管理团队.职位='成员' AND 发布任务.截止日期>convert(varchar(20),getdate(),111) ORDER BY 截止日期 DESC";
            if(Class1.phonenum.control==1)
                sql1 = "SELECT 团队名称,内容,任务.任务号,convert(char(10),发布任务.截止日期, 120)截止日期 FROM 团队,个人管理团队,任务,发布任务 WHERE 发布任务.任务号=任务.任务号 AND 任务.团队号=个人管理团队.团队号 AND 团队.团队号=任务.团队号 AND 个人管理团队.账号='" + phonen + "' AND 个人管理团队.职位='创建者' AND 发布任务.截止日期>convert(varchar(20),getdate(),111) ORDER BY 截止日期 DESC";

            SqlCommand com = new SqlCommand(sql1, conn);
            com.CommandType = CommandType.Text;
            SqlDataReader renwu;
            renwu = com.ExecuteReader();
            while (renwu.Read())
            {
                
                string tt = renwu["团队名称"].ToString();
                string bb = renwu["内容"].ToString();
                string cc = renwu["截止日期"].ToString();
                string dd = renwu["任务号"].ToString();
                cc = cc.Substring(0, 10);
                cc += "   ";
                Actor1.Add(new Actor
                {
                    groupname = tt,
                    content = bb,
                    data = cc,
                    rennum = dd


                }); 
               
                
            }
            

            BindingContext = this; 
            conn.Close();
            if (conn.State == ConnectionState.Closed)
                conn.Open();
            string sql2 = "SELECT 团队名称,内容,任务.任务号,convert(char(10),发布任务.截止日期, 120)截止日期 FROM 团队,个人管理团队,任务,发布任务 WHERE 发布任务.任务号=任务.任务号 AND 任务.团队号=个人管理团队.团队号 AND 团队.团队号=任务.团队号 AND 个人管理团队.账号='" + phonen + "' AND 个人管理团队.职位='成员' AND 发布任务.截止日期<convert(varchar(20),getdate(),111) ORDER BY 截止日期 DESC";
            if(Class1.phonenum.control==1)
                sql2 = "SELECT 团队名称,内容,任务.任务号,convert(char(10),发布任务.截止日期, 120)截止日期 FROM 团队,个人管理团队,任务,发布任务 WHERE 发布任务.任务号=任务.任务号 AND 任务.团队号=个人管理团队.团队号 AND 团队.团队号=任务.团队号 AND 个人管理团队.账号='" + phonen + "' AND 个人管理团队.职位='创建者' AND 发布任务.截止日期<convert(varchar(20),getdate(),111) ORDER BY 截止日期 DESC";

            SqlCommand com1 = new SqlCommand(sql2, conn);
            com1.CommandType = CommandType.Text;
            SqlDataReader renwuyi;
            renwuyi = com1.ExecuteReader();
            while (renwuyi.Read())
            {

                string tt = renwuyi["团队名称"].ToString();
                string bb = renwuyi["内容"].ToString();
                string cc = renwuyi["截止日期"].ToString();
                string dd = renwuyi["任务号"].ToString();
                cc = cc.Substring(0, 10);
                cc += "   ";
                Actor2.Add(new Actor
                {
                    groupname = tt,
                    content = bb,
                    data = cc,
                    rennum = dd
                });

            }

            BindingContext = this;
            conn.Close();

        }

        private void InitMenuBtn()
        {
            // 设置子按钮
            List<SubButton> subs = new List<SubButton>();
            subs.Add(new SubButton { ImageUrl = "jiaru.png", Action = TestMethod1 });
            subs.Add(new SubButton { ImageUrl = "gantan.png", Action = TestMethod2 });


            // 设置按钮的直径为40px
            menuBtn.ButtonDiameter = 50;
            // 设置主按钮的按钮图片
            menuBtn.SetMenuImage("jiahao.png");
            // 绑定子按钮
            menuBtn.SetMenuButton(subs);
        }
        private void TestMethod1(object obj, EventArgs args)
        {
            Navigation.PushAsync(new chuangjian());
        }
        private void TestMethod2(object obj, EventArgs args)
        {
            Navigation.PushAsync(new facontrol());
           
        }
       
        private void fabu(object obj, EventArgs args)
        {
            if (Class1.phonenum.control == 1)
            {
                return;
            }
            else
            {
                Class1.phonenum.control = 1;
                Navigation.PushAsync(new shouye());
            }
        }
        private void renwu(object obj, EventArgs args)
        {
            if (Class1.phonenum.control == 0)
            {
                return;
            }
            else
            {
                Class1.phonenum.control = 0;
                Navigation.PushAsync(new shouye());
            }
        }
        private void person(object obj, EventArgs args)
        {
            Navigation.PushAsync(new Person());
        }
        private void group(object obj, EventArgs args)
        {
            Navigation.PushAsync(new Group());
        }
        private void history(object obj, EventArgs args)
        {
            Navigation.PushAsync(new History());
        }
        private async void exit(object obj, EventArgs args)
        {
            bool answer= await DisplayAlert("问题", "是否确认退出", "确定", "取消");
            if (answer == true)
            {
                await Navigation.PushAsync(new MainPage());
            }
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
        private async void list_n_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var actor = e.Item as Actor;
            if (Class1.phonenum.control == 1)
            {
                string action = await DisplayActionSheet("选择", "取消", null, "修改该任务", "删除该任务");
                if (action == "修改该任务")
                {

                    await Navigation.PushAsync(new change(actor.groupname,actor.content,actor.data,actor.rennum));
                }
                else if (action == "删除该任务")
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    string sql1 = "DELETE FROM 发布任务 WHERE 任务号='" + actor.rennum + "';DELETE FROM 任务 WHERE 任务号='" + actor.rennum+ "'";
                    SqlCommand comm1 = new SqlCommand(sql1, conn);
                    comm1.CommandType = CommandType.Text;
                    comm1.ExecuteNonQuery();
                    await DisplayAlert("通知", "成功删除任务", "确定");
                    conn.Close();
                }
            }
        }

        private async void list_m_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var actor = e.Item as Actor;
            if (Class1.phonenum.control == 1)
            {
                string action = await DisplayActionSheet("选择", "取消", null, "修改该任务", "删除该任务");
                if (action == "修改该任务")
                {

                    await Navigation.PushAsync(new change(actor.groupname, actor.content, actor.data, actor.rennum));
                }
                else if (action == "删除该任务")
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    string sql1 = "DELETE FROM 发布任务 WHERE 任务号='" + actor.rennum + "';DELETE FROM 任务 WHERE 任务号='" + actor.rennum + "'";
                    SqlCommand comm1 = new SqlCommand(sql1, conn);
                    comm1.CommandType = CommandType.Text;
                    comm1.ExecuteNonQuery();
                    await DisplayAlert("通知", "成功删除任务", "确定");
                    conn.Close();
                }
            }
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