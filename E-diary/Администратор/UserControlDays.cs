using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace E_diary.Администратор
{
    public partial class UserControlDays : UserControl
    {
          DataBase database = new DataBase();

     //  String connString = @"Data Source=DESKTOP-MF6817J\SQLEXPRESS03;Initial Catalog=test;Integrated Security=True";

        public static string static_day;
        public UserControlDays()
        {
            InitializeComponent();
        }

        private void UserControlDays_Load(object sender, EventArgs e)
        {

        }
        public void days(int numbay)
        {
            lbdays.Text = numbay + "";
        }

        private void UserControlDays_Click(object sender, EventArgs e)
        {
            static_day = lbdays.Text;

            timer1.Start();

            РасписаниеForm расписаниеform = new РасписаниеForm();
            расписаниеform.Show();
        }

        private void displayEvent()
        {
            //      lb_event.Text = " ";

            string queryString = $"select * from schedule_db";

            SqlCommand command = new SqlCommand(queryString, database.getConnection());

            //    SqlConnection connection = new SqlConnection(database);
            database.openConnection();

            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                lb_event.Text = reader["event"].ToString();
            }
            reader.Dispose();
            command.Dispose();
          
         
            database.closeConnection();

            //SqlConnection conn = new SqlConnection(connString);
            //conn.Open();
            //String sql = "select * from schedule_db";
            //SqlCommand cmd = conn.CreateCommand();
            //cmd.CommandText = sql;
            //cmd.Parameters.AddWithValue("date", UserControlDays.static_day + "-" + _4П9и3П11Расписание.static_month + "-" + _4П9и3П11Расписание.static_year + lb_event.Text);
            //SqlDataReader reader = cmd.ExecuteReader();
            //if (reader.Read())
            //{
            //    lbdays.Text = reader["event"].ToString();
            //}
            //reader.Dispose();
            //cmd.Dispose();
            //conn.Close();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            displayEvent();
        }
    }
}
