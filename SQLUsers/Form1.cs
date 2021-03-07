using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SQLUsers
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            ExcNQ($"INSERT INTO TBUsers(Name, Family) " +
                  $"VALUES('{txtName.Text}','{txtFamily.Text}')");

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            ExcNQ($"UPDATE TBUsers " +
                $"set Name = '{txtName.Text}', " +
                $"Family = '{txtFamily.Text}' " +
                $"WHERE ID = {txtID.Text}");

        }

        private void ExcNQ(string comTxt)
        {
            string conStr = @"Data Source=LAPTOP-OFJP30BV;Initial Catalog=DBUsers;Integrated Security=True";
            SqlConnection con = new SqlConnection(conStr);

            SqlCommand comm = new SqlCommand(comTxt, con);

            con.Open();
            int result = comm.ExecuteNonQuery();
            con.Close();

            ShowUsers();
            MessageBox.Show((result == 1 ? "" : " not ") + "succeeded!");
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            ExcNQ($"DELETE TBUsers  " +
                $"WHERE ID = {txtID.Text}");
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            ShowUsers();
        }

        private void ShowUsers()
        {
            string output = "";
            string conStr = @"Data Source=LAPTOP-OFJP30BV;Initial Catalog=DBUsers;Integrated Security=True";
            SqlConnection con = new SqlConnection(conStr);

            string orderby;
            if (rdbId.Checked)
                orderby = "ID";
            else if (rdbName.Checked)
                orderby = "Name";
            else
                orderby = "Family";

            SqlCommand comm = new SqlCommand($"SELECT * FROM TBUsers order by {orderby}", con);

            con.Open();
            SqlDataReader reader = comm.ExecuteReader();

            while (reader.Read())
            {
                output += reader["ID"] + ", " + reader["Name"] + ", " + reader[2] + "\n";
            }
            con.Close();

            lblRes.Text = output;

            ShowDGVUsers();
        }

        private void ShowDGVUsers()
        {
            string output = "";
            string conStr = @"Data Source=LAPTOP-OFJP30BV;Initial Catalog=DBUsers;Integrated Security=True";
            SqlConnection con = new SqlConnection(conStr);

            string orderby;
            if (rdbId.Checked)
                orderby = "ID";
            else if (rdbName.Checked)
                orderby = "Name";
            else
                orderby = "Family";

            SqlCommand comm = new SqlCommand($"SELECT * FROM TBUsers order by {orderby}", con);

            con.Open();
            SqlDataReader reader = comm.ExecuteReader();
            DataTable users = new DataTable();
            users.Load(reader);
            dgvUsers.DataSource = users;
            con.Close();

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            ShowUsers();
            ShowDGVUsers();
        }

        private void rdbId_CheckedChanged(object sender, EventArgs e)
        {
            ShowUsers();
        }
    }
}
