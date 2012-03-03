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
using BabyGames.Controls;
using BabyGames.Controls.Snake;
using BabyGames.Common;

namespace BabyGames.Views
{
    public partial class Snake : Page
    {
        public Snake()
        {
            InitializeComponent();
            CompositionTarget.Rendering += new EventHandler(CompositionTarget_Rendering);
        }

        // Executes when the user navigates to this page.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        #region Event Handler
        private void button_StartGame_Click(object sender, RoutedEventArgs e)
        {
            this.gameState = GameState.Gaming;
        }

        private void button_ResumeGame_Click(object sender, RoutedEventArgs e)
        {
            this.gameState = GameState.Pausing;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            this.InitGame();
            this.RestartGame();
        }
        /// <summary>
        /// Control the move direction of the snake
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Page_KeyDown(object sender, KeyEventArgs e)
        {
            Direction newDirection = this.moveDirection;
            if (e.Key == Key.Up || e.Key == Key.W)
            {
                newDirection = Direction.Up;
                this.needAcceleration = true;
            }
            else if (e.Key == Key.Down || e.Key == Key.S)
            {
                newDirection = Direction.Down;
                this.needAcceleration = true;
            }
            else if (e.Key == Key.Left || e.Key == Key.A)
            {
                newDirection = Direction.Left;
                this.needAcceleration = true;
            }
            else if (e.Key == Key.Right || e.Key == Key.D)
            {
                newDirection = Direction.Right;
                this.needAcceleration = true;
            }

            SnakeBody head = this.snakeBodyList.First().Key;

            if (head.MoveDirection == Direction.Up && newDirection == Direction.Down)
                return;
            if (head.MoveDirection == Direction.Down && newDirection == Direction.Up)
                return;
            if (head.MoveDirection == Direction.Left && newDirection == Direction.Right)
                return;
            if (head.MoveDirection == Direction.Right && newDirection == Direction.Left)
                return;

            this.moveDirection = newDirection;
        }

        private void Page_KeyUp(object sender, KeyEventArgs e)
        {
            if (this.needAcceleration == true)
            {
                this.needAcceleration = false;
            }
        }

        private void ControlBar_GameStateChange(object sender, GameControlBar.GameControlEventArgs e)
        {
            if (this.gameState == GameState.Over && e.DesiredState == GameState.Gaming)
            {
                this.gameState = GameState.Gaming;
                this.RestartGame();
            }

            if (this.gameState == GameState.Gaming && e.DesiredState == GameState.Pausing)
            {
                this.gameState = GameState.Pausing;

            }

            if (this.gameState == GameState.Pausing && e.DesiredState == GameState.Gaming)
            {
                this.gameState = GameState.Gaming;
            }
        }
        #endregion

        #region GameSettings
        private int GameWidth;
        private int GameHeight;
        private int cellSize;
        private int colCount;
        private int rowCount;

        private int eatingCapacity = 5; // 食量（超过则蜕皮）

        private bool allowCrossBorder = true;

        private double speed = 80; // 蛇的运行速度

        private int miniSnakeLength = 5; // 蛇的最小长度

        private bool showGrid = false;
        #endregion

        #region Game Control
        private GameState gameState = GameState.Over;
        private DateTime prevDateTime = DateTime.Now;
        /// <summary>
        /// 上一个时钟周期里，累计时间
        /// </summary>
        private double prevElapseTime = 0d;
        private double updateInterval = 0.01; // 多少毫秒更新一次游戏状态

        void CompositionTarget_Rendering(object sender, EventArgs e)
        {
            double elapseTime = (DateTime.Now - prevDateTime).TotalSeconds + prevElapseTime;
            if (elapseTime > updateInterval)
            {
                Update();
                elapseTime -= updateInterval;
            }

            prevElapseTime = elapseTime;
            prevDateTime = DateTime.Now;
        }

        private void Update()
        {
            if (gameState != GameState.Gaming)
            {
                return;
            }

            if (this.IsGameOver())
            {
                this.GameOver();
                return;
            }
            double realspeed = this.GetSnakeSpeed();
            double offset = Math.Round(realspeed * updateInterval);
            SnakeBody snakeHead = this.snakeBodyList.First().Key;

            // 蛇头所处位置进入了网格点区域内
            if (Math.Abs(Math.Round((double)snakeHead.GetValue(Canvas.TopProperty) % this.cellSize)) < offset &&
                Math.Abs(Math.Round((double)snakeHead.GetValue(Canvas.LeftProperty) % this.cellSize)) < offset)
            {
                this.UpdateSnakeDirection(this.moveDirection);

                this.AdjustSnakePosition();

                this.UpdateSnakeBodyList();

                this.CreateBeans();

                this.CheckEat();

                this.CheckSkin();
            }

            UpdateSnakePosition();
        }
        private void GameOver()
        {
            this.PlayOver();
            this.gameState = GameState.Over;
            this.gameControlBar.Reset();
        }

