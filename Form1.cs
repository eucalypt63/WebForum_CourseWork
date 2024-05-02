using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebForum.forms;

namespace WebForum
{
    public partial class WindowForm : Form
    {
        static string connectionString = "server=localhost;user=root;database=WebForum";
        static MySqlConnection connection = new MySqlConnection(connectionString);
       
        public WindowForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            connection.Open();
            Authorization authorization = new Authorization();
            authorization.AuthorizationFormIni(this, connection);
        }

        private void WindowForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            connection.Close();
        }
    }
}
