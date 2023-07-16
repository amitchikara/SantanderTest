using System.Collections.Generic;
using System;

namespace BestStories.Exceptions
{
    public class RepositoryException : Exception
    {
        public RepositoryException()
        { }

        public RepositoryException(string message)
            : base(message)
        {
        }

        public RepositoryException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public RepositoryException(string message, List<string> errors)
            : base(message + ": " + string.Join("; ", errors))
        {
        }
    }
}
