using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace BestStories.Core.Models
{
    public class HttpServiceResponse<T>
    {
        public HttpResponseMessage Message { get; set; }
        public T Content { get; set; }
    }
}
