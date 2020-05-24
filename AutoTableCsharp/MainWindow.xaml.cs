using System;
using System.Data;
using System.Data.SqlClient;

namespace AutoTableCsharp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            try
            {
                ConnectTest();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void ConnectTest()
        {
            string connString =
                "Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog=db_University;AttachDbFilename=C:\\Users\\123yk\\RiderProjects\\AutoTableCsharp\\AutoTableCsharp\\CarDatabase.mdf;Integrated Security=True;Pooling=False";
            using (SqlConnection myConnection = new SqlConnection(connString))
            {
                TestOutput.Text = "Open\n";
                try
                {
                    string queryString = "SELECT * FROM Автомобили";
                    SqlCommand command = new SqlCommand(queryString, myConnection);
                    myConnection.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    // Call Read before accessing data.
                    while (reader.Read())
                    {
                        ReadSingleRow((IDataRecord)reader);
                    }

                    // Call Close when done reading.
                    reader.Close();

                    TestOutput.Text += "Good\n";
                }
                catch (Exception e)
                {
                    TestOutput.Text += "Error\n";
                    Console.WriteLine(e);
                }
            }
        }

        private void ReadSingleRow(IDataRecord record)
        {
            string data = "";
            for (int i = 0; i < record.FieldCount; i++)
            {
                data += record[i] + " ";
            }
            TestOutput.Text += String.Format("{0}\n", data);
        }
    }
}