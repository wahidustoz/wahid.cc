
using System;
using Xamarin.Forms;

namespace wahid.cc.Views
{
    public partial class AnimatedStoryboard : ContentPage
    {
        public AnimatedStoryboard()
        {
            InitializeComponent();
        }

        void CollectionScrolled(object sender, ItemsViewScrolledEventArgs e)
        {
            if (Math.Abs(e.HorizontalDelta) < 15)
            {
                e.HorizontalDelta *= 500;
                (sender as CollectionView).SendScrolled(e);

            }

            System.Console.WriteLine("Center Index: " + e.CenterItemIndex);
            System.Console.WriteLine("first visible: " + e.FirstVisibleItemIndex);
            System.Console.WriteLine("last visible: " + e.LastVisibleItemIndex);
            System.Console.WriteLine("Hor Delta: " + e.HorizontalDelta);
            System.Console.WriteLine("Hor Off: " + e.HorizontalOffset);
            System.Console.WriteLine("Ver Delta: " + e.VerticalDelta);
            System.Console.WriteLine("Ver Off: " + e.VerticalOffset);
        }
    }
}
