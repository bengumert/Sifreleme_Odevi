using MainForm.Algorithms;
using MainForm.Services;
using System.Text.Json;
using System.IO;

namespace MainForm
{
    public partial class Form1 : Form
    {
        private readonly EmailService _emailService;
        private List<CryptoMailMessage> _incomingMessages = new List<CryptoMailMessage>();
        private readonly string _settingsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "settings.json");

        public Form1()
        {
            InitializeComponent();
            _emailService = new EmailService();
            SetupInitialUI();
            LoadUserSettings();
        }

        private void SetupInitialUI()
        {
            // Listeye eklenecek tüm algoritmalar
            string[] algoritmalar = {
        "Kaydırmalı",
        "Doğrusal",
        "Yer Değiştirme",
        "Sayı Anahtarlı",
        "Vigenere",
        "Permütasyon",
        "Rota",
        "Zigzag",
        "4 Kare",
        "Hill",
        "DSA (Dijital İmza)"
    };

            // Şifreleme kısmındaki listeyi doldur
            cmbAlgoritma.Items.Clear();
            cmbAlgoritma.Items.AddRange(algoritmalar);

            // Çözme kısmındaki listeyi doldur
            cmbCozAlgoritma.Items.Clear();
            cmbCozAlgoritma.Items.AddRange(algoritmalar);

            // İlk sıradakini seçili getir
            cmbAlgoritma.SelectedIndex = 0;
            cmbCozAlgoritma.SelectedIndex = 0;

            UpdateKeyHint();
        }

        #region Settings Management
        private void LoadUserSettings()
        {
            if (File.Exists(_settingsPath))
            {
                try
                {
                    string json = File.ReadAllText(_settingsPath);
                    var settings = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
                    if (settings != null)
                    {
                        txtKullaniciAdi.Text = settings.GetValueOrDefault("UserName", "");
                        txtKendiMail.Text = settings.GetValueOrDefault("Email", "");
                        txtKendiSifre.Text = settings.GetValueOrDefault("Password", "");
                    }
                }
                catch { /* Ignore errors on load */ }
            }
        }

