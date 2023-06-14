using Acr.UserDialogs;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App3.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VerificationPage : ContentPage
    {
        private int countdownSeconds = 180; // Geri sayım süresi (saniye)
        private bool countdownRunning = false;
        public VerificationPage()
        {
            InitializeComponent();
            StartCountdown();
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
        private async void StartCountdown()
        {
            countdownRunning = true;

            while (countdownSeconds > 0)
            {
                TimeSpan remainingTime = TimeSpan.FromSeconds(countdownSeconds);
                countdownLabel.Text = remainingTime.ToString(@"mm\:ss");

                await System.Threading.Tasks.Task.Delay(1000); // 1 saniye bekleme

                countdownSeconds--;
            }

            countdownRunning = false;

            // Geri sayım tamamlandığında login sayfasına yönlendirme
            await Navigation.PushAsync(new LoginPage());
        }
        private void VerifyButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(verificationCodeEntry.Text))
                {
                    DisplayAlert("Dikkat", "Doğrulama Kodu Hatalı", "Tamam");
                }
                if (false)
                {
                    var verificationCode = HtmlScriptRegex(verificationCodeEntry.Text);
                }
             
            }
            catch (Exception)
            {

                throw;
            }
            finally {

            }
          
         
            //veritanındaki doğrulama koduyla bir mi 

        
        }  
        private async void CloseVerify(object sender, EventArgs e)
        {
            UserDialogs.Instance.ShowLoading("İşlem İptal Edildi!");
            //kullanıyı bulup email ve doğrulama kodunu sil
            //sonra logine gönder
            await Navigation.PushAsync(new LoginPage());
            UserDialogs.Instance.HideLoading();
        }
    }
    
}
