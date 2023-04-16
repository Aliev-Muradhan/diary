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
    public partial class РасписаниеForm : Form
    {

        DataBase database = new DataBase();
        //SqlConnection sqlConnection = new SqlConnection(@"Data Source=DESKTOP-MF6817J\SQLEXPRESS03;Initial Catalog=test;Integrated Security=True");
        public РасписаниеForm()
        {
            InitializeComponent();
        }

        private void РасписаниеForm_Load(object sender, EventArgs e)
        {
            txb_data.Text = UserControlDays.static_day + "-" + _4П9и3П11Расписание.static_month + "-" + _4П9и3П11Расписание.static_year;
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            database.openConnection();
            
            var events = txb_event.Text;
            var date = txb_data.Text;

            if (events != null)
            {
                var addQuery = $"insert into schedule_db (event, date) values ('{events}', '{date}')";

                var command = new SqlCommand(addQuery, database.getConnection());
                command.ExecuteNonQuery();

                MessageBox.Show("Сохранил!!!");
            }
            else
            {
                MessageBox.Show("Не сохранилось!!!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            database.closeConnection();
        }
    }
}
