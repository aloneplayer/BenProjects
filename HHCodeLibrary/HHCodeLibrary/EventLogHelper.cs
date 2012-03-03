using System;
using System.Text;
using System.Diagnostics;

namespace HHCodeLibrary
{
    public static class EventLogHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logSource"></param>
        /// <param name="logName"></param>
        /// <param name="message"></param>
        /// <param name="logEntryType"></param>
        public static void WriteCustomEvent(string logSource, string logName, string message, EventLogEntryType logEntryType)
        {
            // Create an EventLog instance and assign its source.
            EventLog log = new EventLog();

            // Source cannot already exist before creating the log.
            if (!(EventLog.SourceExists(logSource)))
            {
                // Logs and Sources are created as a pair.
                EventLog.CreateEventSource(logSource, logName);
                // Associate the EventLog component with the new log.
                log.Log = logName;
                log.Source = logSource;
            }
            else
            {
                log.Source = logSource;
            }
            // Write an entry to the event log.
            log.WriteEntry(message, logEntryType);
        }

        //public static string ReadEvent()
        //{ 
        
        
        //}
    }
}
