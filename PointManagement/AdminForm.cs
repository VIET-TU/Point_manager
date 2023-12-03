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
    public partial class AdminForm : Form
    {
        Connection db = new Connection();

        public AdminForm()
        {
            InitializeComponent();
        }

        // create student
        private void button_login_Click(object sender, EventArgs e)
        {
            (new CreateStudentForm()).ShowDialog();
        }

        // logout
        private void button2_Click(object sender, EventArgs e)
        {
            (new SignInForm()).Show();
            this.Close();
        }

        // create point
        private void button1_Click(object sender, EventArgs e)
        {
            (new PointForm()).ShowDialog();
        }

        private void AdminForm_Load(object sender, EventArgs e)
        {
            textBox_s.Text = db.ExecuteQuery($"select count(*) as count from Student").Rows[0]["count"].ToString();
            textBox_a1.Text = db.ExecuteQuery($"select count(*) as count from Student where className = '12A1'").Rows[0]["count"].ToString();
            textBox_a2.Text = db.ExecuteQuery($"select count(*) as count from Student where className = '12A2'").Rows[0]["count"].ToString();
            textBox_a3.Text = db.ExecuteQuery($"select count(*) as count from Student where className = '12A3'").Rows[0]["count"].ToString();
            textBox_a4.Text = db.ExecuteQuery($"select count(*) as count from Student where className = '12A4'").Rows[0]["count"].ToString();
            textBox_es.Text = db.ExecuteQuery($"select count(*) as count from Point where average > 8.5").Rows[0]["count"].ToString();
            textBox_gs.Text = db.ExecuteQuery($"select count(*) as count from Point where average > 7.0 and average < 8.4").Rows[0]["count"].ToString();
            textBox_as.Text = db.ExecuteQuery($"select count(*) as count from Point where average > 5.5 and average < 6.9").Rows[0]["count"].ToString();
            textBox_bas.Text = db.ExecuteQuery($"select count(*) as count from Point where average < 5.4").Rows[0]["count"].ToString();

        }
    }
}
