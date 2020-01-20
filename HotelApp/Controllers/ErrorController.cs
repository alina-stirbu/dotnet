using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelApp.API.Controllers
{
    using Microsoft.AspNetCore.Diagnostics;
    using Microsoft.AspNetCore.Hosting;

    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : ControllerBase
    {
        private readonly IWebHostEnvironment webHostEnvironment;

        public ErrorController(IWebHostEnvironment webHostEnvironment)
        {
            this.webHostEnvironment = webHostEnvironment;
        }

        [Route("/error-local-development")]
        public IActionResult ErrorOnLocalDevelopment()
        {
            if (this.webHostEnvironment.EnvironmentName != "Development")
                throw new InvalidOperationException(
                    "Only on Development environment");

            var context = this.HttpContext.Features.Get<IExceptionHandlerFeature>();

            return this.Problem(
                context.Error.StackTrace,
                title: context.Error.Message);
        }

        [Route("/error")]
        public IActionResult Error()
        {
            return this.Problem();
        }
    }
}