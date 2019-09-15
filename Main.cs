using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Authentication_System
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        string username;

        private void Main_Load(object sender, EventArgs e)
        {
            Logowanie login = new Logowanie();
            login.ShowDialog();

            username = login.usernames;
            lblWelcome.Text = "Witaj, " + username +" !";
        }

        private void LblWelcome_Click(object sender, EventArgs e)
        {

        }
    }
}
