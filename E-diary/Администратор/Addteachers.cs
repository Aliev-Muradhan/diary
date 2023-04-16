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
    
    public partial class Addteachers : Form
    {
        public Addteachers()
        {
            InitializeComponent();
        }
        private void Addteachers_Load(object sender, EventArgs e)
        {
            CreateColumns();
            RefreshDataGrid(dataGridView1);
        }

        DataBase database = new DataBase();

        int selectedRow;

        private void CreateColumns()
        {
            dataGridView1.Columns.Add("id", "id");
            dataGridView1.Columns.Add("ФИО", "ФИО");
            dataGridView1.Columns.Add("предмет", "Предмет");
            dataGridView1.Columns.Add("пароль", "Пароль");
            dataGridView1.Columns.Add("IsNew", String.Empty);
        }
        private void ReadSingleRow(DataGridView dgw, IDataRecord record)
        {
            dgw.Rows.Add(record.GetInt32(0), record.GetString(1), record.GetString(2), record.GetString(3), RowState.ModifiedNow);
        }

        private void RefreshDataGrid(DataGridView dgw)
        {
            dgw.Rows.Clear();

            string queryString = $"select * from teachers_db";

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
                txb_item.Text = row.Cells[2].Value.ToString();
                txb_пароль.Text = row.Cells[3].Value.ToString();
            }
        }
        private void Search(DataGridView dgw)
        {
            dgw.Rows.Clear();

            string searchString = $"select * from teachers_db where concat (id, ФИО, предмет, пароль) like '%" + txb_search.Text + "%'";

            SqlCommand com = new SqlCommand(searchString, database.getConnection());

            database.openConnection();

            SqlDataReader read = com.ExecuteReader();

            while (read.Read())
            {
                ReadSingleRow(dgw, read);
            }

            read.Close();
        }

        private void ClearFields()
        {
            txb_ID.Text = "";
            txb_ФИО.Text = "";
            txb_item.Text = "";
            txb_пароль.Text = "";
        }

        private void btn_new_entry_Click(object sender, EventArgs e)
        {
            Add_Form_teachers form_Teachers = new Add_Form_teachers();
            form_Teachers.Show();
        }

        private void txb_search_TextChanged(object sender, EventArgs e)
        {
            Search(dataGridView1); //поисковик
        }

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
                    var deleteQuery = $"delete from teachers_db where id = {id}";

                    var command = new SqlCommand(deleteQuery, database.getConnection());
                    command.ExecuteNonQuery();
                }

                if (rowState == RowState.Modified)
                {
                    var id = dataGridView1.Rows[index].Cells[0].Value.ToString();
                    var ФИО = dataGridView1.Rows[index].Cells[1].Value.ToString();
                    var предмет = dataGridView1.Rows[index].Cells[2].Value.ToString();
                    var пароль = dataGridView1.Rows[index].Cells[3].Value.ToString();

                    var changeQuery = $"update teachers_db set ФИО = '{ФИО}', предмет = '{предмет}', пароль = '{пароль}' where id = '{id}'";

                    var command = new SqlCommand(changeQuery, database.getConnection());

                    command.ExecuteNonQuery();
                }
            }

            database.closeConnection();

        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            deleteRow();
            ClearFields();
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            Update();
        }

        private void Change()
        {
            var selectedRowIndex = dataGridView1.CurrentCell.RowIndex;

            var id = txb_ID.Text;
            var ФИО = txb_ФИО.Text;
            var предмет = txb_item.Text;
            var пароль = txb_пароль.Text;

            if (dataGridView1.Rows[selectedRowIndex].Cells[0].Value.ToString() != string.Empty)
            {
                dataGridView1.Rows[selectedRowIndex].SetValues(id, ФИО,предмет,пароль);
                dataGridView1.Rows[selectedRowIndex].Cells[4].Value = RowState.Modified;
            }
        }

        private void btn_edit_Click(object sender, EventArgs e)
        {
            Change(); 
            ClearFields(); // изменить
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            ClearFields(); //есть
        }

        private void btn_refresh_Click(object sender, EventArgs e)
        {
            RefreshDataGrid(dataGridView1);
            ClearFields();
        }
    }
}
