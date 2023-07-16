using System.Collections.Generic;
using System;

namespace BestStories.Exceptions
{
    public class ConfigurationException : Exception
    {
        public ConfigurationException()
        { }

        public ConfigurationException(string message)
            : base(message)
        {
        }

        public ConfigurationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public ConfigurationException(string message, List<string> errors)
            : base(message + ": " + string.Join("; ", errors))
        {
        }
    }
}
