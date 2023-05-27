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
using App3.Entities;
using System.Collections.ObjectModel;
using Acr.UserDialogs;

namespace App3.Views
{
    public partial class ItemsPage : ContentPage
    {
        ItemsViewModel _viewModel;
        private bool isButtonClickable = true;
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
            var temps = category.GetItems() as List<Category>;
            Category selectOption = new Category()
            {
                Id = 0,
                Name = "Tüm Kategoriler"

            };
             temps.Insert(0, selectOption);
            DropdownPicker.ItemsSource = temps;

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
 
            var selectedOption = DropdownPicker.SelectedItem  as Category;
            List<Product> getFilterProduct = new List<Product>();
           
            if (selectedOption.Id>0)
                getFilterProduct.AddRange(_viewModel.Items.Where(a => a.CategoryId == Convert.ToInt32(selectedOption.Id)));
            else
                //kategori boşsa
                getFilterProduct.AddRange(_viewModel.Items);
          
            ObservableCollection<Product> Items = new ObservableCollection<Product>();
            foreach (var item in getFilterProduct)
            {
                Items.Add(item);
            }
            ItemsListView.ItemsSource = Items;
            //BindingContext = _viewModel = new ItemsViewModel(Convert.ToInt32(selectedOption.Id));
        }
     
        public async void DetayaGit(object sender,EventArgs e)
        {
            var item = ((Button)sender).BindingContext;
            await Navigation.PushAsync(new ItemDetailPage(item.ToString()));
   
        }
        private async void ReklamIzle(object sender, EventArgs e)
        {
            //CrossMTAdmob.Current.AdsId = "ca-app-pub-3940256099942544~3347511713";
            //CrossMTAdmob.Current.LoadInterstitial("ca-app-pub-3940256099942544/5224354917");
            //CrossMTAdmob.Current.ShowInterstitial();
           
            if (isButtonClickable)
            {
                isButtonClickable = false;
                // Buton tıklanabilir durumdaysa işlemleri gerçekleştir
                UserDialogs.Instance.ShowLoading("Please Waitin...");
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
                    UserDialogs.Instance.HideLoading();
                    DisplayAlert("Dikkat", "günlük reklam izleme limitiniz doldu", "tamam");
                }
                // 5 saniye bekle
                await System.Threading.Tasks.Task.Delay(5000);
                UserDialogs.Instance.HideLoading();
                // Bekleme süresi sona erdikten sonra butonu tekrar tıklanabilir hale getir
                isButtonClickable = true;
                
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