using System;
using Com.Cloudrail.SI;
using Com.Cloudrail.SI.Services;
using DFS.Dependency;
using DFS.Droid;
using DFS.Droid.Implementations;
using Xamarin.Forms;

[assembly: Dependency(typeof(InstagramManager))]
namespace DFS.Droid.Implementations
{
    public class InstagramManager: IInstagramManager
    {
        public void Login()
        {
            //Context context = ;
            var cRInstagram = new Instagram(Android.App.Application.Context, "33c8f1v15f9d499c891305d9bf12ef8e", "3b93f768d948405984fd8ad3e94512d7");
            cRInstagram.Login();
        }
    }
}
