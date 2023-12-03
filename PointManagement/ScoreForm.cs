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
    public partial class ScoreForm : Form
    {
        public string studentid;
        Connection db = new Connection();
        public ScoreForm( string studentid)
        {
            InitializeComponent();
            this.studentid = studentid;
        }

        private void ScoreForm_Load(object sender, EventArgs e)
        {
            var student = db.ExecuteQuery($"select * from Point where studentId = '{studentid}'");
            if(student.Rows.Count > 0 )
            {
              var   row = student.Rows[0];

                    textBox_match.Text = row["match"].ToString();
                    textBox_physical.Text = row["physical"].ToString();
                    textBox_chemistry.Text = row["chemistry"].ToString();
                    textBox_average.Text = row["average"].ToString();

                    string xl = "";
                    float average = float.Parse(row["average"].ToString());
                    switch (average)
                    {
                        case float grade when grade > 8.5:
                            xl = "Excellent";
                            break;
                        case float grade when grade > 7.0 && grade < 8.4:
                            xl = "Good";
                            break;
                        case float grade when grade > 5.5 && grade < 6.9:
                            xl = "Average";
                            break;
                        default:
                            xl = "Below Average";
                            break;
                    }

                    textBox_classification.Text = xl;

                
            } else
            {
                MessageBox.Show("Students do not have scores yet");
            }
            
        }
    }
}
