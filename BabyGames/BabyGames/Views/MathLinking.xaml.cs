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
using System.Diagnostics;
using BabyGames.Data;
using System.Threading;
using System.Windows.Threading;
using System.Collections;
using BabyGames.Controls;
using BabyGames.Common;

namespace BabyGames.Views
{
    public partial class MathLinking : Page
    {
        public MathLinking()
        {
            InitializeComponent();
            this.context = SynchronizationContext.Current;
            InitTimer();
        }

        // Executes when the user navigates to this page.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private int columnCount = 4;
        private int rowCount = 3;
        private int cellWidth = 100;
        private int cellHeight = 50;

        private Random randomCreator = new Random();

        private Grid blockLocator;

        private const string BlockNamePattern = "block_{0}_{1}";

        private void InitGame(int colCount, int rowCount)
        {
            this.InitBlockMatrix(colCount, rowCount);

            this.InitRectLocator(colCount, rowCount, cellWidth, cellHeight);

            int count = (colCount * rowCount) / 2;

            List<Arithmetic> gameData = CreateGameData(count, 20);

            this.FillGrid(columnCount, rowCount, gameData);
        }

        private void InitBlockMatrix(int colCount, int rowCount)
        {
            this.blockMatrix = new int[colCount + 2, rowCount + 2];
            for (int x = 0; x < colCount + 2; x++)
            {
                this.blockMatrix[x, 0] = 1;
                this.blockMatrix[x, rowCount + 1] = 1;
            }

            for (int y = 0; y < rowCount + 2; y++)
            {
                this.blockMatrix[0, y] = 1;
                this.blockMatrix[colCount + 1, y] = 1;
            }
        }


        private void InitRectLocator(int colCount, int rowCount, int cellWidth, int cellHeight)
        {
            var locator = new Grid()
            {
                Width = this.GameCarriar.Width,
                Height = this.GameCarriar.Height,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                //If I don't set Background , mouse event in grid will not be fired
                Background = new SolidColorBrush(Colors.Transparent),
            };
            for (int x = 0; x < colCount; x++)
            {
                ColumnDefinition col = new ColumnDefinition()
                {
                    Width = new GridLength(cellWidth),
                };
                locator.ColumnDefinitions.Add(col);
            }
            for (int y = 0; y < rowCount; y++)
            {
                RowDefinition row = new RowDefinition()
                {
                    Height = new GridLength(cellHeight),
                };
                locator.RowDefinitions.Add(row);
            }
            this.blockLocator = locator;
            this.GameCarriar.Children.Add(locator);

            //this.LinesCarriar.Width = locator.Width;
            //this.LinesCarriar.Height = locator.Height;
        }

