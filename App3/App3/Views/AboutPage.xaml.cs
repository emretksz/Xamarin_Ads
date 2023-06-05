using Microsoft.IdentityModel.Tokens;
using System;
using System.ComponentModel;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App3.Views
{
    public partial class AboutPage : ContentPage
    {
        private readonly string privateKey = "uTWwq{#ZhTX&$T4mE]]O,.;C>e1;00S41cuPijS.d&dtk+f9Q'/BPTc(Ymn";
        public AboutPage()
        {
            ///BURADA FARKLI BİR KONTROL YAPACAĞIM TEST EDİYORUM!!! KALDIR SONRA BUNU!!
            CheckAccess();
            InitializeComponent(); 
        }

        private async Task<bool> CheckAccess()
        {
            string token = await SecureStorage.GetAsync("token");

            // JWT doğrulama ve yetkilendirme kontrolü
            if (!string.IsNullOrEmpty(token) && ValidateToken(token))
            {
                string username = GetUsernameFromToken(token);
                // Kullanıcının rolünü veya yetkilerini kontrol edin
                //if (CheckUserRole(username, "Admin"))
                //{
                //    return true; // Erişim izni var
                //}
                return true;
            }
            await Shell.Current.GoToAsync(state: "//LoginPage");
            return false; // Erişim izni yok
        }
        private bool ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(privateKey);

            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                }, out SecurityToken validatedToken);

                return true;
            }
            catch
            {
                return false;
            }
        }
        private string GetUsernameFromToken(string token)
        {
 
            string username = ""; 

            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;
                var typess = jwtToken.Claims.ToList();
                username = jwtToken.Claims.First(claim => claim.Type == "unique_name").Value;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Token çözümleme hatası: " + ex.Message);
            }

            return username;
        }
    }
}