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
using System.Text.RegularExpressions;

namespace otopark_otomasyonu
{
    public partial class AraçOtoparkKaydı : Form
    {
        public AraçOtoparkKaydı()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-2INIFEU\SQLEXPRESS;Initial Catalog=otoparkotomasyonu;Integrated Security=True");
        static bool EmailKontrol(string inputEmail)
        {
            const string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
            @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
            @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";

            return (new Regex(strRegex)).IsMatch(inputEmail);
        }

        private void Marka()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select marka from markabilgileri", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                comboMarka.Items.Add(read["marka"].ToString());
            }
            baglanti.Close();
        }

        private void BoşAraçlar()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select *from araçdurumu WHERE durumu='BOŞ'", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                comboParkYeri.Items.Add(read["parkyeri"].ToString());
            }
            baglanti.Close();
        }

        private void guna2GradientButton2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Programdan Çıkmak İstediğinize Emin Misiniz ?", "Onay Verin", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            Hide();
            AnaSayfa anasyf = new AnaSayfa();
            anasyf.Show();
        }

        private void guna2GradientButton3_Click(object sender, EventArgs e)
        {
            string mail = txtEmail.Text;
            bool kontrol = EmailKontrol(mail);

            if (txtTC.Text.Length==11)
            {
                if (txtTelefon.Text.Length == 11)
                {
                    if (kontrol == true)
                    {
                        baglanti.Open();
                        SqlCommand komut = new SqlCommand("insert into araç_otopark_kaydı(tc,ad,soyad,telefon,email,plaka,marka,seri,renk,parkyeri,tarih) values(@tc,@ad,@soyad,@telefon,@email,@plaka,@marka,@seri,@renk,@parkyeri,@tarih)", baglanti);
                        komut.Parameters.AddWithValue("@tc", txtTC.Text);
                        komut.Parameters.AddWithValue("@ad", txtAd.Text);
                        komut.Parameters.AddWithValue("@soyad", txtSoyad.Text);
                        komut.Parameters.AddWithValue("@telefon", txtTelefon.Text);
                        komut.Parameters.AddWithValue("@email", txtEmail.Text);
                        komut.Parameters.AddWithValue("@plaka", txtPlaka.Text);
                        komut.Parameters.AddWithValue("@marka", comboMarka.Text);
                        komut.Parameters.AddWithValue("@seri", comboSeri.Text);
                        komut.Parameters.AddWithValue("@renk", txtRenk.Text);
                        komut.Parameters.AddWithValue("@parkyeri", comboParkYeri.Text);
                        komut.Parameters.AddWithValue("@tarih", DateTime.Now.ToString());

                        komut.ExecuteNonQuery();

                        SqlCommand komut2 = new SqlCommand("update araçdurumu set durumu='DOLU' where parkyeri='" + comboParkYeri.SelectedItem + "'", baglanti);
                        komut2.ExecuteNonQuery();
                        baglanti.Close();
                        MessageBox.Show("Araç kaydı oluşturuldu", "Kayıt");
                        comboParkYeri.Items.Clear();
                        BoşAraçlar();
                        comboMarka.Items.Clear();
                        Marka();
                        comboSeri.Items.Clear();

                        txtTC.Clear();
                        txtAd.Clear();
                        txtSoyad.Clear();
                        txtTelefon.Clear();
                        txtEmail.Clear();
                        txtPlaka.Clear();
                        comboMarka.SelectedIndex = -1;
                        comboSeri.SelectedIndex = -1;
                        txtRenk.Clear();
                        comboParkYeri.SelectedIndex = -1;
                    }
                    else
                    {
                        MessageBox.Show("E-posta adresinizi @ kullanarak doğru biçimde yazınız","Uyarı",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                    }
                    
                }
                else
                {
                    MessageBox.Show("Telefon Numaranızı 0 ile Birlikte Eksiksiz Giriniz!","Uyarı",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                    txtTelefon.Focus();
                }
                
            }
            else
            {
                MessageBox.Show("TC Kimlik Numaranızı 11 Haneli olacak şekilde giriniz.","Uyarı",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                txtTC.Focus();
            }
            
        }

        private void comboMarka_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboSeri.Items.Clear();
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select marka,seri from seribilgileri where marka='" + comboMarka.SelectedItem + "'", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                comboSeri.Items.Add(read["seri"].ToString());
            }
            baglanti.Close();
        }

        private void btnMarka_Click(object sender, EventArgs e)
        {
            frmMarka marka = new frmMarka();
            marka.ShowDialog();
        }

        private void AraçOtoparkKaydı_Load(object sender, EventArgs e)
        {
            BoşAraçlar();
            Marka();
        }

        private void btnSeri_Click(object sender, EventArgs e)
        {
            frmSeri seri = new frmSeri();
            seri.ShowDialog();
        }

        private void btnMarkaSil_Click(object sender, EventArgs e)
        {
            FrmMarkaSil mrksil = new FrmMarkaSil();
            mrksil.ShowDialog();
        }
    }
}
