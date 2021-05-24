using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace CRMGraduationWork
{
    public partial class Form2 : Form
    {
        private int requestID;
        private int clientID;
        private int issueID;
        private int techID;
        private int masterID;

        public Form2(int ID)
        {
            InitializeComponent();

            requestID = ID;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string clientName = "";
            string clientPhone = "";
            string clientEmail = "";
            string clientAddress = "";
            string createDate = "";
            string techName = "";
            string issueDescription = "";
            string masterName = "";

            clientName = textBox1.Text;
            clientEmail = textBox2.Text;
            clientPhone = textBox3.Text;
            clientAddress = textBox6.Text;
            createDate = textBox4.Text;
            techName = textBox5.Text;
            issueDescription = richTextBox1.Text;
            masterName = comboBox1.Text;

            string connStr = "server=localhost;user=root;database=repair_bot_db;charset=utf8;";
            MySqlConnection conn = new MySqlConnection(connStr);

            try
            {
                conn.Open();
            }
            catch
            {
                MessageBox.Show("Error", "Unable to connect to the server");
                return;
            }

            MySqlCommand command = new MySqlCommand();
            command.Connection = conn;
            command.CommandText = "SELECT * FROM masters WHERE name = @name";
            command.Parameters.AddWithValue("@name", masterName);

            using (MySqlDataReader result = command.ExecuteReader())
            {
                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        this.masterID = Convert.ToInt32(result[0]);
                    }

                }
            }

            conn.Close();
            conn.Dispose();

            string connStr6 = "server=localhost;user=root;database=repair_bot_db;charset=utf8;";
            MySqlConnection conn6 = new MySqlConnection(connStr6);

            try
            {
                conn6.Open();
            }
            catch
            {
                MessageBox.Show("Error", "Unable to connect to the server");
                return;
            }

            MySqlCommand command6 = new MySqlCommand();
            command6.Connection = conn6;
            command6.CommandText = "UPDATE issues SET description = @description WHERE id = @id";
            command6.Parameters.AddWithValue("@description", issueDescription);
            command6.Parameters.AddWithValue("@id", issueID);
            command6.ExecuteNonQuery();

            conn6.Close();
            conn6.Dispose();

            string connStr7 = "server=localhost;user=root;database=repair_bot_db;charset=utf8;";
            MySqlConnection conn7 = new MySqlConnection(connStr7);

            try
            {
                conn7.Open();
            }
            catch
            {
                MessageBox.Show("Error", "Unable to connect to the server");
                return;
            }

            MySqlCommand command7 = new MySqlCommand();
            command7.Connection = conn7;
            command7.CommandText = "UPDATE clients SET name = @name, phone = @phone, email = @email, address = @address WHERE id = @id";
            command7.Parameters.AddWithValue("@name", clientName);
            command7.Parameters.AddWithValue("@phone", clientPhone);
            command7.Parameters.AddWithValue("@email", clientEmail);
            command7.Parameters.AddWithValue("@address", clientAddress);
            command7.Parameters.AddWithValue("@id", clientID);
            command7.ExecuteNonQuery();

            conn7.Close();
            conn7.Dispose();

            string connStr5 = "server=localhost;user=root;database=repair_bot_db;charset=utf8;";
            MySqlConnection conn5 = new MySqlConnection(connStr5);

            try
            {
                conn5.Open();
            }
            catch
            {
                MessageBox.Show("Error", "Unable to connect to the server");
                return;
            }

            MySqlCommand command5 = new MySqlCommand();
            command5.Connection = conn5;
            command5.CommandText = "UPDATE techs SET name = @name WHERE id = @id";
            command5.Parameters.AddWithValue("@name", techName);
            command5.Parameters.AddWithValue("@id", techID);
            command5.ExecuteNonQuery();

            conn5.Close();
            conn5.Dispose();


            string acceptDate = DateTime.UtcNow.AddHours((double)3).ToString("yyyy-MM-dd HH:mm:ss");
            string connStr2 = "server=localhost;user=root;database=repair_bot_db;charset=utf8;";
            MySqlConnection conn2 = new MySqlConnection(connStr2);

            try
            {
                conn2.Open();
            }
            catch
            {
                MessageBox.Show("Error", "Unable to connect to the server");
                return;
            }

            MySqlCommand command2 = new MySqlCommand();
            command2.Connection = conn2;
            command2.CommandText = "UPDATE requests SET status = @status, accept_date = @accept_date, create_date = @create_date, master_id = @master_id WHERE id = @id";
            command2.Parameters.AddWithValue("@status", 1);
            command2.Parameters.AddWithValue("@accept_date", acceptDate);
            command2.Parameters.AddWithValue("@create_date", createDate);
            command2.Parameters.AddWithValue("@master_id", this.masterID);
            command2.Parameters.AddWithValue("@id", this.requestID);
            command2.ExecuteNonQuery();

            conn2.Close();
            conn2.Dispose();

            this.Close();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            string clientName = "";
            string clientEmail = "";
            string clientPhone = "";
            string clientAddress = "";
            string createDate = "";
            string techName = "";
            string issueDescription = "";
            List<string> mastersName = new List<string>();
            string connStr = "server=localhost;user=root;database=repair_bot_db;charset=utf8;";
            MySqlConnection conn = new MySqlConnection(connStr);

            try
            {
                conn.Open();
            }
            catch
            {
                MessageBox.Show("Error", "Unable to connect to the server");
                return;
            }

            MySqlCommand command = new MySqlCommand();
            command.Connection = conn;
            command.CommandText = "SELECT * FROM requests WHERE id = @id";
            command.Parameters.AddWithValue("@id", this.requestID);

            using (MySqlDataReader result = command.ExecuteReader())
            {
                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        createDate = result[1].ToString();
                        this.techID = Convert.ToInt32(result[6]);
                        this.issueID = Convert.ToInt32(result[7]);
                        this.clientID = Convert.ToInt32(result[8]);
                    }
                }
            }

            conn.Close();
            conn.Dispose();

            string connStr2 = "server=localhost;user=root;database=repair_bot_db;charset=utf8;";
            MySqlConnection conn2 = new MySqlConnection(connStr2);

            try
            {
                conn2.Open();
            }
            catch
            {
                MessageBox.Show("Error", "Unable to connect to the server");
                return;
            }

            MySqlCommand command2 = new MySqlCommand();
            command2.Connection = conn2;
            command2.CommandText = "SELECT * FROM masters";

            using (MySqlDataReader result2 = command2.ExecuteReader())
            {
                if (result2.HasRows)
                {
                    while (result2.Read())
                    {
                        mastersName.Add(result2[1].ToString());
                    }
                }
            }

            conn2.Close();
            conn2.Dispose();

            string connStr8 = "server=localhost;user=root;database=repair_bot_db;charset=utf8;";
            MySqlConnection conn8 = new MySqlConnection(connStr8);

            try
            {
                conn8.Open();
            }
            catch
            {
                MessageBox.Show("Error", "Unable to connect to the server");
                return;
            }

            MySqlCommand command8 = new MySqlCommand();
            command8.Connection = conn8;
            command8.CommandText = "SELECT * FROM issues WHERE id = @id";
            command8.Parameters.AddWithValue("@id", this.issueID);

            using (MySqlDataReader result8 = command8.ExecuteReader())
            {
                if (result8.HasRows)
                {
                    while (result8.Read())
                    {
                        issueDescription = result8[1].ToString();
                    }
                }
            }

            conn8.Close();
            conn8.Dispose();

            string connStr9 = "server=localhost;user=root;database=repair_bot_db;charset=utf8;";
            MySqlConnection conn9 = new MySqlConnection(connStr9);

            try
            {
                conn9.Open();
            }
            catch
            {
                MessageBox.Show("Error", "Unable to connect to the server");
                return;
            }

            MySqlCommand command9 = new MySqlCommand();
            command9.Connection = conn9;
            command9.CommandText = "SELECT * FROM clients WHERE id = @id";
            command9.Parameters.AddWithValue("@id", this.clientID);

            using (MySqlDataReader result9 = command9.ExecuteReader())
            {
                if (result9.HasRows)
                {
                    while (result9.Read())
                    {
                        clientName = result9[1].ToString();
                        clientPhone = result9[2].ToString();
                        clientAddress = result9[3].ToString();
                        clientEmail = result9[4].ToString();
                    }
                }
            }

            conn9.Close();
            conn9.Dispose();

            string connStr10 = "server=localhost;user=root;database=repair_bot_db;charset=utf8;";
            MySqlConnection conn10 = new MySqlConnection(connStr10);

            try
            {
                conn10.Open();
            }
            catch
            {
                MessageBox.Show("Error", "Unable to connect to the server");
                return;
            }

            MySqlCommand command10 = new MySqlCommand();
            command10.Connection = conn10;
            command10.CommandText = "SELECT * FROM techs WHERE id = @id";
            command10.Parameters.AddWithValue("@id", this.techID);

            using (MySqlDataReader result10 = command10.ExecuteReader())
            {
                if (result10.HasRows)
                {
                    while (result10.Read())
                    {
                        techName = result10[1].ToString();
                    }
                }
            }

            conn10.Close();
            conn10.Dispose();


            textBox1.Text = clientName;
            textBox2.Text = clientEmail;
            textBox3.Text = clientPhone;
            textBox6.Text = clientAddress;
            textBox4.Text = createDate;
            textBox5.Text = techName;
            richTextBox1.Text = issueDescription;
            comboBox1.DataSource = mastersName;
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
