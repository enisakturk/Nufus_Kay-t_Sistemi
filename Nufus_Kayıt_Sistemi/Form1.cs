using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Nufus_Kayıt_Sistemi
{
    public partial class Form1 : Form
    {
        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-CQJ8G3J\\SQLEXPRESS;Initial Catalog=Adresler;Integrated Security=True");
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Sehir();
            DogumYeri();
            NakilYeri();
        }
        private void Sehir()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from Sehirler ", baglanti);
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                cmbSehir.Items.Add(oku["SehirAdi"]);

            }
            baglanti.Close();
        }
        private void cmbSehir_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbİlçe.Items.Clear();
            cmbİlçe.Text = "";

            baglanti.Open();
            SqlCommand komut2 = new SqlCommand("select * from Ilceler where SehirId=@p1", baglanti);
            komut2.Parameters.AddWithValue("@p1", cmbSehir.SelectedIndex + 1);
            SqlDataReader oku = komut2.ExecuteReader();
            while (oku.Read())
            {
                cmbİlçe.Items.Add(oku["IlceAdi"]);
            }
            baglanti.Close();

        }

        private void cmbİlçe_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbMahalle.Items.Clear();
            cmbMahalle.Text = "";

            baglanti.Open();
            SqlCommand komut3 = new SqlCommand("select * from SemtMah where ilceId=@p2", baglanti);
            komut3.Parameters.AddWithValue("@p2", cmbİlçe.SelectedIndex + 1);
            SqlDataReader oku = komut3.ExecuteReader();
            while (oku.Read())
            {
                cmbMahalle.Items.Add(oku["MahalleAdi"]);
            }
            baglanti.Close();
        }

        private void cmbCinsiyet_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCinsiyet.Text == "Erkek")
            {
                txtÖncekiSyAd.Enabled = false;
            }
            else
            {
                txtÖncekiSyAd.Enabled = true;
            }
        }
        private void DogumYeri()
        {
            baglanti.Open();
            SqlCommand komut4 = new SqlCommand("select * from Sehirler", baglanti);
            SqlDataReader oku = komut4.ExecuteReader();
            while (oku.Read())
            {
                cmbDogumY.Items.Add(oku["SehirAdi"]);
            }
            baglanti.Close();
        }
        private void NakilYeri()
        {
            baglanti.Open();
            SqlCommand komut4 = new SqlCommand("select * from Sehirler", baglanti);
            SqlDataReader oku = komut4.ExecuteReader();
            while (oku.Read())
            {
                cmbNakilGittiğiY.Items.Add(oku["SehirAdi"]);
            }
            baglanti.Close();
        }

        private void resimdegis_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            resim.ImageLocation = openFileDialog1.FileName;
        }
        private void txtSeriNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        private void txtCiltNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        private void txtAileSıraNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtTcKimlik_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        private void txtTcKimlik_TextChanged(object sender, EventArgs e)
        {
            string tckimlik;
            int tek = 0, çift = 0;
            try
            {
                tckimlik = txtTcKimlik.Text;
                int index = 0;
                int toplam = 0;
                if (tckimlik[0].ToString() == "0")
                {
                    errorProvider1.SetError(txtTcKimlik, "İlk basamak 0 olamaz");
                }
                else
                {
                    errorProvider1.Clear();
                    for (int i = 0; i < 9; i += 2)
                    {
                        tek += Convert.ToInt16(txtTcKimlik.Text[i].ToString());
                    }
                    for (int i = 0; i < 9; i += 2)
                    {
                        çift += Convert.ToInt16(txtTcKimlik.Text[i].ToString());
                    }
                    int basamak10 = ((tek * 7) - çift) % 10;
                    if (txtTcKimlik.Text[9].ToString() != basamak10.ToString())
                    {
                        errorProvider1.SetError(txtTcKimlik, "10.basamağı hatalı girdiniz");
                    }
                    else
                    {
                        errorProvider1.Clear();
                    }
                    foreach (char n in tckimlik)
                    {
                        if (index < 10)
                        {
                            toplam += Convert.ToInt16(char.ToString(n));
                        }
                        index++;
                    }
                    if (toplam % 10 == Convert.ToInt16(tckimlik[10].ToString()))
                    {
                        errorProvider1.Clear();
                    }
                    else
                    {
                        errorProvider1.SetError(txtTcKimlik, "11.basamağı hatalı girdiniz");
                    }


                }
            }
            catch
            {

            }
        }

        private void btnkaydet_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Bilgiler Kayıt Edildi");
        }

        

        private void btnİptal_Click(object sender, EventArgs e)
        {
            txtAd.Text = "";
            txtSoyad.Text = "";
            cmbCinsiyet.Text = "";
            txtUyruk.Text = "";
            txtMeslek.Text = "";
            txtMail.Text = "";
            txtÖncekiSyAd.Text = "";
            txtSeriNo.Text = "";
            cmbNakilGittiğiY.Text = "";
            txtAnaAdı.Text = "";
            txtBabaAdı.Text = "";
            cmbMeshep.Text = "";
            cmbSehir.Text = "";
            cmbİlçe.Text = "";
            cmbMahalle.Text = "";
            cmbDogumY.Text = "";
            txtCiltNo.Text = "";
            txtAileSıraNo.Text = "";
            cmbKanGrubu.Text = "";
            cmbMedeniDurum.Text = "";
            txtİkamet.Text = "";
            txtCadde.Text = "";
        }
        

    }
    
}
