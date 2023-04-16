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
using E_diary.Администратор;

namespace E_diary.Вход
{
    public partial class АвторизацияForm : Form
    {
        DataBase database = new DataBase();
        public АвторизацияForm()
        {
            InitializeComponent();
        }

        private void АвторизацияForm_Load(object sender, EventArgs e)
        {
            txpassword.PasswordChar = '*';
            btnShow.Visible = false;
            txlogin.MaxLength = 50;
            txpassword.MaxLength = 50;
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            var loginUser = txlogin.Text;
            var passUser = txpassword.Text;

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();

            string querystring = $"select id, ФИО, пароль, телефон from student_db where ФИО = '{loginUser}' and пароль = '{passUser}'";

            SqlCommand command = new SqlCommand(querystring, database.getConnection());

            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.Rows.Count == 1)
            {
                MessageBox.Show("Вы успешно вошли!", "Успешно!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Administrator_Forms administrator = new Administrator_Forms();
                this.Hide();
                administrator.ShowDialog();
                this.Show();
            }
            else
            {
                MessageBox.Show("Такого аккаунта не существует!", "Аккаунта не существует!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txlogin.Text = "";
            txpassword.Text = "";
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            txpassword.UseSystemPasswordChar = false;
            btnHide.Visible = true;
            btnShow.Visible = false;
        }

        private void btnHide_Click(object sender, EventArgs e)
        {
            txpassword.UseSystemPasswordChar = true;
            btnHide.Visible = false;
            btnShow.Visible = true;
        }
    }
}
