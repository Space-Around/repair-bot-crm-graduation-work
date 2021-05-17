using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using System.Threading;

namespace CRMGraduationWork
{
    public partial class Form1 : Form
    {
        static System.Windows.Forms.Timer myTimer = new System.Windows.Forms.Timer();

        public Form1()
        {
            InitializeComponent();
        }

        public void loadRequestsByStatusInDGV1()
        {
            List<RequestDGV> requests = new List<RequestDGV>();
            requests.Clear();
            int id;
            DateTime createDate;
            System.Windows.Forms.Button AcceptBtn = new System.Windows.Forms.Button();            
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
            command.CommandText = "SELECT * FROM requests WHERE status = @status";
            command.Parameters.AddWithValue("@status", 0);

            using (MySqlDataReader result = command.ExecuteReader())
            {
                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        id = Convert.ToInt32(result[0]);
                        createDate = DateTime.ParseExact(result[4].ToString(), "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                        requests.Add(new RequestDGV { ID = id, CreateDate = createDate });

                    }
                }
            }

            conn.Close();
            conn.Dispose();

            dataGridView1.Columns.Clear();
            dataGridView1.DataSource = requests;

            DataGridViewButtonColumn acceptButton = new DataGridViewButtonColumn();
            acceptButton.Name = "Accept";
            acceptButton.HeaderText = "Accept";
            acceptButton.Text = "Accept";
            acceptButton.UseColumnTextForButtonValue = true;
            this.dataGridView1.Columns.Insert(2, acceptButton);

            DataGridViewButtonColumn rejectButton = new DataGridViewButtonColumn();
            rejectButton.Name = "Reject";
            rejectButton.HeaderText = "Reject";
            rejectButton.Text = "Reject";
            rejectButton.UseColumnTextForButtonValue = true;
            this.dataGridView1.Columns.Insert(3, rejectButton);
        }

        public void loadRequestsByStatusInDGV2()
        {
            List<AcceptedRequestDGB> acceptedRequests = new List<AcceptedRequestDGB>();
            int id;
            DateTime createDate;
            DateTime acceptDate;
            System.Windows.Forms.Button AcceptBtn = new System.Windows.Forms.Button();
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
            command.CommandText = "SELECT * FROM requests WHERE status = @status";
            command.Parameters.AddWithValue("@status", 1);

            using (MySqlDataReader result = command.ExecuteReader())
            {
                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        id = Convert.ToInt32(result[0]);
                        createDate = DateTime.ParseExact(result[4].ToString(), "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                        acceptDate = DateTime.ParseExact(result[5].ToString(), "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                        acceptedRequests.Add(new AcceptedRequestDGB { ID = id, CreateDate = createDate, AcceptDate = acceptDate });
                    }
                }
            }

            conn.Close();
            conn.Dispose();

            dataGridView2.Columns.Clear();
            dataGridView2.DataSource = acceptedRequests;

            DataGridViewButtonColumn acceptButton = new DataGridViewButtonColumn();
            acceptButton.Name = "CreateDoc";
            acceptButton.HeaderText = "Create Doc";
            acceptButton.Text = "Create Doc";
            acceptButton.UseColumnTextForButtonValue = true;
            this.dataGridView2.Columns.Insert(3, acceptButton);

            DataGridViewButtonColumn rejectButton = new DataGridViewButtonColumn();
            rejectButton.Name = "Complete";
            rejectButton.HeaderText = "Complete";
            rejectButton.Text = "Complete";
            rejectButton.UseColumnTextForButtonValue = true;
            this.dataGridView2.Columns.Insert(4, rejectButton);
        }

        public void loadRequestsByStatusInDGV3()
        {
            List<CompletedRequestDGV> acceptedRequests = new List<CompletedRequestDGV>();
            int id;
            DateTime createDate;
            DateTime acceptDate;
            DateTime completeDate;
            System.Windows.Forms.Button AcceptBtn = new System.Windows.Forms.Button();
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
            command.CommandText = "SELECT * FROM requests WHERE status = @status";
            command.Parameters.AddWithValue("@status", 2);

            using (MySqlDataReader result = command.ExecuteReader())
            {
                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        id = Convert.ToInt32(result[0]);
                        createDate = DateTime.ParseExact(result[4].ToString(), "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                        acceptDate = DateTime.ParseExact(result[5].ToString(), "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                        completeDate = DateTime.ParseExact(result[6].ToString(), "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                        acceptedRequests.Add(new CompletedRequestDGV { ID = id, CreateDate = createDate, AcceptDate = acceptDate, CompleteDate = completeDate });
                    }
                }
            }

            conn.Close();
            conn.Dispose();

            dataGridView3.Columns.Clear();
            dataGridView3.DataSource = acceptedRequests;

            DataGridViewButtonColumn acceptButton = new DataGridViewButtonColumn();
            acceptButton.Name = "CreateDoc";
            acceptButton.HeaderText = "Create Doc";
            acceptButton.Text = "Create Doc";
            acceptButton.UseColumnTextForButtonValue = true;
            this.dataGridView3.Columns.Insert(4, acceptButton);

            DataGridViewButtonColumn rejectButton = new DataGridViewButtonColumn();
            rejectButton.Name = "Complete";
            rejectButton.HeaderText = "Complete";
            rejectButton.Text = "Complete";
            rejectButton.UseColumnTextForButtonValue = true;
            this.dataGridView3.Columns.Insert(5, rejectButton);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            myTimer.Tick += new EventHandler(TimerEventProcessor);

            myTimer.Interval = 500;
            myTimer.Start();            
        }

        private void TimerEventProcessor(Object myObject, EventArgs myEventArgs)
        {
            loadRequestsByStatusInDGV1();
            loadRequestsByStatusInDGV2();
            loadRequestsByStatusInDGV3();
        }
    }
}
