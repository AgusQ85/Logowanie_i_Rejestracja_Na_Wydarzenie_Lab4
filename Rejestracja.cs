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
    public partial class Rejestracja : Form
    {
        public Rejestracja()
        {
            InitializeComponent();
        }

        private void TextBox4_TextChanged(object sender, EventArgs e)
        {

        }
                SprawdzW_BD auth;
        private void BtnRegister_Click(object sender, EventArgs e)
        {
            if (txtUsername.Text != string.Empty
                && txtPassword.Text != string.Empty
                && txtConfirmPassword.Text != string.Empty
                && txtEmail.Text != string.Empty)

            {
                if (txtPassword.Text == txtConfirmPassword.Text)
                {
                    checkAccount(txtUsername.Text);
                }
                else
                {
                    MessageBox.Show("Hasla nie sa takie same!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void checkAccount(string username)
        {
            auth = new SprawdzW_BD();

            auth.createDatabase();
            auth.getConnection();

            try {
                using (SQLiteConnection con = new SQLiteConnection(auth.connectionString))
                {
                    SQLiteCommand cmd = new SQLiteCommand();
                    con.Open();

                    int count = 0;
                    string query = @"SELECT * FROM Agus WHERE Username='" + username + "'";
                    cmd.CommandText = query;
                    cmd.Connection = con;

                    SQLiteDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        count++;
                    }
                    if (count == 1)
                    {
                        MessageBox.Show("Cos tam komunikat 33333", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    else if (count == 0)
                    {
                        insertData(txtUsername.Text, txtPassword.Text, txtEmail.Text);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }

        private void insertData(string usernames, string password, string email)
        {
            auth = new SprawdzW_BD();
            auth.getConnection();


            try
            {
                using (SQLiteConnection con = new SQLiteConnection(auth.connectionString))
                {
                    con.Open();
                    SQLiteCommand cmd = new SQLiteCommand();
                    string query = @"INSERT INTO Agus (Username, Password, Email) VALUES (@username, @password, @email)";
                    cmd.CommandText = query;
                    cmd.Connection = con;

                    cmd.Parameters.Add(new SQLiteParameter("@username", usernames));
                    cmd.Parameters.Add(new SQLiteParameter("@password", password));
                    cmd.Parameters.Add(new SQLiteParameter("@email", email));
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Dokonales rejestracji na V semestr UTP!", "Rejestracja pomyslna!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            

        }

    }
}
