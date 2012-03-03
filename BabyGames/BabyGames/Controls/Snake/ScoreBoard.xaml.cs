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
    public partial class ScoreBoard : UserControl
    {
        private int _fps;
        private DateTime _prevDateTime;

        public ScoreBoard()
        {
            InitializeComponent();

            this.Loaded += new RoutedEventHandler(Board_Loaded);
        }

        void Board_Loaded(object sender, RoutedEventArgs e)
        {
            _fps = 0;
            _prevDateTime = DateTime.Now;

            CompositionTarget.Rendering += new EventHandler(CompositionTarget_Rendering);
        }

        /// <summary>
        /// 计算 fps
        /// </summary>
        void CompositionTarget_Rendering(object sender, EventArgs e)
        {
            _fps++;

            if ((DateTime.Now - _prevDateTime).TotalSeconds >= 1)
            {
                TextBox_FPS.Text = _fps.ToString();
                _fps = 0;
                _prevDateTime = DateTime.Now;
            }
        }

        /// <summary>
        /// 得分
        /// </summary>
        public int Score
        {
            get { return int.Parse(TextBox_Score.Text); }
            set { TextBox_Score.Text = value.ToString(); }
        }

        /// <summary>
        /// 级别
        /// </summary>
        public int Level
        {
            get { return int.Parse(TextBox_Level.Text); }
            set { TextBox_Level.Text = value.ToString(); }
        }

        /// <summary>
        /// 当前吃掉的豆子数
        /// </summary>
        public int AteBeansCount
        {
            get { return int.Parse(TextBlock_CurrentEaten.Text); }
            set { TextBlock_CurrentEaten.Text = value.ToString(); }
        }

        /// <summary>
        /// 贪吃蛇的食量，超过食量掉皮
        /// </summary>
        public int EatingCapacity
        {
            get { return int.Parse(TextBox_Capacity.Text); }
            set { TextBox_Capacity.Text = value.ToString(); }
        }

        /// <summary>
        /// 复位
        /// </summary>
        public void Reset()
        {
            Score = 0;
            Level = 1;
            AteBeansCount = 0;
            EatingCapacity = 10;
        }
    }
}