        private void btnAyarlariKaydet_Click(object sender, EventArgs e)
        {
            try
            {
                var settings = new Dictionary<string, string>
                {
                    { "UserName", txtKullaniciAdi.Text },
                    { "Email", txtKendiMail.Text },
                    { "Password", txtKendiSifre.Text }
                };
                string json = JsonSerializer.Serialize(settings);
                File.WriteAllText(_settingsPath, json);
                MessageBox.Show("Ayarlar başarıyla kaydedildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Kaydetme hatası: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Encryption Logic
        private void btnSifrele_Click(object sender, EventArgs e) => ProcessCrypto(true);
        private void btnCoz_Click(object sender, EventArgs e) => ProcessCrypto(false);

        private void ProcessCrypto(bool encrypt)
        {
            string text = encrypt ? txtGiris.Text : txtGelenSifre.Text;
            string key = encrypt ? txtSifreAnahtar.Text : txtCozAnahtar.Text;
            string algorithm = (encrypt ? cmbAlgoritma.SelectedItem : cmbCozAlgoritma.SelectedItem)?.ToString();

            if (string.IsNullOrWhiteSpace(text))
            {
                MessageBox.Show("Lütfen işlem yapılacak metni girin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string result = encrypt ? algorithm switch
                {
                    "Kaydırmalı" => CryptoAlgorithms.CaesarEncrypt(text, key),
                    "Doğrusal" => CryptoAlgorithms.AffineEncrypt(text, key),
                    "Yer Değiştirme" => CryptoAlgorithms.SubstitutionEncrypt(text, key),
                    "Sayı Anahtarlı" => CryptoAlgorithms.NumericKeyEncrypt(text, key),
                    "Vigenere" => CryptoAlgorithms.VigenereEncrypt(text, key),
                    "Permütasyon" => CryptoAlgorithms.PermutationEncrypt(text, key),
                    "Rota" => CryptoAlgorithms.RouteEncrypt(text, key),
                    "Zigzag" => CryptoAlgorithms.RailFenceEncrypt(text, key),
                    "4 Kare" => CryptoAlgorithms.FourSquareEncrypt(text, key), // EKLEDİK
                    "Hill" => CryptoAlgorithms.HillEncrypt(text, key),         // EKLEDİK
                    "DSA (Dijital İmza)" => CryptoAlgorithms.DSAEncrypt(text, key),
                    _ => "Algoritma seçilmedi."
                } : algorithm switch
                {
                    "Kaydırmalı" => CryptoAlgorithms.CaesarDecrypt(text, key),
                    "Doğrusal" => CryptoAlgorithms.AffineDecrypt(text, key),
                    "Yer Değiştirme" => CryptoAlgorithms.SubstitutionDecrypt(text, key),
                    "Sayı Anahtarlı" => CryptoAlgorithms.NumericKeyDecrypt(text, key),
                    "Vigenere" => CryptoAlgorithms.VigenereDecrypt(text, key),
                    "Permütasyon" => CryptoAlgorithms.PermutationDecrypt(text, key),
                    "Rota" => CryptoAlgorithms.RouteDecrypt(text, key),
                    "Zigzag" => CryptoAlgorithms.RailFenceDecrypt(text, key),
                    "4 Kare" => CryptoAlgorithms.FourSquareDecrypt(text, key), // EKLEDİK
                    "Hill" => CryptoAlgorithms.HillDecrypt(text, key),         // EKLEDİK
                    "DSA (Dijital İmza)" => CryptoAlgorithms.DSADecrypt(text, key),
                    _ => "Algoritma seçilmedi."
                };

                if (encrypt) txtSonuc.Text = result;
                else txtCozulenSonuc.Text = result;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Mail Integration
        private async void btnPostaCek_Click(object sender, EventArgs e)
        {
            string email = txtKendiMail.Text;
            string pass = txtKendiSifre.Text;

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(pass))
            {
                MessageBox.Show("Lütfen önce Ayarlar sekmesinden bilgilerinizi girin ve kaydedin.",
                    "Eksik Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tabControl1.SelectedIndex = 2;
                return;
            }

            try
            {
                btnPostaCek.Text = "⌛ Çekiliyor...";
                btnPostaCek.Enabled = false;

                _incomingMessages = await _emailService.ReceiveEncryptedEmailsAsync(email, pass);

                if (_incomingMessages != null && _incomingMessages.Count > 0)
                {
                    // Tarihe göre en yeniden eskiye sırala
                    _incomingMessages = _incomingMessages.OrderByDescending(m => m.Date).ToList();

                    // Kullanıcıya mesaj seçtir
                    ShowMailSelectionDialog();
                }
                else
                {
                    MessageBox.Show("Gelen kutusunda herhangi bir mesaj bulunamadı.",
                        "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"E-posta çekme hatası: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnPostaCek.Text = "📧 E-POSTALARI KONTROL ET";
                btnPostaCek.Enabled = true;
            }
        }

        private void ShowMailSelectionDialog()
        {
            // Dinamik seçim formu oluştur
            var dialog = new Form
            {
                Text = "Mesaj Seçin",
                Size = new Size(900, 400),
                StartPosition = FormStartPosition.CenterParent,
                MinimizeBox = false,
                MaximizeBox = false,
                FormBorderStyle = FormBorderStyle.FixedDialog
            };

            var label = new Label
            {
                Text = $"{_incomingMessages.Count} mesaj bulundu. Çözmek istediğiniz mesajı seçin:",
                Dock = DockStyle.Top,
                Height = 30,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(5)
            };

            var listBox = new ListBox
            {
                Dock = DockStyle.Fill,
                Font = new Font("Consolas", 9f),
                ItemHeight = 18
            };

            foreach (var msg in _incomingMessages)
            {
                listBox.Items.Add($"{msg.Date:dd.MM.yyyy HH:mm}  |  {msg.Sender}  |  {msg.Subject}");
            }

            if (listBox.Items.Count > 0)
                listBox.SelectedIndex = 0;

            var btnSec = new Button
            {
                Text = "Bu Mesajı Seç",
                Dock = DockStyle.Bottom,
                Height = 40,
                BackColor = Color.RoyalBlue,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10f, FontStyle.Bold)
            };

            btnSec.Click += (s, ev) =>
            {
                if (listBox.SelectedIndex < 0) return;

                var selected = _incomingMessages[listBox.SelectedIndex];
                string body = selected.Body?.Trim();

                if (string.IsNullOrEmpty(body))
                {
                    MessageBox.Show(
                        $"Bu mailin içeriği boş.\n\nGönderen: {selected.Sender}\nKonu: {selected.Subject}",
                        "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                txtGelenSifre.Text = body;
                dialog.Close();

                MessageBox.Show(
                    $"Şifreli mesaj yüklendi.\n\nGönderen: {selected.Sender}\nKonu: {selected.Subject}\nTarih: {selected.Date:dd.MM.yyyy HH:mm}",
                    "Mesaj Alındı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                tabControl1.SelectedIndex = 1;
            };

            // Çift tıklayınca da seç
            listBox.DoubleClick += (s, ev) => btnSec.PerformClick();

            dialog.Controls.Add(listBox);
            dialog.Controls.Add(btnSec);
            dialog.Controls.Add(label);
            dialog.ShowDialog(this);
        }
        #endregion

        #region Send Mail
        private async void btnMailGonder_Click(object sender, EventArgs e)
        {
            string senderName = txtKullaniciAdi.Text;
            string senderMail = txtKendiMail.Text;
            string appPassword = txtKendiSifre.Text;
            string recipientEmail = txtMailAdresi.Text;
            string content = txtSonuc.Text;

            if (string.IsNullOrWhiteSpace(senderMail) || string.IsNullOrWhiteSpace(appPassword))
            {
                MessageBox.Show("Lütfen önce Ayarlar sekmesinden bilgilerinizi kaydedin.",
                    "Eksik Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tabControl1.SelectedIndex = 2;
                return;
            }

            if (string.IsNullOrWhiteSpace(recipientEmail) || string.IsNullOrWhiteSpace(content))
            {
                MessageBox.Show("Alıcı adresi ve şifreli metin gereklidir.",
                    "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                btnMailGonder.Text = "⌛ Gönderiliyor...";
                btnMailGonder.Enabled = false;

                // ÖDEV KURALI: Konuda asla yöntem adı veya ipucu bulunmaz.
                string subject = "Gizli Mesaj";
                string finalSenderName = string.IsNullOrWhiteSpace(senderName) ? "Bilinmeyen Kullanıcı" : senderName;

                await _emailService.SendEncryptedEmailAsync(
                    finalSenderName, senderMail, appPassword, recipientEmail, subject, content);

                MessageBox.Show("Mesaj başarıyla gönderildi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Gönderme hatası: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnMailGonder.Text = "✉ E-POSTA GÖNDER";
                btnMailGonder.Enabled = true;
            }
        }
        #endregion

        #region Helper Methods
        private void cmbAlgoritma_SelectedIndexChanged(object sender, EventArgs e) => UpdateKeyHint();
        private void cmbCozAlgoritma_SelectedIndexChanged(object sender, EventArgs e) => UpdateKeyHint();

        private void UpdateKeyHint()
        {
            string alg = tabControl1.SelectedIndex == 0
                ? cmbAlgoritma.SelectedItem?.ToString()
                : cmbCozAlgoritma.SelectedItem?.ToString();

            string hint = alg switch
            {
                "Kaydırmalı" => "Örn: 3",
                "Doğrusal" => "Örn: 5,8 (a,b)",
                "Yer Değiştirme" => "29 harf karışık alfabe",
                "Sayı Anahtarlı" => "Sütun Sayısı (Örn: 4)",
                "Vigenere" => "Anahtar Kelime (Örn: GIZLI)",
                "Permütasyon" => "Örn: 3,1,0,2",
                "Rota" => "Satır Sayısı (Örn: 5)",
                "Zigzag" => "Hat Sayısı (Örn: 3)",
                "4 Kare" => "Anahtar Kelime (Örn: ELMA)", 
                "Hill" => "4 Sayı (Örn: 3,3,2,5)",          
                "DSA (Dijital İmza)" => "Anahtar Gerekmez",
                _ => ""
            };

            lblSifreAnahtarLine.Text = $"Anahtar ({hint}):";
            label1.Text = $"Anahtar ({hint}):";
        }

        private void chkShowPass_CheckedChanged(object sender, EventArgs e)
            => txtKendiSifre.UseSystemPasswordChar = !chkShowPass.Checked;
        #endregion

    }
}