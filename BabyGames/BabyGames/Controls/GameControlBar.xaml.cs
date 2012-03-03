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
using BabyGames.Common;

namespace BabyGames.Controls
{
    public partial class GameControlBar : UserControl
    {
        private GameState gameState;

        public GameControlBar()
        {
            InitializeComponent();
        }

        private void button_StartGame_Click(object sender, RoutedEventArgs e)
        {
            this.StartGame();
        }

        private void button_ResumeGame_Click(object sender, RoutedEventArgs e)
        {
            this.PauseGame();
        }

        private void button_Hint_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SetUIAccordingState(GameState state)
        {
            if (state == GameState.Over)
            {
                this.button_Hint.IsEnabled = false;
                this.button_ResumeGame.IsEnabled = false;
                this.button_ResumeGame.Content = "暂 停";
                this.button_StartGame.IsEnabled = true;
            }
            if (state == GameState.Gaming)
            {
                this.button_Hint.IsEnabled = true;
                this.button_ResumeGame.IsEnabled = true;
                this.button_ResumeGame.Content = "暂 停";
                this.button_StartGame.IsEnabled = false;
            }
            if (state == GameState.Pausing)
            {
                this.button_Hint.IsEnabled = true;
                this.button_ResumeGame.IsEnabled = true;
                this.button_ResumeGame.Content = "继 续";
                this.button_StartGame.IsEnabled = false;
            }
        }

        private void StartGame()
        {
            if (this.gameState == GameState.Over)
            {
                this.gameState = GameState.Gaming;
                this.SetUIAccordingState(this.gameState);

                this.OnGameStateChange(this.gameState);
            }
        }

        private void PauseGame()
        {
            if (this.gameState == GameState.Gaming)
            {
                this.gameState = GameState.Pausing;
                this.SetUIAccordingState(this.gameState);
            }
            else if (this.gameState == GameState.Pausing)
            {
                this.gameState = GameState.Gaming;
                this.SetUIAccordingState(this.gameState);
            }
            this.OnGameStateChange(this.gameState);
        }

        public void Reset()
        {
            this.gameState = GameState.Over;
            this.SetUIAccordingState(GameState.Over);
        }

        public void FinishGame()
        {
            if (this.gameState == GameState.Gaming)
            {
                this.gameState = GameState.Over;
                this.SetUIAccordingState(this.gameState);
                this.OnGameStateChange(this.gameState);
            }
        }
        protected virtual void OnGameStateChange(GameState state)
        {
            if (GameStateChange != null)
            {
                GameStateChange(this, new GameControlEventArgs(state));
            }
        }

        public event EventHandler<GameControlEventArgs> GameStateChange;

        public class GameControlEventArgs : EventArgs
        {
            public GameControlEventArgs(GameState state)
            {
                DesiredState = state;
            }
            public GameState DesiredState { get; set; }
        }
    }
}
