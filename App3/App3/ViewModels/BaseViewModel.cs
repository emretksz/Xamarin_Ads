
using App3.Entities;
using App3.Services;
using App3.Services.Interface;
using App3.Services.Management;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace App3.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public IDataStore<Product> DataStore => DependencyService.Get<IDataStore<Product>>();
        public IProductService DataStore2 => DependencyService.Get<ProductManager>();
        public ICategorySevice DataStore3 => DependencyService.Get<CategoryManager>();

        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        } 
        bool isClickBusy = true;
        public bool IsClickBusy
        {
            get { return isClickBusy; }
            set { SetProperty(ref isClickBusy, value); }
        }

        string title = string.Empty;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName] string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
