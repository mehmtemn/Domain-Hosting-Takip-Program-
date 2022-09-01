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

namespace Domain_Hosting
{
    public partial class KiralamaİslemleriniListeleFrm : Form
    {
        public KiralamaİslemleriniListeleFrm()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source = .\SQLEXPRESS; Initial Catalog = Domain_Hosting; Integrated Security = True");

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtislemıd.Text = dataGridView1.CurrentRow.Cells["IslemID"].Value.ToString();
        }

        private void txtislemıd_TextChanged(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from Tblİslemler where IslemID like '" + txtislemıd.Text + "'", con);
            SqlDataReader read = cmd.ExecuteReader();
            while (read.Read())
            {
                txtislemıd.Text = read["IslemID"].ToString();
                txtürün.Text = read["UrunID"].ToString();
                txtsaglayici.Text = read["SaglayiciID"].ToString();
                dateTimePicker1.CustomFormat = read["Bas_Tarihi"].ToString();
                dateTimePicker2.CustomFormat = read["Bit_Tarihi"].ToString();
                txtfiyat.Text = read["Fiyat"].ToString();
            }
            con.Close();
        }

        DataSet daset = new DataSet();

        private void KiralamaİslemleriniListeleFrm_Load(object sender, EventArgs e)
        {
            KiralamaİslemleriniListele();
        }

        private void txtislemidara_TextChanged(object sender, EventArgs e)
        {
            daset.Tables["Tblİslemler"].Clear();
            con.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select IslemID, Bas_Tarihi, Bit_Tarihi, Fiyat, MusteriAd, TelNo, E_Mail, UrunAd, SaglayiciAd from Tblİslemler join TblMusteri on Tblİslemler.MusteriID = TblMusteri.MusteriID join TblSaglayici on Tblİslemler.SaglayiciID=TblSaglayici.SaglayiciID join TblUrunler on Tblİslemler.UrunID=TblUrunler.UrunID where IslemID like '%" + txtislemidara.Text + "%'", con);
            adtr.Fill(daset, "Tblİslemler");
            dataGridView1.DataSource = daset.Tables["Tblİslemler"];
            con.Close();
        }

        private void btniptal_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnsil_Click(object sender, EventArgs e)
        {
            DialogResult dialog;
            dialog = MessageBox.Show("Bu Kaydı Silmek İstediğinize Emin Misiniz ?", "SİL", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (dialog == DialogResult.Yes)
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("delete from Tblİslemler where IslemID = @IslemID", con);
                cmd.Parameters.AddWithValue("@IslemID", dataGridView1.CurrentRow.Cells["IslemID"].Value.ToString());
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Silme İşleminiz Başarıyla Gerçekleşti.");
                daset.Tables["Tblİslemler"].Clear();
                KiralamaİslemleriniListele();
                foreach (Control item in Controls)
                {
                    if (item is TextBox)
                    {
                        item.Text = "";
                    }
                }
            }
        }

        private void KiralamaİslemleriniListele()
        {
            con.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select IslemID, Bas_Tarihi, Bit_Tarihi, Fiyat, MusteriAd, TelNo, E_Mail, UrunAd, SaglayiciAd from Tblİslemler join TblMusteri on Tblİslemler.MusteriID = TblMusteri.MusteriID join TblSaglayici on Tblİslemler.SaglayiciID=TblSaglayici.SaglayiciID join TblUrunler on Tblİslemler.UrunID=TblUrunler.UrunID", con);
            //SqlDataAdapter adtr = new SqlDataAdapter("select * from Tblİslemler ", con);  bunda sadece ID numaraları gözükür detaya inmez !
            adtr.Fill(daset, "Tblİslemler");
            dataGridView1.DataSource = daset.Tables["Tblİslemler"];
            con.Close();
        }

        private void btngüncelle_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("update Tblİslemler set MusteriID=@MusteriID, UrunID=@UrunID, SaglayiciID=@SaglayiciID, Bas_Tarih=@Bas_Tarih, Bit_Tarih=@Bit_Tarih, Fiyat=@Fiyat where IslemID=@IslemID", con);
            cmd.Parameters.AddWithValue("@MusteriID", txtmüsteri.Text);
            cmd.Parameters.AddWithValue("@UrunID", txtürün.Text);
            cmd.Parameters.AddWithValue("@SaglayiciID", txtsaglayici.Text);
            cmd.Parameters.AddWithValue("@Bas_Tarih", dateTimePicker1.Value);
            cmd.Parameters.AddWithValue("@Bit_Tarih", dateTimePicker2.Value);
            cmd.Parameters.AddWithValue("@Fiyat", txtfiyat.Text);
            cmd.Parameters.AddWithValue("@IslemID", txtislemıd.Text);
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Güncelleme İşleminiz Başarıyla Gerçekleşti.");

            daset.Tables["Tblİslemler"].Clear();
            KiralamaİslemleriniListele();
            foreach (Control item in Controls)
            {
                if (item is TextBox)
                {
                    item.Text = "";
                }
            }
        }

        private void KiralamaİslemleriniListeleFrm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btngüncelle.PerformClick();
            }
        }
    }
}