        /// <summary>
        /// 增加一个尾巴
        /// </summary>
        private void AddTail()
        {
            var prevBody = this.snakeBodyList.Last().Key;
            var prevBodyPoint = this.snakeBodyList.Last().Value;

            SnakeTail tail = new SnakeTail();
            tail.MoveDirection = prevBody.MoveDirection;
            CellPoint tailPoint = new CellPoint(prevBodyPoint.X, prevBodyPoint.Y);

            switch (prevBody.MoveDirection)
            {
                case Direction.Up:
                    tail.SetValue(Canvas.LeftProperty, (double)prevBody.GetValue(Canvas.LeftProperty));
                    tail.SetValue(Canvas.TopProperty, (double)prevBody.GetValue(Canvas.TopProperty) + this.cellSize);
                    tailPoint.Y++;
                    break;
                case Direction.Down:
                    tail.SetValue(Canvas.LeftProperty, (double)prevBody.GetValue(Canvas.LeftProperty));
                    tail.SetValue(Canvas.TopProperty, (double)prevBody.GetValue(Canvas.TopProperty) - this.cellSize);
                    tailPoint.Y--;
                    break;
                case Direction.Left:
                    tail.SetValue(Canvas.LeftProperty, (double)prevBody.GetValue(Canvas.LeftProperty) + this.cellSize);
                    tail.SetValue(Canvas.TopProperty, (double)prevBody.GetValue(Canvas.TopProperty));
                    tailPoint.X++;
                    break;
                case Direction.Right:
                    tail.SetValue(Canvas.LeftProperty, (double)prevBody.GetValue(Canvas.LeftProperty) - this.cellSize);
                    tail.SetValue(Canvas.TopProperty, (double)prevBody.GetValue(Canvas.TopProperty));
                    tailPoint.X--;
                    break;
            }

            tailPoint = AdjustCellPoint(tailPoint);

            this.snakeBodyList.Add(tail, tailPoint);
            canvasSnake.Children.Add(tail);
        }
        #endregion

        #region Beans
        private DateTime _prevAddBeanDateTime = DateTime.Now;
        private int _beansCount = 5; // 豆的最大出现数量
        private int _needBeansCount = 0; // 还需要增加的豆的数量

        // 豆的集合
        private Dictionary<Bean, CellPoint> beanList = new Dictionary<Bean, CellPoint>();

        private void InitBeans()
        {
            _needBeansCount = 5;
            this.beanList.Clear();
            this.canvasBean.Children.Clear();
        }
        private void UpdateBeanCount()
        {
            if (_needBeansCount < _beansCount)
                _needBeansCount++;
        }
        /// <summary>
        /// 生成豆
        /// </summary>
        void CreateBeans()
        {
            if (_needBeansCount > 0 && (DateTime.Now - _prevAddBeanDateTime).TotalSeconds > 3)
            {
                List<CellPoint> emptyCells = GetEmptyCells();
                if (emptyCells.Count == 0)
                {
                    this.GameOver();
                    return;
                }
                CellPoint point = emptyCells[this.randomCreater.Next(0, emptyCells.Count)];

                Bean bean = new Bean();
                bean.SetValue(Canvas.LeftProperty, (double)point.X * this.cellSize);
                bean.SetValue(Canvas.TopProperty, (double)point.Y * this.cellSize);
                this.beanList.Add(bean, point);
                canvasBean.Children.Add(bean);

                bean.BeanAnimation.Completed += delegate
                {
                    int centerX = point.X * this.cellSize + this.cellSize / 2;
                    int centerY = point.Y * this.cellSize + this.cellSize / 2;
                    ripple.ShowRipple(new Point(centerX, centerY));
                    this.PlayDrop();
                };

                _needBeansCount--;
                _prevAddBeanDateTime = DateTime.Now;
            }
        }

