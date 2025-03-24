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
    public partial class Accounts: Form
    {
        readonly private string connectionString = " Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\John Paul\\OneDrive\\Documents\\Accounts.mdf\";Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True";
        public Accounts()
        {
            InitializeComponent();
        }

        private void Accounts_Load(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            
            conn.Open();

            string comm = "SELECT * FROM userAccounts";

            SqlDataAdapter adapter = new SqlDataAdapter(comm, conn);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            accountsDataGrid.DataSource = dt;
            
        }

        private void accountsDataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
