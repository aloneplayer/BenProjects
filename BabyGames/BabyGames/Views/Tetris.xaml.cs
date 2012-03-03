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
using BabyGames.Tetris;
using BabyGames.Common;
using BabyGames.Controls.Tetris;

namespace BabyGames.Views
{
    public partial class Tetris : Page
    {
        public Tetris()
        {
            InitializeComponent();
            CompositionTarget.Rendering += new EventHandler(CompositionTarget_Rendering);
        }

        private DateTime prevDateTime = DateTime.Now;
        /// <summary>
        /// 上一个时钟周期里，累计时间
        /// </summary>
        private double prevElapseTime = 0d;
        private double updateInterval = 300; // 多少毫秒更新一次游戏状态
        void CompositionTarget_Rendering(object sender, EventArgs e)
        {
            double elapseTime = (DateTime.Now - prevDateTime).TotalMilliseconds + prevElapseTime;
            if (elapseTime > updateInterval)
            {
                Update();
                elapseTime -= updateInterval;
            }

            prevElapseTime = elapseTime;
            prevDateTime = DateTime.Now;
        }


        // Executes when the user navigates to this page.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        #region UI Event Handler
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            InitGame();
        }

        private void button_Start_Click(object sender, RoutedEventArgs e)
        {
            this.StartGame();
            this.SetUIAccordingState(GameState.Gaming);
        }

        private void button_Pause_Click(object sender, RoutedEventArgs e)
        {
            if (this.gameState == GameState.Gaming)
            {
                this.gameState = GameState.Pausing;
                this.SetUIAccordingState(GameState.Pausing);
            }
            else if (this.gameState == GameState.Pausing)
            {
                this.gameState = GameState.Gaming;
                this.SetUIAccordingState(GameState.Gaming);
            }
        }

