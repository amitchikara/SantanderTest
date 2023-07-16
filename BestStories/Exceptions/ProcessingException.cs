using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;

namespace BestStories.Exceptions
{
    public class ProcessingException : Exception
    {
        public ProcessingException()
        { }

        public ProcessingException(string message)
            : base(message)
        {
        }

        public ProcessingException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public ProcessingException(string message, Exception innerException, List<ActionResult> results)
            : base(message, innerException)
        {
            Results = results;
        }

        public List<ActionResult> Results { get; private set; }

    }
}
