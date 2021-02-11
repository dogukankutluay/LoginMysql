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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        SqlConnection con;
        SqlCommand cmd1;
        SqlDataReader dr1;             
        private void button3_Click(object sender, EventArgs e)
        {    
            
            string user = textBox2.Text;//user isim at
            con = new SqlConnection("server=.; Initial Catalog=Login;Integrated Security=SSPI");//sql bağlantı
            cmd1 = new SqlCommand();
            con.Open();
            cmd1.Connection = con;
            cmd1.CommandText = "SELECT * FROM Users where KullaniciAdi='" + user + "'";//user hücresindeki datları getirir
            dr1 = cmd1.ExecuteReader();//reader komutunu çalışıtırıyoruz
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "" || textBox6.Text == "") // boşsa sadee burası çalışacak 
            { 
                MessageBox.Show("Boş Alan Bırakmayınız."); 
            }
            else if (textBox3.Text != textBox4.Text) { MessageBox.Show("Lütfen Şifreleri Aynı Giriniz"); }// şifrelerin aynı olma kontrolü
            else if (textBox3.Text.Length>5) {MessageBox.Show("Şifreniz 5 karakterden büyük olamaz"); }// datadki 5 karakter kontrolü yoksa zaten atamaz
            else if (dr1.Read()){MessageBox.Show("Böyle Bir Kullanıcı ismi Bulunmaktadır");}//true ise texbox2 deki text var demektir yoksa girmez                 /  
            else
            {
                try
                {
                    FileStream fsResim = new FileStream(resimAdresi, FileMode.Open, FileAccess.Read);
                    BinaryReader brResim = new BinaryReader(fsResim);
                    byte[] resim = brResim.ReadBytes((int)fsResim.Length);
                    brResim.Close();
                    fsResim.Close();

                    SqlConnection conResim = new SqlConnection("Data Source =.; Initial Catalog = Login; Integrated Security = SSPI");
                    SqlCommand cmdResimKaydet = new SqlCommand("insert into Users (KullaniciAdi,AdSoyad,Sifre,Email,Yas,Resim) values (@kadi,@adsoy,@sifre,@email,@yas,@res)", conResim);
                    cmdResimKaydet.Parameters.Add("@kadi", SqlDbType.NChar, 30).Value = textBox2.Text;
                    cmdResimKaydet.Parameters.Add("@adsoy", SqlDbType.NChar, 20).Value = textBox1.Text;
                    cmdResimKaydet.Parameters.Add("@sifre", SqlDbType.NChar, 6).Value = textBox3.Text;
                    cmdResimKaydet.Parameters.Add("@email", SqlDbType.NChar, 40).Value = textBox5.Text;
                    cmdResimKaydet.Parameters.Add("@yas", SqlDbType.NChar, 2).Value = textBox6.Text;
                    cmdResimKaydet.Parameters.Add("@res", SqlDbType.Image, resim.Length).Value = resim;
                    conResim.Open();
                    cmdResimKaydet.ExecuteNonQuery();
                    conResim.Close();
                    MessageBox.Show($"KAYIT OLUNDU\nHoş Geldin\n{textBox2.Text}");
                    textBox1.Clear();
                    textBox2.Clear();
                    textBox3.Clear();
                    textBox4.Clear();
                    textBox5.Clear();
                    textBox6.Clear();
                }
                catch (ArgumentNullException){MessageBox.Show("Fotoğraf Ekleyiniz lütfen");}         
            }//bütün koşullar false ise kayıt olunur
            con.Close();//data kapatılır
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Form1 frm1 = new Form1();
            frm1.Show();
            this.Hide();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            DialogHazirla();
            pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
           // pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
        }

        string resimAdresi;
        public void DialogHazirla()
        {
            
            openFileDialog1.Title = "Duvar Kagidini Seç"; 
            openFileDialog1.Filter = "Jpeg Dosyalari(*.jpg)|*.jpg|Gif dosyalari(*.gif)|*.gif|Png Dosyalari(*.png)|*.png"; 
        }
        private void button4_Click_1(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = System.Drawing.Image.FromFile(openFileDialog1.FileName);
                resimAdresi = openFileDialog1.FileName.ToString();
            }
        }
    }
}
