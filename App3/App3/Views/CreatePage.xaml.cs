using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App3.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreatePage : ContentPage
    {
        public CreatePage()
        {
            InitializeComponent();
        }
        public string HtmlScriptRegex(string htmlText)
        {
            string textReplace = htmlText;
            string regexPattern = @"[<>£#$½\[\]\}\|\{]|javascript|script";
            if (Regex.IsMatch(textReplace, regexPattern, RegexOptions.IgnoreCase))
            {
                Regex specialCharsRegex = new Regex(regexPattern, RegexOptions.IgnoreCase);
                htmlText = specialCharsRegex.Replace(textReplace, "");

            }
            return htmlText;
        }
        private async void SaveButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                ServicePointManager.ServerCertificateValidationCallback = (s, certificate, chain, sslPolicyErrors) => true;

                //System.Net.ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;

                var name = HtmlScriptRegex(usernameEntry.Text);
                var password = HtmlScriptRegex(passwordEntry.Text);
                var email = HtmlScriptRegex(emailEntry.Text);
                var reference = HtmlScriptRegex(refcodeEntry.Text);
                Random random = new Random();
                int verificationCode = random.Next(100000, 999999);


                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("mail.emretoksoz.com");

                mail.From = new MailAddress("noreply@emretoksoz.com");
                mail.To.Add(email); 
                mail.Subject = "Doğrulama Kodu";
                mail.Body = "Doğrulama kodunuz: " + "\n" + verificationCode.ToString();

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential(""); 
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);

                DisplayAlert("Başarılı", "Doğrulama kodu e-posta olarak gönderildi.", "Tamam");
                await Navigation.PushAsync(new VerificationPage());
            }
            catch (Exception ex)
            {
                DisplayAlert("Hata", "E-posta gönderme işlemi başarısız oldu: " + ex.Message, "Tamam");
            }
        }
    }
}