
using App3.Services.Management;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App3.ViewModels
{
    [QueryProperty(nameof(id), nameof(id))]
    public class ItemDetailViewModel : BaseViewModel
    {
        //private string itemId;
        private string text;
        private string description;
        public string Id { get; set; }
        public ItemDetailViewModel(string id)
        {
            Id = id.ToString();
            LoadItemId(Id);
        }
        public string Text
        {
            get => text;
            set => SetProperty(ref text, value);
        }

        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }

        public string id
        {
            get
            {
                return Id;
            }
            set
            {
                Id = value;
                LoadItemId(value);
            }
        }

        public async void LoadItemId(string itemId)
        {
            try
            {
                ProductManager productManager = new ProductManager();
                var item = await productManager.GetItemAsync(itemId);//await DataStore.GetItemAsync(itemId);
                Id = item.Id.ToString();
                Text = item.Title;
                Description = item.Description;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }
    }
}
