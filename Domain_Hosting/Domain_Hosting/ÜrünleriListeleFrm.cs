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
    public partial class ÜrünleriListeleFrm : Form
    {
        public ÜrünleriListeleFrm()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source = .\SQLEXPRESS; Initial Catalog = Domain_Hosting; Integrated Security = True");

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtürünid.Text = dataGridView1.CurrentRow.Cells["UrunID"].Value.ToString();
        }

        private void txtürünid_TextChanged(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from TblUrunler where UrunID like '" + txtürünid.Text + "'", con);
            SqlDataReader read = cmd.ExecuteReader();
            while (read.Read())
            {
                txtürünid.Text = read["UrunID"].ToString();
                txtad.Text = read["UrunAd"].ToString();
            }
            con.Close();
        }

        DataSet daset = new DataSet();

        private void ÜrünleriListeleFrm_Load(object sender, EventArgs e)
        {
            ÜrünleriListele();
        }

        private void txtürünidara_TextChanged(object sender, EventArgs e)
        {
            daset.Tables["TblUrunler"].Clear();
            con.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select * from TblUrunler where UrunID like '%" + txtürünidara.Text + "%'", con);
            adtr.Fill(daset, "TblUrunler");
            dataGridView1.DataSource = daset.Tables["TblUrunler"];
            con.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnsil_Click(object sender, EventArgs e)
        {
            DialogResult dialog;
            dialog = MessageBox.Show("Bu Ürünü Silmek İstediğinize Emin Misiniz ?", "SİL", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (dialog == DialogResult.Yes)
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("delete from TblUrunler where UrunID = @UrunID", con);
                cmd.Parameters.AddWithValue("@UrunID", dataGridView1.CurrentRow.Cells["UrunID"].Value.ToString());
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Silme İşlemi Başarıyla Gerçekleşmiştir.");
                daset.Tables["TblUrunler"].Clear();
                ÜrünleriListele();

                foreach (Control item in Controls)
                {
                    if (item is TextBox)
                    {
                        item.Text = "";
                    }
                }
            }
        }

        private void ÜrünleriListele()
        {
            con.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select * from TblUrunler", con);
            adtr.Fill(daset, "TblUrunler");
            dataGridView1.DataSource = daset.Tables["TblUrunler"];
            con.Close();
        }

        private void btngüncelle_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("update TblUrunler set Ad=@Ad where UrunID=@UrunID", con);
            cmd.Parameters.AddWithValue("@UrunAd", txtad.Text);
            cmd.Parameters.AddWithValue("@UrunID", txtürünid.Text);
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Güncelleme İşleminiz Başarıyla Gerçekleşmiştir.");
            daset.Tables["TblMusteri"].Clear();

            foreach (Control item in Controls)
            {
                if (item is TextBox) item.Text = "";
            }
        }

        private void ÜrünleriListeleFrm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btngüncelle.PerformClick();
            }
        }
    }
}
