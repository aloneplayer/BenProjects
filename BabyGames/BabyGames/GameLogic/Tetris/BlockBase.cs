using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace BabyGames.Tetris
{
    public abstract class BlockBase
    {
        public BlockBase()
        {
            InitBlock();

            //Random rotate
        }
        /// <summary>
        /// 首次显示block要进行的偏移，实现block逐渐进入游戏场景
        /// </summary>
        public abstract int YOffset { get;}
        /// <summary>
        /// 设置block形状Matrix 和 MaxIndex
        /// </summary>
        protected abstract void InitBlock();

        // block形状
        public int[,] Matrix { get; set; }

        // 形状的索引
        private int currentIndex = 0;

        // block最多可以变形几次
        protected int maxIndex;

  
        /// <summary>
        /// Block变形
        /// </summary>
        /// <returns>返回变形后的矩阵</returns>
        public abstract int[,] GetRotatedMatrix();

        public abstract Color Color { get; }

        /// <summary>
        /// 获取下一个形状的索引。如果超过最大索引则返回最初索引
        /// </summary>
        /// <returns></returns>
        public int GetNextIndex()
        {
            int nextIndex = currentIndex >= maxIndex ? 0 : currentIndex + 1;

            return nextIndex;
        }

        /// <summary>
        /// 变形。设置 Matrix 为变形后的矩阵
        /// </summary>
        public void Rotate()
        {
            Matrix = GetRotatedMatrix();

            currentIndex = GetNextIndex();
        }
    }
}
