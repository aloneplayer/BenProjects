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

namespace BabyGames.Controls.MathShooter
{
    public partial class Crosshair : UserControl
    {
        public Crosshair()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 开火。缩小瞄准镜以提示开火
        /// </summary>
        public void Fire()
        {
            this.Scaler.ScaleX = this.Scaler.ScaleY = 0.8;
            this.PlayFire();
        }

        /// <summary>
        /// 瞄准镜复位
        /// </summary>
        public void Reset()
        {
            this.Scaler.ScaleX = this.Scaler.ScaleY = 1;
        }


        private void PlayFire()
        {
            //sound_Shoot.Position = TimeSpan.FromMilliseconds(0);
            //sound_Shoot.Play();
        }  
    }
}
