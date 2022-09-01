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
    public partial class SaglayiciFirmaListeleFrm : Form
    {
        public SaglayiciFirmaListeleFrm()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source = .\SQLEXPRESS; Initial Catalog = Domain_Hosting; Integrated Security = True");

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtfirmaid.Text = dataGridView1.CurrentRow.Cells["SaglayiciID"].Value.ToString();
        }

        private void txtfirmaid_TextChanged(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from TblSaglayici where SaglayiciID like '" + txtfirmaid.Text + "'", con);
            SqlDataReader read = cmd.ExecuteReader();
            while (read.Read())
            {
                txtfirmaid.Text = read["SaglayiciID"].ToString();
                txtad.Text = read["SaglayiciAd"].ToString();
            }
            con.Close();
        }

        DataSet daset = new DataSet();

        private void SaglayiciFirmaListeleFrm_Load(object sender, EventArgs e)
        {
            SaglayiciFirmaListele();
        }

        private void txtfirmaraid_TextChanged(object sender, EventArgs e)
        {
            daset.Tables["TblSaglayici"].Clear();
            con.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select * from TblSaglayici where SaglayiciID like '%" + txtfirmaraid + "%'", con);
            adtr.Fill(daset, "TblSaglayici");
            dataGridView1.DataSource = daset.Tables["TblSaglayici"];
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
                SqlCommand cmd = new SqlCommand("delete from TblSaglayici where SaglayiciID = @SaglayiciID", con);
                cmd.Parameters.AddWithValue("@SaglayiciID", dataGridView1.CurrentRow.Cells["SaglayiciID"].Value.ToString());
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Silme İşlemi Başarıyla Gerçekleşmiştir.");
                SaglayiciFirmaListele();
                daset.Tables["TblSaglayici"].Clear();

                foreach (Control item in Controls)
                {
                    if (item is TextBox)
                    {
                        item.Text = "";
                    }
                }
            }
        }

        private void SaglayiciFirmaListele()
        {
            con.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select * from TblSaglayici", con);
            adtr.Fill(daset, "TblSaglayici");
            dataGridView1.DataSource = daset.Tables["TblSaglayici"];
            con.Close();
        }

        private void btngüncelle_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("update TblSaglayici set Ad=@Ad where SaglayiciID=@SaglayiciID", con);
            cmd.Parameters.AddWithValue("@SaglayiciAd", txtad.Text);
            cmd.Parameters.AddWithValue("@SaglayiciID", txtfirmaid.Text);
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Güncelleme İşleminiz Başarıyla Gerçekleşmiştir.");

            foreach (Control item in Controls)
            {
                if (item is TextBox) item.Text = "";
            }
        }

        private void SaglayiciFirmaListeleFrm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btngüncelle.PerformClick();
            }
        }
    }
}
