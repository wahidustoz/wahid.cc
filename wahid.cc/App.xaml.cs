using System;
using System.Collections.Generic;
using wahid.cc.Models;
using wahid.cc.Themes;
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

            MainPage = SelectMainPage();
        }

        private Page SelectMainPage()
        {
            if (Application.Current.Properties.ContainsKey("skip") &&
                (bool)Application.Current.Properties["skip"] == true)
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
            if (!Application.Current.Properties.ContainsKey("theme"))
                Application.Current.Properties.Add("theme", Theme.Day);

            ICollection<ResourceDictionary> mergedDictionaries = Application.Current.Resources.MergedDictionaries;
            var theme = (Theme)Application.Current.Properties["theme"];


            if (mergedDictionaries != null)
            {
                mergedDictionaries.Clear();

                switch (theme)
                {
                    case Theme.Night: mergedDictionaries.Add(new DarkTheme()); break;
                    default:
                        mergedDictionaries.Add(new LightTheme());
                        break;
                }
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
