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
    public partial class StudentForm : Form
    {
        public string username;
        public string studentid;
        Connection db = new Connection();
        public StudentForm(string username)
        {
            InitializeComponent();
            this.username = username;
            comboBox_gender.Items.AddRange(new object[] { "Male", "FeMale" });

            this.studentid = db.ExecuteQuery($"select * from [User] join Student on Student.studentId = [User].studentId where username = '{username}'").Rows[0]["studentId"].ToString();
        }

        private void button_update_Click(object sender, EventArgs e)
        {
            if(button_update.Text.Equals("Save"))
            {
                string id = textBox_id.Text;
                string surname = textBox_surname.Text;
                string gender = comboBox_gender.Text;
                DateTime birthday = dateTimePicker_birthday.Value;
                string address = textBox_address.Text;

                if (!string.IsNullOrEmpty(id) &&
                    !string.IsNullOrEmpty(surname) &&
                    !string.IsNullOrEmpty(gender) &&
                    birthday != null &&
                    !string.IsNullOrEmpty(address))
                {

                    db.ExecuteNonQuery($"UPDATE Student SET  surname = N'{surname}', gender = N'{gender}', birthday = '{birthday}', address = N'{address}' WHERE studentId = '{id}';");
                    MessageBox.Show("Update student success");
                }
                else
                {

                    MessageBox.Show("Please enter complete information.");
                }

                StudentForm_Load(sender, e);
                button_update.Text = "Update";

                return;

            }

            textBox_surname.ReadOnly = !true;
            comboBox_gender.Enabled = !false;
            dateTimePicker_birthday.Enabled = !false;
            textBox_address.Enabled = !false;

            button_update.Text = "Save";
        }

        private void StudentForm_Load(object sender, EventArgs e)
        {

            var student = db.ExecuteQuery($"select * from Student where studentId = '{studentid}'").Rows[0];
           

            textBox_id.Text = student["studentId"].ToString();
            textBox_surname.Text = student["surname"].ToString();
            textBox_class.Text = student["className"].ToString();
            comboBox_gender.Text = student["gender"].ToString();
            dateTimePicker_birthday.Value = DateTime.Parse(student["birthday"].ToString());
            textBox_address.Text = student["address"].ToString();

            textBox_surname.ReadOnly = true;
            comboBox_gender.Enabled = false;
            dateTimePicker_birthday.Enabled = false;
            textBox_address.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button_change_passwrod_Click(object sender, EventArgs e)
        {
            (new ChangePasswrod(username)).ShowDialog();
        }

        private void button_logout_Click(object sender, EventArgs e)
        {
            (new SignInForm()).Show();
            this.Close();
        }

        private void button_class_member_Click(object sender, EventArgs e)
        {
            string className = db.ExecuteQuery($"select * from Student where studentId = '{studentid}'").Rows[0]["className"].ToString();
            (new ClassMemberForm(className)).ShowDialog();
        }

        private void button_score_Click(object sender, EventArgs e)
        {
            (new ScoreForm(studentid)).ShowDialog();
        }
    }
}
