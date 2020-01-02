
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Lottie.Forms;
using wahid.cc.Models;
using wahid.cc.Themes;
using wahid.cc.ViewModels;
using Xamanimation;
using Xamarin.Forms;

namespace wahid.cc.Views
{
    public partial class AnimatedStoryboard : ContentPage
    {
        double distanceFromCenter;
        double percentFromCenter;
        const double LabelOffset = 200;
        int CardIndex = 0;

        public AnimatedStoryboard()
        {
            InitializeComponent();
        }

        private void LoadStoryboards()
        {
            #region Initialize Storyboard Elements

            //(BindingContext as AnimatedStoryboardViewModel).Storyboards.Clear();

            (BindingContext as AnimatedStoryboardViewModel).Storyboards.Clear();

            (BindingContext as AnimatedStoryboardViewModel).Storyboards.Add(
                new Storyboard()
            {
                Title = "Analytics".ToLower(),
                Description = "Keep track of every cent you earn/spend. Jolt down how much money people owe you!".ToLower(),
                Animation = "analytics.json",
                To = (string)Application.Current.Resources["Page1To"],
                From = (string)Application.Current.Resources["Page1From"]

            });


            (BindingContext as AnimatedStoryboardViewModel).Storyboards.Add(
                new Storyboard()
            {
                Title = "Income Calculator".ToLower(),
                Description = "Calculate and predict your income beforehand. Organize your income sources and track them easily.".ToLower(),
                Animation = "rise.json",
                To = (string)Application.Current.Resources["Page2To"],
                From = (string)Application.Current.Resources["Page2From"]
            });

            (BindingContext as AnimatedStoryboardViewModel).Storyboards.Add
                (
                new Storyboard()
                {
                    Title = "AI assistant".ToLower(),
                    Description = "AI assisted data analizer will predict your expenditure learning from your pruchase behaviors.".ToLower(),
                    Animation = "money.json",
                    To = (string)Application.Current.Resources["Page2To"],
                    From = (string)Application.Current.Resources["Page2From"]
                }
                );
               
            #endregion
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            maincarousel.UserInteracted += Maincarousel_UserInteracted;
            maincarousel.ItemAppeared += Maincarousel_ItemAppeared;
            LoadStoryboards();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            maincarousel.UserInteracted -= Maincarousel_UserInteracted;
            maincarousel.ItemAppeared -= Maincarousel_ItemAppeared;
        }

        async void OnTapGestureRecognizerTapped(object sender, EventArgs args)
        {
            if (!Application.Current.Properties.ContainsKey("skip"))
                Application.Current.Properties.Add("skip", false);

            Application.Current.Properties["skip"] = dontremind.IsChecked;
            await Application.Current.SavePropertiesAsync();

            await Navigation.PushModalAsync(new ContentPage());
        }


        void OnModeChangerTapped(object sender, EventArgs args)
        {

            if (!Application.Current.Properties.ContainsKey("theme"))
            {
                Application.Current.Properties.Add("theme", Theme.Day);
            }

            ICollection<ResourceDictionary> mergedDictionaries = Application.Current.Resources.MergedDictionaries;
            var theme = (Theme)Application.Current.Properties["theme"];


            if (mergedDictionaries != null)
            {
                mergedDictionaries.Clear();

                switch (theme)
                {
                    case Theme.Night:
                        // Theme is Night so change it to Day
                        modeSwitcher.PlayProgressSegment(1.0f, 0.0f);
                        mergedDictionaries.Add(new LightTheme());
                        Application.Current.Properties["theme"] = Theme.Day; break;


                    default:
                        // Change back to Night
                        modeSwitcher.PlayProgressSegment(0.0f, 1.0f);
                        mergedDictionaries.Add(new DarkTheme());
                        Application.Current.Properties["theme"] = Theme.Night; break;
                }
            }

            maincarousel.CurrentView.Animate(new ColorAnimation() { ToColor = Color.FromHex(Application.Current.Resources["Page" + (CardIndex + 1) + "From"].ToString()), Delay = 100 });
        }

        private void Maincarousel_ItemAppeared(PanCardView.CardsView view, PanCardView.EventArgs.ItemAppearedEventArgs args)
        {
            var card = view.CurrentView as StoryCard;
            var item = args.Item as Storyboard;
            CardIndex = args.Index;

            card.Animate(new ColorAnimation() { ToColor = Color.FromHex(Application.Current.Resources["Page" + (CardIndex + 1) + "To"].ToString()), Delay = 100});

        }

        private void Maincarousel_UserInteracted(PanCardView.CardsView view, PanCardView.EventArgs.UserInteractedEventArgs args)
        {
            distanceFromCenter = args.Diff;
            percentFromCenter = args.Diff / this.Width;

            var card = view.CurrentView as StoryCard;

            if(args.Status == PanCardView.Enums.UserInteractionStatus.Running)
            {

                AnimateFrontCard(card);

                var nextCard = view.CurrentBackViews.First() as StoryCard;

                AnimateNextCard(nextCard);

                nextCard.BackgroundColor = card.BackgroundColor;
            }
            if (args.Status == PanCardView.Enums.UserInteractionStatus.Ended ||
                args.Status == PanCardView.Enums.UserInteractionStatus.Ending)
            {

                var nextCard = view.CurrentBackViews.First() as StoryCard;
                ResetAnimations(card, nextCard);
            }

        }

        private void ResetAnimations(StoryCard card, StoryCard nextCard)
        {
            nextCard.Title.TranslateTo(0, 0, 1);
            nextCard.Description.TranslateTo(0, 0, 1);
            nextCard.Lottie.PlayProgressSegment(nextCard.Lottie.Progress, 0.5f);

            card.Title.TranslateTo(0, 0, 1);
            card.Description.TranslateTo(0, 0, 1);
            card.Lottie.PlayProgressSegment(card.Lottie.Progress, 0.5f);
        }

        private void AnimateNextCard(StoryCard nextCard)
        {
            nextCard.Title.TranslationX = distanceFromCenter / (Width / LabelOffset) + NextCardOffset(distanceFromCenter);
            nextCard.Description.TranslationX = distanceFromCenter / (Width /  LabelOffset) + NextCardOffset(distanceFromCenter);
            nextCard.Lottie.Progress = 0.5f + (float)(percentFromCenter / 2);
        }

        private double NextCardOffset(double distanceFromCenter)
        {
            return distanceFromCenter > 0 ? -LabelOffset : LabelOffset;
        }

        private void AnimateFrontCard(StoryCard card)
        {
            card.Title.TranslationX = distanceFromCenter / (Width / LabelOffset);
            card.Description.TranslationX = distanceFromCenter / (Width / LabelOffset);
            card.Lottie.Progress = 0.5f + (float)(percentFromCenter / 2) * -1;
        }
    }
}
