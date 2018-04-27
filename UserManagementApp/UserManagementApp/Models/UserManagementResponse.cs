using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagementApp.Models
{
    public class UserManagementResponse
    {
        public ObservableCollection<UserDetails> Data { get; set; }
        public bool Success { get; set; }
    }
}
