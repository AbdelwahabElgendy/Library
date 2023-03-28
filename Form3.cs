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
    public partial class Form3 : Form
    {
        private int ID;
        public Form3(int ID)
        {
            InitializeComponent();
            this.ID = ID;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void Form3_Load(object sender, EventArgs e)
        {
            

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
        
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            MySqlConnection connection = new MySqlConnection("server=localhost;port=3306;database=library;uid=root;password=root;");
            string query = "SELECT * FROM officers WHERE officer_name LIKE @value";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@value", "%" + textBox1.Text + "%");
            connection.Open();
            DataTable dataTable = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            adapter.Fill(dataTable);
            DataView.DataSource = dataTable;
            connection.Close();
            if (DataView.RowCount == 0)
            {
                const string message = "لم يتم العثور على ظابط بالمعلومات المدخلة";
                const string caption = "تحذير";
                var result = MessageBox.Show(message, caption,
                                             MessageBoxButtons.OK,
                                             MessageBoxIcon.Error);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (DataView.SelectedRows.Count == 0)
            {
                const string message = "برجاء إختيار ظابط من القائمة السابقة";
                const string caption = "تحذير";
                var result = MessageBox.Show(message, caption,
                                             MessageBoxButtons.OK,
                                             MessageBoxIcon.Error);
            }
            else 
            {
                DataGridViewRow row = DataView.SelectedRows[0];
                int BOOKID = ID;
                string OFFICERID = row.Cells["id"].Value.ToString();
                //label4.Text = OFFICERID;
                string connectionString = "server=localhost;user id=root;password=root;database=library"; 

                int count = 0; 

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open(); 

                    string query = "SELECT COUNT(idbookshistory) FROM bookshistory"; 

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        count = Convert.ToInt32(command.ExecuteScalar());
                    }
                    count++;
                    DateTime today = DateTime.Today; // Get today's date

                    string formattedDate = today.ToString("yyyy-MM-dd");
                    query = "INSERT INTO bookshistory (idbookshistory,BookID,OfficerID,StartDate) VALUES (@idbookshistory, @BookID, @OfficerID,@StartDate)"; // Parameterized query with placeholders for variable values

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@idbookshistory", count); // Add parameter values to the command object
                        command.Parameters.AddWithValue("@BookID", BOOKID);
                        command.Parameters.AddWithValue("@OfficerID", OFFICERID);
                        command.Parameters.AddWithValue("@StartDate", formattedDate);

                        int rowsAffected = command.ExecuteNonQuery(); // Execute the query and get the number of rows affected

                        
                        string updateQuery = "UPDATE books SET availability = 0 WHERE id = @ID";

                            
                        MySqlCommand command2 = new MySqlCommand(updateQuery, connection);

                            
                        command2.Parameters.AddWithValue("@ID", BOOKID);
                        command2.ExecuteNonQuery();
                        
                    }
                    const string message = "تم تسجيل الاستعارة بنجاح";
                    const string caption = "عملية ناجحة";
                    var result = MessageBox.Show(message, caption,
                                                 MessageBoxButtons.OK,
                                                 MessageBoxIcon.Information);
                    if (result == DialogResult.OK)
                    {

                        Form2 form2 = new Form2();
                        form2.Show();
                        this.Hide();
                    }

                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
           
        }
    }
}
