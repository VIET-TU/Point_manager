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
    public partial class ClassMemberForm : Form
    {
        Connection db = new Connection();
        public string className;
        public ClassMemberForm(string className)
        {
            InitializeComponent();
            this.className = className;
        }

        private void ClassMemberForm_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.ExecuteQuery($"select * from Student where className = '{className}'");
            textBox_surname.Text = string.Empty;
        }

        private void button_login_Click(object sender, EventArgs e)
        {
            var surname = textBox_surname.Text.Trim();
            if(surname.Length > 0)
            {
                dataGridView1.DataSource = db.ExecuteQuery($"select * from Student where className = '{className}' and lower(surname) like lower('%{surname}%')");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ClassMemberForm_Load(sender,e);
        }
    }
}
