using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crystalbyte.Chocolate
{
    public enum LogSeverity
    {
        LogseverityVerbose = -1,
        LogseverityInfo,
        LogseverityWarning,
        LogseverityError,
        LogseverityErrorReport,
        LogseverityDisable = 99,
    }
}
