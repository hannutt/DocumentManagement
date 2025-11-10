
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;
using static System.Net.WebRequestMethods;
namespace DocumentManagement
{
    public class DBconnection
    {

        public static String connStr()
        {
            string connectionString = "Data Source=\"C:\\Codes\\c#\\DocumentManagement\\DocumentManagement\\documentDB.db\"";
            return connectionString;
        }

        public void fetchSqlData(System.Windows.Controls.ComboBox options)
        {
            List<String> sqlData = new List<String>();

            //string connectionString = "Data Source=\"C:\\Codes\\c#\\DocumentManagement\\DocumentManagement\\documentDB.db\"";
            var connection = new SQLiteConnection(connStr());
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
            connection.Close();
        }
        public void saveValues(System.Windows.Controls.TextBox extText)
        {

            var connection = new SQLiteConnection(connStr());
            //string valueToSave=extText.Text;
            string query = "INSERT INTO filetypes (filetype) VALUES (@filetype)";
            connection.Open();
            var command = new SQLiteCommand(query, connection);
            command.Parameters.AddWithValue("@filetype", extText.Text);
            command.ExecuteNonQuery();
            connection.Close();

        }
        public void saveHiddenFileName(string selectedFile)
        {
            try
            {

                var connection = new SQLiteConnection(connStr());
                //string valueToSave=extText.Text;
                string query = "INSERT INTO hiddenfiles (filename) VALUES (@filename)";
                connection.Open();
                var command = new SQLiteCommand(query, connection);
                command.Parameters.AddWithValue("@filename", selectedFile);
                command.ExecuteNonQuery();
                connection.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }
        public void getHiddenFileNames(System.Windows.Controls.Primitives.Popup hidddenFilesPopup, System.Windows.Controls.ListBox hdFileList)
        {
            List<string> hiddenFiles = new List<string>();
            try
            {

                var connection = new SQLiteConnection(connStr());
                //string valueToSave=extText.Text;
                string query = "SELECT filename FROM hiddenfiles";
                connection.Open();
                var command = new SQLiteCommand(query, connection);
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                        hiddenFiles.Add(reader.GetString(0));

                    }
                    hdFileList.ItemsSource = hiddenFiles;


                }
                connection.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        public void removeHiddeFileFromDb(string fileName)
        {
            try
            {

                var connection = new SQLiteConnection(connStr());
                //string valueToSave=extText.Text;
                string query = $"DELETE FROM hiddenfiles WHERE filename='{fileName}'";
                connection.Open();
                var command = new SQLiteCommand(query, connection);
                command.ExecuteNonQuery();
                connection.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        public void readSavedFiles(System.Windows.Controls.ComboBox savedFilescb)
        {
            try
            {
                List<String> Files = new List<string>();

                var connection = new SQLiteConnection(connStr());
                //string valueToSave=extText.Text;
                string query = "SELECT filename FROM backups";
                connection.Open();
                var command = new SQLiteCommand(query, connection);
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                        Files.Add(reader.GetString(0));

                    }
                    savedFilescb.ItemsSource = Files;
                    connection.Close();
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        public void BackUpFile(string savefile)
        {

            try
            {
                string savingTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                var filename = Path.GetFileName(savefile);
                //var savefile = "C:\\Users\\Omistaja\\Desktop\\history3.txt";
                var lines = System.IO.File.ReadLines(savefile);
                string textResult = "";
                foreach (var line in lines)
                {
                    textResult += line;
                }


                var connection = new SQLiteConnection(connStr());
                //string valueToSave=extText.Text;
                string query = "INSERT INTO backups (filename,content,savetime) VALUES (@filename,@content,@savetime)";
                connection.Open();
                var command = new SQLiteCommand(query, connection);
                command.Parameters.AddWithValue("@filename", filename);
                command.Parameters.AddWithValue("@content", textResult);
                command.Parameters.AddWithValue("@savetime", savingTime);
                command.ExecuteNonQuery();
                connection.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }
        public void readBackupFile(string fname, System.Windows.Controls.ListBox lbFiles)
        {
            try
            {

                var connection = new SQLiteConnection(connStr());
                // Open the connection
                connection.Open();
                string query = $"SELECT content FROM backups WHERE filename='{fname}'";
                var command = new SQLiteCommand(query, connection);

                //command.Parameters.Add(new SqlParameter("@filename",fname.ToString()));
                var reader = command.ExecuteReader();
                string data = "";
                while (reader.Read())
                {
                    data = (reader.GetString(0));

                }
                lbFiles.Items.Add(data);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void trackDeletedFiles(string selectedFile)
        {
            try
            {
                var connection = new SQLiteConnection(connStr());
                //string valueToSave=extText.Text;
                string query = "INSERT INTO deletedfiles (filename) VALUES (@filename)";
                connection.Open();
                var command = new SQLiteCommand(query, connection);
                command.Parameters.AddWithValue("@filename",selectedFile);
                command.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);


            }
        }

    }

    }



