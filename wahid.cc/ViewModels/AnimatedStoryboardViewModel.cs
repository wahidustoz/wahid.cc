using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using wahid.cc.Models;
using Xamarin.Forms;

namespace wahid.cc.ViewModels
{
    public class AnimatedStoryboardViewModel : ViewModelBase
    {
        ObservableCollection<Storyboard> _storyboards;

        public AnimatedStoryboardViewModel()
        {
            Storyboards = new ObservableCollection<Storyboard>();
            //#region Initialize Storyboard Elements
            //Storyboards = new ObservableCollection<Storyboard>()
            //{
            //    new Storyboard()
            //    {
            //        Title="Analytics".ToLower(),
            //        Description = "Keep track of every cent you earn/spend. Jolt down how much money people owe you!".ToLower(),
            //        Animation = "analytics.json",
            //        To = "#011936",
            //        From = "#090B08"

            //    },
            //    new Storyboard()
            //    {
            //        Title="Income Calculator".ToLower(),
            //        Description = "Calculate and predict your income beforehand. Organize your income sources and track them easily.".ToLower(),
            //        Animation = "rise.json",
            //        From = "#011936",
            //        To="#261427"
            //    },
            //    new Storyboard()
            //    {
            //        Title="AI assistant".ToLower(),
            //        Description = "AI assisted data analizer will predict your expenditure learning from your pruchase behaviors.".ToLower(),
            //        Animation = "money.json",
            //        From = "#261427",
            //        To = "#0D0630"
            //    }
            //};
            //#endregion

        }

        public ObservableCollection<Storyboard> Storyboards
        {
            get { return _storyboards; }

            set
            {
                SetProperty(ref _storyboards, value);
            }
        }
    }
}
