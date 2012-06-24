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

namespace BlackCoach.Core
{
    public class Exercise : IRemind
    {
        public string Title
        {
            get;
            set;
        }

        public void Execute()
        {
            throw new NotImplementedException();
        }


        public int Hour
        {
            get;
            set;
        }

        public int Minut
        {
            get;
            set;
        }

        public string TimeString
        {
            get
            {
                return string.Format("{0}:{1}", Hour, Minut);
            }
        }

        public bool Enabled
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public RemindMethod RemindMethod
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public int MaxDelayTimes
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public int DelayTimes
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public bool IsDone
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public void Delay()
        {
            throw new NotImplementedException();
        }

        public ITask Task
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }
}
