using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication4
{
    public partial class frmCalisanListesi : Form
    {

        public frmCalisanListesi()
        {
            InitializeComponent();

            grdListe.AutoGenerateColumns = false;
            txtId.Text = "0";

            SehirleriDoldur();

            CalisanListesiniGetir();

            txtAd.Focus();
        }

        private void SehirleriDoldur()
        {
            var sehirRepo = new SehirRepository();
            var liste = sehirRepo.TumKayitlariGetir();

            cmbSehir.DisplayMember = "SehirAdi";
            cmbSehir.ValueMember = "SehirKodu";
            cmbSehir.DataSource = liste;

            cmbSehir.AutoCompleteSource = AutoCompleteSource.CustomSource;
            var otomatikDoldurmaListesi = new AutoCompleteStringCollection();
            otomatikDoldurmaListesi.AddRange(liste.Select(x => x.SehirAdi).ToArray());
            cmbSehir.AutoCompleteCustomSource = otomatikDoldurmaListesi;
        }

        private void FormuBosalt()
        {
            txtId.Text = "0";
            txtAd.Text = string.Empty;
            txtSoyad.Text = string.Empty;
            txtTelefon.Text = string.Empty;
            txtAdres.Text = string.Empty;
            cmbSehir.SelectedValue = 0;

            txtAd.Focus();
        }

        private void FormuDoldur(Calisan calisan)
        {
            txtId.Text = calisan.Id.ToString();
            txtAd.Text = calisan.Ad;
            txtSoyad.Text = calisan.Soyad;
            txtTelefon.Text = calisan.Telefon;
            txtAdres.Text = calisan.Adres;
            cmbSehir.SelectedValue = calisan.SehirKodu;
        }

        private void CalisanListesiniGetir()
        {
            var calisanRepo = new CalisanRepository();
            var liste = calisanRepo.TumKayitlariGetir();

            grdListe.SuspendLayout();

            grdListe.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            grdListe.Rows.Clear();

            foreach (var satir in liste)
            {
                grdListe.Rows.Add(new[] { satir.Id.ToString(), satir.Ad, satir.Soyad, satir.Telefon, satir.Adres, satir.SehirAdi });
            }

            grdListe.ResumeLayout(false);
        }

        private void grdListe_SelectionChanged(object sender, EventArgs e)
        {
            if (grdListe.SelectedRows.Count == 0)
                return;

            var secilenSatir = grdListe.SelectedRows[0];
            var secilenId = Convert.ToInt32(secilenSatir.Cells[0].Value);

            var calisanRepo = new CalisanRepository();
            var calisan = calisanRepo.Getir(secilenId);

            FormuDoldur(calisan);
        }

        private void btnYeni_Click(object sender, EventArgs e)
        {
            FormuBosalt();

            txtAd.Focus();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            var calisan = new Calisan
            {
                Id = Convert.ToInt32(txtId.Text),
                Ad = txtAd.Text,
                Soyad = txtSoyad.Text,
                Telefon = txtTelefon.Text,
                Adres = txtAdres.Text,
                SehirKodu = Convert.ToInt32(cmbSehir.SelectedValue)
            };

            var calisanRepo = new CalisanRepository();
            calisanRepo.Kaydet(calisan);

            CalisanListesiniGetir();

            txtAd.Focus();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            var calisanId = Convert.ToInt32(txtId.Text);

            if (calisanId == 0)
            {
                MessageBox.Show("Çalışan kaydı seçmediniz.", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var cevap = MessageBox.Show("Silecek misin?", "CİDDİ MİSİN?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (cevap == System.Windows.Forms.DialogResult.No)
                return;

            var calisanRepo = new CalisanRepository();
            calisanRepo.Sil(calisanId);

            CalisanListesiniGetir();

            grdListe.Focus();
        }

        private void btnBira_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Bu kadar çalışma yeter. Bir sigara bir de mümkünse bira.", "YETER", MessageBoxButtons.OK, MessageBoxIcon.Asterisk); 
        }
    }
}
