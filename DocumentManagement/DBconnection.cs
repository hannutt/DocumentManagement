
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data.SQLite;
namespace DocumentManagement
{
    public class DBconnection
    {
        public void fetchSqlData(System.Windows.Controls.ComboBox options) {
            List<String>sqlData=new List<String>();
          
            string connectionString = "Data Source=\"C:\\Codes\\c#\\DocumentManagement\\DocumentManagement\\documentDB.db\"";
            var connection = new SQLiteConnection(connectionString);
            // Open the connection
            connection.Open();
            string query = "SELECT * FROM filetypes";
            var command = new SQLiteCommand(query, connection);
            var reader = command.ExecuteReader();
            // Loop through the result set and read data
            while (reader.Read())
            {
               
                sqlData.Add(reader.GetString(1));
                
            }
            options.ItemsSource = sqlData;
        }
        public void saveValues(System.Windows.Controls.TextBox extText) {


            string connectionString = "Data Source=\"C:\\Codes\\c#\\DocumentManagement\\DocumentManagement\\documentDB.db\"";
            var connection = new SQLiteConnection(connectionString);
            //string valueToSave=extText.Text;
            string query = "INSERT INTO filetypes (filetype) VALUES (@filetype)";
            connection.Open();
            var command = new SQLiteCommand(query, connection);
            command.Parameters.AddWithValue("@filetype", extText.Text);
            command.ExecuteNonQuery();

        }
    }


    
}


   
