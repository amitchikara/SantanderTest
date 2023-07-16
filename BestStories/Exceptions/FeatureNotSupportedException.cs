using System;

namespace BestStories.Exceptions
{
    public class FeatureNotSupportedException : Exception
    {
        public FeatureNotSupportedException()
        { }

        public FeatureNotSupportedException(string message)
            : base(message)
        {
        }

        public FeatureNotSupportedException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

    }
}
