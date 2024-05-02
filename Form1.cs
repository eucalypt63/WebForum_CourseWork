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
        Authorization authorization = new Authorization();
        public WindowForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            authorization.AuthorizationFormIni(this);
        }
    }
}
