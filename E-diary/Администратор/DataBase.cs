using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
namespace E_diary.Администратор
{
    internal class DataBase
    {
        SqlConnection sqlConnection = new SqlConnection(@"Data Source=DESKTOP-MF6817J\SQLEXPRESS03;Initial Catalog=test;Integrated Security=True");

        //Если соединение с БД закрыто то открываем
        public void openConnection()
        {
            if (sqlConnection.State == System.Data.ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
        }

        //Если соединение с БД открыто то закрываем
        public void closeConnection()
        {
            if (sqlConnection.State == System.Data.ConnectionState.Open)
            {
                sqlConnection.Close();
            }
        }

        //возврашает строку подключения
        public SqlConnection getConnection()
        {
            return sqlConnection;
        }

    }
}
