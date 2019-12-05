using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleAppIClock
{
    public interface IClock
    {
        DateTime Now { get; }

        DateTime UtcNow { get; }

        BusinessDate Today { get; }
    }
}
