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

        public Form2(int ID)
        {
            InitializeComponent();

            requestID = ID;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Console.WriteLine(comboBox1.Text);
            /*
            string acceptDate = DateTime.UtcNow.Date.ToString("yyyy-MM-dd HH:mm:ss");
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
            command.CommandText = "UPDATE requests SET status = @status, accept_date = @accept_date WHERE id = @id";
            command.Parameters.AddWithValue("@status", 1);
            command.Parameters.AddWithValue("@accpet_date", acceptDate);
            command.Parameters.AddWithValue("@id", this.requestID);
            command.ExecuteNonQuery();

            conn.Close();
            conn.Dispose();*/
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            Console.WriteLine(requestID);
            string name = "";
            string email = "";
            string phone = "";
            string createDate = "";
            string typeOfEquipment = "";
            string issue = "";
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
                        name = result[1].ToString();
                        email = result[2].ToString();
                        phone = result[3].ToString();
                        createDate = result[4].ToString();
                        typeOfEquipment = result[7].ToString();
                        issue = result[8].ToString();
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


            textBox1.Text = name;
            textBox2.Text = email;
            textBox3.Text = phone;
            textBox4.Text = createDate;
            textBox5.Text = typeOfEquipment;
            richTextBox1.Text = issue;
            comboBox1.DataSource = mastersName;
        }
    }
}
