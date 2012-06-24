using System;
using System.Collections.Generic;
using System.Linq;

namespace BlackCoach.Core
{
    public class ReminderEngine
    {
        private IRemindRepository remindRepository;

        private State _engineState;

        public enum State
        {
            Running = 0,
            Pause
        }

        public List<IRemind> RemindList;

        public State EngineState
        {
            get
            {
                return _engineState;
            }
            set
            {
                _engineState = value;
            }
        }

        public void ClockTickHandler()
        {
            if (EngineState == State.Running)
            {

            }
        }

        public void Pause()
        {
            _engineState = State.Pause;
        }

        public void ResumeEngine()
        {

        }


        private void Sort()
        {

        }

        private IRemind GetTheFirstRemind()
        {
            DateTime currentTime = DateTime.Now;
            var query = from remind in RemindList
                        where (remind.Time > currentTime && !remind.IsDone)
                        select remind;

            IRemind currentRemind = query.FirstOrDefault<IRemind>();
            if (currentTime != null)
            {
                currentRemind.Execute();
            }
            return null;
        }
    }
}
