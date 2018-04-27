using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using UserManagementApp.Annotations;
using UserManagementApp.Helpers;
using UserManagementApp.Models;
using UserManagementApp.Services;

namespace UserManagementApp.ViewModels
{
    public class UserManagementViewModel : INotifyPropertyChanged
    {
        private readonly IPageService _pageService;
        private readonly ApiServices _apiServices = new ApiServices();
        private bool _isLoading;
        private ObservableCollection<UserDetails> _usersDetailsList;

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

        public ObservableCollection<UserDetails> UsersDetailsList
        {
            get => _usersDetailsList;
            set
            {
                _usersDetailsList = value;
                OnPropertyChanged();
            }
        }

        public UserManagementViewModel(IPageService pageService)
        {
            _pageService = pageService;
            Title = "Users Management";
            GetUsersList();
        }

        private async void GetUsersList()
        {
            IsLoading = true;
            UsersDetailsList = await _apiServices.GetListOfUsersAsync();
            IsLoading = false;
        }


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
