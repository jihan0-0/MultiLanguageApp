using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace LanguageApp
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=localhost;Initial Catalog=MultiLanguageDB;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT COUNT(*) FROM Users WHERE Username = @Username AND Password = @Password";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Username", txtUsername.Text);
                        command.Parameters.AddWithValue("@Password", txtPassword.Text);
                        int userCount = (int)command.ExecuteScalar();
                        if (userCount > 0)
                        {
                            // Login berhasil, buka form utama
                            MainForm mainForm = new MainForm();
                            mainForm.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Username atau Password salah.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Terjadi kesalahan: " + ex.Message);
                }
            }
        }

        private void btnCreateAccount_Click(object sender, EventArgs e)
        {
            // Buka form pembuatan akun baru
            CreateAccountForm createAccountForm = new CreateAccountForm();
            createAccountForm.Show();
            this.Hide();
        }
    }
}
