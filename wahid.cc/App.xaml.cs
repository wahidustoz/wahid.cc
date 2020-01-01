using System;
using wahid.cc.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace wahid.cc
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            XF.Material.Forms.Material.Init(this);

            if (!Application.Current.Properties.ContainsKey("skip"))
            {
                Console.WriteLine("Not Contains");
                MainPage = new NavigationPage(new AnimatedStoryboard());
            }
            else
            {
                Console.WriteLine("Contains");
                if ((bool)Application.Current.Properties["skip"] == true)
                    MainPage = new ContentPage();
                else
                    MainPage = new NavigationPage(new AnimatedStoryboard());
            }
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
