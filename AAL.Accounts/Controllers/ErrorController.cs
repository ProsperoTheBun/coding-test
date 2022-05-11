namespace AAL.Accounts.Controllers
{
    using Microsoft.AspNetCore.Diagnostics;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Generic error handling
    /// </summary>
    /// <remarks>
    /// https://tools.ietf.org/html/rfc7807
    /// </remarks>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [ApiController]
    public class ErrorController : ControllerBase
    {
        /// <summary>
        /// API calls that throw errors are rerouted to this endpoint.
        /// </summary>
        /// <returns></returns>
        [Route("/error")]
        public IActionResult Error()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            return Problem(title: "API Error", detail: context.Error.Message);
        }
    }
}