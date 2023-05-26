using App3.Services;
using App3.ViewModels;
using App3.Views;
using Entities.Models;
using MarcTron.Plugin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Text;
using System.Text.RegularExpressions;
using App3.Services.Management;
using App3.Models;

namespace App3.Views
{
    public partial class ItemsPage : ContentPage
    {
        ItemsViewModel _viewModel;
        public ICommand GoToDetailPageCommand => new Command<string>(GoToDetailPage);
        private void GoToDetailPage(string itemId)
        {
            // itemId değerine göre ilgili modelin detay sayfasına geçiş yapabilirsiniz
        }
        public ItemsPage()
        {
            InitializeComponent();
            //CrossMTAdmob.Current.OnInterstitialLoaded += (s, args) => {
            //    CrossMTAdmob.Current.ShowInterstitial();
            //};
            //CrossMTAdmob.Current.OnRewardedLoaded += (s, args) => {
            //    CrossMTAdmob.Current.ShowRewarded();
            //};

            ProductManager productManager = new ProductManager();
            CategoryManager category = new CategoryManager();
            puan.Text = "Reklam Sayısı: " + "10";
            BindingContext = _viewModel = new ItemsViewModel();
            DropdownPicker.ItemsSource = category.GetItems() as List<Category>;
            CrossMTAdmob.Current.OnRewardedLoaded += (s, args) =>
            {
                CrossMTAdmob.Current.ShowRewarded();
            };

        }

        
        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
           public void DropdownPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
 
            var selectedOption = DropdownPicker.SelectedItem as string;
            BindingContext = _viewModel.DataStore2.GetItemByCategoryIdAsync(Convert.ToInt32(selectedOption));
        }
     
        public async void DetayaGit(object sender,EventArgs e)
        {
            var item = ((Button)sender).BindingContext;
            await Navigation.PushAsync(new ItemDetailPage(item.ToString()));
   
        }
        private void ReklamIzle(object sender, EventArgs e)
        {
            //CrossMTAdmob.Current.AdsId = "ca-app-pub-3940256099942544~3347511713";
            //CrossMTAdmob.Current.LoadInterstitial("ca-app-pub-3940256099942544/5224354917");
            //CrossMTAdmob.Current.ShowInterstitial();
            var split = puan.Text.Split(':');
            if (split[1] != "0")
            {
                // CrossMTAdmob.Current.LoadInterstitial("ca-app-pub-3940256099942544/5224354917");

                var point = Convert.ToInt32(split[1].ToString().Trim());
                point = point - 1;
                puan.Text = "Reklam Sayısı:" + point.ToString();
                CrossMTAdmob.Current.LoadRewarded("ca-app-pub-3940256099942544/5224354917");
            }
            else
            {
                DisplayAlert("Dikkat", "günlük reklam izleme limitiniz doldu", "tamam");
            }
               /*CrossMTAdmob.Current.LoadInterstitial("ca-app-pub-3940256099942544/1033173712");*/
        }
        public async void ReklamIzle2(object sender, EventArgs e)
        {
            var split = puan.Text.Split(':');
            if (split[1]!= "0")
            {
               // CrossMTAdmob.Current.LoadInterstitial("ca-app-pub-3940256099942544/5224354917");

                var point = Convert.ToInt32(split[1].ToString().Trim());
                point = point-1;
                puan.Text = "Reklam Sayısı:"+ point.ToString();
            }
            else
            {
                DisplayAlert("Dikkat", "günlük reklam izleme limitiniz doldu","tamam");
            }
        

        }
        //private List<Product> GetProductsFromDatabase()
        //{
        //    using (var dbContext = new ET_AdsContext())
        //    {
        //        var products = dbContext.Products.ToList();
        //        return products;
        //    }
        //}

    }
}