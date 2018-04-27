// Helpers/Settings.cs
using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System;

namespace UserManagementApp.Helpers
{
    /// <summary>
    /// This is the Settings static class that can be used in your Core solution or in any
    /// of your client applications. All settings are laid out the same exact way with getters
    /// and setters. 
    /// </summary>

    public static class Settings
	{
        #region Setting Constants

	        private const string username = "username";
	        private const string fullname = "fullname";
	        private const string accessToken = "accessToken";
	        private const string accessTokenExpirationDate = "AccessTokenExpirationDate";

        #endregion


        private static ISettings AppSettings
		{
			get
			{
				return CrossSettings.Current;
			}
		}


	    public static string Username
	    {
	        get
	        {
	            return AppSettings.GetValueOrDefault(username, "");
	        }
	        set
	        {
	            AppSettings.AddOrUpdateValue(username, value);
	        }
	    }

	    public static string Fullname
	    {
	        get
	        {
	            return AppSettings.GetValueOrDefault(fullname, "");
	        }
	        set
	        {
	            AppSettings.AddOrUpdateValue(fullname, value);
	        }
	    }

        public static string AccessToken
	    {
	        get
	        {
	            return AppSettings.GetValueOrDefault(accessToken, "");
	        }
	        set
	        {
	            AppSettings.AddOrUpdateValue(accessToken, value);
	        }
	    }

	    //public static DateTime AccessTokenExpirationDate
	    //{
	    //    get
	    //    {
	    //        return AppSettings.GetValueOrDefault(accessTokenExpirationDate, DateTime.UtcNow);
	    //    }
	    //    set
	    //    {
	    //        AppSettings.AddOrUpdateValue(accessTokenExpirationDate, value);
	    //    }
	    //}
    }
}