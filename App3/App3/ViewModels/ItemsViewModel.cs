
using App3.Entities;
using App3.Entities.Dto;
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
        public int categoryId = 0;
        public List<Product> TempProduct { get; set; }
        //public ObservableCollection<Category> CategoryList { get; }
        //public Command LoadItemsCommand { get; }


        public ItemsViewModel(int categoryId = 0)
        {
            Title = "Browse";
            //Items = new ObservableCollection<Product>();
            //LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand(categoryId));
        }

       public async Task<ItemsViewDto> ExecuteLoadItemsCommand(int categoryId=0)
        {
            ItemsViewDto viewList = new ItemsViewDto();
            viewList.Items = new List<Product>();
            viewList.CategoryList = new List<Category>();
            IsBusy = true;
            ProductManager productManager = new ProductManager();
            TempProduct = new List<Product>();
            try
            {
                //Items.Clear();
                IEnumerable <Product> productList = new List<Product>();
           
                if (categoryId>0)
                    productList = await productManager.GetItemByCategoryIdAsync(categoryId);
                else
                {

                    var result = await productManager.ProductAndCategory();
                    productList = result.Product;
                    foreach (var item in result.Category)
                    {
                        viewList.CategoryList.Add(item);
                    }
                  
                }
                 
                foreach (var item in productList)
                {
                    viewList.Items.Add(item);
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

            TempProduct.AddRange(viewList.Items);
            viewList.LoadItemsCommand = new Command(async () => await GetProduct());
            return viewList;
        }
        public async Task<List<Product>> GetProduct()
        {
            return TempProduct;
        }
        public void OnAppearing()
        {
            IsBusy = true;
         
        }

    
    }
}