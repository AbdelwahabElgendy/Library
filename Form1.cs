using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Scientific_Library
{
    public partial class Form1 : Form
    {
        public Form2 f2 = new Form2();
       
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "admin" && textBox2.Text == "admin1234")
            {
                const string message = "تسجيل دخول ناجح";
                const string caption = "نجاح";
                var result = MessageBox.Show(message, caption,
                                             MessageBoxButtons.OK,
                                             MessageBoxIcon.Information);
                if (result == DialogResult.OK)
                {
                    // cancel the closure of the form.
                    
                    f2.Show();
                    this.Hide();
                }
            }
            else 
            {
                const string message =
                     "خطأ في الدخول";
                const string caption = "إنذار";
                var result = MessageBox.Show(message, caption,
                                             MessageBoxButtons.OK,
                                             MessageBoxIcon.Warning);
                if (result == DialogResult.OK)
                {
                    // cancel the closure of the form.
                    
                }
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
