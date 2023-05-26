using App3.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App3.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            this.BindingContext = new LoginViewModel();
        }

        public async void OnLoginButtonClicked(object sender,EventArgs e)
        {
            string username = usernameEntry.Text;
            string password = passwordEntry.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                await DisplayAlert("Hata", "Lütfen kullanıcı adı ve şifrenizi girin.", "Tamam");
            }
            else if (username == "emre" && password == "1234")
            {
                await Shell.Current.GoToAsync(state: "//main");
            }
            else
            {
                await DisplayAlert("Hata", "Yanlış kullanıcı adı veya şifre.", "Tamam");
            }
       
        }
    }
}