        /// <summary>
        /// 吃豆
        /// </summary>
        private void CheckEat()
        {
            // 是否有被吃的豆
            var bean = this.beanList.FirstOrDefault(p => p.Value == this.snakeBodyList.First().Value).Key;
            if (bean != null)
            {
                this.beanList.Remove(bean);
                this.canvasBean.Children.Remove(bean);

                this.PlayEat();

                this.AddTail();
                this.UpdateBeanCount();

                this.AddScore();
            }
        }
        #endregion

        #region Skin
        // 蜕下来的皮的集合
        private Dictionary<SnakeSkin, CellPoint> skinList = new Dictionary<SnakeSkin, CellPoint>();

        /// <summary>
        /// 蜕皮
        /// </summary>
        private void CheckSkin()
        {
            if (this.snakeBodyList.Count >= eatingCapacity + this.miniSnakeLength)
            {
                CreateSkin(eatingCapacity);
            }
        }

        private void CreateSkin(int count)
        {
            this.PlaySkin();

            while (count > 0)
            {
                KeyValuePair<SnakeBody, CellPoint> body = this.snakeBodyList.Last();

                CellPoint skinPoint = body.Value;
                SnakeSkin skin = new SnakeSkin();
                skin.SetValue(Canvas.LeftProperty, (double)skinPoint.X * this.cellSize);
                skin.SetValue(Canvas.TopProperty, (double)skinPoint.Y * this.cellSize);
                this.skinList.Add(skin, skinPoint);
                this.canvasSkin.Children.Add(skin);

                this.emptyCells.Remove(skinPoint);

                canvasSnake.Children.Remove(body.Key);
                this.snakeBodyList.Remove(body.Key);
                count--;
            }
        }
        #endregion

        #region Snake
        /// <summary>
        /// Allow the snake cross the border of the game ground
        /// </summary>
        //private double acceleration = 80;
        private bool needAcceleration = false;
        private Direction moveDirection;

        // 贪吃蛇身体的集合
        private Dictionary<SnakeBody, CellPoint> snakeBodyList = new Dictionary<SnakeBody, CellPoint>();
        private void InitSnake()
        {
            this.snakeBodyList.Clear();
            this.canvasSnake.Children.Clear();

            InitHead();

            for (int i = 0; i < this.miniSnakeLength - 1; i++)
            {
                AddTail();
            }
        }

        private void InitHead()
        {
            SnakeBody head = new SnakeHead();
            head.MoveDirection = moveDirection = Direction.Up;

            int x = this.colCount / 2;
            int y = this.rowCount / 2;

            this.snakeBodyList.Add(head, new CellPoint(x, y));
            this.canvasSnake.Children.Add(head);

            Canvas.SetLeft(head, (double)x * this.cellSize);
            Canvas.SetTop(head, (double)y * this.cellSize);
        }

        private void InitScoreBoard()
        {
            this.scoreBoard.EatingCapacity = this.eatingCapacity;
        }

        /// <summary>
        /// 更新蛇的每一段的运动方向
        /// </summary>
        private void UpdateSnakeDirection(Direction moveDirection)
        {
            for (int i = snakeBodyList.Count - 1; i > -1; i--)
            {
                if (i == 0)
                    snakeBodyList.ElementAt(i).Key.MoveDirection = moveDirection;
                else
                    snakeBodyList.ElementAt(i).Key.MoveDirection = snakeBodyList.ElementAt(i - 1).Key.MoveDirection;
            }
        }

        private double GetSnakeSpeed()
        {
            double realspeed = speed;
            //if (this.needAcceleration)
            //{
            //    realspeed += acceleration;
            //}
            return realspeed;
        }
        #endregion

        #region Game Logic
        // 空网格的集合
        private List<CellPoint> emptyCells = new List<CellPoint>();

        private Random randomCreater = new Random();

        private void InitGame()
        {
            this.GameWidth = 800;
            this.GameHeight = 528;
            this.cellSize = 16;

            this.colCount = this.GameWidth / cellSize;
            this.rowCount = this.GameHeight / cellSize;

            this.GameGrid.Width = 800;
            this.GameGrid.Height = 528;
            this.GameGrid.ShowGridLines = this.showGrid;
            if (this.showGrid)
            {
                for (int x = 0; x < colCount; x++)
                {
                    ColumnDefinition col = new ColumnDefinition()
                    {
                        Width = new GridLength(cellSize),
                    };
                    GameGrid.ColumnDefinitions.Add(col);
                }
                for (int y = 0; y < rowCount; y++)
                {
                    RowDefinition row = new RowDefinition()
                    {
                        Height = new GridLength(cellSize),
                    };
                    GameGrid.RowDefinitions.Add(row);
                }
            }

            //Set clip. Avoid the snake get out of the game carrier
            RectangleGeometry rg = new RectangleGeometry();
            rg.Rect = new Rect(0, 0, this.GameWidth, this.GameHeight);
            this.GameCarrier.Clip = rg;

            ripple.RippleBackground = this.Image_Background;
        }

