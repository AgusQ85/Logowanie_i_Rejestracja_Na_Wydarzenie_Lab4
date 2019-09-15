using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

namespace Authentication_System
{
    public partial class Logowanie : Form
    {
        public Logowanie()
        {
            InitializeComponent();
        }

        public string usernames;
        SprawdzW_BD auth;

        private void BtnRegister_Click(object sender, EventArgs e)
        {
            Rejestracja reg = new Rejestracja();
            reg.ShowDialog();
        }


        private void BtnLogin_Click(object sender, EventArgs e)
        {
            if (txtUsername.Text != string.Empty
                && txtPassword.Text != string.Empty)
            {
                checkAccount(txtUsername.Text, txtPassword.Text);
            }
        }

        private void checkAccount (string username, string password)
        {
            auth = new SprawdzW_BD();
            auth.getConnection();

            try
            {
                using (SQLiteConnection con = new SQLiteConnection(auth.connectionString))
                {
                    con.Open();
                    SQLiteCommand cmd = new SQLiteCommand();
                    string query = @"SELECT * FROM Agus WHERE Username='" + username +"'";

                    int count = 0;
                    cmd.CommandText = query;
                    cmd.Connection = con;

                    SQLiteDataReader read = cmd.ExecuteReader();
                    while (read.Read())
                    {
                        count++;
                    }

                    if (count == 1)
                    {
                        MessageBox.Show("Zalogowano pomyslnie!", "Komunikat poprawnego logowania", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        usernames = username;
                        this.Close();
                    }

                    else
                    {
                        MessageBox.Show("Podany uzytkownik nie istnieje lub podales nieprawidlowy login lub haslo.", "Bledne dane", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }




          
        }
    }
}
