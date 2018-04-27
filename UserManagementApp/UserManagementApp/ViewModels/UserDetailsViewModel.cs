using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UserManagementApp.Annotations;
using UserManagementApp.Helpers;
using UserManagementApp.Services;
using UserManagementApp.Views;
using Xamarin.Forms;

namespace UserManagementApp.ViewModels
{
    public class UserDetailsViewModel : INotifyPropertyChanged
    {
        private readonly IPageService _pageService;
        private ApiServices _apiServices = new ApiServices();
        private string _email;
        private string _firstName;
        private string _lastName;
        private bool _isAdmin;
        private bool _isLoading;

        public string Title { get; set; }

        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                OnPropertyChanged();
            }
        }

        public bool IsAdmin
        {
            get => _isAdmin;
            set
            {
                _isAdmin = value;
                OnPropertyChanged();
            }
        }

        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged();
            }
        }

        public string FirstName
        {
            get => _firstName;
            set
            {
                _firstName = value;
                OnPropertyChanged();
            }
        }

        public string LastName
        {
            get => _lastName;
            set
            {
                _lastName = value;
                OnPropertyChanged();
            }
        }

        public UserDetailsViewModel(IPageService pageService)
        {
            _pageService = pageService;
            Title = "User Detail";

            GetUserDetails();
        }

        private async void GetUserDetails()
        {
            IsLoading = true;

            var userDetailModel = await _apiServices.GetUserDetailsAsync();

            if (userDetailModel != null)
            {
                Email = userDetailModel.Email;
                FirstName = userDetailModel.FirstName;
                LastName = userDetailModel.LastName;

                Settings.Fullname = userDetailModel.FullName;

                IsAdmin = userDetailModel.Role.Equals("Admin");
            }

            IsLoading = false;

            //TODO Add Toast or Alert Dialog
        }

        public ICommand LogOutCommand
        {
            get
            {
                return new Command(async () =>
                {
                    var response = await _pageService.DisplayAlert("Log Out", "Do you want to log out from the system?", "Yes", "No");
                    if (response)
                    {
                        Settings.AccessToken = "";
                        Settings.Fullname = "";
                        await _pageService.PushAsync(new LoginPage());
                    }
                });
            }
        }

        public ICommand ManageUsersCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await _pageService.PushAsync(new UserManagementPage());
                });
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