        private List<Arithmetic> CreateGameData(int count, int maxNumber)
        {
            List<Arithmetic> data = new List<Arithmetic>();

            for (int i = 0; i < count; i++)
            {
                int result = this.randomCreator.Next(maxNumber);

                OperationType operation = CreateOperation();

                if (operation == OperationType.Plus)
                {
                    data.Add(this.CreatePlusArithmetic(result));
                }
                else
                {
                    data.Add(this.CreateSubArithmetic(result, maxNumber));
                }

                operation = CreateOperation();
                if (operation == OperationType.Plus)
                {
                    data.Add(this.CreatePlusArithmetic(result));
                }
                else
                {
                    data.Add(this.CreateSubArithmetic(result, maxNumber));
                }
            }
            return data;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private OperationType CreateOperation()
        {
            int i = (this.randomCreator.Next()) % 2;
            if (i == 0)
            {
                return OperationType.Plus;
            }
            else
                return OperationType.Subtract;
        }

        private Arithmetic CreatePlusArithmetic(int result)
        {
            int operate1, operate2;
            operate1 = this.randomCreator.Next(result);
            operate2 = result - operate1;

            var arithmetic = new Arithmetic()
            {
                Operater1 = operate1,
                Operater2 = operate2,
                Result = result,
                Operation = OperationType.Plus
            };

            return arithmetic;

        }

        private Arithmetic CreateSubArithmetic(int result, int maxNumber)
        {
            int operate1, operate2;
            operate1 = this.randomCreator.Next(result, maxNumber);
            operate2 = operate1 - result;

            var arithmetic = new Arithmetic()
            {
                Operater1 = operate1,
                Operater2 = operate2,
                Result = result,
                Operation = OperationType.Subtract,
            };

            return arithmetic;

        }
        private void FillGrid(int colCount, int rowCount, List<Arithmetic> gameData)
        {
            List<int> xIndex = new List<int>();

            for (int i = 0; i < colCount; i++)
            {
                xIndex.Add(i);
            }
            this.RandomList(ref xIndex);

            List<int> yIndex = new List<int>();

            for (int i = 0; i < rowCount; i++)
            {
                yIndex.Add(i);
            }
            this.RandomList(ref yIndex);

            this.RandomList(ref gameData);

            int dataIndex = 0;
            for (int y = 0; y < rowCount; y++)
            {
                for (int x = 0; x < colCount; x++)
                {
                    this.CreateBlock(xIndex[x], yIndex[y], gameData[dataIndex]);
                    dataIndex++;
                }
            }
        }
        private void CreateBlock(int colIndex, int rowIndex, Arithmetic data)
        {
            ArithmeticBlock block = new ArithmeticBlock()
            {
                Width = this.cellWidth,
                Height = this.cellHeight,
                Data = data,
                //Name uses col/row index
                Name = string.Format(BlockNamePattern, colIndex, rowIndex),
                //X,Y uses block matrix index
                X = colIndex + 1,
                Y = rowIndex + 1,
            };

            this.blockLocator.Children.Add(block);
            Grid.SetColumn(block, colIndex);
            Grid.SetRow(block, rowIndex);
            block.Refresh();
        }

        private void RandomList<T>(ref List<T> list)
        {
            for (int index = list.Count - 1; index > 0; index--)
            {
                int lowerIndex = this.randomCreator.Next(index);

                T temp = list[index];
                list[index] = list[lowerIndex];
                list[lowerIndex] = temp;
            }
        }

        private SynchronizationContext context;

        #region Game Logic
        /// <summary>
        /// 在周围要留出一圈空的block
        /// block Matris' size is [colCount + 2, rowCount + 2]
        /// Grid's size is colCount, rowCount
        /// 注意Grid坐标和blockMatrix的转换
        /// </summary>
        private int[,] blockMatrix;
        private ArithmeticBlock highlightedBlock = null;
        private List<ArithmeticBlock> deadBlocks = new List<ArithmeticBlock>();

        private List<ArithmeticBlock> hintBlocks = new List<ArithmeticBlock>();

        private GameState gameState = GameState.Over;

        private List<Point> GetJoinpoints(ArithmeticBlock block1, ArithmeticBlock block2)
        {
            List<Point> points = new List<Point>();
            if (this.NoBlocksBetween(block1, block2))
            {
                points.Add(this.GetJoinpoint(block1));
                points.Add(this.GetJoinpoint(block2));
                return points;
            }

            // 最小路径长度
            int minLength = int.MaxValue;

            // 找出两个block x方向上的空节点
            List<Point> block1XList = this.GetXPositions(block1.Y);
            List<Point> block2XList = this.GetXPositions(block2.Y);

            // 找出两个block Y方向上的空节点
            List<Point> block1YList = this.GetYPositions(block1.X);
            List<Point> block2YList = this.GetYPositions(block2.X);

            // 交叉点
            var crossLinkPoints = from p1x in block1XList
                                  join p2y in block2YList
                                  on p1x equals p2y
                                  select p1x;

            foreach (var p in crossLinkPoints)
            {
                if (NoBlocksBetween(block1.X, block1.Y, (int)p.X, (int)p.Y) &&
                     NoBlocksBetween((int)p.X, (int)p.Y, block2.X, block2.Y) &&
                     this.blockMatrix[(int)p.X, (int)p.Y] == 1)
                {
                    int length = Math.Abs(block1.X - (int)p.X) + Math.Abs((int)p.Y - block2.Y);

                    // 查找最短连接路径
                    if (length < minLength)
                    {
                        minLength = length;
                        points.Clear();
                        points.Add(this.GetJoinpoint(block1));
                        points.Add(this.GetJoinpoint((int)p.X, (int)p.Y));
                        points.Add(this.GetJoinpoint(block2));
                    }
                }
            }

            var crossLinkPoints2 = from p1y in block1YList
                                   join p2x in block2XList
                                   on p1y equals p2x
                                   select p1y;

            // 交叉点
            foreach (var p in crossLinkPoints2)
            {
                if (NoBlocksBetween(block1.X, block1.Y, (int)p.X, (int)p.Y) &&
                     NoBlocksBetween((int)p.X, (int)p.Y, block2.X, block2.Y) &&
                    this.blockMatrix[(int)p.X, (int)p.Y] == 1)
                {
                    int length = Math.Abs(block1.Y - (int)p.Y) + Math.Abs((int)p.X - block2.X);

                    // 查找最短连接路径
                    if (length < minLength)
                    {
                        minLength = length;
                        points.Clear();
                        points.Add(this.GetJoinpoint(block1));
                        points.Add(this.GetJoinpoint((int)p.X, (int)p.Y));
                        points.Add(this.GetJoinpoint(block2));
                    }
                }
            }


            // 在两点各自的 X 轴方向上找可消点（两个可消点的 X 坐标相等）
            var xLinkPoints = from p1x in block1XList
                              join p2x in block2XList
                              on p1x.X equals p2x.X
                              select new { p1x, p2x };


            foreach (var px in xLinkPoints)
            {
                if (NoBlocksBetween(block1.X, block1.Y, (int)px.p1x.X, (int)px.p1x.Y) &&
                    NoBlocksBetween((int)px.p1x.X, (int)px.p1x.Y, (int)px.p2x.X, (int)px.p2x.Y) &&
                    NoBlocksBetween((int)px.p2x.X, (int)px.p2x.Y, block2.X, block2.Y))
                {
                    int length = Math.Abs(block1.X - (int)px.p1x.X) + Math.Abs((int)px.p1x.Y - (int)px.p2x.Y) + Math.Abs((int)px.p2x.X - block2.X);

                    // 查找最短连接路径
                    if (length < minLength)
                    {
                        minLength = length;
                        points.Clear();
                        points.Add(this.GetJoinpoint(block1));
                        points.Add(this.GetJoinpoint((int)px.p1x.X, (int)px.p1x.Y));
                        points.Add(this.GetJoinpoint((int)px.p2x.X, (int)px.p2x.Y));
                        points.Add(this.GetJoinpoint(block2));
                    }
                }
            }


            // 在两点各自的 Y 轴方向上找可消点（两个可消点的 Y 坐标相等）
            var yLinkPoints = from p1y in block1YList
                              join p2y in block2YList
                              on p1y.Y equals p2y.Y
                              select new { p1y, p2y };
            foreach (var py in yLinkPoints)
            {
                if (NoBlocksBetween(block1.X, block1.Y, (int)py.p1y.X, (int)py.p1y.Y) &&
                    NoBlocksBetween((int)py.p1y.X, (int)py.p1y.Y, (int)py.p2y.X, (int)py.p2y.Y) &&
                    NoBlocksBetween((int)py.p2y.X, (int)py.p2y.Y, (int)block2.X, (int)block2.Y))
                {
                    int length = Math.Abs(block1.Y - (int)py.p1y.Y) + Math.Abs((int)py.p1y.X - (int)py.p2y.X) + Math.Abs((int)py.p2y.Y - block2.Y);

                    // 查找最短连接路径
                    if (length < minLength)
                    {
                        minLength = length;
                        points.Clear();

                        points.Add(this.GetJoinpoint(block1));
                        points.Add(this.GetJoinpoint((int)py.p1y.X, (int)py.p1y.Y));
                        points.Add(this.GetJoinpoint((int)py.p2y.X, (int)py.p2y.Y));
                        points.Add(this.GetJoinpoint(block2));
                    }
                }
            }

            return points;
        }
        /// <summary>
        /// 直线上的两个 block 是否可以消去
        /// </summary>
        private bool NoBlocksBetween(ArithmeticBlock b1, ArithmeticBlock b2)
        {
            return this.NoBlocksBetween(b1.X, b1.Y, b2.X, b2.Y);
        }
        /// <summary>
        ///
        /// </summary>
        /// <param name="x1"> Coordinate in the block matrix</param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <returns></returns>
        private bool NoBlocksBetween(int x1, int y1, int x2, int y2)
        {
            bool result = true;
            if (x1 != x2 && y1 != y2)
                return false;

            if (x1 == x2)
            {
                if (Math.Abs(y1 - y2) == 1)
                    return true;
                for (int i = Math.Min(y1, y2) + 1; i < Math.Max(y1, y2); i++)
                {
                    if (this.blockMatrix[x1, i] == 0) //There is a block between b1 and b2
                    {
                        return false;
                    }
                }
            };

            if (y1 == y2)
            {
                if (Math.Abs(x1 - x2) == 1)
                    return true;

                for (int i = Math.Min(x1, x2) + 1; i < Math.Max(x1, x2); i++)
                {
                    if (this.blockMatrix[i, y1] == 0) //There is a block between b1 and b2
                    {
                        return false;
                    }
                }
            };
            return result;
        }
        /// <summary>
        /// 获取指定的 block 的 X 轴方向上的所有的空节点
        /// 返回值为Matrix坐标
        /// </summary>
        private List<Point> GetXPositions(int blockY)
        {
            var result = new List<Point>();
            for (int i = 0; i < this.columnCount + 2; i++)
            {
                int value = this.blockMatrix[i, blockY];
                if (value == 1)
                    result.Add(new Point(i, blockY));
            }

            return result;
        }

        /// <summary>
        /// 获取指定的 block 的 Y 轴方向上的所有的空节点
        /// 返回值为Matrix坐标
        /// </summary>
        private List<Point> GetYPositions(int bolckX)
        {
            var result = new List<Point>();
            for (int i = 0; i < this.rowCount + 2; i++)
            {
                int value = this.blockMatrix[bolckX, i];
                if (value == 1)
                    result.Add(new Point(bolckX, i));
            }

            return result;
        }

        private bool CheckGameOver()
        {
            for (int i = 0; i < this.rowCount + 2; i++)
            {
                for (int j = 0; j < this.columnCount + 2; j++)
                {
                    if (this.blockMatrix[j, i] == 0)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private void ShowCelebration()
        {
            PlayApplause();

        }

        void StartEventHandler(object sender, EventArgs e)
        {
            this.RestartGame();
        }
        private void RestartGame()
        {
            if (this.gameState == GameState.Over)
            {
                this.gameState = GameState.Gaming;
                this.InitGame(this.columnCount, rowCount);
                this.SetUIAccordingState(this.gameState);
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
        }

        private void FinishGame()
        {
            if (this.gameState == GameState.Gaming)
            {
                this.gameState = GameState.Over;
                this.SetUIAccordingState(this.gameState);
            }
        }

        private void ClearDeadBlocks()
        {
            this.LinesCarriar.Children.Clear();
            if (this.deadBlocks.Count > 1)
            {
                for (int i = 0; i < this.deadBlocks.Count; i++)
                {

                    this.RemoveBlocksFromCanvas(this.deadBlocks[i]);
                    this.RemoveBlockFromMatrix(this.deadBlocks[i]);
                }

                this.deadBlocks.Clear();
            }
        }

        private void RemoveBlocksFromCanvas(ArithmeticBlock block)
        {
            this.blockLocator.Children.Remove(block);
        }
        private void RemoveBlockFromMatrix(ArithmeticBlock block)
        {
            int x = block.X;
            int y = block.Y;

            this.blockMatrix[x, y] = 1;
        }

        private void DrawLinkLine(List<Point> points)
        {
            for (int i = 0; i < points.Count - 1; i++)
            {
                Line line = new Line()
                {
                    X1 = points[i].X,
                    X2 = points[i + 1].X,
                    Y1 = points[i].Y,
                    Y2 = points[i + 1].Y,
                    Stroke = new SolidColorBrush(Colors.Red),
                    StrokeThickness = 3,
                };
                LinesCarriar.Children.Add(line);
            }
        }

        private Point GetJoinpoint(ArithmeticBlock b)
        {
            int x = (b.X - 1) * this.cellWidth + this.cellWidth / 2;
            int y = (b.Y - 1) * this.cellHeight + this.cellHeight / 2;
            return new Point(x, y);
        }

        private Point GetJoinpoint(int blockX, int blockY)
        {
            int x = (blockX - 1) * this.cellWidth + this.cellWidth / 2;
            int y = (blockY - 1) * this.cellHeight + this.cellHeight / 2;
            return new Point(x, y);
        }

        private void Hint()
        {
            this.hintBlocks = this.FindMatch();
            for (int i = 0; i < hintBlocks.Count; i++)
            {
                hintBlocks[0].Hint();
                hintBlocks[1].Hint();
            }
        }

        private void RemoveHint()
        {
            for (int i = 0; i < hintBlocks.Count; i++)
            {
                hintBlocks[0].RemoveHint();
                hintBlocks[1].RemoveHint();
            }
        }

        private List<ArithmeticBlock> FindMatch()
        {
            List<ArithmeticBlock> matchBlocks = new List<ArithmeticBlock>();
            for (int x = 1; x < this.columnCount + 2; x++)
            {
                for (int y = 1; y < this.rowCount + 2; y++)
                {
                    if (this.blockMatrix[x, y] == 0)
                    {
                        string blockName = string.Format(BlockNamePattern, x - 1, y - 1);
                        ArithmeticBlock block = this.blockLocator.FindName(blockName) as ArithmeticBlock;

                        if (block != null)
                        {
                            ArithmeticBlock matchBlock = this.FindMathBlock(block);

                            if (matchBlock != null)
                            {
                                matchBlocks.Add(block);
                                matchBlocks.Add(matchBlock);

                                return matchBlocks;
                            }
                        }
                    }
                }
            }
            return matchBlocks;
        }
        private ArithmeticBlock FindMathBlock(ArithmeticBlock block)
        {
            for (int x = 1; x < this.columnCount + 2; x++)
            {
                for (int y = 1; y < this.rowCount + 2; y++)
                {
                    if (this.blockMatrix[x, y] == 0)
                    {
                        string blockName = string.Format(BlockNamePattern, x - 1, y - 1);
                        ArithmeticBlock matchBlock = this.blockLocator.FindName(blockName) as ArithmeticBlock;

                        if (matchBlock != null && block != matchBlock)
                        {
                            if (matchBlock.Data.Result == block.Data.Result)
                            {
                                if (this.GetJoinpoints(matchBlock, block).Count > 1)
                                {
                                    return matchBlock;
                                }
                            }
                        }
                    }
                }
            }
            return null;
        }

        private void ReorderBlocks()
        {
            List<ArithmeticBlock> blocks = new List<ArithmeticBlock>();

            for (int i = 0; i < this.blockLocator.Children.Count; i++)
            {
                ArithmeticBlock block = this.blockLocator.Children[i] as ArithmeticBlock;
                if (block != null)
                {
                    string n = block.Name;
                    block.Name = "_" + block.Name; //避免重新排序时名字重复

                    this.blockLocator.FindName(n);
                    blocks.Add(block);
                }
            }

            this.RandomList(ref blocks);

            int blockIndex = 0;
            for (int x = 1; x < this.columnCount + 2; x++)
            {
                for (int y = 1; y < this.rowCount + 2; y++)
                {
                    if (this.blockMatrix[x, y] == 0)
                    {
                        Grid.SetColumn(blocks[blockIndex], x - 1);
                        Grid.SetRow(blocks[blockIndex], y - 1);
                        blocks[blockIndex].X = x;
                        blocks[blockIndex].Y = y;
                        blocks[blockIndex].Name = string.Format(BlockNamePattern, x - 1, y - 1);
                        blockIndex++;
                    }
                }
            }
        }
        #endregion

        #region Event Handler
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            this.ShowSplashDialog();
        }
        private void GameCarriar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (this.gameState == GameState.Gaming)
            {
                Point mousePoint = e.GetPosition(this.GameCarriar);

                int x = (int)mousePoint.X / this.cellWidth;
                int y = (int)mousePoint.Y / this.cellHeight;

                string blockName = string.Format(BlockNamePattern, x, y);

                ArithmeticBlock currentBlock = this.GameCarriar.FindName(blockName) as ArithmeticBlock;
                if (currentBlock == null) //No block under mouse
                {
                    return;
                }

                this.RemoveHint();

                if (this.highlightedBlock == null) //No block was selected at last click
                {
                    this.highlightedBlock = currentBlock;
                    currentBlock.HighLight();
                }
                else if (this.highlightedBlock == currentBlock) // click the same block
                {
                    return;
                }
                else
                {
                    if (currentBlock.Data.Result == this.highlightedBlock.Data.Result)
                    {
                        List<Point> linkPoints = this.GetJoinpoints(this.highlightedBlock, currentBlock);
                        if (linkPoints.Count > 1)
                        {
                            this.DrawLinkLine(linkPoints);

                            this.deadBlocks.Clear();
                            this.deadBlocks.Add(currentBlock);
                            this.deadBlocks.Add(highlightedBlock);

                            this.highlightedBlock = null;


                            Thread thread = new Thread(t =>
                            {
                                Thread.Sleep(150);

                                this.context.Post
                                (
                                    r =>
                                    {
                                        this.ClearDeadBlocks();
                                    },
                                    null
                                );
                            });
                            thread.Start();
                            return;
                        }
                    }

                    this.highlightedBlock.RemoveHighLight();
                    this.highlightedBlock = currentBlock;
                    currentBlock.HighLight();
                }
            }
        }

        private void button_StartGame_Click(object sender, RoutedEventArgs e)
        {
            this.RestartGame();
            //this.RestartGameWithTestData();
        }

        private void button_ResumeGame_Click(object sender, RoutedEventArgs e)
        {
            this.PauseGame();
        }

        private void button_Test_Click(object sender, RoutedEventArgs e)
        {
            //this.PlayApplause();

            //Reorder if necessary
            List<ArithmeticBlock> matchBlocks = this.FindMatch();
            if (matchBlocks.Count == 0)
            {
                this.ReorderBlocks();
            }

        }

        private void button_Hint_Click(object sender, RoutedEventArgs e)
        {
            this.Hint();
        }

        #endregion

        #region Timer
        /// <summary>
        ///  
        /// </summary>
        private DispatcherTimer Timer;

        private void InitTimer()
        {
            Timer = new DispatcherTimer();
            Timer.Interval = TimeSpan.FromMilliseconds(400);
            Timer.Tick += new EventHandler(Timer_Tick);
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (this.CheckGameOver())
            {
                this.gameState = GameState.Over;
                this.ShowCelebration();

                this.button_ResumeGame.IsEnabled = false;
                this.button_StartGame.IsEnabled = true;
                this.Timer.Stop();
            }
            else
            {
                //Reorder if necessary
                List<ArithmeticBlock> matchBlocks = this.FindMatch();
                if (matchBlocks.Count == 0)
                {
                    this.ReorderBlocks();
                }
            }
        }
        #endregion

        #region Sound
        private void PlayApplause()
        {
            applausePlayer.Source = new Uri(@"/Sound/applause.wmv", UriKind.Relative);
            applausePlayer.Play();
        }
        #endregion

        #region UI Logic
        private void SetUIAccordingState(GameState state)
        {
            if (state == GameState.Over)
            {
                this.button_Hint.IsEnabled = false;
                this.button_ResumeGame.IsEnabled = false;
                this.button_ResumeGame.Content = "暂 停";
                this.button_StartGame.IsEnabled = true;

                this.Timer.Stop();
            }
            if (state == GameState.Gaming)
            {
                this.button_Hint.IsEnabled = true;
                this.button_ResumeGame.IsEnabled = true;
                this.button_ResumeGame.Content = "暂 停";
                this.button_StartGame.IsEnabled = false;
                this.Timer.Start();
            }
            if (state == GameState.Pausing)
            {
                this.button_Hint.IsEnabled = true;
                this.button_ResumeGame.IsEnabled = true;
                this.button_ResumeGame.Content = "继 续";
                this.button_StartGame.IsEnabled = false;
                this.Timer.Stop();
            }
        }

        private void ShowSplashDialog()
        {
            GameSplashDialog dialog = new GameSplashDialog();
            dialog.Style = (Style)App.Current.Resources["Dialog"];
            dialog.textBlock_GameName.Text = "算术连连看";
            dialog.Opacity = 0.85;
            dialog.Closed += new EventHandler(this.StartEventHandler);
            dialog.Show();
        }

        private void ShowGameOverDialog()
        {

        }
        #endregion

        #region Test

        private void RestartGameWithTestData()
        {
            if (this.gameState == GameState.Over)
            {
                this.gameState = GameState.Gaming;

                this.columnCount = 2;
                this.rowCount = 2;

                this.InitBlockMatrix(this.columnCount, this.rowCount);

                this.InitRectLocator(this.columnCount, this.rowCount, cellWidth, cellHeight);


                CreateBlock(0, 0, new Arithmetic()
                    {
                        Operater1 = 1,
                        Operater2 = 1,
                        Operation = OperationType.Plus,
                        Result = 2,
                    }
                    );
                CreateBlock(1, 1, new Arithmetic()
                {
                    Operater1 = 0,
                    Operater2 = 2,
                    Operation = OperationType.Plus,
                    Result = 2,
                }
                   );

                CreateBlock(1, 0, new Arithmetic()
                {
                    Operater1 = 2,
                    Operater2 = 1,
                    Operation = OperationType.Plus,
                    Result = 3,
                }
                   );

                CreateBlock(0, 1, new Arithmetic()
                {
                    Operater1 = 3,
                    Operater2 = 0,
                    Operation = OperationType.Plus,
                    Result = 3,
                }
                   );
                this.SetUIAccordingState(this.gameState);
            }
        }
        private void CreateTestData()
        {



        }

        #endregion
    }
}
