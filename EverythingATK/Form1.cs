using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace EverythingATK
{
    public partial class everythingATK : Form
    {
        public everythingATK()
        {
            InitializeComponent();
        }      

        private void emptyInput()
        {
            String title = "Data invaild";
            String message = "The data is invalid, please check the data again";
            MessageBox.Show(message, title);
        }

        private void insertInto( String ID, String name, int price, int stock )
        {
            SqlConnection connection = new SqlConnection(@"Data source=SENDIKO; Initial Catalog=everythingatk; Integrated Security=true; TrustServerCertificate=True;");
            connection.Open();
            String insertQuery = "INSERT INTO atk (id, name, price, stock) VALUES ('" + ID + "', '" + name + "', '" + price + "', '" + stock + "')";
            SqlCommand command = new SqlCommand(insertQuery, connection);
            SqlDataReader reader = command.ExecuteReader();
            connection.Close();
            String title = "Query update";
            String message = "Data successfully created!";
            DialogResult result = MessageBox.Show(message, title);
            if (result == DialogResult.OK)
            {
                loadIntoDataGridView();
                this.textBox1.Text = "";
                this.textBox2.Text = "";
                this.textBox3.Text = "";
                this.textBox4.Text = "";
            }
        }

        private void updateData( String ID, String name, int price, int stock )
        {
            SqlConnection connection = new SqlConnection(@"Data source=SENDIKO; Initial Catalog=everythingatk; Integrated Security=true; TrustServerCertificate=True;");
            connection.Open();
            String updateQuery = "UPDATE atk SET name = '"+ name +"', price = '"+ price +"', stock = '"+ stock +"' WHERE id = '"+ ID +"';";
            SqlCommand command = new SqlCommand(updateQuery, connection);
            SqlDataReader reader = command.ExecuteReader();
            String title = "Query update";
            String message = "Data successfully updated!";
            DialogResult result = MessageBox.Show(message, title);
            if (result == DialogResult.OK)
            {
                loadIntoDataGridView();
                this.textBox1.Text = "";
                this.textBox2.Text = "";
                this.textBox3.Text = "";
                this.textBox4.Text = "";
            }
        }

        private void deleteData( String ID )
        {
            SqlConnection connection = new SqlConnection(@"Data source=SENDIKO; Initial Catalog=everythingatk; Integrated Security=true; TrustServerCertificate=True;");
            connection.Open();
            String deleteQuery = "DELETE FROM atk WHERE id = '" + ID + "'";
            SqlCommand command = new SqlCommand(deleteQuery, connection);
            SqlDataReader reader = command.ExecuteReader();
            connection.Close();
            String title = "Query update";
            String message = "Data successfully deleted!";
            DialogResult result = MessageBox.Show(message, title);
            if (result == DialogResult.OK)
            {
                loadIntoDataGridView();
                this.textBox1.Text = "";
                this.textBox2.Text = "";
                this.textBox3.Text = "";
                this.textBox4.Text = "";
            }
        }

        private void loadIntoTextBox(String dataId, String dataName, String dataPrice, String dataStock)
        {
            this.textBox1.Text = dataId;
            this.textBox2.Text = dataName;
            this.textBox3.Text = dataPrice;
            this.textBox4.Text = dataStock;
        }
        
        private void loadIntoDataGridView()
        { 
            SqlConnection connection = new SqlConnection(@"Data source=SENDIKO; Initial Catalog=everythingatk; Integrated Security=true; TrustServerCertificate=True;");
            connection.Open();
            String readQuery = "SELECT * FROM atk";
            SqlCommand command = new SqlCommand(readQuery, connection);
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            DataTable data = new DataTable();
            adapter.Fill(data);
            dataGridView1.DataSource = data;
            connection.Close();
        }

        private int convertIntoInt(String data)
        {
            return int.Parse(data);
        }

        private bool checkIfEmpty(String ID, String name, String price, String stock )
        { 
            if (ID.Length == 0 && name.Length == 0 && price.Length == 0 && stock.Length == 0)
            {
                return true;
            } else
            {
                return false;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            loadIntoDataGridView();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            String ID = this.textBox1.Text;
            String name = this.textBox2.Text;
            String pricee = this.textBox3.Text;
            String stockk  = this.textBox4.Text;
            if (checkIfEmpty(ID, name, pricee, stockk))
            {
                emptyInput();
            } else
            {
                int price = convertIntoInt(pricee);
                int stock = convertIntoInt(stockk);
                insertInto(ID, name, price, stock);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            String dataId = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            String dataName = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            String dataPrice = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            String dataStock = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            loadIntoTextBox(dataId, dataName, dataPrice, dataStock);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            String ID = this.textBox1.Text;
            String name = this.textBox2.Text;
            String pricee = this.textBox3.Text;
            String stockk = this.textBox4.Text;
            if (checkIfEmpty(ID, name, pricee, pricee))
            {
                emptyInput();
            } else
            {
                int price = convertIntoInt(pricee);
                int stock = convertIntoInt(stockk);
                updateData(ID, name, price, stock);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            String ID = this.textBox1.Text;
            if(ID.Length== 0)
            {
                emptyInput();
            } else
            {
                deleteData(ID);
            }
        }
    }
}
