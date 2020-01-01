
using System;
using System.Linq;
using wahid.cc.Models;
using Xamanimation;
using Xamarin.Forms;

namespace wahid.cc.Views
{
    public partial class AnimatedStoryboard : ContentPage
    {
        double distanceFromCenter;
        double percentFromCenter;
        const double LabelOffset = 200;

        public AnimatedStoryboard()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            maincarousel.UserInteracted += Maincarousel_UserInteracted;
            maincarousel.ItemAppeared += Maincarousel_ItemAppeared;
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




        private void Maincarousel_ItemAppeared(PanCardView.CardsView view, PanCardView.EventArgs.ItemAppearedEventArgs args)
        {
            var card = view.CurrentView as StoryCard;
            var item = args.Item as Storyboard;

            card.Animate(new ColorAnimation() { ToColor = Color.FromHex(item.To), Delay = 100});

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


            Console.WriteLine(args.Diff);
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
