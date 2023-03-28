using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Scientific_Library
{
    public partial class Form2 : Form
    {


        public Form3 f3;
        public Form2()
        {
            InitializeComponent();

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            radioButton3.Checked = true;
            radioButton4.Checked = true;

            using (MySqlConnection connection = new MySqlConnection("server=localhost;user id=root;password=root;database=library"))
            {
                connection.Open();
                string query = "SELECT DISTINCT subject FROM books";
                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string value = reader.GetString(0);
                            comboBox1.Items.Add(value);
                        }
                    }
                }
                connection.Close();
            }


            string connectionString = "server=localhost;user id=root;password=root;database=library";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM books WHERE availability = 1";
                MySqlCommand command = new MySqlCommand(query, connection);
                connection.Open();
                int count = Convert.ToInt32(command.ExecuteScalar());
                connection.Close();
                label1.Text = "عدد الكتب المتاحة :" + count.ToString();

            }


            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM books WHERE availability = 0";
                MySqlCommand command = new MySqlCommand(query, connection);
                connection.Open();
                int count = Convert.ToInt32(command.ExecuteScalar());
                connection.Close();
                label2.Text = "عدد الكتب المهداة:" + count.ToString();

            }

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM books";
                MySqlCommand command = new MySqlCommand(query, connection);
                connection.Open();
                int count = Convert.ToInt32(command.ExecuteScalar());
                connection.Close();
                label3.Text = "عدد الكتب في المكتبة:" + count.ToString();

            }



        }

        private void Exit_Click(object sender, EventArgs e)
        {
            
            Application.Exit();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = -1;
            MySqlConnection connection = new MySqlConnection("server=localhost;user=root;password=root;database=library");
            connection.Open();
            MySqlCommand command = new MySqlCommand("SELECT * FROM books where books.availability = 1; ", connection);
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            DataView.DataSource = dataTable;

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = -1;
            MySqlConnection connection = new MySqlConnection("server=localhost;user=root;password=root;database=library");
            connection.Open();
            MySqlCommand command = new MySqlCommand("SELECT * FROM books where books.availability = 0;", connection);
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            DataView.DataSource = dataTable;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = -1;

            MySqlConnection connection = new MySqlConnection("server=localhost;user=root;password=root;database=library");
            connection.Open();
            MySqlCommand command = new MySqlCommand("SELECT * FROM books ;", connection);
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            DataView.DataSource = dataTable;

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Borrow_Click(object sender, EventArgs e)
        {
            if (DataView.SelectedRows.Count > 0 )
            {
                DataGridViewRow row = DataView.SelectedRows[0];
                if (row.Cells["Availability"].Value.ToString()=="1")

                {
                    
                    int ID = Convert.ToInt32(row.Cells["id"].Value);
                    this.Hide();
                    f3 = new Form3(ID);
                    f3.Show();
                }
                else 
                {
                    
                    const string message = "برجاء اختيار كتاب من قائمة الكتب المتاحة";
                    const string caption = "تحذير";
                    var result = MessageBox.Show(message, caption,
                                                 MessageBoxButtons.OK,
                                                 MessageBoxIcon.Error);
                }
            }
            else 
            {
                const string message = "برجاء اختيار كتاب من القائمة السابقة";
                const string caption = "تحذير";
                var result = MessageBox.Show(message, caption,
                                             MessageBoxButtons.OK,
                                             MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = -1;
            if (radioButton4.Checked == false && radioButton5.Checked == false)
            {
                const string message = "برجاء اختيار مؤشر للبحث";
                const string caption = "تحذير";
                var result = MessageBox.Show(message, caption,
                                             MessageBoxButtons.OK,
                                             MessageBoxIcon.Error);
            }
            else if (textBox1.Text == "")
            {
                const string message = "برجاء ادخال بيانات للبحث";
                const string caption = "تحذير";
                var result = MessageBox.Show(message, caption,
                                             MessageBoxButtons.OK,
                                             MessageBoxIcon.Error);
            }
            else 
            {
                if (radioButton4.Checked)
                {
                    MySqlConnection connection = new MySqlConnection("server=localhost;port=3306;database=library;uid=root;password=root;");
                    string query = "SELECT * FROM books WHERE BookName LIKE @value";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    //cmd.Parameters.AddWithValue("@value", textBox1.Text);
                    cmd.Parameters.AddWithValue("@value", "%" + textBox1.Text + "%");
                    connection.Open();
                    DataTable dataTable = new DataTable();
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    adapter.Fill(dataTable);
                    DataView.DataSource = dataTable;
                    connection.Close();
                    if (DataView.RowCount == 0)
                    {
                        const string message = "لم يتم العثور على كتاب بالمعلومات المدخلة";
                        const string caption = "تحذير";
                        var result = MessageBox.Show(message, caption,
                                                     MessageBoxButtons.OK,
                                                     MessageBoxIcon.Error);
                    }
                }
                else if (radioButton5.Checked)
                {
                    MySqlConnection connection = new MySqlConnection("server=localhost;port=3306;database=library;uid=root;password=root;");
                    string query = "SELECT * FROM books WHERE BookSerial LIKE @value";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    //cmd.Parameters.AddWithValue("@value", textBox1.Text);
                    cmd.Parameters.AddWithValue("@value", "%" + textBox1.Text + "%");
                    connection.Open();
                    DataTable dataTable = new DataTable();
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    adapter.Fill(dataTable);
                    DataView.DataSource = dataTable;
                    connection.Close();
                    if (DataView.RowCount == 0) 
                    {
                        const string message = "لم يتم العثور على كتاب بالمعلومات المدخلة";
                        const string caption = "تحذير";
                        var result = MessageBox.Show(message, caption,
                                                     MessageBoxButtons.OK,
                                                     MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex < 0)
            {
                DataView.DataSource = null;
            }
            else 
            {
                string selectedItem = comboBox1.SelectedItem.ToString();
                string query = "";
                MySqlConnection connection = new MySqlConnection("server=localhost;port=3306;database=library;uid=root;password=root;");
                if (radioButton3.Checked)
                {
                    query = "SELECT * FROM books WHERE Subject = @SelectedItem"; 
                }
                else if (radioButton1.Checked)
                {
                    query = "SELECT * FROM books WHERE Subject = @SelectedItem and availability = 1";
                }
                else if (radioButton2.Checked)
                {
                    query = "SELECT * FROM books WHERE Subject = @SelectedItem and availability = 0";
                }
                
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@SelectedItem", comboBox1.SelectedItem.ToString());
                connection.Open();
                DataTable dataTable = new DataTable();
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(dataTable);
                DataView.DataSource = dataTable;
                connection.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button3.Enabled = true;
            MySqlConnection connection = new MySqlConnection("server=localhost;port=3306;database=library;uid=root;password=root;");
            string query = "SELECT bookshistory.idbookshistory, books.bookname,officers.officer_name , bookshistory.StartDate,bookshistory.EndDate,books.availability FROM books INNER JOIN officers inner join bookshistory ON bookshistory.bookid = books.id and bookshistory.OfficerID = officers.id";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            connection.Open();
            DataTable dataTable = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            adapter.Fill(dataTable);
            DataView.DataSource = dataTable;
            connection.Close();
            if (DataView.RowCount == 0)
            {
                const string message = "لم يتم العثور على تاريخ للكتب المستعارة";
                const string caption = "تحذير";
                var result = MessageBox.Show(message, caption,
                                             MessageBoxButtons.OK,
                                             MessageBoxIcon.Error);
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            if (DataView.SelectedRows.Count>0)
            {

                DataGridViewRow row = DataView.SelectedRows[0];
                if (row.Cells["availability"].Value.ToString() == "0" )
                {

                    const string message = "هل انت متأكد من قرار إعادة الكتاب";
                    const string caption = "تحذير";
                    var result = MessageBox.Show(message, caption,
                                                 MessageBoxButtons.YesNo,
                                                 MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        if (true) 
                        {
                            int BookHistoryID = Convert.ToInt32(row.Cells["idbookshistory"].Value);
                            string connectionString = "server=localhost;user id=root;password=root;database=library";

                            DateTime today = DateTime.Today; // Get today's date

                            string formattedDate = today.ToString("yyyy-MM-dd");
                            string query = "UPDATE bookshistory Set EndDate = @EndDate where idBooksHistory = @ID ";
                            string query2 = "UPDATE books SET availability = 1 WHERE books.id = (SELECT BookID FROM library.bookshistory WHERE idBooksHistory = @ID)";
                            using (MySqlConnection connection = new MySqlConnection(connectionString))
                            {

                                connection.Open();
                                MySqlCommand command = new MySqlCommand(query, connection);
                                command.Parameters.AddWithValue("@EndDate", formattedDate);
                                command.Parameters.AddWithValue("@ID", BookHistoryID);
                                command.ExecuteNonQuery();
                                command = new MySqlCommand(query2, connection);
                                command.Parameters.AddWithValue("@ID", BookHistoryID);
                                command.ExecuteNonQuery();

                            }

                        }
                        else { }

                        button3.Enabled = false;
                    }
                    else if (result == DialogResult.No)
                    {

                    }
                }
                else
                {
                    const string message = "برجاء إختيار كتاب من قائمة الكتب المستعارة";
                    const string caption = "تحذير";
                    var result = MessageBox.Show(message, caption,
                                                 MessageBoxButtons.OK,
                                                 MessageBoxIcon.Error);
                }
                
            }
            else 
            {
                const string message = "برجاء اختيار كتاب من قائمة الكتب المستعارة";
                const string caption = "تحذير";
                var result = MessageBox.Show(message, caption,
                                             MessageBoxButtons.OK,
                                             MessageBoxIcon.Error);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (DataView.SelectedRows.Count > 0)
            {
                DataGridViewRow row = DataView.SelectedRows[0];
                if (row.Cells["availability"].Value.ToString() == "0")
                {
                    int ID = Convert.ToInt32(row.Cells["id"].Value);
                    string connectionString = "server=localhost;user id=root;password=root;database=library";




                    string query = "UPDATE books SET availability = 1 WHERE books.id =  @ID";
                    using (MySqlConnection connection = new MySqlConnection(connectionString))
                    {

                        connection.Open();
                        MySqlCommand command = new MySqlCommand(query, connection);
                        command = new MySqlCommand(query, connection);
                        command.Parameters.AddWithValue("@ID", ID);
                        command.ExecuteNonQuery();

                    }
                }
                else
                {
                    const string message = "برجاء اختيار كتاب من قائمة الكتب المستعارة";
                    const string caption = "تحذير";
                    var result = MessageBox.Show(message, caption,
                                                 MessageBoxButtons.OK,
                                                 MessageBoxIcon.Error);
                }

            }
            else
            {
                const string message = "برجاء اختيار كتاب من قائمة الكتب المستعارة";
                const string caption = "تحذير";
                var result = MessageBox.Show(message, caption,
                                             MessageBoxButtons.OK,
                                             MessageBoxIcon.Error);
            }
        }



        //private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        //{
        //    // Close all forms except the main form
        //    foreach (Form form in Application.OpenForms)
        //    {
        //        if (form != this && form.GetType() != typeof(Form1))
        //        {
        //            form.Close();
        //        }
        //    }
        //}

    }
}
