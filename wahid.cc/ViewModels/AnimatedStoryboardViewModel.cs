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
                    Title="Custrom Entry",
                    Description = "You can customize the entry control in both paltforms to fit your needs. Simply change the attached bindable properties!",
                    Background = System.Drawing.Color.Azure
                },
                new Storyboard()
                {
                    Title="Custom Converters",
                    Description = "You can use simple to use custom converters. Simple change the attached bindable properties!",
                    Background = System.Drawing.Color.BlanchedAlmond
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
