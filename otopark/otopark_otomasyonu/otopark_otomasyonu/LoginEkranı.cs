using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace otopark_otomasyonu
{
    public partial class LoginEkranı : Form
    {
        public LoginEkranı()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-2INIFEU\SQLEXPRESS;Initial Catalog=otoparkotomasyonu;Integrated Security=True");


        private void guna2GradientButton2_Click(object sender, EventArgs e)
        {
            string kadi = textBox1.Text;
            string parola = textBox2.Text;
            baglanti.Open();
            string sql = "select * from yöneticibilgi where kullanıcıadı = @kullaniciadi and şifre = @sifre";
            SqlCommand komut = new SqlCommand(sql, baglanti);
            komut.Parameters.Add(new SqlParameter("@kullaniciadi", kadi));
            komut.Parameters.Add(new SqlParameter("@sifre", parola));

            SqlDataReader reader = komut.ExecuteReader();

            if (reader.Read())
            {
                Hide();
                AnaSayfa anasyf = new AnaSayfa();
                anasyf.Show();
            }
            else
            {
                MessageBox.Show("Hatalı giriş yaptınız.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox1.Clear();
                textBox2.Clear();
            }

            baglanti.Close();
        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Programdan Çıkmak İstediğinize Emin Misiniz ?", "Onay Verin", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void LoginEkranı_Load(object sender, EventArgs e)
        {
            pictureBox1.Location = new Point(-500, pictureBox1.Location.Y);
            //this.Opacity = 0.88;
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            pictureBox1.Location = new Point(pictureBox1.Location.X+2,pictureBox1.Location.Y);
            if (pictureBox1.Location.X==0 && pictureBox1.Location.Y==0)
            {
                timer1.Stop();
                textBox1.Visible = true;
                textBox2.Visible = true;
                guna2PictureBox1.Visible = true;
                guna2GradientButton1.Visible = true;
                guna2GradientButton2.Visible = true;
                label1.Visible = true;
            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                guna2GradientButton2.PerformClick();
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
