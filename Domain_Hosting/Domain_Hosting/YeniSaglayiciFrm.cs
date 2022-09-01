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
    public partial class YeniSaglayiciFrm : Form
    {
        public YeniSaglayiciFrm()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source = .\SQLEXPRESS; Initial Catalog = Domain_Hosting; Integrated Security = True");

        private void btniptal_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnekle_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("insert into TblSaglayici (SaglayiciAd) values (@SaglayiciAd)", con);
            cmd.Parameters.AddWithValue("@SaglayiciAd", txtad.Text);
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Müşteri Ekleme İşlemi Başarıyla Gerçekleşmiştir.");

            foreach (Control item in Controls)
            {
                if (item is TextBox) item.Text = "";
            }
        }

        private void YeniSaglayiciFrm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnekle.PerformClick();
            }
        }
    }
}