        private void RestartGame()
        {
            this.InitScoreBoard();
            this.canvasSkin.Children.Clear();
            this.skinList.Clear();

            //Empty cells
            emptyCells.Clear();

            for (int i = 0; i < this.colCount; i++)
            {
                for (int j = 0; j < this.rowCount; j++)
                {
                    emptyCells.Add(new CellPoint(i, j));
                }
            }

            this.InitSnake();

            this.InitBeans();
        }

        private void UpdateSnakePosition()
        {
            double offset = Math.Round(this.GetSnakeSpeed() * updateInterval);

            foreach (var body in snakeBodyList.Keys)
            {
                if (body.MoveDirection == Direction.Up)
                {
                    body.SetValue(Canvas.TopProperty, Math.Round((double)body.GetValue(Canvas.TopProperty) - offset));
                }
                else if (body.MoveDirection == Direction.Down)
                {
                    body.SetValue(Canvas.TopProperty, Math.Round((double)body.GetValue(Canvas.TopProperty) + offset));
                }
                else if (body.MoveDirection == Direction.Left)
                {
                    body.SetValue(Canvas.LeftProperty, Math.Round((double)body.GetValue(Canvas.LeftProperty) - offset));
                }
                else if (body.MoveDirection == Direction.Right)
                {
                    body.SetValue(Canvas.LeftProperty, Math.Round((double)body.GetValue(Canvas.LeftProperty) + offset));
                }
            }
        }

        /// <summary>
        /// 修正指定的位置信息为整数
        /// </summary>
        private int RoundPosition(double position)
        {
            double result;
            double offset = Math.Round(this.GetSnakeSpeed() * updateInterval);

            double temp = Math.Round(position % this.cellSize);
            if (Math.Abs(temp) < offset)
                result = Math.Round(position - temp);
            else
                result = Math.Round(position - temp) + Math.Sign(temp) * this.cellSize;

            return (int)result;
        }

        /// <summary>
        /// 修正蛇的每一段的调整到ViewPort中
        /// </summary>
        private void AdjustSnakePosition()
        {
            foreach (var body in this.snakeBodyList.Keys)
            {
                double x = RoundPosition((double)body.GetValue(Canvas.LeftProperty));
                double y = RoundPosition((double)body.GetValue(Canvas.TopProperty));

                if (x == this.GameWidth)
                {
                    x = 0d;
                }
                else if (x == -this.cellSize)
                {
                    x = this.GameWidth - this.cellSize;
                }
                else if (y == this.GameHeight)
                {
                    y = 0d;
                }
                else if (y == -this.cellSize)
                {
                    y = this.GameHeight - this.cellSize;
                }

                body.SetValue(Canvas.LeftProperty, x);
                body.SetValue(Canvas.TopProperty, y);
            }
        }

        private CellPoint AdjustCellPoint(CellPoint point)
        {
            if (this.allowCrossBorder)
            {
                if (point.X > this.colCount - 1)
                    point.X = this.colCount - point.X;
                else if (point.X < 0)
                    point.X = point.X + this.colCount;

                if (point.Y > this.rowCount - 1)
                    point.Y = this.rowCount - point.Y;
                else if (point.Y < 0)
                    point.Y = point.Y + this.rowCount;
            }
            return point;
        }

        private void UpdateSnakeBodyList()
        {
            for (int i = 0; i < this.snakeBodyList.Count; i++)
            {
                SnakeBody body = this.snakeBodyList.ElementAt(i).Key;
                CellPoint point = this.snakeBodyList[body];
                if (body.MoveDirection == Direction.Up)
                    point.Y--;
                else if (body.MoveDirection == Direction.Down)
                    point.Y++;
                else if (body.MoveDirection == Direction.Left)
                    point.X--;
                else if (body.MoveDirection == Direction.Right)
                    point.X++;

                point = AdjustCellPoint(point);

                this.snakeBodyList[body] = point;
            }
        }
        private bool IsGameOver()
        {
            bool meetSkin = this.skinList.Any(p => p.Value == this.snakeBodyList.First().Value);

            bool meetBorder = false;
            if (!this.allowCrossBorder)
            {
                SnakeBody head = this.snakeBodyList.First().Key;
                double x = Canvas.GetLeft(head);
                double y = Canvas.GetTop(head);

                meetBorder = x < 0 || x > (this.GameWidth - this.cellSize) ||
                             y < 0 || y > (this.GameHeight - this.cellSize);
            }

            bool eatTail = this.snakeBodyList.Any(p => (p.Value == this.snakeBodyList.First().Value && p.Key != this.snakeBodyList.First().Key));

            return meetSkin || meetBorder || eatTail;
        }