        private void Page_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.gameState != GameState.Gaming)
                return;

            if (e.Key == Key.Left)
            {
                this.MoveToLeft();
            }
            else if (e.Key == Key.Right)
            {
                this.MoveToRight();
            }
            else if (e.Key == Key.Up || e.Key == Key.Space)
            {
                this.Rotate();
            }
            else if (e.Key == Key.Down)
            {
                this.MoveToDown();
            }
        }

        #endregion

        #region Game Settings
        private int colCount = 15;
        private int rowCount = 30;
        #endregion

        #region Game Control
        private List<BlockBase> blockList;
        /// <summary>
        /// 俄罗斯方块容器
        /// </summary>
        public BlockUnit[,] poolBlocks { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public BlockUnit[,] proviewerBlocks { get; set; }

        private BlockBase currentBlock = null;

        private int currentBlockX; // Block (4*4)的X 坐标
        private int currentBlockY; // Block (4*4)的Y 坐标

        private BlockBase nextBlock = null;

        // 初始速率（毫秒）
        private int initSpeed = 400;
        // 每增加一个级别所需增加的速率（毫秒）
        private int speedIncrement = 50;

        private int level = 1;
        private int score = 0;
        /// <summary>
        /// 总共消除了多少行
        /// </summary>
        private int removedRowsCount = 0;

        private GameState gameState = GameState.Over;

        private Random randomCreater = new Random();

        private void InitGame()
        {
            //游戏中有8种Block
            blockList = new List<BlockBase>();
            blockList.Add(new BlockI());
            blockList.Add(new BlockL());
            blockList.Add(new BlockL2());
            blockList.Add(new BlockN());
            blockList.Add(new BlockN2());
            blockList.Add(new BlockO());
            blockList.Add(new BlockT());
            blockList.Add(new BlockCross());

            // 初始化方块容器（用 Block 对象填满整个容器）
            poolBlocks = new BlockUnit[this.colCount, this.rowCount];
            for (int y = 0; y < this.rowCount; y++)
            {
                for (int x = 0; x < this.colCount; x++)
                {
                    var block = new BlockUnit();
                    block.Top = y * block.rectBlock.ActualHeight;
                    block.Left = x * block.rectBlock.ActualWidth;
                    block.Color = null;

                    poolBlocks[x, y] = block;
                }
            }

            // 初始化block 预览器（用 Block 对象将其填满）
            this.proviewerBlocks = new BlockUnit[4, 4];
            for (int y = 0; y < 4; y++)
            {
                for (int x = 0; x < 4; x++)
                {
                    var block = new BlockUnit();
                    block.Top = y * block.rectBlock.ActualHeight;
                    block.Left = x * block.rectBlock.ActualWidth;
                    block.Color = null;
                    proviewerBlocks[x, y] = block;
                }
            }

            foreach (BlockUnit block in poolBlocks)
            {
                this.canvasBlockPool.Children.Add(block);
            }

            foreach (BlockUnit block in proviewerBlocks)
            {
                this.canvasBlockProviewer.Children.Add(block);
            }
        }

        private void StartGame()
        {
            this.textBox_GameOver.Visibility = Visibility.Collapsed;
            this.ClearBlockPool();
            this.gameState = GameState.Gaming;

            this.level = 1;
            this.score = 0;
            this.removedRowsCount = 0;
            this.updateInterval = 300;
            //reset the score broad
            DisplayScore();

            this.CreateNewBlock();

            this.DisplayCurrentBlock(0, 0);
            this.PreviewNextBlock();
        }

        private bool IsGameOver()
        {
            for (int x = 0; x < this.colCount; x++)
            {
                if (this.poolBlocks[x, 0].Color != null)
                {
                    return true;
                }
            }
            return false;
        }

        private void GameOver()
        {
            this.gameState = GameState.Over;
            this.SetUIAccordingState(GameState.Over);
            this.textBox_GameOver.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// 加分算法： 2 的 rowcount 次幂 乘以 10
        /// </summary>
        /// <param name="rowCount"></param>
        private void AddScore(int rowCount)
        {
            this.score += 10 * (int)Math.Pow(2, rowCount);
            // 更新总的已消行数
            this.removedRowsCount += rowCount;

            // 根据已消行数计算级别：已消行数/5 的平方根 取整
            this.level = (int)Math.Sqrt(this.removedRowsCount / 5);

            this.updateInterval -= this.level * 10;

            //Update UI
            this.DisplayScore();
        }

        private void DisplayScore()
        {
            this.textBlock_Level.Text = this.level.ToString();
            this.textBlock_Score.Text = this.score.ToString();
            this.textBlock_Rows.Text = this.removedRowsCount.ToString();
        }

        
        public void ClearBlockPool()
        {
            for (int x = 0; x < colCount; x++)
            {
                for (int y = 0; y < rowCount; y++)
                {
                    this.poolBlocks[x, y].Color = null;
                }
            }
        }

        private void ResetCurrentBlockPosition()
        {
            // reset current block coordinate
            currentBlockX = 7;
            currentBlockY = - this.currentBlock.YOffset;
        }
        private void Update()
        {
            if (this.gameState != GameState.Gaming) return;

            MoveToDown();
        }
        #endregion

        #region Game Logic
        private int GetSpeed()
        {
            return initSpeed + (level - 1) * speedIncrement;
        }
        /// <summary>
        /// 
        /// </summary>
        private void CreateNewBlock()
        {
            if (nextBlock == null)
            {
                this.currentBlock = GetRandomBlock();
            }
            else
            {
                this.currentBlock = this.nextBlock;
            }
            this.nextBlock = GetRandomBlock();

            this.ResetCurrentBlockPosition();
        }

        private BlockBase GetRandomBlock()
        {
            return blockList[randomCreater.Next(0, 8)];
            //Test
            //return blockList[0];
        }

        /// <summary>
        /// Block matrix中的x，y坐标和游戏中的是反的，要拧过来 
        /// </summary>
        private void PreviewNextBlock()
        {
            // 清空
            foreach (BlockUnit block in this.proviewerBlocks)
            {
                block.Color = null;
            }

            // 根据 _nextPiece 的矩阵设置相对应的 Block 对象的呈现
            for (int x = 0; x < 4; x++)
            {
                for (int y = 0; y < 4; y++)
                {
                    //Block matrix中的x，y坐标和游戏中的是反的，要拧过来 
                    if (this.nextBlock.Matrix[y, x] == 1)
                    {
                        this.proviewerBlocks[x, y].Color = this.nextBlock.Color;
                    }
                }
            }
        }

        private void RemoveCurrentBlock()
        {
            for (int x = 0; x < 4; x++)
            {
                for (int y = 0; y < 4; y++)
                {
                    //Block matrix中的x，y坐标和游戏中的是反的，要拧过来 
                    if (this.currentBlock.Matrix[y, x] == 1)
                    {
                        int poolX = x + currentBlockX;
                        int poolY = y + currentBlockY;
                        if (0 <= poolX && poolX < this.colCount && 0 <= poolY && poolY < this.rowCount)
                        {
                            this.poolBlocks[x + this.currentBlockX, y + this.currentBlockY].Color = null;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 根据游戏规则，如果某行出现连续的直线则将其删除，该线以上的部分依次向下移动
        /// </summary>
        private void ProcessBlock()
        {
            // 删除的行数
            int rows = 0;

            // 行的遍历（Y 方向）
            for (int y = 0; y < this.rowCount; y++)
            {
                // 该行是否是一条连续的直线
                bool isLine = true;

                // 列的遍历（X 方向）
                for (int x = 0; x < this.colCount; x++)
                {
                    if (this.poolBlocks[x, y].Color == null)
                    {
                        // 出现断行，则继续遍历下一行
                        isLine = false;
                        break;
                    }
                }

                // 该行是一条连续的直线则将其删除，并将该行以上的部分依次向下移动
                if (isLine)
                {
                    rows++;

                    // 删除该行
                    for (int x = 0; x < this.colCount; x++)
                    {
                        this.poolBlocks[x, y].Color = null;
                    }

                    // 将被删除行的以上行依次向下移动
                    for (int i = y; i > 0; i--)
                    {
                        for (int x = 0; x < this.colCount; x++)
                        {
                            this.poolBlocks[x, i].Color = this.poolBlocks[x, i - 1].Color;
                        }
                    }
                }
            }

            // 加分，计算方法： 2 的 removeRowCount 次幂 乘以 10
            if (rows > 0)
            {
                this.AddScore(rows);
            }
        }
        /// <summary>
        /// 显示current block
        /// </summary>
        /// <param name="offsetX">X 方向上的偏移量</param>
        /// <param name="offsetY">Y 方向上的偏移量</param>
        private void DisplayCurrentBlock(int offsetX, int offsetY)
        {
            for (int y = 0; y < 4; y++)
            {
                for (int x = 0; x < 4; x++)
                {
                    //Block matrix中的x，y坐标和游戏中的是反的，要拧过来 
                    if (this.currentBlock.Matrix[y, x] == 1)
                    {
                        int poolX = x + currentBlockX + offsetX;
                        int poolY = y + currentBlockY + offsetY;
                        if (0 <= poolX && poolX < this.colCount && 0 <= poolY && poolY < this.rowCount)
                        {
                            this.poolBlocks[poolX, poolY].Color = this.currentBlock.Color;
                        }
                    }
                }
            }

            currentBlockX += offsetX;
            currentBlockY += offsetY;
        }
        /// <summary>
        /// 向左移动
        /// </summary>
        public void MoveToLeft()
        {
            if (!IsBoundary(this.currentBlock.Matrix, -1, 0))
            {
                RemoveCurrentBlock();
                DisplayCurrentBlock(-1, 0);
            }
        }

        /// <summary>
        /// 向右移动
        /// </summary>
        public void MoveToRight()
        {
            if (!IsBoundary(this.currentBlock.Matrix, 1, 0))
            {
                RemoveCurrentBlock();
                DisplayCurrentBlock(1, 0);
            }
        }

        /// <summary>
        /// 向下移动
        /// </summary>
        public void MoveToDown()
        {
            if (!IsBoundary(this.currentBlock.Matrix, 0, 1))
            {
                RemoveCurrentBlock();
                DisplayCurrentBlock(0, 1);
            }
            else
            {
                // 如果触及底边了，则消除可消的行并且创建新的形状
                ProcessBlock();
                if (this.IsGameOver())
                {
                    this.GameOver();
                }
                else
                {
                    CreateNewBlock();
                    DisplayCurrentBlock(0, 0);
                    PreviewNextBlock();
                }
            }
        }

        /// <summary>
        /// 变形
        /// </summary>
        public void Rotate()
        {
            if (!IsBoundary(this.currentBlock.GetRotatedMatrix(), 0, 0))
            {
                RemoveCurrentBlock();
                this.currentBlock.Rotate();
                DisplayCurrentBlock(0, 0);
            }
        }

        /// <summary>
        /// 边界判断（是否超过边界）
        /// </summary>
        /// <param name="matrix">当前操作的形状的4×4矩阵</param>
        /// <param name="offsetX">矩阵 X 方向的偏移量</param>
        /// <param name="offsetY">矩阵 Y 方向的偏移量</param>
        /// <returns></returns>
        private bool IsBoundary(int[,] matrix, int offsetX, int offsetY)
        {
            this.RemoveCurrentBlock();

            for (int x = 0; x < 4; x++)
            {
                for (int y = 0; y < 4; y++)
                {
                    //Block matrix中的x，y坐标和游戏中的是反的，要拧过来 
                    if (matrix[y, x] == 1)
                    {
                        bool outLeft = x + currentBlockX + offsetX < 0;
                        bool outRight = x + currentBlockX + offsetX > this.colCount - 1;
                        bool outDown = y + currentBlockY + offsetY > this.rowCount - 1;
                        if (outLeft || outRight || outDown)
                        {
                            this.DisplayCurrentBlock(0, 0);
                            return true;
                        }
                        else
                        {
                            int poolX = x + currentBlockX + offsetX;
                            int poolY = y + currentBlockY + offsetY;
                            if (0 <= poolX && poolX < this.colCount && 0 <= poolY && poolY < this.rowCount)
                            {
                                if (this.poolBlocks[poolX, poolY].Color != null)
                                {
                                    this.DisplayCurrentBlock(0, 0);
                                    return true;
                                }
                            }
                        }
                    }
                }
            }
            this.DisplayCurrentBlock(0, 0);
            return false;
        }
        #endregion

        #region UI Control
        private void SetUIAccordingState(GameState state)
        {
            if (state == GameState.Over)
            {
                this.button_Pause.IsEnabled = false;
                this.button_Pause.Content = "暂 停";
                this.button_Start.IsEnabled = true;
            }
            if (state == GameState.Gaming)
            {
                this.button_Pause.IsEnabled = true;
                this.button_Pause.Content = "暂 停";
                this.button_Start.IsEnabled = false;
            }
            if (state == GameState.Pausing)
            {
                this.button_Pause.IsEnabled = true;
                this.button_Pause.Content = "继 续";
            }
        }
        #endregion
    }
}
