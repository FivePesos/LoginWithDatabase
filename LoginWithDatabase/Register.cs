using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.Data;
using Microsoft.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;

namespace LoginWithDatabase
{
    public partial class Register: Form
    {
        SqlConnection conn;
        SqlCommand command;

        public readonly string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\John Paul\\OneDrive\\Documents\\Accounts.mdf\";Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True";
        public Register()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Register_Load(object sender, EventArgs e)
        {
            txtBoxConfirmPassword.Text = "";
            txtBoxPassword.Text = "";
            txtBoxPassword.PasswordChar = '*';
            txtBoxConfirmPassword.PasswordChar = '*';
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            //If the the password and confirm password do not match we throw an error
            if (txtBoxPassword.Text != txtBoxConfirmPassword.Text)
            {
                MessageBox.Show("Passwords do not match!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtBoxUsername.Text = "";
                txtBoxPassword.Text = "";
                txtBoxConfirmPassword.Text = "";
            }

            //Establish the sql connection using the SqlConnection
            conn = new SqlConnection(connectionString);

            //Open the connection
            conn.Open();
            
            //Command to add username and password to the database
            String comm = "INSERT INTO userAccounts(Username, Password) VALUES (@Username, @Password)";

            //Establish the command using the SqlCommand class
            command = new SqlCommand(comm, conn);

            //used to safely pass values into a SQL query when using SqlCommand
            //AddWithValue("@parameterName", value)
            //@parameterName is the placeholder in the SQL Query
            //value is the actual value to be inserted
            command.Parameters.AddWithValue("@Username", txtBoxUsername.Text); //Adds the prompted username to the database
            command.Parameters.AddWithValue("@Password", txtBoxPassword.Text); //Adds the prompted password to the database

            try
            {
                //ExecuteNonQuery to execute the command 
                command.ExecuteNonQuery();
                MessageBox.Show("Sign-up successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtBoxUsername.Text = "";
                txtBoxPassword.Text = "";
                txtBoxConfirmPassword.Text = "";
                conn.Close();
            }
            catch (SqlException ex)
            {
                //2627 is an SQL Server error code
                //error number 2627, which means a UNIQUE constraint violation (duplicate entry).
                if (ex.Number == 2627) 
                    MessageBox.Show("Username already exists!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                    MessageBox.Show("Database error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if(this.Owner != null)
            {
                this.Hide();
                this.Owner.Show();
            }
        }
    }
}
