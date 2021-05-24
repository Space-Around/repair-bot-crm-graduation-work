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
    public partial class Form3 : Form
    {
        private int requestID;
        private int masterID;
        private int clientID;
        private int issueID;
        private int techID;

        public Form3(int ID)
        {
            InitializeComponent();

            requestID = ID;
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int issuePrice = 0;
            string issueRefinements = "";

            issuePrice = Convert.ToInt32(textBox6.Text);
            issueRefinements = richTextBox2.Text;


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
            command2.CommandText = "UPDATE issues SET price = @price, refinements = @refinements WHERE id = @id";
            command2.Parameters.AddWithValue("@price", issuePrice);
            command2.Parameters.AddWithValue("@refinements", issueRefinements);
            command2.Parameters.AddWithValue("@id", this.issueID);
            command2.ExecuteNonQuery();

            conn2.Close();
            conn2.Dispose();

            this.Close();
        }

        private void Form3_Load(object sender, EventArgs e)
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
            command2.CommandText = "SELECT * FROM masters WHERE id = @id";
            command2.Parameters.AddWithValue("@id", this.masterID);

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
            textBox7.Text = clientAddress;
            textBox4.Text = createDate;
            textBox5.Text = techName;
            richTextBox1.Text = issueDescription;
            comboBox1.DataSource = mastersName;
        }
    }
}
