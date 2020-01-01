using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using wahid.cc.Models;

namespace wahid.cc.ViewModels
{
    public class AnimatedStoryboardViewModel : ViewModelBase
    {
        ObservableCollection<Storyboard> _storyboards;

        public AnimatedStoryboardViewModel()
        {
            #region Initialize Storyboard Elements
            _storyboards = new ObservableCollection<Storyboard>()
            {
                new Storyboard()
                {
                    Title="Analytics".ToLower(),
                    Description = "Keep track of every cent you earn/spend. Jolt down how much money people owe you!".ToLower(),
                    Animation = "analytics.json",
                    To = "#233A4D",
                    From = "#3B2845"

                },
                new Storyboard()
                {
                    Title="Income Calculator".ToLower(),
                    Description = "Calculate and predict your income beforehand. Organize your income sources and track them easily.".ToLower(),
                    Animation = "rise.json",
                    From = "#2D232E",
                    To="#C64191"
                },
                new Storyboard()
                {
                    Title="AI assistant".ToLower(),
                    Description = "AI assisted data analizer will predict your expenditure learning from your pruchase behaviors.".ToLower(),
                    Animation = "money.json",
                    From = "#274060",
                    To = "#4A306D"
                }
            };
            #endregion

        }

        public ObservableCollection<Storyboard> Storyboards
        {
            get { return _storyboards; }
        }
    }
}
