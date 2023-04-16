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
    public partial class Add_Form_teachers : Form
    {
        public Add_Form_teachers()
        {
            InitializeComponent();
        }
        DataBase database = new DataBase();
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!checkuser() == true)
            {
                database.openConnection();

                var ФИО = txb_ФИО.Text;
                var предмет = txb_Предмет.Text;
                var пароль = txb_Пароль.Text;

                if (ФИО != null && пароль != null && предмет != null)
                {
                    var addQuery = $"insert into teachers_db (ФИО, предмет, пароль) values ('{ФИО}', '{предмет}', '{пароль}')";

                    var command = new SqlCommand(addQuery, database.getConnection());
                    command.ExecuteNonQuery();

                    MessageBox.Show("Запись успешно создана!!", "Успешно!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Заполните все данные!!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                database.closeConnection();
            }
        }

        private void Add_Form_teachers_Load(object sender, EventArgs e)
        {

        }
        private Boolean checkuser()
        {
            var ФИО = txb_ФИО.Text;
            var предмет = txb_Предмет.Text;
            var пароль = txb_Пароль.Text;

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();
            string querystring = $"select * from teachers_db where ФИО = '{ФИО}' and предмет = '{предмет}' and пароль = '{пароль}'";

            SqlCommand command = new SqlCommand(querystring, database.getConnection());

            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.Rows.Count > 0)
            {
                MessageBox.Show("Пользователь уже существует!!");
                return true;
            }
            else return false;

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            txb_ФИО.Text = "";
            txb_Предмет.Text = "";
            txb_Пароль.Text = "";
        }
    }
}
