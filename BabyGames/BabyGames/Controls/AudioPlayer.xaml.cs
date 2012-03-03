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

namespace BabyGames.Controls
{
    /// <summary>
    /// Audio player contains some MediaElements for audio playing
    /// Serval audios can be played at same time.
    /// </summary>
    public partial class AudioPlayer : UserControl
    {
        private const int PlayerCount = 8;
        private int currentPlayerIndex;

        public AudioPlayer()
        {
            InitializeComponent();

            this.Loaded += new RoutedEventHandler(AudioPlayer_Loaded);
        }

        void AudioPlayer_Loaded(object sender, RoutedEventArgs e)
        {
            // Create some MediaElements for audio playing
            for (int i = 0; i < PlayerCount; i++)
            {
                var element = new MediaElement();
                element.Volume = 0.7d;

                LayoutRoot.Children.Add(element);
            }
        }

        public void Play(string address)
        {
            if (currentPlayerIndex > PlayerCount - 1)
            {
                currentPlayerIndex = 0;
            }

            var element = LayoutRoot.Children[currentPlayerIndex] as MediaElement;
            element.Source = new Uri(address, UriKind.Relative);
            element.Stop();
            element.Play();

            currentPlayerIndex++;
        }
    }
}
