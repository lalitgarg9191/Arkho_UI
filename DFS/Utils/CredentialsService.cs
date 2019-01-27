using System;
using System.Diagnostics;
using System.Linq;
using DFS.Models;
using Newtonsoft.Json;
using Xamarin.Auth;
using static DFS.Models.LoginResponse;

namespace DFS.Utils
{
    public class CredentialsService
    {
        public static void SaveCredentials(string userName=null, string password=null,Member member=null,FacebookUser fbUser=null,InstagramUser instagramUser=null,InstagramMedia instagramMedia=null, string userType=null)
        {
            var isExist = CredentialsService.DoCredentialsExist();
            if (isExist && instagramMedia != null)
            {
                var account = CredentialsService.GetAccount();
                var data = JsonConvert.SerializeObject(instagramMedia);
                account.Properties.Add("InstgramMedia", data);
                AccountStore.Create().Save(account, "Fitness");
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(userName) && !string.IsNullOrWhiteSpace(password))
                {
                    Account account = new Account
                    {
                        Username = userName,
                    };
                    account.Properties.Add("Password", password);
                    account.Properties.Add("UserType", userType);
                    var data = JsonConvert.SerializeObject(member);
                    account.Properties.Add("Member", data);

                    if (fbUser != null)
                    {
                        var fbData = JsonConvert.SerializeObject(fbUser);
                        account.Properties.Add("FacebookUser", fbData);
                    }

                    if (instagramUser != null)
                    {
                        var instData = JsonConvert.SerializeObject(instagramUser);
                        account.Properties.Add("InstagramUser", instData);
                    }

                    if (instagramMedia != null)
                    {
                        var mediaData = JsonConvert.SerializeObject(instagramMedia);
                        account.Properties.Add("InstagramMedia", mediaData);
                    }

                    AccountStore.Create().Save(account, "Fitness");
                }
            }
        }

        public static void DeleteCredentials()
        {
            var account = AccountStore.Create().FindAccountsForService(App.AppName).FirstOrDefault();
            if (account != null)
            {

                AccountStore.Create().Delete(account, "Fitness");
            }
        }

        public static bool DoCredentialsExist()
        {
            try
            {
                return AccountStore.Create().FindAccountsForService(App.AppName).Any() ? true : false;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                return false;                   
            }
        }

        public static Account GetAccount()
        {
            try
            {
                if (DoCredentialsExist()) {
                    var accounts = AccountStore.Create().FindAccountsForService(App.AppName);
                    if (accounts.Any())
                        return accounts.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                return null;
            }
            return null;
        }
    }
}
