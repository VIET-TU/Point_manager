using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PointManagement
{
    public partial class CreateStudentForm : Form
    {
        Connection db = new Connection();
        public CreateStudentForm()
        {
            InitializeComponent();

            comboBox_gender.Items.AddRange(new object[] { "Male", "FeMale" });

            comboBox_class.Items.AddRange(new object[] { "12A1", "12A2", "12A3", "12A4" });

        }


        private void CreateStudentForm_Load(object sender, EventArgs e)
        {

            dataGridView1.DataSource = db.ExecuteQuery($"select * from Student");

            textBox_id.Text = "";
            textBox_surname.Text = "";
            comboBox_class.Text = "";
            comboBox_gender.Text = "";
            dateTimePicker_birthday.Value = new DateTime(2006, 1, 1);
            textBox_address.Text = "";

            textBox_id.ReadOnly = false;


        }

        // button create
        private void button2_Click(object sender, EventArgs e)
        {
            string id = textBox_id.Text;
            string surname = textBox_surname.Text;
            string class_ = comboBox_class.Text;
            string gender = comboBox_gender.Text;
            DateTime birthday = dateTimePicker_birthday.Value;
            string address = textBox_address.Text;

            if (!string.IsNullOrEmpty(id) &&
                !string.IsNullOrEmpty(surname) &&
                !string.IsNullOrEmpty(class_) &&
                !string.IsNullOrEmpty(gender) &&
                birthday != null &&
                !string.IsNullOrEmpty(address))
            {
                // kiem tra ma hoc sinh da ton tai hay chua
                DataTable student = db.ExecuteQuery($"select * from Student where studentId = '{id}'");
                if (student.Rows.Count > 0)
                {
                    MessageBox.Show("Students already exist");
                }

                db.ExecuteNonQuery($"INSERT INTO Student  VALUES  ('{id}', N'{surname}', '{class_}', N'{gender}', '{birthday}', N'{address}')");
                db.ExecuteNonQuery($"insert into [User] values ('{id}','{id}','student','{id}')");
                MessageBox.Show("Create student success");


                CreateStudentForm_Load(sender, e);


            }
            else
            {

                MessageBox.Show("Please enter complete information.");
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var id = dataGridView1[0, e.RowIndex].Value.ToString();

                if (id.Length > 0)
                {
                    var student = db.ExecuteQuery($"select * from Student where studentId = '{id}'").Rows[0];
                    if (student != null)
                    {
                        textBox_id.Text = student["studentId"].ToString();
                        textBox_surname.Text = student["surname"].ToString();
                        comboBox_class.Text = student["className"].ToString();
                        comboBox_gender.Text = student["gender"].ToString();
                        dateTimePicker_birthday.Value = DateTime.Parse(student["birthday"].ToString());
                        textBox_address.Text = student["address"].ToString();

                        textBox_id.ReadOnly = true;


                    }
                    else
                    {
                        MessageBox.Show("Students not  exist");

                    }
                }

            }
        }

        private void button_update_Click(object sender, EventArgs e)
        {
            string id = textBox_id.Text;
            string surname = textBox_surname.Text;
            string class_ = comboBox_class.Text;
            string gender = comboBox_gender.Text;
            DateTime birthday = dateTimePicker_birthday.Value;
            string address = textBox_address.Text;

            if (!string.IsNullOrEmpty(id) &&
                !string.IsNullOrEmpty(surname) &&
                !string.IsNullOrEmpty(class_) &&
                !string.IsNullOrEmpty(gender) &&
                birthday != null &&
                !string.IsNullOrEmpty(address))
            {
                // kiem tra ma hoc sinh da ton tai hay chua
              

                db.ExecuteNonQuery($"UPDATE Student SET  surname = N'{surname}', className = '{class_}', gender = N'{gender}', birthday = '{birthday}', address = N'{address}' WHERE studentId = '{id}';");
                MessageBox.Show("Update student success");


                CreateStudentForm_Load(sender, e);


            }
            else
            {

                MessageBox.Show("Please enter complete information.");
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button_delete_Click(object sender, EventArgs e)
        {
            string id = textBox_id.Text;

            if (!string.IsNullOrEmpty(id))
               
            {
                // kiem tra ma hoc sinh da ton tai hay chua

                db.ExecuteNonQuery($"delete from Student where studentId = '{id}'");
                db.ExecuteNonQuery($"delete from [User] where username = '{id}'");
                MessageBox.Show("Delete student success");
                CreateStudentForm_Load(sender, e);


            }
            else
            {

                MessageBox.Show("Please enter complete information.");
            }
        }
    }
}
