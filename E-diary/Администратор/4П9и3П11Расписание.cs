using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace E_diary.Администратор
{
    //БД
    enum RowState
    {
        Existed,
        New,
        Modified,
        ModifiedNow,
        Deleted
    }
    //

    public partial class _4П9и3П11Расписание : Form
    {
        //БД -->
        DataBase database = new DataBase();

        int selectedRow;

        private void CreateColumns()
        {
            dataGridView1.Columns.Add("id", "id");
            dataGridView1.Columns.Add("ФИО", "ФИО");
            dataGridView1.Columns.Add("пароль", "Пароль");
            dataGridView1.Columns.Add("телефон", "Телефон");
            dataGridView1.Columns.Add("IsNew", String.Empty);
        }

        private void ReadSingleRow(DataGridView dgw, IDataRecord record)
        {
            dgw.Rows.Add(record.GetInt32(0), record.GetString(1), record.GetString(2), record.GetString(3),RowState.ModifiedNow);
        }

        //Вывод данных в dataGrid
        private void RefreshDataGrid(DataGridView dgw)
        {
            dgw.Rows.Clear();

            string queryString = $"select * from student_db";

            SqlCommand command = new SqlCommand(queryString, database.getConnection());

            database.openConnection();

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                ReadSingleRow(dgw, reader);
            }
            reader.Close();
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selectedRow = e.RowIndex;

            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[selectedRow];

                txb_ID.Text = row.Cells[0].Value.ToString();
                txb_ФИО.Text = row.Cells[1].Value.ToString();
                txb_пароль.Text = row.Cells[2].Value.ToString();
                txb_телефон.Text = row.Cells[3].Value.ToString();
            }
        }
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshDataGrid(dataGridView1);
            ClearFields();
        }

        private void btn_new_entry_Click(object sender, EventArgs e)
        {
            Add_Form add_Form = new Add_Form();
            add_Form.Show();
        }

        private void Search(DataGridView dgw)
        {
            dgw.Rows.Clear();

            string searchString = $"select * from student_db where concat (id, ФИО, пароль, телефон) like '%" + txb_search.Text + "%'";

            SqlCommand com = new SqlCommand(searchString,database.getConnection());

            database.openConnection();

            SqlDataReader read = com.ExecuteReader();

            while (read.Read())
            {
                ReadSingleRow(dgw, read);
            }

            read.Close();
        }
        private void txb_search_TextChanged(object sender, EventArgs e)
        {
            Search(dataGridView1);
        }

        //метод удаления
        private void deleteRow()
        {
            int index = dataGridView1.CurrentCell.RowIndex;

            dataGridView1.Rows[index].Visible = false;

            if (dataGridView1.Rows[index].Cells[0].Value.ToString() == string.Empty)
            {
                dataGridView1.Rows[index].Cells[4].Value = RowState.Deleted;
                return;
            }

            dataGridView1.Rows[index].Cells[4].Value = RowState.Deleted;
        }

        //метод для сохранения изминений в общем 
        private void Update()
        {
            database.openConnection();

            for (int index = 0; index < dataGridView1.Rows.Count; index++)
            {
                var rowState = (RowState)dataGridView1.Rows[index].Cells[4].Value;

                if (rowState == RowState.Existed)
                    continue;
                

                if (rowState == RowState.Deleted)
                {
                    var id = Convert.ToInt32(dataGridView1.Rows[index].Cells[0].Value);
                    var deleteQuery = $"delete from student_db where id = {id}";

                    var command = new SqlCommand(deleteQuery, database.getConnection());
                    command.ExecuteNonQuery();
                }

                if (rowState == RowState.Modified)
                {
                    var id = dataGridView1.Rows[index].Cells[0].Value.ToString();
                    var ФИО = dataGridView1.Rows[index].Cells[1].Value.ToString();
                    var пароль = dataGridView1.Rows[index].Cells[2].Value.ToString();
                    var телефон = dataGridView1.Rows[index].Cells[3].Value.ToString();

                    var changeQuery = $"update student_db set ФИО = '{ФИО}', пароль = '{пароль}', телефон = '{телефон}' where id = '{id}'";

                    var command = new SqlCommand(changeQuery, database.getConnection());

                    command.ExecuteNonQuery();
                }
            }

            database.closeConnection();
            
        }
        //кнопка удаления
        private void btn_delete_Click(object sender, EventArgs e)
        {
            deleteRow();
            ClearFields();
        }
        //кнопка сохранения
        private void btn_save_Click(object sender, EventArgs e)
        {
            Update();
        }

        //метод для кнопки изменить
        private void Change()
        {
            var selectedRowIndex = dataGridView1.CurrentCell.RowIndex;

            var id = txb_ID.Text;
            var ФИО = txb_ФИО.Text;
            var пароль = txb_пароль.Text;
            var телефон = txb_телефон.Text;

            if(dataGridView1.Rows[selectedRowIndex].Cells[0].Value.ToString() != string.Empty)
            {
                dataGridView1.Rows[selectedRowIndex].SetValues(id, ФИО, пароль, телефон);
                dataGridView1.Rows[selectedRowIndex].Cells[4].Value = RowState.Modified;
            }
        }
        //кнопка изменить
        private void btn_edit_Click(object sender, EventArgs e)
        {
            Change();
            ClearFields();
        }

        //метод очистки textBox
        private void ClearFields()
        {
            txb_ID.Text = "";
            txb_ФИО.Text = "";
            txb_пароль.Text = "";
            txb_телефон.Text = "";

        }
        
        private void btn_clear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }
        //<-- БД




       

        public _4П9и3П11Расписание()
        {
            InitializeComponent();

            StartPosition = FormStartPosition.CenterScreen;
        }


        //private void saveButton_Click(object sender, EventArgs e)
        //{
        //   // студентыTableAdapter1.Update(электронный_дневникDataSet1);

        //    //using (SqlConnection connection = new SqlConnection(connectionString))
        //    //{
        //    //    connection.Open();
        //    //    adapter = new SqlDataAdapter(sql, connection);
        //    //    commandBuilder = new SqlCommandBuilder(adapter);
        //    //    adapter.InsertCommand = new SqlCommand("Студенты", connection);
        //    //    adapter.InsertCommand.CommandType = CommandType.StoredProcedure;
        //    //    adapter.InsertCommand.Parameters.Add(new SqlParameter("@№", SqlDbType.Int, 10, "№"));
        //    //    adapter.InsertCommand.Parameters.Add(new SqlParameter("@ФИО", SqlDbType.NVarChar, 50, "ФИО"));
        //    //    adapter.InsertCommand.Parameters.Add(new SqlParameter("@Пол", SqlDbType.NVarChar, 7, "Пол"));
        //    //    adapter.InsertCommand.Parameters.Add(new SqlParameter("@Место проживания", SqlDbType.NVarChar, 50, "Место проживания"));
        //    //    adapter.InsertCommand.Parameters.Add(new SqlParameter("@Контакты", SqlDbType.NVarChar, 20, "Контакты"));

        //    //    //SqlParameter parameter = adapter.InsertCommand.Parameters.Add("@Id", SqlDbType.Int, 0, "Id");
        //    //    //parameter.Direction = ParameterDirection.Output;

        //    //    adapter.Update(ds);
        //    //}
        //}
        //private void deleteButton_Click(object sender, EventArgs e)
        //{
        //    //foreach (DataGridViewRow row in dataGridView1.SelectedRows)
        //    //{
        //    //    dataGridView1.Rows.Remove(row);
        //    //}
        //}

        private void _4П9и3П11Расписание_Load(object sender, EventArgs e)
        {    
            CreateColumns();
            RefreshDataGrid(dataGridView1);


            displaDays();
        }

        //Календарь -->

        int month, year;

        public static int static_month, static_year;

        private void displaDays()
        {

            DateTime now = DateTime.Now;
            month = now.Month;
            year = now.Year;

            String monthname = DateTimeFormatInfo.CurrentInfo.GetMonthName(month);
            LBDATA.Text = monthname + " " + year;

            
            static_month = month;
            static_year = year;
            

            //////выбираем первый день месяца
            DateTime startofthemonth = new DateTime(year, month, 1);

            //////получаем дни месяца
             int days = DateTime.DaysInMonth(year, month);

            //////преобразуйте значение startofthemonth в целое число
             int dayoftheweek = Convert.ToInt32(startofthemonth.DayOfWeek.ToString("d"));

            //////сначала давайте создадим пустой пользовательский элемент управления
            for (int i = 1; i < dayoftheweek; i++)
            {
                UserControlBlank ucblank = new UserControlBlank();
                daycontainer.Controls.Add(ucblank);
            }

            ////теперь, чтобы не создавать usercontrol в течение нескольких дней
            for (int i = 1; i <= days; i++)
            {
                UserControlDays ucdays = new UserControlDays();
                ucdays.days(i);
                daycontainer.Controls.Add(ucdays);
            }


        }
        private void btnprevious_Click(object sender, EventArgs e)
        {
            daycontainer.Controls.Clear();

            month--;

            static_month = month;
            static_year = year;

            String monthname = DateTimeFormatInfo.CurrentInfo.GetMonthName(month);
            LBDATA.Text = monthname + " " + year;

            DateTime now = DateTime.Now;

            //////выбираем первый день месяца
            DateTime startofthemonth = new DateTime(year, month, 1);

            //////получаем дни месяца
            int days = DateTime.DaysInMonth(year, month);

            //////преобразуйте значение startofthemonth в целое число
            int dayoftheweek = Convert.ToInt32(startofthemonth.DayOfWeek.ToString("d"));

            //////сначала давайте создадим пустой пользовательский элемент управления
            for (int i = 1; i < dayoftheweek; i++)
            {
                UserControlBlank ucblank = new UserControlBlank();
                daycontainer.Controls.Add(ucblank);

            }

            ////теперь, чтобы не создавать usercontrol в течение нескольких дней
            for (int i = 1; i <= days; i++)
            {
                UserControlDays ucdays = new UserControlDays();
                ucdays.days(i);
                daycontainer.Controls.Add(ucdays);
            }
        }


        private void btnnext_Click(object sender, EventArgs e)
        {

            daycontainer.Controls.Clear();

            month++;

            static_month = month;
            static_year = year;

            String monthname = DateTimeFormatInfo.CurrentInfo.GetMonthName(month);
            
            LBDATA.Text = monthname + " " + year;

            DateTime now = DateTime.Now;

            //выбираем первый день месяца
            DateTime startofthemonth = new DateTime(year, month, 1);

            //получаем дни месяца
            
            int days = DateTime.DaysInMonth(year, month);

            //////преобразуйте значение startofthemonth в целое число
            int dayoftheweek = Convert.ToInt32(startofthemonth.DayOfWeek.ToString("d"));

            //////сначала давайте создадим пустой пользовательский элемент управления
            for (int i = 1; i < dayoftheweek; i++)
            {
                UserControlBlank ucblank = new UserControlBlank();
                daycontainer.Controls.Add(ucblank);

            }

            ////теперь, чтобы не создавать usercontrol в течение нескольких дней
            for (int i = 1; i <= days; i++)
            {
                UserControlDays ucdays = new UserControlDays();
                ucdays.days(i);
                daycontainer.Controls.Add(ucdays);
            }
        }
        //<--Календрь 

    }
}
