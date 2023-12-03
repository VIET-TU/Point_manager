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
    public partial class PointForm : Form
    {
        Connection db = new Connection();

        public PointForm()
        {
            InitializeComponent();
            var ids = db.ExecuteQuery("select * from Student");
            foreach (DataRow row in ids.Rows)
            {
                comboBox_sid.Items.Add(row["studentId"].ToString());
            }
            comboBox_class.Items.AddRange(new object[] { "12A1", "12A2", "12A3", "12A4" });



        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void button_create_Click(object sender, EventArgs e)
        {
            string student_id = comboBox_sid.Text;
            
         
            if( string.IsNullOrEmpty(student_id) || string.IsNullOrEmpty(textBox_match.Text) || string.IsNullOrEmpty(textBox_physical.Text)
                || string.IsNullOrEmpty(textBox_chemistry.Text) ) {
                MessageBox.Show("Please enter complete information.");
                return;
            }

            double match = double.Parse(textBox_match.Text);
            double physcial = double.Parse(textBox_physical.Text);
            double chemistry = double.Parse(textBox_chemistry.Text);


            if ( match < 0 || match > 10 || physcial < 0 || physcial > 10 || chemistry < 0
                    || chemistry > 10)
            {
                MessageBox.Show("Score must be between 0 and 10");
                return;

            }


            double average = (match + physcial + chemistry) / 3;

            var student = db.ExecuteQuery($"select * from Point where studentId = '{student_id}'");

            if(student.Rows.Count > 0)
            {
                
                MessageBox.Show("This student's grades already exist");
                return;
            }

            db.ExecuteNonQuery($"INSERT INTO Point  VALUES ('{student_id}', {match}, {physcial}, {chemistry}, {average})");

            PointForm_Load(sender, e);
            MessageBox.Show("Create score success");

        }

        private void PointForm_Load(object sender, EventArgs e)
        {
            comboBox_sid.SelectedIndex = -1;
            textBox_match.Text = string.Empty;
            textBox_physical.Text = string.Empty;
            textBox_chemistry.Text = string.Empty;
            comboBox_sid.Enabled = true;
            comboBox_class.SelectedIndex = -1;
            dataGridView1.DataSource = db.ExecuteQuery("SELECT  Point.id, s.studentId , s.surname,s.className ,Point.match, Point.physical, Point.chemistry, Point.average FROM Point JOIN Student AS s ON s.studentId = Point.studentId; ");
        }

        private void button_update_Click(object sender, EventArgs e)
        {
            string student_id = comboBox_sid.Text;


            if (string.IsNullOrEmpty(student_id) || string.IsNullOrEmpty(textBox_match.Text) || string.IsNullOrEmpty(textBox_physical.Text)
                || string.IsNullOrEmpty(textBox_chemistry.Text))
            {
                MessageBox.Show("Please enter complete information.");
                return;
            }

            double match = double.Parse(textBox_match.Text);
            double physcial = double.Parse(textBox_physical.Text);
            double chemistry = double.Parse(textBox_chemistry.Text);


            if (match < 0 || match > 10 || physcial < 0 || physcial > 10 || chemistry < 0
                    || chemistry > 10)
            {
                MessageBox.Show("Score must be between 0 and 10");
                return;

            }

            double average = (match + physcial + chemistry) / 3;

            db.ExecuteNonQuery($"UPDATE Point SET match = {match}, physical = {physcial}, chemistry = {chemistry}, average = {average} WHERE studentId = '{student_id}';");

            PointForm_Load(sender, e);
            MessageBox.Show("Update score success");



        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var student_id = dataGridView1[1, e.RowIndex].Value.ToString();

                if (student_id.Length > 0)
                {
                    var student = db.ExecuteQuery($"select * from Point where studentId = '{student_id}'").Rows[0];
                    if (student != null)
                    {
                        comboBox_sid.Text = student["studentId"].ToString();
                        textBox_match.Text = student["match"].ToString();
                        textBox_physical.Text = student["physical"].ToString();
                        textBox_chemistry.Text = student["chemistry"].ToString();

                        comboBox_sid.Enabled = false;


                    }
                    else
                    {
                        MessageBox.Show("Students not  exist");

                    }
                }

            }
        }

        private void button_new_Click(object sender, EventArgs e)
        {
            PointForm_Load(sender,e);
        }

        private void comboBox_class_SelectedIndexChanged(object sender, EventArgs e)
        {
            string classname =  comboBox_class.Text;
                if(!string.IsNullOrEmpty(classname))
            {
                dataGridView1.DataSource = db.ExecuteQuery($"SELECT  Point.id, s.studentId , s.surname,s.className ,Point.match, Point.physical, Point.chemistry, Point.average FROM Point JOIN Student AS s ON s.studentId = Point.studentId where s.className = '{classname}'");
            }
        }

        private void button_delete_Click(object sender, EventArgs e)
        {
            string student_id = comboBox_sid.Text;


            if (string.IsNullOrEmpty(student_id) )
            {
                MessageBox.Show("Please enter complete information.");
                return;
            }

            db.ExecuteNonQuery($"delete from Point where studentId = '{student_id}'");

            PointForm_Load(sender, e);
            MessageBox.Show("Delete score success");
        }
    }
}
