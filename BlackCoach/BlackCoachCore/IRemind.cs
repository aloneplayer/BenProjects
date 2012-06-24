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
using BlackCoach.Core.Enums;

namespace BlackCoachCore
{
    public interface IRemind
    {
        /// <summary>
        /// 何时提醒
        /// </summary>
        DateTime Time { get; set; }

        /// <summary>
        /// 是否工作
        /// </summary>
        bool Enabled { get; set; }

        /// <summary>
        /// 提醒的方式，闹铃还是振动
        /// </summary>
        RemindMethod RemindMethod { get; set; }

        /// <summary>
        /// 最多可以Delay几次
        /// </summary>
        int MaxDelayTimes
        {
            get;
            set;
        }

        /// <summary>
        /// 已经Delay几次
        /// </summary>
        int DelayTimes { get; set; }

        bool IsDone { get; set; }

        void Execute();

        void Delay();
    }
}
