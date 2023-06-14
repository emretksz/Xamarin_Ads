using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using App3.Services.Management;
using App3.ViewModels;
using App3.Entities;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Xamarin.Essentials;

namespace App3.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        private readonly string privateKey = "uTWwq{#ZhTX&$T4mE]]O,.;C>e1;00S41cuPijS.d&dtk+f9Q'/BPTc(Ymn";
        public LoginPage()
        {
          var qq= SecureStorage.GetAsync("token").Result;
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
        public async void CreatePageOpen(object sender,EventArgs e)
        {
            await Navigation.PushAsync(new CreatePage());
            //await Shell.Current.GoToAsync(state: "//CreatePage");
        }
        /// <summary>
        /// 
        /// 
        /// CREATE ALANINDA BU KONTROL YAPILIYORRRRR CREATE BUTONU KOY SONRA LOGİN EKRANI !!!!!!!!!!!!!!!!!!!!!!!!!!!
        /// EKSİK BURALARR!!!!!!!!!!!!!!!!!!!!!!!
        /// 
        /// <param name="e"></param>

        public async void OnCreateUser(object sender, EventArgs e)
        {
            // Örnek kullanım:
            // Rastgele bir tuz oluşturulması gerekmektedir
            UserManager userManager = new UserManager();
            string jtwUser = "";
            if (String.IsNullOrEmpty(usernameEntry.Text)==false&& String.IsNullOrEmpty(passwordEntry.Text) == false)
            {
                Entities.User user = new Entities.User();
                user.UserName= usernameEntry.Text;
                
                var checkUser = await userManager.CheckUserForCreate(user);
                switch (checkUser)
                {
                    case "1":
                        var password = HtmlScriptRegex(passwordEntry.Text);
                        string hashedPassword = HashPassword(password, privateKey);
                        user.Password = hashedPassword;
                       var result = await userManager.AddUser(user);
                        jtwUser = user.UserName;
                        if (result=="0")
                        {
                            await DisplayAlert("Hata", "Kayıt İşlemi Başarısız!", "Tamam");
                            return;
                        }
                        break; 
                    case "0":
                        await DisplayAlert("Hata", "Kullanıcı adı ve şifrenizi doğru girdiğinizden emin olun!", "Tamam");
                        return;
                        break;           
                    case "-1":
                        await DisplayAlert("Hata", "Kayıt İşlemi Başarısız!", "Tamam");
                        return;
                        break;
            
                }
             
            }
            else
            {
                await DisplayAlert("Hata", "Kullanıcı Adı ve Şifre Boş Olamaz!", "Tamam");
            }
        
            string token = await GenerateToken(jtwUser);

            // JWT'yi cihazda saklama örneği
            // Xamarin.Essentials SecureStorage kullanarak
           await  SecureStorage.SetAsync("token", token);
            string tokena = await SecureStorage.GetAsync("token");
            await Shell.Current.GoToAsync(state: "//main");
        }
        public  string HashPassword(string password, string salt)
        {
            using (var sha256 = SHA256.Create())
            {
                var saltedPassword = $"{password}{salt}";
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(saltedPassword));
                return Convert.ToBase64String(hashedBytes);
            }
        }
        public string HtmlScriptRegex( string htmlText)
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
        private async Task<string> GenerateToken(string username)
        {
            // JWT ayarları
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(privateKey); // Gizli anahtarınızı burada belirtin
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
            new Claim(ClaimTypes.Name, username),
            // Kullanıcının rollerini veya diğer bilgilerini burada ekleyebilirsiniz.
            // new Claim(ClaimTypes.Role, "Admin"),
            // new Claim("CustomClaim", "Value")
        }),
                Expires = DateTime.UtcNow.AddDays(7), // Token'in geçerlilik süresini belirleyin
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }
    }
}