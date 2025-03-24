using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data;
using Microsoft.Data.SqlClient;
namespace LoginWithDatabase
{
    public partial class Login: Form
    {
        private readonly string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\John Paul\\OneDrive\\Documents\\Accounts.mdf\";Integrated Security=True;Connect Timeout=30;Trust Server Certificate=True";

        SqlConnection conn;
        SqlCommand comm;

        public Login()
        {
            InitializeComponent();
        }

        private void signUpLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Register reg = new Register();
            reg.Owner = this;
            this.Hide();
            reg.ShowDialog();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            txtUsername.Text = "";
            txtBoxPassword.Text = "";
            txtBoxPassword.PasswordChar = '*';
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            //Establish Connection using SqlConnection
            conn = new SqlConnection(connectionString);

            //COUNT(*) returns the total number of rows in a table that match a condition.
            string command = "SELECT COUNT(*) FROM userAccounts " + "WHERE Username = @Username AND Password = @Password";

            //Open the connection
            conn.Open();

            //Establish the command using SqlCommand
            comm = new SqlCommand(command, conn);

            //Adds the value @Username with txtUsername.Text
            comm.Parameters.AddWithValue("@Username", txtUsername.Text);
            //Adds the value @Password with txtBoxPassword.Text
            comm.Parameters.AddWithValue("@Password", txtBoxPassword.Text);

            //✔️ Returns: A single value (object
            //Executes the query, and returns the first column of the first row in
            //the result set returned by the query. Additional columns or rows are ignored
            int count = (int)comm.ExecuteScalar();

            if(count > 0)
            {
                Accounts acc = new Accounts();
                acc.Owner = this;
                this.Hide();
                acc.ShowDialog();
                conn.Close();
            }
            else
            {
                MessageBox.Show("Wrong credentials");
            }
        }
    }
}
