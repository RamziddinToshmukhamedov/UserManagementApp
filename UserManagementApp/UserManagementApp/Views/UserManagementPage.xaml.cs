using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagementApp.Helpers;
using UserManagementApp.Models;
using UserManagementApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UserManagementApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserManagementPage : ContentPage
    {
        public UserManagementPage()
        {
            BindingContext = new UserManagementViewModel(new PageService());

            InitializeComponent();
        }

        private void ListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;
            UsersListView.SelectedItem = null;
        }
    }
}