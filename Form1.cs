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
    public partial class Form1 : Form
    {
        SqlConnection con;
        SqlCommand cmd1;
        SqlDataReader dr1;
        public Form1()
        {
            InitializeComponent();
        }
        public static string userisim;
        private void button3_Click(object sender, EventArgs e)
        {
            userisim = textBox1.Text;
            string user = textBox1.Text;
            string pass = textBox2.Text;
            con = new SqlConnection("server=.; Initial Catalog=Login;Integrated Security=SSPI");//bağlantı
            cmd1 = new SqlCommand();
            con.Open();
            cmd1.Connection = con;
            cmd1.CommandText = "SELECT * FROM Users where KullaniciAdi='" + user + "' AND Sifre='" + pass + "'";//datadaki kadi ve sifreleri getirir
            dr1 = cmd1.ExecuteReader();
            if (dr1.Read())// eğer texte girilenler datada varsa true döner yoksa girmeyeceği için atama yapmaz profile
            {
                string nokta=":      ";
                string kadi = "", sifre = "", email = "", yas = "",adsoyad="";
                SqlConnection cnn = new SqlConnection("server=.; Initial Catalog=Login;Integrated Security=SSPI");
                cnn.Open();
                SqlCommand cmd = new SqlCommand("Select *from [Users] where KullaniciAdi=@deger", cnn);
                cmd.Parameters.AddWithValue("@deger", user);
                SqlDataReader dr = cmd.ExecuteReader();

                Byte[] data = new Byte[0];
                while (dr.Read())// varsa sürekli degeri atar
                {
                    kadi =nokta+dr["KullaniciAdi"].ToString();
                    sifre = nokta+dr["Sifre"].ToString();
                    email = nokta + dr["Email"].ToString();
                    yas = nokta + dr["Yas"].ToString();
                    adsoyad = nokta +dr["AdSoyad"].ToString();
                    if (dr["Resim"] != null)
                    {                        
                        data = (Byte[])(dr["Resim"]);                      
                    }
                }               
                dr.Close();
                cmd.Dispose();
                cnn.Close();
                Form3 frm3 = new Form3() ;// form 3 profili açılır
                frm3.label7.Text = kadi;// proifledeki labellara datadaki readla değişkenlere atadığımız datalar atılır
                frm3.label8.Text = sifre;
                frm3.label9.Text = email;
                frm3.label10.Text = yas;
                frm3.label11.Text = adsoyad;
                frm3.label13.Text = adsoyad;
                MemoryStream mem = new MemoryStream(data);
                frm3.pictureBox1.Image = Image.FromStream(mem);
                
                frm3.Show();
                this.Hide();                                           
            }
            else// if çalışmazsa burası çalışır
            {
                MessageBox.Show("Kullanıcı adınız veye şifreniz hatalı");
                textBox1.Clear();
                textBox2.Clear();
            }
            con.Close();           
        }
        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }//formdan çıkıldığında arkada çalışmasını durdurur
        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }       
        private void button2_Click(object sender, EventArgs e)
        {
            Form2 frm2 = new Form2();
            frm2.Show();
            this.Hide();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
        
    }
}
