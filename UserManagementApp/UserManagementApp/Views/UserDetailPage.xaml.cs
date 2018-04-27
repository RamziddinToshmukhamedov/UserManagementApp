using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagementApp.Helpers;
using UserManagementApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UserManagementApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class UserDetailPage : ContentPage
	{
		public UserDetailPage ()
		{
		    BindingContext = new UserDetailsViewModel(new PageService());

            InitializeComponent();
        }
	}
}