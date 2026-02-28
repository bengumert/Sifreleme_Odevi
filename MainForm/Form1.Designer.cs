namespace MainForm
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            pnlMailCard = new Panel();
            txtMailAdresi = new TextBox();
            txtAliciMail = new Label();
            btnMailGonder = new Button();
            pnlActionCard = new Panel();
            lblSifreAnahtarLine = new Label();
            txtSifreAnahtar = new TextBox();
            btnSifrele = new Button();
            cmbAlgoritma = new ComboBox();
            lblAlgSec = new Label();
            pnlOutputCard = new Panel();
            txtSonuc = new RichTextBox();
            label2 = new Label();
            pnlInputCard = new Panel();
            txtGiris = new RichTextBox();
            label4 = new Label();
            tabPage2 = new TabPage();
            pnlCozOutputCard = new Panel();
            txtCozulenSonuc = new RichTextBox();
            label3 = new Label();
            pnlCozActionCard = new Panel();
            label1 = new Label();
            btnCoz = new Button();
            txtCozAnahtar = new TextBox();
            cmbCozAlgoritma = new ComboBox();
            lblCozAlg = new Label();
            pnlCozInputCard = new Panel();
            lstGelenMesajlar = new ListBox();
            txtGelenSifre = new RichTextBox();
            label5 = new Label();
            btnPostaCek = new Button();
            tabPage3 = new TabPage();
            pnlSettingsCard = new Panel();
            btnAyarlariKaydet = new Button();
            chkShowPass = new CheckBox();
            txtKendiSifre = new TextBox();
            lblKendiSifre = new Label();
            txtKendiMail = new TextBox();
            lblKendiMail = new Label();
            lblAyarInfo = new Label();
            txtKullaniciAdi = new TextBox();
            lblKullaniciAdi = new Label();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            pnlMailCard.SuspendLayout();
            pnlActionCard.SuspendLayout();
            pnlOutputCard.SuspendLayout();
            pnlInputCard.SuspendLayout();
            tabPage2.SuspendLayout();
            pnlCozOutputCard.SuspendLayout();
            pnlCozActionCard.SuspendLayout();
            pnlCozInputCard.SuspendLayout();
            tabPage3.SuspendLayout();
            pnlSettingsCard.SuspendLayout();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Controls.Add(tabPage3);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            tabControl1.Location = new Point(0, 0);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(1000, 800);
            tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            tabPage1.BackColor = Color.FromArgb(20, 20, 20);
            tabPage1.Controls.Add(pnlMailCard);
            tabPage1.Controls.Add(pnlActionCard);
            tabPage1.Controls.Add(pnlOutputCard);
            tabPage1.Controls.Add(pnlInputCard);
            tabPage1.ForeColor = Color.White;
            tabPage1.Location = new Point(4, 26);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(15);
            tabPage1.Size = new Size(992, 770);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "   ŞİFRELE VE GÖNDER   ";
            // 
            // pnlMailCard
            // 
            pnlMailCard.BackColor = Color.FromArgb(32, 32, 32);
            pnlMailCard.Controls.Add(txtMailAdresi);
            pnlMailCard.Controls.Add(txtAliciMail);
            pnlMailCard.Controls.Add(btnMailGonder);
            pnlMailCard.Location = new Point(20, 680);
            pnlMailCard.Name = "pnlMailCard";
            pnlMailCard.Size = new Size(952, 60);
            pnlMailCard.TabIndex = 4;
            // 
            // txtMailAdresi
            // 
            txtMailAdresi.BackColor = Color.FromArgb(45, 45, 45);
            txtMailAdresi.BorderStyle = BorderStyle.FixedSingle;
            txtMailAdresi.ForeColor = Color.White;
            txtMailAdresi.Location = new Point(115, 18);
            txtMailAdresi.Name = "txtMailAdresi";
            txtMailAdresi.PlaceholderText = "alıcı@mail.com";
            txtMailAdresi.Size = new Size(400, 25);
            txtMailAdresi.TabIndex = 5;
            // 
            // txtAliciMail
            // 
            txtAliciMail.AutoSize = true;
            txtAliciMail.Location = new Point(15, 20);
            txtAliciMail.Name = "txtAliciMail";
            txtAliciMail.Size = new Size(89, 19);
            txtAliciMail.TabIndex = 4;
            txtAliciMail.Text = "Alıcı E-Posta:";
            // 
            // btnMailGonder
            // 
            btnMailGonder.BackColor = Color.FromArgb(0, 122, 204);
            btnMailGonder.Cursor = Cursors.Hand;
            btnMailGonder.FlatAppearance.BorderSize = 0;
            btnMailGonder.FlatStyle = FlatStyle.Flat;
            btnMailGonder.Font = new Font("Segoe UI Bold", 9.75F, FontStyle.Bold);
            btnMailGonder.Location = new Point(530, 14);
            btnMailGonder.Name = "btnMailGonder";
            btnMailGonder.Size = new Size(200, 32);
            btnMailGonder.TabIndex = 3;
            btnMailGonder.Text = "✉ E-POSTA GÖNDER";
            btnMailGonder.UseVisualStyleBackColor = false;
            btnMailGonder.Click += btnMailGonder_Click;
            // 
            // pnlActionCard
            // 
            pnlActionCard.BackColor = Color.FromArgb(32, 32, 32);
            pnlActionCard.Controls.Add(lblSifreAnahtarLine);
            pnlActionCard.Controls.Add(txtSifreAnahtar);
            pnlActionCard.Controls.Add(btnSifrele);
            pnlActionCard.Controls.Add(cmbAlgoritma);
            pnlActionCard.Controls.Add(lblAlgSec);
            pnlActionCard.Location = new Point(20, 290);
            pnlActionCard.Name = "pnlActionCard";
            pnlActionCard.Size = new Size(952, 85);
            pnlActionCard.TabIndex = 3;
            // 
            // lblSifreAnahtarLine
            // 
            lblSifreAnahtarLine.AutoSize = true;
            lblSifreAnahtarLine.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            lblSifreAnahtarLine.ForeColor = Color.Silver;
            lblSifreAnahtarLine.Location = new Point(240, 15);
            lblSifreAnahtarLine.Name = "lblSifreAnahtarLine";
            lblSifreAnahtarLine.Size = new Size(107, 15);
            lblSifreAnahtarLine.TabIndex = 10;
            lblSifreAnahtarLine.Text = "Şifreleme Anahtarı:";
            // 
            // txtSifreAnahtar
            // 
            txtSifreAnahtar.BackColor = Color.FromArgb(45, 45, 45);
            txtSifreAnahtar.BorderStyle = BorderStyle.FixedSingle;
            txtSifreAnahtar.ForeColor = Color.White;
            txtSifreAnahtar.Location = new Point(240, 40);
            txtSifreAnahtar.Name = "txtSifreAnahtar";
            txtSifreAnahtar.Size = new Size(400, 25);
            txtSifreAnahtar.TabIndex = 9;
            // 
            // btnSifrele
            // 
            btnSifrele.BackColor = Color.FromArgb(0, 122, 204);
            btnSifrele.Cursor = Cursors.Hand;
            btnSifrele.FlatAppearance.BorderSize = 0;
            btnSifrele.FlatStyle = FlatStyle.Flat;
            btnSifrele.Font = new Font("Segoe UI Bold", 12F, FontStyle.Bold);
            btnSifrele.Location = new Point(660, 20);
            btnSifrele.Name = "btnSifrele";
            btnSifrele.Size = new Size(270, 45);
            btnSifrele.TabIndex = 2;
            btnSifrele.Text = "ŞİFRELE 🔒";
            btnSifrele.UseVisualStyleBackColor = false;
            btnSifrele.Click += btnSifrele_Click;
            // 
            // cmbAlgoritma
            // 
            cmbAlgoritma.BackColor = Color.FromArgb(45, 45, 45);
            cmbAlgoritma.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbAlgoritma.FlatStyle = FlatStyle.Flat;
            cmbAlgoritma.ForeColor = Color.White;
            cmbAlgoritma.FormattingEnabled = true;
            cmbAlgoritma.Items.AddRange(new object[] { "Kaydırmalı", "Doğrusal", "Yer Değiştirme", "Sayı Anahtarlı", "Permütasyon", "Rota", "Zigzag" });
            cmbAlgoritma.Location = new Point(15, 40);
            cmbAlgoritma.Name = "cmbAlgoritma";
            cmbAlgoritma.Size = new Size(200, 25);
            cmbAlgoritma.TabIndex = 1;
            cmbAlgoritma.SelectedIndexChanged += cmbAlgoritma_SelectedIndexChanged;
            // 
            // lblAlgSec
            // 
            lblAlgSec.AutoSize = true;
            lblAlgSec.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            lblAlgSec.ForeColor = Color.Silver;
            lblAlgSec.Location = new Point(15, 15);
            lblAlgSec.Name = "lblAlgSec";
            lblAlgSec.Size = new Size(84, 15);
            lblAlgSec.TabIndex = 0;
            lblAlgSec.Text = "Algoritma Seç:";
            // 
            // pnlOutputCard
            // 
            pnlOutputCard.BackColor = Color.FromArgb(32, 32, 32);
            pnlOutputCard.Controls.Add(txtSonuc);
            pnlOutputCard.Controls.Add(label2);
            pnlOutputCard.Location = new Point(20, 390);
            pnlOutputCard.Name = "pnlOutputCard";
            pnlOutputCard.Padding = new Padding(12);
            pnlOutputCard.Size = new Size(952, 275);
            pnlOutputCard.TabIndex = 2;
            // 
            // txtSonuc
            // 
            txtSonuc.BackColor = Color.FromArgb(20, 20, 20);
            txtSonuc.BorderStyle = BorderStyle.None;
            txtSonuc.Dock = DockStyle.Bottom;
            txtSonuc.Font = new Font("Consolas", 10.5F);
            txtSonuc.ForeColor = Color.FromArgb(80, 250, 120);
            txtSonuc.Location = new Point(12, 45);
            txtSonuc.Name = "txtSonuc";
            txtSonuc.ReadOnly = true;
            txtSonuc.Size = new Size(928, 218);
            txtSonuc.TabIndex = 6;
            txtSonuc.Text = "";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Bold", 11F, FontStyle.Bold);
            label2.ForeColor = Color.FromArgb(0, 160, 250);
            label2.Location = new Point(12, 12);
            label2.Name = "label2";
            label2.Size = new Size(153, 20);
            label2.TabIndex = 7;
            label2.Text = "Şifreli Metin (Giden):";
            // 
            // pnlInputCard
            // 
            pnlInputCard.BackColor = Color.FromArgb(32, 32, 32);
            pnlInputCard.Controls.Add(txtGiris);
            pnlInputCard.Controls.Add(label4);
            pnlInputCard.Location = new Point(20, 20);
            pnlInputCard.Name = "pnlInputCard";
            pnlInputCard.Padding = new Padding(12);
            pnlInputCard.Size = new Size(952, 255);
            pnlInputCard.TabIndex = 1;
            // 
            // txtGiris
            // 
            txtGiris.BackColor = Color.FromArgb(20, 20, 20);
            txtGiris.BorderStyle = BorderStyle.None;
            txtGiris.Dock = DockStyle.Bottom;
            txtGiris.Font = new Font("Segoe UI", 10.5F);
            txtGiris.ForeColor = Color.White;
            txtGiris.Location = new Point(12, 45);
            txtGiris.Name = "txtGiris";
            txtGiris.Size = new Size(928, 198);
            txtGiris.TabIndex = 0;
            txtGiris.Text = "";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI Bold", 11F, FontStyle.Bold);
            label4.ForeColor = Color.FromArgb(0, 160, 250);
            label4.Location = new Point(12, 12);
            label4.Name = "label4";
            label4.Size = new Size(135, 20);
            label4.TabIndex = 8;
            label4.Text = "Metin Girişi (Düz):";
            // 
            // tabPage2
            // 
            tabPage2.BackColor = Color.FromArgb(20, 20, 20);
            tabPage2.Controls.Add(pnlCozOutputCard);
            tabPage2.Controls.Add(pnlCozActionCard);
            tabPage2.Controls.Add(pnlCozInputCard);
            tabPage2.Controls.Add(btnPostaCek);
            tabPage2.ForeColor = Color.White;
            tabPage2.Location = new Point(4, 26);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(15);
            tabPage2.Size = new Size(992, 770);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "   AL VE ÇÖZ   ";
            // 
            // pnlCozOutputCard
            // 
            pnlCozOutputCard.BackColor = Color.FromArgb(32, 32, 32);
            pnlCozOutputCard.Controls.Add(txtCozulenSonuc);
            pnlCozOutputCard.Controls.Add(label3);
            pnlCozOutputCard.Location = new Point(20, 520);
            pnlCozOutputCard.Name = "pnlCozOutputCard";
            pnlCozOutputCard.Padding = new Padding(12);
            pnlCozOutputCard.Size = new Size(952, 230);
            pnlCozOutputCard.TabIndex = 9;
            // 
            // txtCozulenSonuc
            // 
            txtCozulenSonuc.BackColor = Color.FromArgb(20, 20, 20);
            txtCozulenSonuc.BorderStyle = BorderStyle.None;
            txtCozulenSonuc.Dock = DockStyle.Bottom;
            txtCozulenSonuc.Font = new Font("Segoe UI", 10.5F);
            txtCozulenSonuc.ForeColor = Color.FromArgb(80, 250, 120);
            txtCozulenSonuc.Location = new Point(12, 45);
            txtCozulenSonuc.Name = "txtCozulenSonuc";
            txtCozulenSonuc.ReadOnly = true;
            txtCozulenSonuc.Size = new Size(928, 173);
            txtCozulenSonuc.TabIndex = 6;
            txtCozulenSonuc.Text = "";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI Bold", 11F, FontStyle.Bold);
            label3.ForeColor = Color.FromArgb(250, 160, 0);
            label3.Location = new Point(12, 12);
            label3.Name = "label3";
            label3.Size = new Size(110, 20);
            label3.TabIndex = 7;
            label3.Text = "Orijinal Mesaj:";
            // 
            // pnlCozActionCard
            // 
            pnlCozActionCard.BackColor = Color.FromArgb(32, 32, 32);
            pnlCozActionCard.Controls.Add(label1);
            pnlCozActionCard.Controls.Add(btnCoz);
            pnlCozActionCard.Controls.Add(txtCozAnahtar);
            pnlCozActionCard.Controls.Add(cmbCozAlgoritma);
            pnlCozActionCard.Controls.Add(lblCozAlg);
            pnlCozActionCard.Location = new Point(20, 420);
            pnlCozActionCard.Name = "pnlCozActionCard";
            pnlCozActionCard.Size = new Size(952, 85);
            pnlCozActionCard.TabIndex = 3;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            label1.ForeColor = Color.Silver;
            label1.Location = new Point(240, 15);
            label1.Name = "label1";
            label1.Size = new Size(95, 15);
            label1.TabIndex = 5;
            label1.Text = "Çözme Anahtarı:";
            // 
            // btnCoz
            // 
            btnCoz.BackColor = Color.FromArgb(0, 122, 204);
            btnCoz.Cursor = Cursors.Hand;
            btnCoz.FlatAppearance.BorderSize = 0;
            btnCoz.FlatStyle = FlatStyle.Flat;
            btnCoz.Font = new Font("Segoe UI Bold", 12F, FontStyle.Bold);
            btnCoz.Location = new Point(660, 20);
            btnCoz.Name = "btnCoz";
            btnCoz.Size = new Size(270, 45);
            btnCoz.TabIndex = 4;
            btnCoz.Text = "ŞİFREYİ ÇÖZ 🔓";
            btnCoz.UseVisualStyleBackColor = false;
            btnCoz.Click += btnCoz_Click;
            // 
            // txtCozAnahtar
            // 
            txtCozAnahtar.BackColor = Color.FromArgb(45, 45, 45);
            txtCozAnahtar.BorderStyle = BorderStyle.FixedSingle;
            txtCozAnahtar.ForeColor = Color.White;
            txtCozAnahtar.Location = new Point(240, 40);
            txtCozAnahtar.Name = "txtCozAnahtar";
            txtCozAnahtar.Size = new Size(400, 25);
            txtCozAnahtar.TabIndex = 3;
            // 
            // cmbCozAlgoritma
            // 
            cmbCozAlgoritma.BackColor = Color.FromArgb(45, 45, 45);
            cmbCozAlgoritma.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCozAlgoritma.FlatStyle = FlatStyle.Flat;
            cmbCozAlgoritma.ForeColor = Color.White;
            cmbCozAlgoritma.FormattingEnabled = true;
            cmbCozAlgoritma.Items.AddRange(new object[] { "Kaydırmalı", "Doğrusal", "Yer Değiştirme", "Sayı Anahtarlı", "Permütasyon", "Rota", "Zigzag" });
            cmbCozAlgoritma.Location = new Point(15, 40);
            cmbCozAlgoritma.Name = "cmbCozAlgoritma";
            cmbCozAlgoritma.Size = new Size(200, 25);
            cmbCozAlgoritma.TabIndex = 2;
            cmbCozAlgoritma.SelectedIndexChanged += cmbCozAlgoritma_SelectedIndexChanged;
            // 
            // lblCozAlg
            // 
            lblCozAlg.AutoSize = true;
            lblCozAlg.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            lblCozAlg.ForeColor = Color.Silver;
            lblCozAlg.Location = new Point(15, 15);
            lblCozAlg.Name = "lblCozAlg";
            lblCozAlg.Size = new Size(84, 15);
            lblCozAlg.TabIndex = 11;
            lblCozAlg.Text = "Algoritma Seç:";
            // 
            // pnlCozInputCard
            // 
            pnlCozInputCard.BackColor = Color.FromArgb(32, 32, 32);
            pnlCozInputCard.Controls.Add(txtGelenSifre);
            pnlCozInputCard.Controls.Add(label5);
            pnlCozInputCard.Location = new Point(20, 70);
            pnlCozInputCard.Name = "pnlCozInputCard";
            pnlCozInputCard.Padding = new Padding(12);
            pnlCozInputCard.Size = new Size(952, 340);
            pnlCozInputCard.TabIndex = 1;
            // 
            // txtGelenSifre
            // 
            txtGelenSifre.BackColor = Color.FromArgb(20, 20, 20);
            txtGelenSifre.BorderStyle = BorderStyle.None;
            txtGelenSifre.Dock = DockStyle.Bottom;
            txtGelenSifre.Font = new Font("Consolas", 10.5F);
            txtGelenSifre.ForeColor = Color.Silver;
            txtGelenSifre.Location = new Point(12, 45);
            txtGelenSifre.Name = "txtGelenSifre";
            txtGelenSifre.Size = new Size(928, 283);
            txtGelenSifre.TabIndex = 1;
            txtGelenSifre.Text = "";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI Bold", 11F, FontStyle.Bold);
            label5.ForeColor = Color.FromArgb(250, 160, 0);
            label5.Location = new Point(12, 12);
            label5.Name = "label5";
            label5.Size = new Size(207, 20);
            label5.TabIndex = 8;
            label5.Text = "Gelen Mesajlar ve İçerik:";
            // 
            // btnPostaCek
            // 
            btnPostaCek.BackColor = Color.FromArgb(0, 122, 204);
            btnPostaCek.Cursor = Cursors.Hand;
            btnPostaCek.FlatAppearance.BorderSize = 0;
            btnPostaCek.FlatStyle = FlatStyle.Flat;
            btnPostaCek.Font = new Font("Segoe UI Bold", 10F, FontStyle.Bold);
            btnPostaCek.Location = new Point(20, 15);
            btnPostaCek.Name = "btnPostaCek";
            btnPostaCek.Size = new Size(250, 40);
            btnPostaCek.TabIndex = 0;
            btnPostaCek.Text = "📧 E-POSTALARI KONTROL ET";
            btnPostaCek.UseVisualStyleBackColor = false;
            btnPostaCek.Click += btnPostaCek_Click;
            // 
            // tabPage3
            // 
            tabPage3.BackColor = Color.FromArgb(20, 20, 20);
            tabPage3.Controls.Add(pnlSettingsCard);
            tabPage3.ForeColor = Color.White;
            tabPage3.Location = new Point(4, 26);
            tabPage3.Name = "tabPage3";
            tabPage3.Padding = new Padding(30);
            tabPage3.Size = new Size(992, 770);
            tabPage3.TabIndex = 2;
            tabPage3.Text = "   AYARLAR   ";
            // 
            // pnlSettingsCard
            // 
            pnlSettingsCard.BackColor = Color.FromArgb(32, 32, 32);
            pnlSettingsCard.Controls.Add(lblKullaniciAdi);
            pnlSettingsCard.Controls.Add(txtKullaniciAdi);
            pnlSettingsCard.Controls.Add(btnAyarlariKaydet);
            pnlSettingsCard.Controls.Add(chkShowPass);
            pnlSettingsCard.Controls.Add(txtKendiSifre);
            pnlSettingsCard.Controls.Add(lblKendiSifre);
            pnlSettingsCard.Controls.Add(txtKendiMail);
            pnlSettingsCard.Controls.Add(lblKendiMail);
            pnlSettingsCard.Controls.Add(lblAyarInfo);
            pnlSettingsCard.Location = new Point(30, 30);
            pnlSettingsCard.Name = "pnlSettingsCard";
            pnlSettingsCard.Size = new Size(932, 450);
            pnlSettingsCard.TabIndex = 0;
            // 
            // 
            // txtKullaniciAdi
            // 
            txtKullaniciAdi.BackColor = Color.FromArgb(45, 45, 45);
            txtKullaniciAdi.BorderStyle = BorderStyle.FixedSingle;
            txtKullaniciAdi.ForeColor = Color.White;
            txtKullaniciAdi.Location = new Point(40, 100);
            txtKullaniciAdi.Name = "txtKullaniciAdi";
            txtKullaniciAdi.PlaceholderText = "Adınız Soyadınız";
            txtKullaniciAdi.Size = new Size(500, 25);
            txtKullaniciAdi.TabIndex = 7;
            // 
            // lblKullaniciAdi
            // 
            lblKullaniciAdi.AutoSize = true;
            lblKullaniciAdi.Location = new Point(40, 75);
            lblKullaniciAdi.Name = "lblKullaniciAdi";
            lblKullaniciAdi.Size = new Size(111, 19);
            lblKullaniciAdi.TabIndex = 8;
            lblKullaniciAdi.Text = "Görünen Adınız:";
            // 
            // txtKendiMail
            // 
            txtKendiMail.BackColor = Color.FromArgb(45, 45, 45);
            txtKendiMail.BorderStyle = BorderStyle.FixedSingle;
            txtKendiMail.ForeColor = Color.White;
            txtKendiMail.Location = new Point(40, 180);
            txtKendiMail.Name = "txtKendiMail";
            txtKendiMail.Size = new Size(500, 25);
            txtKendiMail.TabIndex = 2;
            // 
            // lblKendiMail
            // 
            lblKendiMail.AutoSize = true;
            lblKendiMail.Location = new Point(40, 155);
            lblKendiMail.Name = "lblKendiMail";
            lblKendiMail.Size = new Size(144, 19);
            lblKendiMail.TabIndex = 1;
            lblKendiMail.Text = "Kendi E-Posta Adresin:";
            // 
            // txtKendiSifre
            // 
            txtKendiSifre.BackColor = Color.FromArgb(45, 45, 45);
            txtKendiSifre.BorderStyle = BorderStyle.FixedSingle;
            txtKendiSifre.ForeColor = Color.White;
            txtKendiSifre.Location = new Point(40, 260);
            txtKendiSifre.Name = "txtKendiSifre";
            txtKendiSifre.Size = new Size(500, 25);
            txtKendiSifre.TabIndex = 4;
            txtKendiSifre.UseSystemPasswordChar = true;
            // 
            // lblKendiSifre
            // 
            lblKendiSifre.AutoSize = true;
            lblKendiSifre.Location = new Point(40, 235);
            lblKendiSifre.Name = "lblKendiSifre";
            lblKendiSifre.Size = new Size(160, 19);
            lblKendiSifre.TabIndex = 3;
            lblKendiSifre.Text = "Uygulama Şifresi (Gmail):";
            // 
            // chkShowPass
            // 
            chkShowPass.AutoSize = true;
            chkShowPass.Font = new Font("Segoe UI", 9F);
            chkShowPass.ForeColor = Color.Silver;
            chkShowPass.Location = new Point(40, 295);
            chkShowPass.Name = "chkShowPass";
            chkShowPass.Size = new Size(100, 19);
            chkShowPass.TabIndex = 5;
            chkShowPass.Text = "Şifreyi Göster";
            chkShowPass.UseVisualStyleBackColor = true;
            chkShowPass.CheckedChanged += chkShowPass_CheckedChanged;
            // 
            // btnAyarlariKaydet
            // 
            btnAyarlariKaydet.BackColor = Color.FromArgb(0, 192, 0);
            btnAyarlariKaydet.Cursor = Cursors.Hand;
            btnAyarlariKaydet.FlatAppearance.BorderSize = 0;
            btnAyarlariKaydet.FlatStyle = FlatStyle.Flat;
            btnAyarlariKaydet.Font = new Font("Segoe UI Bold", 11F, FontStyle.Bold);
            btnAyarlariKaydet.ForeColor = Color.Black;
            btnAyarlariKaydet.Location = new Point(40, 340);
            btnAyarlariKaydet.Name = "btnAyarlariKaydet";
            btnAyarlariKaydet.Size = new Size(250, 45);
            btnAyarlariKaydet.TabIndex = 6;
            btnAyarlariKaydet.Text = "💾 AYARLARI KAYDET";
            btnAyarlariKaydet.UseVisualStyleBackColor = false;
            btnAyarlariKaydet.Click += btnAyarlariKaydet_Click;
            // 
            // lblAyarInfo
            // 
            lblAyarInfo.AutoSize = true;
            lblAyarInfo.Font = new Font("Segoe UI Bold", 14F, FontStyle.Bold);
            lblAyarInfo.ForeColor = Color.FromArgb(0, 160, 250);
            lblAyarInfo.Location = new Point(40, 40);
            lblAyarInfo.Name = "lblAyarInfo";
            lblAyarInfo.Size = new Size(244, 25);
            lblAyarInfo.TabIndex = 0;
            lblAyarInfo.Text = "⚙ E-POSTA YAPILANDIRMA";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(15, 15, 15);
            ClientSize = new Size(1000, 800);
            Controls.Add(tabControl1);
            Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            ForeColor = Color.White;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Antigravity Crypto & Mail System - Enterprise Edition";
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            pnlMailCard.ResumeLayout(false);
            pnlMailCard.PerformLayout();
            pnlActionCard.ResumeLayout(false);
            pnlActionCard.PerformLayout();
            pnlOutputCard.ResumeLayout(false);
            pnlOutputCard.PerformLayout();
            pnlInputCard.ResumeLayout(false);
            pnlInputCard.PerformLayout();
            tabPage2.ResumeLayout(false);
            pnlCozOutputCard.ResumeLayout(false);
            pnlCozOutputCard.PerformLayout();
            pnlCozActionCard.ResumeLayout(false);
            pnlCozActionCard.PerformLayout();
            pnlCozInputCard.ResumeLayout(false);
            pnlCozInputCard.PerformLayout();
            tabPage3.ResumeLayout(false);
            pnlSettingsCard.ResumeLayout(false);
            pnlSettingsCard.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private TabPage tabPage3;
        private RichTextBox txtGiris;
        private ComboBox cmbAlgoritma;
        private Button btnSifrele;
        private Button btnPostaCek;
        private ComboBox cmbCozAlgoritma;
        private RichTextBox txtGelenSifre;
        private Button btnCoz;
        private TextBox txtCozAnahtar;
        private Label label1;
        private Button btnMailGonder;
        private Label txtAliciMail;
        private RichTextBox txtSonuc;
        private TextBox txtMailAdresi;
        private Label label2;
        private Label label3;
        private RichTextBox txtCozulenSonuc;
        private Label label4;
        private Label label5;
        private Label lblSifreAnahtarLine;
        private TextBox txtSifreAnahtar;
        private Panel pnlInputCard;
        private Panel pnlOutputCard;
        private Panel pnlActionCard;
        private Label lblAlgSec;
        private Panel pnlMailCard;
        private Panel pnlCozInputCard;
        private Panel pnlCozActionCard;
        private Label lblCozAlg;
        private Panel pnlCozOutputCard;
        private Panel pnlSettingsCard;
        private Label lblKendiMail;
        private Label lblAyarInfo;
        private TextBox txtKendiSifre;
        private Label lblKendiSifre;
        private TextBox txtKendiMail;
        private CheckBox chkShowPass;
        private Button btnAyarlariKaydet;
        private ListBox lstGelenMesajlar;
        private TextBox txtKullaniciAdi;
        private Label lblKullaniciAdi;
    }
}
