﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MyInfoCommonUtility.Logger
{
    public interface ILoggerManager
    {
        void LogInfo(string message);
        void LogWarn(string message);
        void LogDebug(string message);
        void LogError(string message);
        void LogException(Exception exception, string message);

        string getLastLogEntry();
    }
}
