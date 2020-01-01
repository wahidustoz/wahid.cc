using System;
using System.Collections.Generic;
using Lottie.Forms;
using Xamarin.Forms;

namespace wahid.cc.Views
{
    public partial class StoryCard : ContentView
    {

        public StoryCard()
        {
            InitializeComponent();
        }

        public Label Title => title;

        public Label Description => description;

        public AnimationView Lottie => lottie;

    }
}
