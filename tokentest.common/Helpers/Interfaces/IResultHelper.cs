using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace tokentest.Common.Helpers.Interfaces
{
    public interface IResultHelper
    {
        Task<ObjectResult> Response(System.Net.HttpStatusCode statusCode, object data = null);
    }
}
