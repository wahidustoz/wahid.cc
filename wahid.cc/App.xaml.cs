using System;
using System.Collections.Generic;
using wahid.cc.Models;
using wahid.cc.Themes;
using wahid.cc.Views;
using Xamarin.Essentials;
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

            MainPage = SelectMainPage();

        }

        private Page SelectMainPage()
        {
            if (Preferences.ContainsKey("skip") && Preferences.Get("skip", false))
                return new ContentPage();

            return new NavigationPage(new AnimatedStoryboard());
        }

        protected override void OnStart()
        {
            // Handle when your app starts

            LoadTheme();
        }

        private void LoadTheme()
        {
            ICollection<ResourceDictionary> mergedDictionaries = Application.Current.Resources.MergedDictionaries;
            var theme = Preferences.Get("theme", Theme.Day.ToString());
            Console.WriteLine(theme);

            if (mergedDictionaries != null)
            {
                mergedDictionaries.Clear();

                if(theme == Theme.Day.ToString())
                    mergedDictionaries.Add(new LightTheme());
                else
                    mergedDictionaries.Add(new DarkTheme());
            }
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
