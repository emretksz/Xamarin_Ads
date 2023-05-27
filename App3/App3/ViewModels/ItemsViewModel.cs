
using App3.Entities;
using App3.Services.Management;
using App3.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace App3.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        private Product _selectedItem;
        public int categoryId = 0;
      
        public ObservableCollection<Product> Items { get; }
        public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get; }
        public Command<Product> ItemTapped { get; }

        public ItemsViewModel(int categoryId = 0)
        {
            Title = "Browse";
            Items = new ObservableCollection<Product>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand(categoryId));

            ItemTapped = new Command<Product>(OnItemSelected);

            AddItemCommand = new Command(OnAddItem);
        }

        async Task ExecuteLoadItemsCommand(int categoryId=0)
        {
            IsBusy = true;
            ProductManager productManager = new ProductManager();
            try
            {
                Items.Clear();
                IEnumerable <Product> productList = new List<Product>();
                if (categoryId>0)
                    productList = await productManager.GetItemByCategoryIdAsync(categoryId);
                else
                    productList = await productManager.GetItemsAsync();
                 
                foreach (var item in productList)
                {
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedItem = null;
        }

        public Product SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }

        private async void OnAddItem(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewItemPage));
        }

        async void OnItemSelected(Product item)
        {
            if (item == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.id)}={item.Id}");
        }
    }
}