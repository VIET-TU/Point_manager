using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PointManagement
{
    public partial class SignInForm : Form
    {
        Connection db = new Connection();
        public SignInForm()
        {
            InitializeComponent();
        }

        private void button_login_Click(object sender, EventArgs e)
        {
            string username = textBox_username.Text.Trim();
            string password = textBox_passwrod.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Error: Enter missing information");
            }
            else
            {
                var user = db.ExecuteQuery($"SELECT * FROM [User] WHERE username = '{username}' AND password = '{password}'");

                if (user.Rows.Count > 0)
                {
                    string role = user.Rows[0]["role"].ToString();

                    if (role.Equals("admin", StringComparison.OrdinalIgnoreCase))
                    {
                        OpenAdminForm();
                    }
                    else
                    {
                        OpenStudentForm(username);
                    }
                }
                else
                {
                    MessageBox.Show("Sign-in failed");
                }
            }


        }

        void OpenAdminForm()
        {
            AdminForm adminForm = new AdminForm();
            adminForm.Show();
            this.Hide();
        }

        void OpenStudentForm(string username)
        {
            StudentForm studentForm = new StudentForm(username);
            studentForm.Show();
            this.Hide();
        }

        private void SignInForm_Load(object sender, EventArgs e)
        {

        }
    }
}
