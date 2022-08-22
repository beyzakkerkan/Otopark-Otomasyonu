using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace otopark_otomasyonu
{
    public partial class AnaSayfa : Form
    {
        public AnaSayfa()
        {
            InitializeComponent();
        }

        private void guna2GradientButton4_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Programdan Çıkmak İstediğinize Emin Misiniz ?", "Onay Verin", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            Hide();
            AraçOtoparkKaydı araçkayıt = new AraçOtoparkKaydı();
            araçkayıt.Show();
        }

        private void guna2GradientButton2_Click(object sender, EventArgs e)
        {
            Hide();
            AraçOtoparkYerleri bosyerler = new AraçOtoparkYerleri();
            bosyerler.Show();
        }

        private void guna2GradientButton3_Click(object sender, EventArgs e)
        {
             Hide();
            AraçOtoparkÇıkış arabacıkıs = new AraçOtoparkÇıkış();
            arabacıkıs.Show();
        }
    }
}
