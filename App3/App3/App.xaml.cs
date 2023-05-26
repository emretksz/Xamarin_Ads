using App3.Services.Management;
using App3.Views;
using System;
using System.IO;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App3
{
    public partial class App : Application
    {
        public static string DatabasePath { get; private set; }
        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            DependencyService.Register<UserManager>();
            MainPage = new AppShell();
            DatabasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ET_AdsContext.db");

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            //Encoding encoding = Encoding.GetEncoding(1254);
            //Console.OutputEncoding = Encoding.GetEncoding(1254);
            //Encoding encoding = Encoding.GetEncoding(1254);
            //Console.OutputEncoding = Encoding.GetEncoding("windows-1254");
            //Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            //Encoding encoding = Encoding.GetEncoding(1254);
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
