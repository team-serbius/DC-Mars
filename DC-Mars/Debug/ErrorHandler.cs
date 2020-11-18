using System;
using System.Collections.Generic;
using System.Text;

namespace DC_Mars.Debug
{
    public class ErrorHandler
    {
        private Logging logger = new Logging();

        private int ErrorCount;
        private string ErrorReason;

        public int GetErrorCounter()
        {
            return ErrorCount;
        }

        public string GetErrorReason()
        {
            return ErrorReason;
        }

        public void IncrementError(int val)
        {
            ErrorCount = ErrorCount + val;
            logger.LogCustom("Error count since Execution: " + val, 2);
        }

        public void SetErrorReason(string error)
        {
            ErrorReason = error;
        }
    }
}