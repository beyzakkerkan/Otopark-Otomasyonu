using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace otopark_otomasyonu
{
    public partial class FrmMarkaSil : Form
    {
        public FrmMarkaSil()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-2INIFEU\SQLEXPRESS;Initial Catalog=otoparkotomasyonu;Integrated Security=True");
        DataSet daset = new DataSet();
        private void MarkaListele()
        
        {
            baglanti.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select * from markabilgileri", baglanti);
            adtr.Fill(daset, "markabilgileri");
            guna2DataGridView1.DataSource = daset.Tables["markabilgileri"];
            baglanti.Close();
        }

        private void guna2GradientButton2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bu markayı silmek istiyor musunuz ?", "Onay Verin", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("delete from markabilgileri where marka=@marka", baglanti);
                komut.Parameters.AddWithValue("@marka", guna2DataGridView1.CurrentRow.Cells["marka"].Value.ToString());
                SqlCommand komut2 = new SqlCommand("delete from seribilgileri where marka=@marka", baglanti);
                komut2.Parameters.AddWithValue("@marka", guna2DataGridView1.CurrentRow.Cells["marka"].Value.ToString());

                komut.ExecuteNonQuery();
                komut2.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Silme İşlemi Gerçekleşti", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                daset.Tables["markabilgileri"].Clear();
                MarkaListele();

            }
        }

        private void FrmMarkaSil_Load(object sender, EventArgs e)
        {
            MarkaListele();
        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            Hide();
        }
    }
}
