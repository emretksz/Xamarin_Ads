using App3.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace App3.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage(string id)
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel(id);
        }
    }
}