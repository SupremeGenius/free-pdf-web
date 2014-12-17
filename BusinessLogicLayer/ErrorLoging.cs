using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace BusinessLogicLayer
{
    public class ErrorLoging
    {
        private static EventLog errorLog;

        static ErrorLoging()
        {            
            if(!EventLog.SourceExists("FreePDF"))
                EventLog.CreateEventSource("FreePDF", "Application");

            errorLog = new EventLog("Application", System.Environment.MachineName, "FreePDF");
        }

        public static void WriteLog(String Message, EventLogEntryType LogType)
        {
            errorLog.WriteEntry(Message, LogType);
        }

        public static void WriteLog(String Message)
        {
            errorLog.WriteEntry(Message);
        }
    }
}
