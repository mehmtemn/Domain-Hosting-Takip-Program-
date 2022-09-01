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

namespace Domain_Hosting
{
    public partial class MusteriEkleFrm : Form
    {
        public MusteriEkleFrm()
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
            SqlCommand cmd = new SqlCommand("insert into TblMusteri (MusteriAd, TelNo, E_Mail) values (@MusteriAd, @TelNo, @E_Mail)", con);
            cmd.Parameters.AddWithValue("@MusteriAd", txtad.Text);
            cmd.Parameters.AddWithValue("@TelNo", txttelno.Text);
            cmd.Parameters.AddWithValue("@E_Mail", txtmail.Text);
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Müşteri Ekleme İşlemi Başarıyla Gerçekleşmiştir.");

            foreach (Control item in Controls)
            {
                if (item is TextBox)
                {
                    item.Text = "";
                }
            }
        }
        
        private void MusteriEkleFrm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnekle.PerformClick();
            }
        }
    }
}
