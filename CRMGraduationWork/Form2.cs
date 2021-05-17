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
            string name = "";
            string email = "";
            string phone = "";
            string createDate = "";
            string typeOfEquipment = "";
            string issue = "";
            string masterName = "";
            int masterID = -1;

            name = textBox1.Text;
            email = textBox2.Text;
            phone = textBox3.Text;
            createDate = textBox4.Text;
            typeOfEquipment = textBox5.Text;
            issue = richTextBox1.Text;
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
                        masterID = Convert.ToInt32(result[0]);
                    }

                }
            }

            conn.Close();
            conn.Dispose();


            string acceptDate = DateTime.UtcNow.Date.ToString("yyyy-MM-dd HH:mm:ss");
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
            command2.CommandText = "UPDATE requests SET status = @status, accept_date = @accept_date, name = @name, email = @email, phone = @phone, create_date = @create_date, type_of_equipment = @type_of_equipment, issue = @issue, master_id = @master_id WHERE id = @id";
            command2.Parameters.AddWithValue("@status", 1);
            command2.Parameters.AddWithValue("@name", name);
            command2.Parameters.AddWithValue("@email", email);
            command2.Parameters.AddWithValue("@phone", phone);
            command2.Parameters.AddWithValue("@create_date", createDate);
            command2.Parameters.AddWithValue("@type_of_equipment", typeOfEquipment);
            command2.Parameters.AddWithValue("@issue", issue);
            command2.Parameters.AddWithValue("@accept_date", acceptDate);
            command2.Parameters.AddWithValue("@master_id", masterID);
            command2.Parameters.AddWithValue("@id", this.requestID);
            command2.ExecuteNonQuery();

            conn2.Close();
            conn2.Dispose();

            this.Close();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
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
