using BestStories.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BestStories.Core.HttpServices
{
    public interface IGenericHttpService
    {
        Task<HttpServiceResponse<IEnumerable<T>>> GetAll<T>(string addressSuffix);
        Task<HttpServiceResponse<T>> Get<T>(string addressSuffix, string id);
        Task<HttpServiceResponse<T>> Get<T>(string addressSuffix);
        Task<HttpServiceResponse<T>> Post<T>(string addressSuffix, string jsonBody);
        Task<HttpServiceResponse<T>> Put<T>(string addressSuffix, string id, string jsonBody);
        Task<HttpServiceResponse<T>> Delete<T>(string addressSuffix, string id);
    }
}
