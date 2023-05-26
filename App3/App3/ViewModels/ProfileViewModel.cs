using App3.Services.Management;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace App3.ViewModels
{
    public class ProfileViewModel: BaseViewModel
    {
      

        public ProfileViewModel()
        {
            LoadItemId();
        }

        string fullName;
        string age;
        string gender;
        int point;
        public string FullName { get => fullName; set => SetProperty(ref fullName, value); }
        public string Age { get => age; set => SetProperty(ref age, value); }
        public string Gender { get => gender; set => SetProperty(ref gender, value); }
        public int Point { get => point; set => SetProperty(ref point, value); }

        public async void LoadItemId()
        {
            try
            {
                UserManager _userManager = new UserManager();
                var result = _userManager.GetUsers();
                FullName = result.FullName; Age = result.Age.ToString(); Gender = result.Gender.ToString(); Point = result.Point;


            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }
    }
}
