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
    public partial class Add_Form : Form
    {
        DataBase database = new DataBase();

        public Add_Form()
        {
            InitializeComponent();
        }

        private void Add_Form_Load(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
           
            if (!checkuser() == true )
            {
                database.openConnection();

                var ФИО = txb_ФИО.Text;
                var пароль = txb_Пароль.Text;
                var телефон = txb_Телефон.Text;

                if (ФИО != null && пароль != null && телефон != null)
                {
                    var addQuery = $"insert into student_db (ФИО, пароль, телефон) values ('{ФИО}', '{пароль}', '{телефон}')";

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

        private Boolean checkuser()
        {
            var ФИО = txb_ФИО.Text;
            var пароль = txb_Пароль.Text;
            var телефон = txb_Телефон.Text;

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();
            string querystring = $"select * from student_db where ФИО = '{ФИО}' and пароль = '{пароль}' and телефон = '{телефон}'";

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
            txb_Пароль.Text = "";
            txb_Телефон.Text = "";
        }
    }
}
