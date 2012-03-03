using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Navigation;

namespace BabyGames.Views
{
    public partial class MathShooter : Page
    {
        public MathShooter()
        {
            InitializeComponent();
        }

        // Executes when the user navigates to this page.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }
        #region UI Event Handler
        private void Page_MouseMove(object sender, MouseEventArgs e)
        {
            Point p = e.GetPosition(sender as FrameworkElement);
            this.Crosshair.SetValue(Canvas.LeftProperty, p.X - this.Crosshair.ActualWidth / 2);
            this.Crosshair.SetValue(Canvas.TopProperty, p.Y - this.Crosshair.ActualHeight / 2);
        }

        private void Page_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Crosshair.Fire();
        }

        private void Page_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Crosshair.Reset();
        }
        #endregion

        private void sound_BG_MediaEnded(object sender, RoutedEventArgs e)
        {
            sound_BG.Position = TimeSpan.FromMilliseconds(0);
            sound_BG.Play();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            RectangleGeometry rg = new RectangleGeometry();
            rg.Rect = new Rect(0, 0, 800, 600);
            this.GameCarrier.Clip = rg;
        }
    }
}
