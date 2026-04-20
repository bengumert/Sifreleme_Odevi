using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailKit;
using MailKit.Net.Imap;
using MailKit.Search;
using MimeKit;
using MailKit.Security;
using MailKit.Net.Smtp;

namespace MainForm.Services
{
    public class CryptoMailMessage
    {
        public string Sender { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public DateTime Date { get; set; }
    }

    public class EmailService
    {
        public string SmtpServer { get; set; } = "smtp.gmail.com";
        public int SmtpPort { get; set; } = 465;
        public string ImapServer { get; set; } = "imap.gmail.com";
        public int ImapPort { get; set; } = 993;

        public async Task SendEncryptedEmailAsync(string senderDisplayName, string senderEmail, string appPassword, string recipientEmail, string subject, string body)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(senderDisplayName, senderEmail));
            message.To.Add(new MailboxAddress("", recipientEmail));
            message.Subject = "[Mesaj] " + subject;

            message.Body = new TextPart("plain")
            {
                Text = body
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(SmtpServer, SmtpPort, true);
                await client.AuthenticateAsync(senderEmail, appPassword);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
        }

        public async Task<List<CryptoMailMessage>> ReceiveEncryptedEmailsAsync(string email, string appPassword)
        {
            var messages = new List<CryptoMailMessage>();

            using (var client = new ImapClient())
            {
                await client.ConnectAsync(ImapServer, ImapPort, true);
                await client.AuthenticateAsync(email, appPassword);

                var inbox = client.Inbox;
                await inbox.OpenAsync(FolderAccess.ReadOnly);

                // Tüm gelen kutusu maillerini çek
                var uids = await inbox.SearchAsync(SearchQuery.All);

                // En son 20 maili tara, kendi gönderdiğimiz hariç ilk 3'ü al
                var filtered = new List<CryptoMailMessage>();
                foreach (var uid in uids.Reverse().Take(20))
                {
                    var message = await inbox.GetMessageAsync(uid);

                    // Kendi gönderdiğimiz mailleri atla
                    string fromAddress = message.From.Mailboxes.FirstOrDefault()?.Address ?? "";
                    if (fromAddress.Equals(email, StringComparison.OrdinalIgnoreCase))
                        continue;

                    string body = message.TextBody;
                    if (string.IsNullOrWhiteSpace(body))
                    {
                        body = message.HtmlBody;
                        if (!string.IsNullOrWhiteSpace(body))
                            body = System.Text.RegularExpressions.Regex.Replace(body, "<[^>]+>", "").Trim();
                    }
                    if (string.IsNullOrWhiteSpace(body))
                    {
                        foreach (var part in message.BodyParts.OfType<TextPart>())
                        {
                            if (!string.IsNullOrWhiteSpace(part.Text))
                            {
                                body = part.Text.Trim();
                                break;
                            }
                        }
                    }

                    filtered.Add(new CryptoMailMessage
                    {
                        Sender = message.From.ToString(),
                        Subject = message.Subject,
                        Body = body?.Trim() ?? "",
                        Date = message.Date.LocalDateTime  // UTC değil, yerel saat
                    });

                    if (filtered.Count >= 3) break;
                }

                messages.AddRange(filtered);

                await client.DisconnectAsync(true);
            }

            return messages;
        }
    }
}