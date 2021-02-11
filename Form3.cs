using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoginForm
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }
        public void loaddata()
        {
            
            pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            string connectionString = "Data Source=.;Initial Catalog=Login;Integrated Security=True";
            string sql = "SELECT *FROM Yorum1 ";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlDataAdapter dataadapter = new SqlDataAdapter(sql, connection);
            DataSet ds = new DataSet();
            connection.Open();

            dataadapter.Fill(ds, "Yorum1");

            connection.Close();
            dataGridView1.DataSource = ds;
            dataGridView1.DataMember = "Yorum1";
            this.dataGridView1.Columns["ID"].Visible = false;
            this.dataGridView1.Columns[1].HeaderText = "Kullanıcı";

            textBox1.Clear();
        }
        
        private void Form3_Load(object sender, EventArgs e)
        {
            loaddata();
            //this.dataGridView1.Columns["ID"].Visible = false;


        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }
        private void button1_Click(object sender, EventArgs e)
        {
            Form1 frm1 = new Form1();
            frm1.Show();
            this.Close();
        }               
        private void button2_Click_1(object sender, EventArgs e)
        {
            if (textBox1.Text !="")
            {
                string sql = "Data Source =.; Initial Catalog = Login; Integrated Security = SSPI";
                SqlConnection con = new SqlConnection(sql);
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                dataGridView1.Columns[0].Width = 90;
                cmd.CommandText = "INSERT INTO Yorum1(Kullanici_Adi,Yorum) VALUES('" + Form1.userisim + "','" + textBox1.Text + "')";


                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            else { MessageBox.Show("Yorum Yazınız Lütfen"); }
            
            loaddata();

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

       
       
        
    }
}
