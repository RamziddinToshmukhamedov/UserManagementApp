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
    public class LoginViewModel : INotifyPropertyChanged
    {
        private readonly IPageService _pageService;
        private readonly ApiServices _apiServices = new ApiServices();
        private string _errorMessage;
        private bool _isLoading;

        public LoginViewModel(IPageService pageService)
        {
            _pageService = pageService;
            IsLoading = false;
            Username = "jumaniyozov@oybek.com";
            Password = "123456aa";
        }

        public string Username { get; set; }
        public string Password { get; set; }

        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                OnPropertyChanged();
            }
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                OnPropertyChanged();
            }
        }

        public ICommand LoginCommand
        {
            get {
                return new Command(async() =>
                {
                    IsLoading = true;

                    if (!string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password))
                    {
                        var result = await _apiServices.LoginAsync(Username, Password);
                        ErrorMessage = result;

                        if (result.Equals(""))
                            await _pageService.PushAsync(new UserDetailPage());
                    }
                    else
                    {
                        ErrorMessage = "Username or Password is empty";
                    }

                    IsLoading = false;
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
