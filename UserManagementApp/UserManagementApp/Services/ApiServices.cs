using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UserManagementApp.Helpers;
using UserManagementApp.Models;
using Constants = UserManagementApp.Helpers.Constants;

namespace UserManagementApp.Services
{
    public class ApiServices
    {
        public async Task<string> LoginAsync(string username, string password)
        {
            var keyValues = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("username", username),
                new KeyValuePair<string, string>("password", password),
                new KeyValuePair<string, string>("grant_type", "password")
            };

            var request = new HttpRequestMessage(HttpMethod.Post, Constants.BaseApiAddress + "oauth/token");

            request.Content = new FormUrlEncodedContent(keyValues);

            var client = new HttpClient();
            var response = await client.SendAsync(request);

            var content = await response.Content.ReadAsStringAsync();

            if (content == null)
            {
                return "";
            }

            JObject jwtDynamic = JsonConvert.DeserializeObject<dynamic>(content);

            //var accessTokenExpiration = jwtDynamic.Value<int>("expires_in");
            var accessToken = jwtDynamic.Value<string>("access_token");
            
            if (!string.IsNullOrEmpty(accessToken))
            {
                Settings.AccessToken = accessToken;
                //Settings.AccessTokenExpirationDate = accessTokenExpiration;
                return "";
            }

            var errorDescription = jwtDynamic.Value<string>("error_description");

            return errorDescription;
        }

        public async Task<UserDetails> GetUserDetailsAsync()
        {
            var client = new HttpClient();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Settings.AccessToken);

            var json = await client.GetStringAsync(Constants.BaseApiAddress + "api/User/Details");

            if (json != null)
            {
                var response = JsonConvert.DeserializeObject<UserDetailsResponse>(json);

                var userDetails = response.Data;

                return userDetails;
            }

            return null;
        }

        public async Task<ObservableCollection<UserDetails>> GetListOfUsersAsync()
        {
            var client = new HttpClient();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Settings.AccessToken);

            var json = await client.GetStringAsync(Constants.BaseApiAddress + "api/UserManagement");

            if (json != null)
            {
                var response = JsonConvert.DeserializeObject<UserManagementResponse>(json);

                var usersList = response.Data;

                return usersList;
            }

            return null;
        }
    }
}
