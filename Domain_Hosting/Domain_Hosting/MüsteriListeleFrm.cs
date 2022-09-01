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
    public partial class MüsteriListeleFrm : Form
    {
        public MüsteriListeleFrm()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source = .\SQLEXPRESS; Initial Catalog = Domain_Hosting; Integrated Security = True");

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtmüsid.Text = dataGridView1.CurrentRow.Cells["MusteriID"].Value.ToString();
        }

        private void txtmüsid_TextChanged(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from TblMusteri where MusteriID like '" + txtmüsid.Text + "'", con);
            SqlDataReader read = cmd.ExecuteReader();
            while(read.Read())
            {
                txtmüsid.Text = read["MusteriID"].ToString();
                txtad.Text = read["MusteriAd"].ToString();
                txttelno.Text = read["TelNo"].ToString();
                txtmail.Text = read["E_Mail"].ToString();
            }
            con.Close();
        }

        DataSet daset = new DataSet();

        private void MüsteriListeleFrm_Load(object sender, EventArgs e)
        {
            MüsterileriListele();
        }

        private void txtmüsidara_TextChanged(object sender, EventArgs e)
        {
            daset.Tables["TblMusteri"].Clear();
            con.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select * from TblMusteri where MusteriID like '%" + txtmüsidara.Text + "%'", con);
            adtr.Fill(daset, "TblMusteri");
            dataGridView1.DataSource = daset.Tables["TblMusteri"];
            con.Close();
        }

        private void button2_Click(object sender, EventArgs e)  // İptal Butonu
        {
            this.Close();
        }

        private void btnsil_Click(object sender, EventArgs e)
        {
            DialogResult dialog;
            dialog = MessageBox.Show("Bu Müşteriyi Silmek İstediğinize Emin Misiniz ?", "SİL", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (dialog == DialogResult.Yes)
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("delete from TblMusteri where MusteriID = @MusteriID", con);
                cmd.Parameters.AddWithValue("@MusteriID", dataGridView1.CurrentRow.Cells["MusteriID"].Value.ToString());
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Silme İşlemi Başarıyla Gerçekleşmiştir.");
                daset.Tables["TblMusteri"].Clear(); 
                MüsterileriListele();
                foreach (Control item in Controls)
                {
                    if (item is TextBox)
                    {
                        item.Text = "";
                    }
                }
            }
        }

        private void MüsterileriListele()
        {
            con.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select * from TblMusteri", con);
            adtr.Fill(daset, "TblMusteri");
            dataGridView1.DataSource = daset.Tables["TblMusteri"];
            con.Close();
        }

        private void btngüncelle_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("update TblMusteri set MusteriAd=@MusteriAd, TelNo=@TelNo, E_Mail=@E_Mail where MusteriID=@MusteriID", con);
            cmd.Parameters.AddWithValue("@MusteriAd", txtad.Text);
            cmd.Parameters.AddWithValue("@TelNo", txttelno.Text);
            cmd.Parameters.AddWithValue("@E_Mail", txtmail.Text);
            cmd.Parameters.AddWithValue("@MusteriID", txtmüsid.Text);
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Güncelleme İşleminiz Başarıyla Gerçekleşti.");

            daset.Tables["TblMusteri"].Clear();
            MüsterileriListele();
            foreach (Control item in Controls)
            {
                if (item is TextBox)
                {
                    item.Text = "";
                }
            }
        }
    }
}
