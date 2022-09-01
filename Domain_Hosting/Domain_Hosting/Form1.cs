using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Domain_Hosting
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnmusteriekle_Click(object sender, EventArgs e)
        {
            MusteriEkleFrm müsekle = new MusteriEkleFrm();
            müsekle.ShowDialog();
        }

        private void btnmusterilistele_Click(object sender, EventArgs e)
        {
            MüsteriListeleFrm müslis = new MüsteriListeleFrm();
            müslis.ShowDialog();
        }

        private void btnurunekle_Click(object sender, EventArgs e)
        {
            YeniÜrünEkleFrm yeniürün = new YeniÜrünEkleFrm();
            yeniürün.ShowDialog();
        }

        private void btnurunlerilistele_Click(object sender, EventArgs e)
        {
            ÜrünleriListeleFrm ürünliste = new ÜrünleriListeleFrm();
            ürünliste.ShowDialog();
        }

        private void btnfirmaekle_Click(object sender, EventArgs e)
        {
            YeniSaglayiciFrm yenisagfrm = new YeniSaglayiciFrm();
            yenisagfrm.ShowDialog();
        }

        private void btnfirmalistele_Click(object sender, EventArgs e)
        {
            SaglayiciFirmaListeleFrm firmalis = new SaglayiciFirmaListeleFrm();
            firmalis.ShowDialog();
        }

        private void btnkiralamaislemi_Click(object sender, EventArgs e)
        {
            YeniKiralamaFrm yenikira = new YeniKiralamaFrm();
            yenikira.ShowDialog();
        }

        private void btnkiraislistele_Click(object sender, EventArgs e)
        {
            KiralamaİslemleriniListeleFrm kiraliste = new KiralamaİslemleriniListeleFrm();
            kiraliste.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            YaklasanİslemlerFrm yakislem = new YaklasanİslemlerFrm();
            yakislem.ShowDialog();
        }
    }
}
