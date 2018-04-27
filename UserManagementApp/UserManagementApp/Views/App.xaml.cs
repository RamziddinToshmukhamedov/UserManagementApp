using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Plugin.Connectivity;
using Plugin.Connectivity.Abstractions;
using UserManagementApp.Helpers;
using UserManagementApp.Views;
using Xamarin.Forms;

namespace UserManagementApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = SetMainPage();
        }

        protected override void OnStart()
        {
            base.OnStart();
            CrossConnectivity.Current.ConnectivityChanged += HandleConnectivityChanged;

            //CrossConnectivity.Current.ConnectivityChanged += (sender, args) => {
            //    if (!CrossConnectivity.Current.IsConnected)
            //        MainPage = new NavigationPage(new CheckConnectivityPage());
            //};
        }

        void HandleConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            Type currentPage = this.MainPage.GetType();
            if (e.IsConnected && currentPage == typeof(CheckConnectivityPage))
                this.MainPage = SetMainPage();
            else if (!e.IsConnected && currentPage != typeof(CheckConnectivityPage))
                this.MainPage = new CheckConnectivityPage();
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        private Page SetMainPage()
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                return new CheckConnectivityPage();
            }
            else
            {
                if (!string.IsNullOrEmpty(Settings.AccessToken))
                {
                    return new NavigationPage(new UserDetailPage())
                    {
                        BarBackgroundColor = (Color)Current.Resources["colorAccent"],
                        BarTextColor = Color.White
                    };
                }
                else
                {
                    return new NavigationPage(new LoginPage())
                    {
                        BarBackgroundColor = (Color)Current.Resources["colorAccent"],
                        BarTextColor = Color.White
                    };
                }
            }
        }
    }
}