        /// <summary>
        /// 生成豆
        /// </summary>
        void UpdateBean()
        {
            if (_needBeansCount > 0 && (DateTime.Now - _prevAddBeanDateTime).TotalSeconds > 3)
            {
                List<CellPoint> emptyCells = GetEmptyCells();
                if (emptyCells.Count == 0)
                {
                    GameOver();
                    return;
                }
                CellPoint point = emptyCells[this.randomCreater.Next(0, emptyCells.Count)];

                Bean bean = new Bean();
                bean.SetValue(Canvas.LeftProperty, point.X * this.cellSize);
                bean.SetValue(Canvas.TopProperty, point.Y * this.cellSize);
                this.beanList.Add(bean, point);
                canvasBean.Children.Add(bean);

                bean.BeanAnimation.Completed += delegate
                {
                    int x = point.X * this.cellSize + this.cellSize / 2;
                    int y = point.Y * this.cellSize + this.cellSize / 2;
                    ripple.ShowRipple(new Point(x, y));
                    this.PlayDrop();
                };

                _needBeansCount--;
                _prevAddBeanDateTime = DateTime.Now;
            }
        }

        /// <summary>
        /// 获取空网格集合
        /// </summary>
        private List<CellPoint> GetEmptyCells()
        {
            List<CellPoint> result = new List<CellPoint>();

            List<CellPoint> aroundHeadCells = new List<CellPoint>();
            CellPoint headPoint = this.snakeBodyList.First().Value;
            for (int i = -5; i < 5; i++)
            {
                for (int j = -5; j < 5; j++)
                {
                    CellPoint point = new CellPoint(headPoint.X + i, headPoint.Y + j);
                    point = AdjustCellPoint(point);

                    aroundHeadCells.Add(point);
                }
            }

            // skin 的占位情况因为确定了就不变了，所以在 AddSkin() 处计算
            // 为了以下 LINQ 的可用，需要重写 CellPoint 的 public override bool Equals(object obj)
            result = this.emptyCells.Where(p => !this.snakeBodyList.Select(x => x.Value).Contains(p)).ToList();
            result = result.Where(p => !this.snakeBodyList.Select(x => x.Value).Contains(p)).ToList();
            result = result.Where(p => !aroundHeadCells.Contains(p)).ToList();

            return result;
        }
        #endregion

        #region Score
        private void AddScore()
        {

            // 加分
            this.scoreBoard.Score += 50;

            // 吃掉的豆的数量
            this.scoreBoard.AteBeansCount += 1;
            if (this.scoreBoard.AteBeansCount >= this.scoreBoard.EatingCapacity)
                this.scoreBoard.AteBeansCount = 0;

            // 级别
            this.scoreBoard.Level = (int)(this.scoreBoard.Score / 50 / 50) + 1;

            // 可以吃掉的豆的最大数
            this.scoreBoard.EatingCapacity = this.scoreBoard.Level * 5;
            if (this.scoreBoard.EatingCapacity > 50)
                this.scoreBoard.EatingCapacity = 50;
            this.eatingCapacity = this.scoreBoard.EatingCapacity;

            // 贪吃蛇的速度
            this.speed = this.scoreBoard.Level * 10 + 70;
            if (this.speed > 160)
                this.speed = 160;

        }
        #endregion

        #region Play sound
        /// <summary>
        /// 播放水滴音效
        /// </summary>
        public void PlayDrop()
        {
            audoPlayer.Play("/Sound/drop.mp3");
        }

        /// <summary>
        /// 播放吃豆音效
        /// </summary>
        public void PlayEat()
        {
            audoPlayer.Play("/Sound/eat.mp3");
        }

        /// <summary>
        /// 播放蜕皮音效
        /// </summary>
        public void PlaySkin()
        {
            audoPlayer.Play("/Sound/skin.mp3");
        }

        /// <summary>
        /// 播放 Game Over 音效
        /// </summary>
        public void PlayOver()
        {
            audoPlayer.Play("/Sound/whistle.mp3");
        }
        #endregion
    }
}
