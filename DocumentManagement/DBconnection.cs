
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

        public void fetchSqlData() {
            //"C:\Codes\c#\DocumentManagement\DocumentManagement\documentDB.db"
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
                Trace.WriteLine(reader.GetString(1));
            }
        }
    }




}

   
