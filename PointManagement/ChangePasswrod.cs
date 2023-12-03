using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace PointManagement
{
    public partial class ChangePasswrod : Form
    {
        public string username;
        Connection db = new Connection();
        public ChangePasswrod(string username)
        {
            InitializeComponent();
            this.username = username;
        }

        private void button_save_Click(object sender, EventArgs e)
        {
            string old_password = textBox_old.Text;
            string new_password = textBox_new.Text;

            if (
                !string.IsNullOrEmpty(old_password) &&
                !string.IsNullOrEmpty(new_password) )
            {
                var user = db.ExecuteQuery($"select * from [User] where username = '{username}' and password = '{old_password}'").Rows[0];
               
                db.ExecuteNonQuery($"update [User] set password = '{new_password}' where username = '{username}'");
                MessageBox.Show("Update new passwrod successfully");
                this.Close();

            }
            else
            {
                MessageBox.Show("Missing information");

            }
        }
    }
}
