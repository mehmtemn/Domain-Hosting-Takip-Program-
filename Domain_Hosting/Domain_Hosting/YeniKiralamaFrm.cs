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
    public partial class YeniKiralamaFrm : Form
    {
        public YeniKiralamaFrm()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source = .\SQLEXPRESS; Initial Catalog = Domain_Hosting; Integrated Security = True");

        private void YeniKiralamaFrm_Load(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand();
            con.Open();

            cmd.CommandText = "SELECT * FROM TblMusteri";
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;

            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                //cmbxmüsteri.Items.Add(dr["Ad"].ToString());
                cmbxmüsteri.Items.Add(dr["MusteriID"].ToString() + "-- " + dr["MusteriAd"].ToString());
            }
            con.Close();

            SqlCommand kmt = new SqlCommand();
            kmt.CommandText = "SELECT * FROM TblUrunler";
            kmt.Connection = con;
            kmt.CommandType = CommandType.Text;

            SqlDataReader read;
            con.Open();
            read = kmt.ExecuteReader();
            while (read.Read())
            {
                //cmbxürün.Items.Add(read["Ad"].ToString());
                cmbxürün.Items.Add(read["UrunID"].ToString() + "-- " + read["UrunAd"].ToString());
            }
            con.Close();

            SqlCommand command = new SqlCommand();
            command.CommandText = "SELECT * FROM TblSaglayici";
            command.Connection = con;
            command.CommandType = CommandType.Text;

            SqlDataReader dat;
            con.Open();
            dat = command.ExecuteReader();
            while (dat.Read())
            {
                //cmbxsaglayici.Items.Add(dat["Ad"].ToString());
                cmbxsaglayici.Items.Add(dat["SaglayiciID"].ToString() + "-- " + dat["SaglayiciAd"].ToString());
            }
            con.Close();
        }

        private void btniptal_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnekle_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand c = new SqlCommand("insert into Tblİslemler (MusteriID, UrunID, SaglayiciID, Bas_Tarihi, Bit_Tarihi, Fiyat) values (@MusteriID, @UrunID, @SaglayiciID, @Bas_Tarihi, @Bit_Tarihi, @Fiyat)", con);
            
            string resultMusteri = cmbxmüsteri.SelectedItem.ToString();
            string[] valuesMusteri = resultMusteri.Split('-');
            c.Parameters.AddWithValue("@MusteriID", Convert.ToInt32(valuesMusteri[0].ToString()));

            string resultUrun = cmbxürün.SelectedItem.ToString();
            string[] valuesUrun = resultUrun.Split('-');
            c.Parameters.AddWithValue("@UrunID", Convert.ToInt32(valuesUrun[0].ToString()));

            string resultSaglayici = cmbxsaglayici.SelectedItem.ToString();
            string[] valuesSaglayici = resultSaglayici.Split('-');
            c.Parameters.AddWithValue("@SaglayiciID", Convert.ToInt32(valuesSaglayici[0].ToString()));

            c.Parameters.AddWithValue("@Bas_Tarihi", dateTimePicker1.Value);
            c.Parameters.AddWithValue("@Bit_Tarihi", dateTimePicker1.Value.AddYears(Convert.ToInt32(cmbxsüre.SelectedItem)));
            c.Parameters.AddWithValue("@Fiyat", txtfiyat.Text);
            c.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Yeni Kiralama İşlemi Başarıyla Gerçekleşmiştir.");

            foreach (Control item in Controls)
            {
                if (item is TextBox) item.Text = "";
            }
        }

        private void YeniKiralamaFrm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnekle.PerformClick();
            }
        }
    }
}
