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
    public partial class YaklasanİslemlerFrm : Form
    {
        public YaklasanİslemlerFrm()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source = .\SQLEXPRESS; Initial Catalog = Domain_Hosting; Integrated Security = True");

        DataTable List()
        {
            DataTable tbl = new DataTable();
            SqlDataAdapter adtr = new SqlDataAdapter("select IslemID, Bas_Tarihi, Bit_Tarihi, Fiyat, MusteriAd, TelNo, E_Mail, UrunAd, SaglayiciAd from Tblİslemler join TblMusteri on Tblİslemler.MusteriID = TblMusteri.MusteriID join TblSaglayici on Tblİslemler.SaglayiciID=TblSaglayici.SaglayiciID join TblUrunler on Tblİslemler.UrunID=TblUrunler.UrunID", con);
            adtr.Fill(tbl);
            dataGridView1.DataSource = tbl;
            //dataGridView1.Columns[1].Visible = false;
            Count();
            return tbl;
        }

        DataTable Filtrele()
        {
            string sql = "select IslemID, Bas_Tarihi, Bit_Tarihi, Fiyat, MusteriAd, TelNo, E_Mail, UrunAd, SaglayiciAd from Tblİslemler join TblMusteri on Tblİslemler.MusteriID = TblMusteri.MusteriID join TblSaglayici on Tblİslemler.SaglayiciID=TblSaglayici.SaglayiciID join TblUrunler on Tblİslemler.UrunID=TblUrunler.UrunID Where Bit_Tarihi between @p1 and @p2";
            DataTable tbl = new DataTable();
            SqlDataAdapter adtr = new SqlDataAdapter(sql, con);
            adtr.SelectCommand.Parameters.Add("@p1", SqlDbType.Date).Value = dateTimePicker1.Value;
            adtr.SelectCommand.Parameters.Add("@p2", SqlDbType.Date).Value = dateTimePicker2.Value;
            adtr.Fill(tbl);
            dataGridView1.DataSource = tbl;
            //dataGridView1.Columns[1].Visible = false; 
            Count();
            return tbl;
        }

        void Count()  
        {
            label4.Text = $"TOPLAM KAYIT SAYISI : {dataGridView1.RowCount - 1}";
        }

        private void btnlistele_Click(object sender, EventArgs e)
        {
            List();
        }

        private void btnfiltrele_Click(object sender, EventArgs e)
        {
            Filtrele();
        }
    }
}
