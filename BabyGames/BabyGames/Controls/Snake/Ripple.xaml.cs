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
using System.Windows.Media.Imaging;

namespace BabyGames.Controls.Snake
{
    public partial class Ripple : UserControl
    {
        // 以下为初始参数
        private double _maxRadius = 100d; // 水波的最大扩散处离原点的距离
        private double _speed = 60d; // 水波扩散的速度
        private double _maxFrameRate = 80d; // 最大帧率
        private double _maxSpacing = 15d; // 波的纹理的最大宽度

        // 以下为需要计算的参数
        private double _period = 0d; // 多少毫秒计算一次
        private double _rippleOutsideStep = 0d; // 水波外圈速度
        private double _rippleInsideStep = 0d; // 水波内圈速度

        private GeometryGroup _rippleGroup = new GeometryGroup();

        public Ripple()
        {
            InitializeComponent();

            this.Loaded += new RoutedEventHandler(Ripple_Loaded);
        }

        void Ripple_Loaded(object sender, RoutedEventArgs e)
        {
            _period = 1 / _maxFrameRate;
            _rippleOutsideStep = _speed * _period;
            _rippleInsideStep = _rippleOutsideStep + _maxSpacing / (_maxRadius / _rippleOutsideStep);

            ImageHolder.Clip = _rippleGroup;

            CompositionTarget.Rendering += new EventHandler(CompositionTarget_Rendering);
        }

        private DateTime _prevDateTime = DateTime.Now;
        private double _leftoverLength = 0d;
        void CompositionTarget_Rendering(object sender, EventArgs e)
        {
            if (_rippleGroup.Children.Count == 0)
            {
                _prevDateTime = DateTime.Now;
                _leftoverLength = 0d;
                return;
            }

            double length = (DateTime.Now - _prevDateTime).TotalSeconds + _leftoverLength;

            while (length > _period)
            {
                List<Geometry> removeGeometries = new List<Geometry>();

                foreach (var child in _rippleGroup.Children)
                {
                    var ripple = child as GeometryGroup;

                    ((EllipseGeometry)ripple.Children[0]).RadiusX += _rippleOutsideStep;
                    ((EllipseGeometry)ripple.Children[0]).RadiusY += _rippleOutsideStep;
                    ((EllipseGeometry)ripple.Children[1]).RadiusX += _rippleInsideStep;
                    ((EllipseGeometry)ripple.Children[1]).RadiusY += _rippleInsideStep;

                    if (((EllipseGeometry)ripple.Children[0]).RadiusX > _maxRadius)
                    {
                        removeGeometries.Add(ripple);
                    }
                }

                foreach (var removeGeometry in removeGeometries)
                {
                    _rippleGroup.Children.Remove(removeGeometry);
                }

                length -= _period;
            }

            _leftoverLength = length;
            _prevDateTime = DateTime.Now;

            ImageHolder.Clip = _rippleGroup;
        }

        /// <summary>
        /// 以指定的点为原点生成水波纹动画（调用前应先设置背景）
        /// </summary>
        public void ShowRipple(Point center)
        {
            GeometryGroup ripple = new GeometryGroup();

            EllipseGeometry rippleOutside = new EllipseGeometry();
            rippleOutside.RadiusX = rippleOutside.RadiusY = _maxSpacing;
            rippleOutside.Center = center;

            EllipseGeometry rippleInside = new EllipseGeometry();
            rippleInside.RadiusX = rippleInside.RadiusY = 0;
            rippleInside.Center = center;

            ripple.Children.Add(rippleOutside);
            ripple.Children.Add(rippleInside);

            _rippleGroup.Children.Add(ripple);
        }
        /// <summary>
        /// 水波纹的背景
        /// </summary>
        public FrameworkElement RippleBackground
        {
            set
            {
                ImageHolder.Source = new WriteableBitmap(value, null);
                ImageHolder.Width = value.Width + value.Width / 100;
                ImageHolder.Height = value.Height + value.Height / 100;
            }
        }
    }
}
