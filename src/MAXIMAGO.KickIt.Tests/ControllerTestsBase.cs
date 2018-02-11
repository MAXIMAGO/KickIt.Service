using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace MAXIMAGO.KickIt.Tests
{
    public abstract class ControllerTestsBase
    {
        protected TResult GetResult<TResult>(IActionResult result)
            where TResult : IActionResult
        {
            return Assert.IsType<TResult>(result);
        }
    }
}
